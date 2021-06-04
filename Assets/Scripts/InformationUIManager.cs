using PokAEmon.Enums;
using PokAEmon.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationUIManager : MonoBehaviour
{
    public Image fillImage;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI xpText;
    public GameObject player;
    public GameObject ui;
    public TextMeshProUGUI amountAnsweredQuestions;
    public TextMeshProUGUI amountAnsweredEasyQuestions;
    public TextMeshProUGUI amountAnsweredMediumQuestions;
    public TextMeshProUGUI amountAnsweredHardQuestions;

    protected float maxValue = PokAEmon.BackgroundWorkers.Cache.CurrentPlayer.PlayerExperience.NeededXPForNextLevel;
    protected float minValue = 0f;

    private float currentValue = PokAEmon.BackgroundWorkers.Cache.CurrentPlayer.PlayerExperience.XP;

    // Start is called before the first frame update
    void Start()
    {
        ui.SetActive(false);
        levelText.text = PokAEmon.BackgroundWorkers.Cache.CurrentPlayer.PlayerExperience.Level.ToString();
        xpText.text = PokAEmon.BackgroundWorkers.Cache.CurrentPlayer.PlayerExperience.XP.ToString() + "/" + PokAEmon.BackgroundWorkers.Cache.CurrentPlayer.PlayerExperience.NeededXPForNextLevel.ToString();
    }

    public void WakeInfoMenu()
    {
        levelText.text = PokAEmon.BackgroundWorkers.Cache.CurrentPlayer.PlayerExperience.Level.ToString();
        xpText.text = PokAEmon.BackgroundWorkers.Cache.CurrentPlayer.PlayerExperience.XP.ToString() + "/" + PokAEmon.BackgroundWorkers.Cache.CurrentPlayer.PlayerExperience.NeededXPForNextLevel.ToString();
        fillImage.fillAmount = PokAEmon.BackgroundWorkers.Cache.CurrentPlayer.PlayerExperience.XP / PokAEmon.BackgroundWorkers.Cache.CurrentPlayer.PlayerExperience.NeededXPForNextLevel;

        amountAnsweredQuestions.text = PokAEmon.BackgroundWorkers.Cache.TotalAnsweredQuestions.ToString();
        amountAnsweredEasyQuestions.text = PokAEmon.BackgroundWorkers.Cache.GetAmountOfCorrectAnsweredExercisesForDifficulty(Difficulty.Easy).ToString();
        amountAnsweredMediumQuestions.text = PokAEmon.BackgroundWorkers.Cache.GetAmountOfCorrectAnsweredExercisesForDifficulty(Difficulty.Medium).ToString();
        amountAnsweredHardQuestions.text = PokAEmon.BackgroundWorkers.Cache.GetAmountOfCorrectAnsweredExercisesForDifficulty(Difficulty.Hard).ToString();

        player.GetComponent<PlayerController>().suspendMovement();
        ui.SetActive(true);
    }

    public void CloseInfoMenu()
    {
        ui.SetActive(false);
        player.GetComponent<PlayerController>().resumeMovement();
    }
}
