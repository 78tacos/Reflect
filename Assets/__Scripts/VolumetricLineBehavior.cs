using UnityEngine;

namespace VolumetricLines
{
	/// <summary>
	/// Render a single volumetric line
	/// 
	/// Based on the Volumetric lines algorithm by Sebastien Hillaire
	/// http://sebastien.hillaire.free.fr/index.php?option=com_content&view=article&id=57&Itemid=74
	/// 
	/// Thread in the Unity3D Forum:
	/// http://forum.unity3d.com/threads/181618-Volumetric-lines
	/// 
	/// Unity3D port by Johannes Unterguggenberger
	/// johannes.unterguggenberger@gmail.com
	/// 
	/// Thanks to Michael Probst for support during development.
	/// 
	/// Thanks for bugfixes and improvements to Unity Forum User "Mistale"
	/// http://forum.unity3d.com/members/102350-Mistale
    /// 
    /// Shader code optimization and cleanup by Lex Darlog (aka DRL)
    /// http://forum.unity3d.com/members/lex-drl.67487/
    /// 
	/// </summary>
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	[RequireComponent(typeof(CapsuleCollider))]
	// [ExecuteInEditMode]
	public class VolumetricLineBehavior : MonoBehaviour 
	{
		public float laserSpeed = 0.001f;
		public float spawnOffset = 0.0f;
		public GameObject laserPrefab;
		[Tooltip("If true, the laser extends by moving its start point; otherwise it grows by moving the end point.")]
		public bool growFromStart = true;
		public bool isHit = false;
		[Tooltip("Keep the original start point (e.g. turret muzzle) after the laser collides with a mirror.")]
		public bool lockStartOnHit = true;

		private CapsuleCollider m_collider;

		// Used to compute the average value of all the Vector3's components:
		static readonly Vector3 Average = new Vector3(1f/3f, 1f/3f, 1f/3f);

		#region private variables
		/// <summary>
		/// Template material to be used
		/// </summary>
		[SerializeField]
		public Material m_templateMaterial;

		/// <summary>
		/// Set to false in order to change the material's properties as specified in this script.
		/// Set to true in order to *initially* leave the material's properties as they are in the template material.
		/// </summary>
		[SerializeField] 
		private bool m_doNotOverwriteTemplateMaterialProperties;

		/// <summary>
		/// The start position relative to the GameObject's origin
		/// </summary>
		[SerializeField] 
		private Vector3 m_startPos;
		
		/// <summary>
		/// The end position relative to the GameObject's origin
		/// </summary>
		[SerializeField] 
		private Vector3 m_endPos = new Vector3(0f, 0f, 100f);

		/// <summary>
		/// Line Color
		/// </summary>
		[SerializeField] 
		private Color m_lineColor;

		/// <summary>
		/// The width of the line
		/// </summary>
		[SerializeField] 
		private float m_lineWidth;

		/// <summary>
		/// Light saber factor
		/// </summary>
		[SerializeField]
		[Range(0.0f, 1.0f)]
		private float m_lightSaberFactor;

		/// <summary>
		/// This GameObject's specific material
		/// </summary>
		private Material m_material;
		
		/// <summary>
		/// This GameObject's mesh filter
		/// </summary>
		private MeshFilter m_meshFilter;
		#endregion

		private Vector3 m_initialStartPos;
		private bool m_hasInitialStart;

		#region properties
		/// <summary>
		/// Gets or sets the tmplate material.
		/// Setting this will only have an impact once. 
		/// Subsequent changes will be ignored.
		/// </summary>
		public Material TemplateMaterial
		{
			get { return m_templateMaterial; }
			set { m_templateMaterial = value; }
		}

		/// <summary>
		/// Gets or sets whether or not the template material properties
		/// should be used (false) or if the properties of this MonoBehavior
		/// instance should be used (true, default).
		/// Setting this will only have an impact once, and then only if it
		/// is set before TemplateMaterial has been assigned.
		/// </summary>
		public bool DoNotOverwriteTemplateMaterialProperties
		{
			get { return m_doNotOverwriteTemplateMaterialProperties; }
			set { m_doNotOverwriteTemplateMaterialProperties = value; }
		}
		
		/// <summary>
		/// Get or set the line color of this volumetric line's material
		/// </summary>
		public Color LineColor
		{
			get { return m_lineColor;  }
			set
			{
				CreateMaterial();
				if (null != m_material)
				{
					m_lineColor = value;
					m_material.color = m_lineColor;
				}
			}
		}

