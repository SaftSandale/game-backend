using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    #region Unity Variables
    public GameObject ui;
    public GameObject resumeButton;
    public GameObject editorButton;
    public GameObject exitButton;
    public GameObject player;
    #endregion

    #region Unity Methods
    void Start()
    {
        ui.SetActive(false);
    }
    #endregion

    public void WakePauseMenu()
    {
        player.GetComponent<PlayerController>().suspendMovement();
        ui.SetActive(true);
    }

    public void ResumeGame()
    {
        player.GetComponent<PlayerController>().resumeMovement();
        ui.SetActive(false);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void ExitGame()
    {
        PokAEmon.BackgroundWorkers.Cache.SaveCacheToJson();
        Application.Quit();
    }
}
