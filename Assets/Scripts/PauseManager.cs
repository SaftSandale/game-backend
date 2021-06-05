using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// PauseManager Script verwaltet das Pause Menü.
/// </summary>
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

    private void Start()
    {
        ui.SetActive(false);
    }
    #endregion

    #region Methods

    /// <summary>
    /// Ruft das Pause Menü auf.
    /// </summary>
    public void WakePauseMenu()
    {
        player.GetComponent<PlayerController>().suspendMovement();
        ui.SetActive(true);
    }

    /// <summary>
    /// Schließt das Pause Menü und kehrt zum Spiel zurück.
    /// </summary>
    public void ResumeGame()
    {
        player.GetComponent<PlayerController>().resumeMovement();
        ui.SetActive(false);
    }

    /// <summary>
    /// Kehrt zum Hauptmenü zurück.
    /// </summary>
    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    /// <summary>
    /// Schließt das Spiel.
    /// </summary>
    public void ExitGame()
    {
        PokAEmon.BackgroundWorkers.DataCache.SaveCacheToJson();
        Application.Quit();
    }
    #endregion
}
