using PokAEmon.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static PokAEmon.BackgroundWorkers.Cache QuestionCache { get; set; }


    public GameObject nameTextBox;
    public GameObject infoMessage;
    public GameObject mainMenu;
    public GameObject playMenu;
    public GameObject editorMenu;

    private 

    // Start is called before the first frame update
    void Start()
    {
        QuestionCache = new PokAEmon.BackgroundWorkers.Cache(100);
    }

    public void ChangeToPlayMenu()
    {
        mainMenu.SetActive(false);
        playMenu.SetActive(true);
    }
    public void ChangeToEditorMenu()
    {
        mainMenu.SetActive(false);
        editorMenu.SetActive(true);
    }

    

    public void PlayGame()
    {
        string userName = nameTextBox.GetComponent<Text>().text;
        if (userName != String.Empty)
        {
            PokAEmon.BackgroundWorkers.Cache.CurrentPlayer = new Player(userName, new Experience());
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            infoMessage.GetComponent<TextMeshProUGUI>().text = "Bitte erst einen Namen eingeben";
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
