using PokAEmon.BackgroundWorkers;
using PokAEmon.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// InformationUIManager Script ist f�r die Handhabung des Informationsmen�s zust�ndig.
/// </summary>
public class InformationUIManager : MonoBehaviour
{
    #region Unity Variables

    public Image fillImage;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI xpText;
    public Image endFillImage;
    public TextMeshProUGUI endLevelText;
    public TextMeshProUGUI endXpText;
    public GameObject player;
    public GameObject ui;
    public TextMeshProUGUI amountAnsweredQuestions;
    public TextMeshProUGUI amountAnsweredEasyQuestions;
    public TextMeshProUGUI amountAnsweredMediumQuestions;
    public TextMeshProUGUI amountAnsweredHardQuestions;
    #endregion

    #region Unity Methods

    private void Start()
    {
        ui.transform.GetChild(0).gameObject.SetActive(false);
        ui.transform.GetChild(1).gameObject.SetActive(false);
        levelText.text = DataCache.CurrentPlayer.PlayerExperience.Level.ToString();
        xpText.text = DataCache.CurrentPlayer.PlayerExperience.XP.ToString() + "/" + DataCache.CurrentPlayer.PlayerExperience.NeededXPForNextLevel.ToString();
    }
    #endregion

    #region Methods

    /// <summary>
    /// L�dt alle anzuzeigenden Daten und bef�llt die Textfelder des Info Men�s mit diesen Daten.
    /// </summary>
    public void WakeInfoMenu()
    {
        levelText.text = DataCache.CurrentPlayer.PlayerExperience.Level.ToString();
        xpText.text = DataCache.CurrentPlayer.PlayerExperience.XP.ToString() + "/" + DataCache.CurrentPlayer.PlayerExperience.NeededXPForNextLevel.ToString();
        fillImage.fillAmount = DataCache.CurrentPlayer.PlayerExperience.XP / DataCache.CurrentPlayer.PlayerExperience.NeededXPForNextLevel;

        amountAnsweredQuestions.text = DataCache.TotalAnsweredQuestions.ToString();
        amountAnsweredEasyQuestions.text = DataCache.GetAmountOfCorrectAnsweredExercisesForDifficulty(Difficulty.Easy).ToString();
        amountAnsweredMediumQuestions.text = DataCache.GetAmountOfCorrectAnsweredExercisesForDifficulty(Difficulty.Medium).ToString();
        amountAnsweredHardQuestions.text = DataCache.GetAmountOfCorrectAnsweredExercisesForDifficulty(Difficulty.Hard).ToString();

        player.GetComponent<PlayerController>().suspendMovement();
        ui.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void WakeEndScreen()
    {
        endLevelText.text = DataCache.CurrentPlayer.PlayerExperience.Level.ToString();
        endXpText.text = DataCache.CurrentPlayer.PlayerExperience.XP.ToString() + "/" + DataCache.CurrentPlayer.PlayerExperience.NeededXPForNextLevel.ToString();
        endFillImage.fillAmount = DataCache.CurrentPlayer.PlayerExperience.XP / DataCache.CurrentPlayer.PlayerExperience.NeededXPForNextLevel;

        player.GetComponent<PlayerController>().suspendMovement();
        ui.transform.GetChild(1).gameObject.SetActive(true);
    }

    /// <summary>
    /// Schlie�t das Info Men�.
    /// </summary>
    public void CloseInfoMenu()
    {
        ui.transform.GetChild(0).gameObject.SetActive(false);
        player.GetComponent<PlayerController>().resumeMovement();
    }
    #endregion
}
