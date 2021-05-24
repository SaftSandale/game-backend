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
    #region Unity Variables
    public GameObject ui;
    public Exercise exercise;
    public GameObject[] abuttons;
    public GameObject questionText;
    public GameObject player;
    #endregion

    #region Unity Methods
    void Start()
    {
        ui.SetActive(false);
    }
    #endregion

    #region Methods
    /// <summary>
    /// Lädt eine Aufgabe und sperrt die Bewegung des Spielers. Zeigt anschließend die UI für die  Aufgabe an.
    /// </summary>
    public void wakeQuizManager()
    {
        getExercise();
        player.GetComponent<PlayerController>().suspendMovement();
        ui.SetActive(true);
    }

    /// <summary>
    /// Passt die Erfahrung des Spielers an, wenn die Frage richtig  beantwortet wurde und schaltet die Bewegung wieder frei.
    /// </summary>
    /// <param name="isCorrect">Boolean, ob die Frage richtig beantwortet wurde.</param>
    public void respond(bool isCorrect)
    {
        PokAEmon.BackgroundWorkers.Cache.CurrentPlayer.UpdateXP(exercise.Difficulty, isCorrect);
        ui.SetActive(false);
        player.GetComponent<PlayerController>().resumeMovement();
    }

    /// <summary>
    /// Ermittelt eine Aufgabe passend zu Thema, Fach und Schwierigkeit.
    /// </summary>
    void getExercise()
    {
        //This information should come from object or position
        Subject subject = new Subject("Anwendungsentwicklung");
        var topic = "OOP";
        var difficulty = Difficulty.Easy;
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
            else if (answers.Count() == 0)
            {
                button.SetActive(false);
            }
        }
        MainMenu.QuestionCache.addElement(exercise.ID);

        Debug.Log(exercise.ExerciseText);
    }
    #endregion
}
