using PokAEmon.BackgroundWorkers;
using PokAEmon.Controllers;
using PokAEmon.Enums;
using PokAEmon.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    #region Unity Variables
    public GameObject ui;
    public Exercise exercise;
    public GameObject[] abuttons;
    public GameObject questionText;
    public GameObject player;
    public GameObject input_Subject;
    public GameObject input_Topic;
    public GameObject input_Difficulty;
    public bool IsQuizManagerActive;
    private Image image;
    private Color save;
    private bool highGrass;
    #endregion

    #region Unity Methods
    void Start()
    {
        ui.transform.GetChild(0).gameObject.SetActive(false);
        ui.transform.GetChild(1).gameObject.SetActive(false);
    }
    #endregion

    #region Methods
    /// <summary>
    /// Lädt eine Aufgabe und sperrt die Bewegung des Spielers. Zeigt anschließend die UI für die  Aufgabe an.
    /// </summary>
    public void wakeQuizManager(string _subject, string _topic, Difficulty _difficulty, bool _highGrass)
    {
        IsQuizManagerActive = true;
        highGrass = _highGrass;
        player.GetComponent<PlayerController>().suspendMovement();
        FillExcersiseUI(_subject, _topic, _difficulty);
        ui.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void wakeExtendedQuizManager()
    {
        IsQuizManagerActive = true;
        player.GetComponent<PlayerController>().suspendMovement();
        ui.transform.GetChild(1).gameObject.SetActive(true);
        input_Subject.GetComponent<TMP_Dropdown>().options.Add(new TMPro.TMP_Dropdown.OptionData("Fach auswählen"));

        foreach (var subject in PokAEmon.BackgroundWorkers.Cache.AllSubjects)
        {
            input_Subject.GetComponent<TMP_Dropdown>().options.Add(new TMPro.TMP_Dropdown.OptionData(subject.SubjectName));

        }
    }

    public void Input_Subject_TopicChange()
    {
        string subjectName = input_Subject.GetComponent<TMP_Dropdown>().options[input_Subject.GetComponent<TMP_Dropdown>().value].text;
        Subject subject = PokAEmon.BackgroundWorkers.Cache.AllSubjects.FirstOrDefault(s => s.SubjectName == subjectName);
        foreach(Exercise ex in subject.Exercises)
        {
            if(input_Topic.GetComponent<TMP_Dropdown>().options.FirstOrDefault(x => x.text == ex.ExerciseTopic) == null)
            {
                input_Topic.GetComponent<TMP_Dropdown>().options.Add(new TMPro.TMP_Dropdown.OptionData(ex.ExerciseTopic));
            }
            
        }
        
    }

    /// <summary>
    /// Passt die Erfahrung des Spielers an, wenn die Frage richtig  beantwortet wurde und schaltet die Bewegung wieder frei.
    /// </summary>
    /// <param name="isCorrect">Boolean, ob die Frage richtig beantwortet wurde.</param>
    public void respond(bool isCorrect)
    {
        image = ui.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        save = image.color;
        PokAEmon.BackgroundWorkers.Cache.CurrentPlayer.UpdateXP(exercise.Difficulty, isCorrect);
        PokAEmon.BackgroundWorkers.Cache.SaveAmountCorrectAnsweredQuestion(exercise, isCorrect);
        ui.SetActive(false);
        player.GetComponent<PlayerController>().resumeMovement();
        if (isCorrect)
        {
            image.color = Color.green;
        }
        else
        {
            image.color = Color.red;
        }
        Invoke("ResetColor", 0.25f);
        

        Subject subject = PokAEmon.BackgroundWorkers.Cache.AllSubjects.FirstOrDefault(s => s.Exercises.Contains(exercise));
        if (!highGrass)
        {
            wakeQuizManager(subject.SubjectName, exercise.ExerciseTopic, exercise.Difficulty, highGrass);
        }
        IsQuizManagerActive = !highGrass;
    }

    void ResetColor()
    {
        image.color = save;   
    }


    public void cancel()
    {
        ui.transform.GetChild(0).gameObject.SetActive(false);
        ui.transform.GetChild(1).gameObject.SetActive(false);
        player.GetComponent<PlayerController>().resumeMovement();
        input_Subject.GetComponent<TMP_Dropdown>().options.Clear();
        IsQuizManagerActive = false;
    }

    public void confirm()
    {
        string subjectName = input_Subject.GetComponent<TMP_Dropdown>().options[input_Subject.GetComponent<TMP_Dropdown>().value].text;
        Subject subject = PokAEmon.BackgroundWorkers.Cache.AllSubjects.FirstOrDefault(s => s.SubjectName == subjectName);
        string topic = input_Topic.GetComponent<TMP_Dropdown>().options[input_Subject.GetComponent<TMP_Dropdown>().value].text;
        Difficulty difficulty = ((Difficulty)((input_Difficulty.GetComponent<TMP_Dropdown>().value + 1) * 10));

        FillExcersiseUI(subjectName, topic, difficulty);
        ui.transform.GetChild(0).gameObject.SetActive(true);
        ui.transform.GetChild(1).gameObject.SetActive(false);
    }

    /// <summary>
    /// Ermittelt eine Aufgabe passend zu Thema, Fach und Schwierigkeit.
    /// </summary>
    void FillExcersiseUI(string subjectName, string topic, Difficulty difficulty)
    {
        Subject subject = PokAEmon.BackgroundWorkers.Cache.AllSubjects.FirstOrDefault(s => s.SubjectName == subjectName);
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