		/// <summary>
		/// Get or set the line width of this volumetric line's material
		/// </summary>
		public float LineWidth
		{
			get { return m_lineWidth; }
			set
			{
				CreateMaterial();
				if (null != m_material)
				{
					m_lineWidth = value;
					m_material.SetFloat("_LineWidth", m_lineWidth);
				}
				UpdateBounds();
				UpdateCollider();
			}
		}

		/// <summary>
		/// Get or set the light saber factor of this volumetric line's material
		/// </summary>
		public float LightSaberFactor
		{
			get { return m_lightSaberFactor; }
			set
			{
				CreateMaterial();
				if (null != m_material)
				{
					m_lightSaberFactor = value;
					m_material.SetFloat("_LightSaberFactor", m_lightSaberFactor);
				}
			}
		}

		/// <summary>
		/// Get or set the start position of this volumetric line's mesh
		/// </summary>
		public Vector3 StartPos
		{
			get { return m_startPos; }
			set
			{
				m_startPos = value;
				SetStartAndEndPoints(m_startPos, m_endPos);
			}
		}

		/// <summary>
		/// Get or set the end position of this volumetric line's mesh
		/// </summary>
		public Vector3 EndPos
		{
			get { return m_endPos; }
			set
			{
				m_endPos = value;
				SetStartAndEndPoints(m_startPos, m_endPos);
			}
		}

		#endregion
		
		#region methods
		/// <summary>
		/// Creates a copy of the template material for this instance
		/// </summary>
		private void CreateMaterial()
		{
			if (null == m_material || null == GetComponent<MeshRenderer>().sharedMaterial)
			{
				if (null != m_templateMaterial)
				{
					m_material = Material.Instantiate(m_templateMaterial);
					GetComponent<MeshRenderer>().sharedMaterial = m_material;
					SetAllMaterialProperties();
				}
				else 
				{
					m_material = GetComponent<MeshRenderer>().sharedMaterial;
				}
			}
		}

		/// <summary>
		/// Destroys the copy of the template material which was used for this instance
		/// </summary>
		private void DestroyMaterial()
		{
			if (null != m_material)
			{
				DestroyImmediate(m_material);
				m_material = null;
			}
		}

		/// <summary>
		/// Calculates the (approximated) _LineScale factor based on the object's scale.
		/// </summary>
		private float CalculateLineScale()
		{
			return Vector3.Dot(transform.lossyScale, Average);
		}

		/// <summary>
		/// Updates the line scaling of this volumetric line based on the current object scaling.
		/// </summary>
		public void UpdateLineScale()
		{
			if (null != m_material) 
			{
				m_material.SetFloat("_LineScale", CalculateLineScale());
			}
		}

		/// <summary>
		/// Sets all material properties (color, width, light saber factor, start-, endpos)
		/// </summary>
		private void SetAllMaterialProperties()
		{
			SetStartAndEndPoints(m_startPos, m_endPos);

			if (null != m_material)
			{
				if (!m_doNotOverwriteTemplateMaterialProperties)
				{
					m_material.color = m_lineColor;
					m_material.SetFloat("_LineWidth", m_lineWidth);
					m_material.SetFloat("_LightSaberFactor", m_lightSaberFactor);
				}
				UpdateLineScale();
			}
		}

		/// <summary>
		/// Calculate the bounds of this line based on start and end points,
		/// the line width, and the scaling of the object.
		/// </summary>
		private Bounds CalculateBounds()
		{
			var maxWidth = Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);
			var scaledLineWidth = maxWidth * LineWidth * 0.5f;

			var min = new Vector3(
				Mathf.Min(m_startPos.x, m_endPos.x) - scaledLineWidth,
				Mathf.Min(m_startPos.y, m_endPos.y) - scaledLineWidth,
				Mathf.Min(m_startPos.z, m_endPos.z) - scaledLineWidth
			);
			var max = new Vector3(
				Mathf.Max(m_startPos.x, m_endPos.x) + scaledLineWidth,
				Mathf.Max(m_startPos.y, m_endPos.y) + scaledLineWidth,
				Mathf.Max(m_startPos.z, m_endPos.z) + scaledLineWidth
			);
			
			return new Bounds
			{
				min = min,
				max = max
			};
		}

		/// <summary>
		/// Updates the bounds of this line according to the current properties, 
		/// which there are: start point, end point, line width, scaling of the object.
		/// </summary>
		public void UpdateBounds()
		{
			if (null != m_meshFilter)
			{
				var mesh = m_meshFilter.sharedMesh;
				Debug.Assert(null != mesh);
				if (null != mesh)
				{
					mesh.bounds = CalculateBounds();
				}
			}
		}

