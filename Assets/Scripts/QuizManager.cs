using PokAEmon.BackgroundWorkers;
using PokAEmon.Controllers;
using PokAEmon.Enums;
using PokAEmon.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public GameObject ui;
    public Exercise exercise;
    public GameObject[] abuttons;
    public GameObject questionText;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        ui.SetActive(false);
    }

    public void wakeQuizManager()
    {
        getExercise();
        player.GetComponent<PlayerController>().suspendMovement();
        ui.SetActive(true);
    }

    public void respond(bool isCorrect)
    {
        if (isCorrect)
        {
            Debug.Log("Diese Antwort ist korrekt!");
            PokAEmon.BackgroundWorkers.Cache.CurrentPlayer.UpdateXP(exercise.Difficulty, true);
        }
        else if (!isCorrect)
        {
            Debug.Log("FALSCH!");
            PokAEmon.BackgroundWorkers.Cache.CurrentPlayer.UpdateXP(exercise.Difficulty, false);
        }
        else
        {
            Debug.Log("Ein Fehler ist aufgetreten.");
        }
        ui.SetActive(false);
        player.GetComponent<PlayerController>().resumeMovement();
    }

    void getExercise()
    {
        //This information should come from object or position
        Subject subject = new Subject("Anwendungsentwicklung");
        var topic = "OOP";
        var difficulty = Difficulty.Easy;
        var exerciseController = new ExerciseController();
        exercise = ExerciseController.GetRandomSuitableExercise(subject, topic, difficulty);

        questionText.GetComponent<Text>().text = exercise.ExerciseText;

        var answers = exercise.GetShuffledAnswers();
        foreach (GameObject button in abuttons)
        {
            if (answers.Count() > 0)
            {
                var currentAnswer = answers.FirstOrDefault();
                button.transform.GetChild(0).GetComponent<Text>().text = currentAnswer.Text;
                answers.Remove(currentAnswer);
            }
            else if(answers.Count() == 0)
            {
                button.SetActive(false);
            }
        }
        MainMenu.QuestionCache.addElement(exercise.ID);

        Debug.Log(exercise.ExerciseText);
    }
}
