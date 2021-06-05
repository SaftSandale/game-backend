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
        ui.SetActive(false);
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
        ui.SetActive(true);
    }

    /// <summary>
    /// Schlie�t das Info Men�.
    /// </summary>
    public void CloseInfoMenu()
    {
        ui.SetActive(false);
        player.GetComponent<PlayerController>().resumeMovement();
    }
    #endregion
}
