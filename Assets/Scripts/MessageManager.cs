using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    public GameObject ui;
    public GameObject messageText;
    public GameObject player;

    public GameObject interactIcon;

    //public TextAsset textFile;
    //public string[] textLines;

    public float delay = 0.06f;
    public string fullText = "Willkommen bei PokAEmon " + PokAEmon.BackgroundWorkers.Cache.CurrentPlayer.PlayerName + "! \n" +
        "Nutze die Pfeiltasten oder WASD um dich zu bewegen. Drücke E, wenn du in meiner Nähe bist, um mit mir zu interagieren!";
    private string currentText = string.Empty;

    // Start is called before the first frame update
    void Start()
    {
        //if (textFile != null)
        //{
        //    textLines = (textFile.text.Split('\n'));
        //}
        ui.SetActive(true);
        player.GetComponent<PlayerController>().suspendMovement();
        StartCoroutine(DisplayText());
        interactIcon.SetActive(true);
    }

    private IEnumerator DisplayText()
    {
        for (int i = 0; i < fullText.Length + 1; i++)
        {
            currentText = fullText.Substring(0, i);
            messageText.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }

    public void WakeMessageBox()
    {
        ui.SetActive(true);
        interactIcon.SetActive(false);
        fullText = "Sehr gut, den ersten Schritt hast du geschafft! Im laufe des Spiels musst du Fragen beantworten, um aus der Schule zu entkommen..." +
            "Die Schule ist in Bereiche unterteilt. Beantwortest du 10 Fragen zu einem Thema richtig, schaltest du den nächsten Bereich frei...";
        StartCoroutine(DisplayText());
    }
}
