using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Levels()
    {
        SceneManager.LoadScene("Level Menu");
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("LastScene"));
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