		/// <summary>
		/// Sets the start and end points - updates the data of the Mesh.
		/// </summary>
		public void SetStartAndEndPoints(Vector3 startPoint, Vector3 endPoint)
		{
			m_startPos = startPoint;
			m_endPos = endPoint;

			Vector3[] vertexPositions = {
				m_startPos,
				m_startPos,
				m_startPos,
				m_startPos,
				m_endPos,
				m_endPos,
				m_endPos,
				m_endPos,
			};
			
			Vector3[] other = {
				m_endPos,
				m_endPos,
				m_endPos,
				m_endPos,
				m_startPos,
				m_startPos,
				m_startPos,
				m_startPos,
			};

			if (null != m_meshFilter)
			{
				var mesh = m_meshFilter.sharedMesh;
				Debug.Assert(null != mesh);
				if (null != mesh)
				{
					mesh.vertices = vertexPositions;
					mesh.normals = other;
					UpdateBounds();
					UpdateCollider();
				}
			}
		}

		public void UpdateCollider()
		{
			if (null == m_collider)
			{
				// Try to get the component if it's null
				m_collider = GetComponent<CapsuleCollider>();
				if (null == m_collider)
				{
					// Still null? Log an error and exit.
					Debug.LogError("VolumetricLineBehavior: No CapsuleCollider found.");
					return;
				}
			}

			// Ensure the collider is Y-Axis aligned
			m_collider.direction = 1; // 1 = Y-Axis

			// Calculate the total length of the line
			float length = Vector3.Distance(m_startPos, m_endPos);

			// Calculate the center point of the line
			Vector3 center = (m_startPos + m_endPos) / 2f;

			// Set the collider properties
			m_collider.center = center;
			m_collider.height = length;
			// m_collider.radius = m_lineWidth / 2f; // Use half the line width for the radius
		}

		#endregion

		#region event functions
		void Start () 
		{
			m_collider = GetComponent<CapsuleCollider>();

			Mesh mesh = new Mesh();
			m_meshFilter = GetComponent<MeshFilter>();
			m_meshFilter.mesh = mesh;
			SetStartAndEndPoints(m_startPos, m_endPos);
			RecordInitialStart();
			mesh.uv = VolumetricLineVertexData.TexCoords;
			mesh.uv2 = VolumetricLineVertexData.VertexOffsets;
			mesh.SetIndices(VolumetricLineVertexData.Indices, MeshTopology.Triangles, 0);
			CreateMaterial();
			// TODO: Need to set vertices before assigning new Mesh to the MeshFilter's mesh property => Why?
		}

		void OnDestroy()
		{
			if (null != m_meshFilter) 
			{
				if (Application.isPlaying) 
				{
					Mesh.Destroy(m_meshFilter.sharedMesh);
				}
				else // avoid "may not be called from edit mode" error
				{
					Mesh.DestroyImmediate(m_meshFilter.sharedMesh);
				}
				m_meshFilter.sharedMesh = null;
			}
			DestroyMaterial();
		}
		
		void Update()
		{



			// This will increase the length of the line
			if (!this.isHit)
            {
                Vector3 growthDirection = (this.EndPos - this.StartPos).normalized;
				if (growthDirection.sqrMagnitude <= Mathf.Epsilon)
				{
					growthDirection = Vector3.forward;
				}

				if (growFromStart)
				{
					Vector3 currentStartPos = this.StartPos;
					currentStartPos -= growthDirection * laserSpeed;
					this.StartPos = currentStartPos;
				}
				else
				{
					Vector3 currentEndPos = this.EndPos;
					currentEndPos += growthDirection * laserSpeed;
					this.EndPos = currentEndPos;
				}
            }


			if (transform.hasChanged)
			{
				UpdateLineScale();
				UpdateBounds();
			}

		}

		void OnValidate()
		{
			// This function is called when the script is loaded or a value is changed in the inspector (Called in the editor only).
			//  => make sure, everything stays up-to-date
			if(string.IsNullOrEmpty(gameObject.scene.name) || string.IsNullOrEmpty(gameObject.scene.path)) {
				return; // ...but not if a Prefab is selected! (Only if we're using it within a scene.)
			}
			CreateMaterial();
			SetAllMaterialProperties();
			UpdateBounds();
			UpdateCollider();
		}
	
