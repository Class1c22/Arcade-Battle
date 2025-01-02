using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void PlayGame()
    {

        SceneManager.LoadScene("Game");
    }

    public void ShowRules()

    {
        SceneManager.LoadScene("Rules");

    }
    public void Menu()
    {
        
        SceneManager.LoadScene("PreGame");
    }
}
