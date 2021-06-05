using PokAEmon.Model;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// MessageManager Script verwaltet Text Ausgaben in der Messagebox.
/// </summary>
public class MessageManager : MonoBehaviour
{
    #region Unity Variables

    public GameObject ui;
    public GameObject messageText;
    public GameObject player;
    public GameObject interactIcon;
    #endregion

    #region Variables

    private readonly float mTextDelay = 0.06f;
    private string mFullText = string.Empty;
    private string mCurrentText = string.Empty;
    #endregion

    #region Unity Methods

    private void Start()
    {
        ui.SetActive(true);
        player.GetComponent<PlayerController>().suspendMovement();
        var welcomeTextLine = PokAEmon.BackgroundWorkers.DataCache.AllTextLines.First();
        var welcomeText = welcomeTextLine.TextString;
        mFullText = ReplacePlayerName(welcomeText);
        StartCoroutine(DisplayText(ReplacePlayerName(mFullText)));
        welcomeTextLine.AlreadyTold = true;
        interactIcon.SetActive(true);
    }

    private void Update()
    {
        if (messageText.GetComponent<Text>().text == mFullText)
        {
            player.GetComponent<PlayerController>().resumeMovement();
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) 
                || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) 
                || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                ui.SetActive(false);
                mFullText = string.Empty;
            }
        }
    }
    #endregion

    #region Methods

    /// <summary>
    /// Gibt Text im Schreibmaschinen Effekt aus.
    /// </summary>
    /// <param name="textToDisplay">Text, der ausgegeben werden soll.</param>
    /// <returns></returns>
    private IEnumerator DisplayText(string textToDisplay)
    {
        for (int i = 0; i < textToDisplay.Length + 1; i++)
        {
            mCurrentText = textToDisplay.Substring(0, i);
            messageText.GetComponent<Text>().text = mCurrentText;
            yield return new WaitForSeconds(mTextDelay);
        }
    }

    /// <summary>
    /// Startet die Ausgabe eines Texts, zeigt die Textbox an und verbietet die Bewegung solange der Text angezeigt wird.
    /// </summary>
    /// <param name="textLine">Die aktuelle TextLine.</param>
    public void DisplayMessage(TextLine textLine)
    {
        ui.SetActive(true);
        interactIcon.SetActive(false);
        player.GetComponent<PlayerController>().suspendMovement();
        var textToDisplay = ReplacePlayerName(textLine.TextString);
        mFullText = textToDisplay;
        StartCoroutine(DisplayText(mFullText));
        textLine.AlreadyTold = true;
        
    }

    /// <summary>
    /// Gibt eine Fehlernachricht aus.
    /// </summary>
    /// <param name="message">Die anzuzeigende Nachricht.</param>
    public void DisplayWrongInteractionMessage(string message)
    {
        ui.SetActive(true);
        interactIcon.SetActive(false);
        player.GetComponent<PlayerController>().suspendMovement();
        mFullText = message;
        StartCoroutine(DisplayText(mFullText));
    }

    /// <summary>
    /// Ersetzt PlayerName aus der JSON Datei mit dem eigentlichen Spielernamen.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    private string ReplacePlayerName(string text)
    {
        if (text.Contains("PlayerName"))
        {
            var textWithPlayerName = text.Replace("PlayerName", PokAEmon.BackgroundWorkers.DataCache.CurrentPlayer.PlayerName);
            return textWithPlayerName;
        }
        else
        {
            return text;
        }
    }
    #endregion
}