		void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawLine(gameObject.transform.TransformPoint(m_startPos), gameObject.transform.TransformPoint(m_endPos));
		}
		#endregion

		#region Collision Stuff

		void OnTriggerEnter(Collider other)
		{
			if (this.isHit || !other.CompareTag("Mirror"))
			{
				return;
			}

			this.isHit = true;

			Vector3 incomingDirection = GetIncomingDirection();

			if (TryGetHitInfo(other, incomingDirection, out RaycastHit hitInfo))
			{
				Vector3 orientedNormal = EnsureNormalFacesLaser(hitInfo.normal, incomingDirection);
				Vector3 reflectedDirection = Vector3.Reflect(incomingDirection, orientedNormal).normalized;
				ClampLaserToHit(hitInfo.point);
				RestoreInitialStart();
				SpawnReflectedLaser(hitInfo.point, reflectedDirection);
			}
			else
			{
				Vector3 fallbackNormal = EnsureNormalFacesLaser(GetMirrorNormal(other), incomingDirection);
				Vector3 reflectedDirection = Vector3.Reflect(incomingDirection, fallbackNormal).normalized;
				Vector3 fallbackPoint = other.ClosestPoint(transform.position);
				ClampLaserToHit(fallbackPoint);
				RestoreInitialStart();
				SpawnReflectedLaser(fallbackPoint, reflectedDirection);
			}

			Debug.Log("Laser hit a mirror trigger!");
		}

		private Vector3 GetIncomingDirection()
		{
			Vector3 worldStart = transform.TransformPoint(StartPos);
			Vector3 worldEnd = transform.TransformPoint(EndPos);
			Vector3 direction = (worldEnd - worldStart).normalized;

			if (direction.sqrMagnitude <= Mathf.Epsilon)
			{
				direction = transform.forward;
			}

			return direction;
		}

		private Vector3 GetMirrorNormal(Collider mirrorCollider)
		{
			Mirror mirror = mirrorCollider.GetComponent<Mirror>();
			if (mirror != null)
			{
				return mirror.GetWorldNormal();
			}

			return mirrorCollider.transform.forward;
		}

		private bool TryGetHitInfo(Collider mirrorCollider, Vector3 incomingDirection, out RaycastHit hitInfo)
		{
			Vector3 worldStart = transform.TransformPoint(StartPos);
			Vector3 worldEnd = transform.TransformPoint(EndPos);
			float maxDistance = Vector3.Distance(worldStart, worldEnd) + 2f;

			Ray ray = new Ray(worldStart, incomingDirection);
			if (mirrorCollider.Raycast(ray, out hitInfo, maxDistance))
			{
				return true;
			}

			hitInfo = new RaycastHit
			{
				point = worldEnd,
				normal = GetMirrorNormal(mirrorCollider)
			};
			return false;
		}

		private Vector3 EnsureNormalFacesLaser(Vector3 surfaceNormal, Vector3 incomingDirection)
		{
			Vector3 normalizedNormal = surfaceNormal.normalized;
			if (Vector3.Dot(incomingDirection, normalizedNormal) > 0f)
			{
				normalizedNormal = -normalizedNormal;
			}

			return normalizedNormal;
		}

		private void ClampLaserToHit(Vector3 hitPoint)
		{
			Vector3 localHit = transform.InverseTransformPoint(hitPoint);
			EndPos = localHit;
		}

		private void SpawnReflectedLaser(Vector3 hitPoint, Vector3 reflectedDirection)
		{
			if (laserPrefab == null || reflectedDirection.sqrMagnitude <= Mathf.Epsilon)
			{
				Debug.LogWarning("VolumetricLineBehavior: Unable to spawn reflected laser. Missing prefab or invalid direction.");
				return;
			}

			Vector3 spawnPosition = hitPoint + reflectedDirection * spawnOffset;
			Quaternion spawnRotation = Quaternion.LookRotation(reflectedDirection, Vector3.up);

			GameObject newLaser = Instantiate(laserPrefab, spawnPosition, spawnRotation);
			VolumetricLineBehavior newBehavior = newLaser.GetComponent<VolumetricLineBehavior>();

			if (newBehavior == null)
			{
				Debug.LogWarning("VolumetricLineBehavior: Spawned prefab does not contain VolumetricLineBehavior.");
				return;
			}

			float currentLength = (EndPos - StartPos).magnitude;
			if (currentLength <= Mathf.Epsilon)
			{
				currentLength = 1f;
			}

			newBehavior.SetStartAndEndPoints(Vector3.zero, Vector3.forward * currentLength);
			newBehavior.growFromStart = false;
			newBehavior.isHit = false;
			newBehavior.lockStartOnHit = true;
			newBehavior.RecordInitialStart();
		}

		public void RecordInitialStart()
		{
			m_initialStartPos = StartPos;
			m_hasInitialStart = true;
		}

		private void RestoreInitialStart()
		{
			if (lockStartOnHit && m_hasInitialStart)
			{
				StartPos = m_initialStartPos;
			}
		}

		#endregion
	}
}
