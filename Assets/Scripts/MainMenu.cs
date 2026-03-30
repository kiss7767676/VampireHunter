using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    public string firstLevelName;
    public string secondLevelName;


    public void StartGame1()
    {

        SceneManager.LoadScene(firstLevelName);
    }

    public void StartGame2()
    {
        SceneManager.LoadScene(secondLevelName);
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("I'm Quitting");
    }
}
