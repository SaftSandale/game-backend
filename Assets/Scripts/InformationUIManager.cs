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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ui.SetActive(false);
            player.GetComponent<PlayerController>().resumeMovement();
        }
    }

    public void WakeInfoMenu()
    {
        levelText.text = PokAEmon.BackgroundWorkers.Cache.CurrentPlayer.PlayerExperience.Level.ToString();
        xpText.text = PokAEmon.BackgroundWorkers.Cache.CurrentPlayer.PlayerExperience.XP.ToString() + "/" + PokAEmon.BackgroundWorkers.Cache.CurrentPlayer.PlayerExperience.NeededXPForNextLevel.ToString();
        fillImage.fillAmount = PokAEmon.BackgroundWorkers.Cache.CurrentPlayer.PlayerExperience.XP / PokAEmon.BackgroundWorkers.Cache.CurrentPlayer.PlayerExperience.NeededXPForNextLevel;

        player.GetComponent<PlayerController>().suspendMovement();
        ui.SetActive(true);
    }
}
