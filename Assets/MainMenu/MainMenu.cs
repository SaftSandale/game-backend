using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static PokAEmon.BackgroundWorkers.Cache questionCache { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        questionCache = new PokAEmon.BackgroundWorkers.Cache(100);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex  + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
