using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene("Office-Level1");
    }

    public void ReturnToStart()
    {
        SceneManager.LoadScene("Start-Menu");
    }

    public void QuitGame()
    {
        Debug.Log("The game has QUIT!"); // the application won't close in unity editor
;        Application.Quit();
    }
}
