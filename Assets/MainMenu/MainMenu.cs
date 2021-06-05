using PokAEmon.BackgroundWorkers;
using PokAEmon.Model;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Unity Script des Hauptmenüs.
/// </summary>
public class MainMenu : MonoBehaviour
{
    #region Public Variables

    /// <summary>
    /// Property, die Instanz der DataCache Klasse speichert.
    /// </summary>
    public static DataCache QuestionCache { get; set; }

    /// <summary>
    /// Gibt an, ob der Nutzer vom Editor zurück ins Hauptmenu geleitet wurde.
    /// </summary>
    public static bool returnedFromEditor = false;
    #endregion

    #region Unity Variables
    public GameObject nameTextBox;
    public GameObject infoMessage;
    public GameObject mainMenu;
    public GameObject playMenu;
    public GameObject editorMenu;
    #endregion

    #region Unity Methods
    private void Start()
    {
        if (QuestionCache == null)
            QuestionCache = new DataCache(100);
        if (returnedFromEditor)
        {
            ChangeToEditorMenu();
            returnedFromEditor = false;
        }
    }
    #endregion

    #region Methods

    /// <summary>
    /// Wechselt im Hauptmenu die Ansicht zur Spielen Ansicht.
    /// </summary>
    public void ChangeToPlayMenu()
    {
        mainMenu.SetActive(false);
        playMenu.SetActive(true);
    }

    /// <summary>
    /// Wechselt im Hauptmenu die Ansicht zur Editor Ansicht.
    /// </summary>
    public void ChangeToEditorMenu()
    {
        mainMenu.SetActive(false);
        editorMenu.SetActive(true);
    }

    /// <summary>
    /// Wechselt im Hauptmenu die Ansicht, um eine Aufgabe zu erstellen.
    /// </summary>
    public void CreateNewExercise()
    {
        ExerciseEditor.SubjectName = "";
        ExerciseEditor.EditedExercise = new Exercise();
        ExerciseEditor.isNewExercise = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Wechselt die Szene von Hauptmenu zum eigentlichen Spiel.
    /// </summary>
    public void PlayGame()
    {
        string userName = nameTextBox.GetComponent<Text>().text;
        if (userName != String.Empty)
        {
            DataCache.CurrentPlayer = new Player(userName, new Experience());
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
        else
        {
            infoMessage.GetComponent<TextMeshProUGUI>().text = "Bitte erst einen Namen eingeben";
        }
    }

    /// <summary>
    /// Beendet das Spiel und speichert die JSON Datei.
    /// </summary>
    public void QuitGame()
    {
        DataCache.SaveCacheToJson();
        Application.Quit();
    }
    #endregion
}
