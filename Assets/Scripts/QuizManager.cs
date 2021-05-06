using PokAEmon.Controllers;
using PokAEmon.Enums;
using PokAEmon.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public GameObject ui;
    public Exercise exercise;
    public GameObject[] abuttons;
    public GameObject questionText;
    private GameObject test;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        ui.SetActive(false);
    }

    public void wakeQuizManager()
    {
        getExercise();
        getExercise();
        player.GetComponent<PlayerController>().suspendMovement();
        ui.SetActive(true);
    }

    public void respond(bool _isCorrect)
    {
        ui.SetActive(false);
        player.GetComponent<PlayerController>().resumeMovement();
    }

    void getExercise()
    {
        Subject subject = new Subject("Anwendungsentwicklung");
        var topic = "OOP";
        var difficulty = Difficulty.Easy;
        var exerciseController = new ExerciseController();
        var exercise = ExerciseController.GetRandomSuitableExercise(subject, topic, difficulty);

        questionText.GetComponent<Text>().text = exercise.ExerciseText;

        foreach(Answer answer in exercise.Answers)
        foreach(GameObject button in abuttons)
        {
            button.GetComponent<AnswerPress>().isCorrect = answer.isCorrect;
            button.transform.GetChild(0).GetComponent<Text>().text = answer.Text;
        }
    }
}
