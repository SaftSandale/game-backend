using PokAEmon.BackgroundWorkers;
using PokAEmon.Controllers;
using PokAEmon.Enums;
using PokAEmon.Model;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

/// <summary>
/// QuizManager Script verwaltet alle Aktionen, die mit der UI für Aufgaben zu tun haben.
/// </summary>
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
    private string subject_Save;
    private string topic_Save;
    private Difficulty difficulty_Save;
    #endregion

    #region Unity Methods

    private void Start()
    {
        ui.transform.GetChild(0).gameObject.SetActive(false);
        ui.transform.GetChild(1).gameObject.SetActive(false);
        ui.transform.GetChild(2).gameObject.SetActive(false);
    }
    #endregion

    #region Methods

    /// <summary>
    /// Lädt eine Aufgabe und sperrt die Bewegung des Spielers. Zeigt anschließend die UI für die  Aufgabe an.
    /// </summary>
    public void WakeQuizManager(string _subject, string _topic, Difficulty _difficulty, bool _highGrass)
    {
        if (DataCache.AllSubjectsUnusedExercises.FirstOrDefault(s => s.SubjectName == _subject).Exercises.Where(e => e.ExerciseTopic == _topic && e.Difficulty == _difficulty).Count() != 0)
        {
            IsQuizManagerActive = true;
            highGrass = _highGrass;
            player.GetComponent<PlayerController>().suspendMovement();
            FillExcersiseUI(_subject, _topic, _difficulty);
            ui.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            ui.transform.GetChild(0).gameObject.SetActive(false);
            WakeEmptyQuizManager(_subject, _topic, _difficulty);
        }
    }

    /// <summary>
    /// Ruft den ExtendedQuizManager auf, der alle Aufgaben anzeigt und dem Spieler die Möglichkeit bietet, Aufgaben auszuwählen.
    /// </summary>
    public void WakeExtendedQuizManager()
    {
        IsQuizManagerActive = true;
        player.GetComponent<PlayerController>().suspendMovement();
        ui.transform.GetChild(1).gameObject.SetActive(true);
        input_Subject.GetComponent<TMP_Dropdown>().options.Add(new TMP_Dropdown.OptionData("Fach auswählen"));

        foreach (var subject in DataCache.AllSubjects)
        {
            input_Subject.GetComponent<TMP_Dropdown>().options.Add(new TMP_Dropdown.OptionData(subject.SubjectName));
        }
        input_Subject.GetComponent<TMP_Dropdown>().options.Add(new TMP_Dropdown.OptionData("  "));
    }

    /// <summary>
    /// Ruft den EmptyQuizManager auf, der dem Spieler sagt, dass es zum gewählten Thema und Schwierigkeit keine Aufgaben mehr gibt.
    /// </summary>
    public void WakeEmptyQuizManager(string _subject, string _topic, Difficulty _difficulty)
    {
        IsQuizManagerActive = false;
        ui.transform.GetChild(2).gameObject.SetActive(true);
        player.GetComponent<PlayerController>().suspendMovement();
        subject_Save = _subject;
        topic_Save = _topic;
        difficulty_Save = _difficulty;
    }

    /// <summary>
    /// Entfernt die IDs der Fragen dieses Themas aus den Cache.
    /// </summary>
    public void RefreshExcersiseCache()
    {
        List<Exercise> excersisesToRefresh = DataCache.AllSubjects.FirstOrDefault(s => s.SubjectName == subject_Save).Exercises.Where(e => e.ExerciseTopic == topic_Save && e.Difficulty == difficulty_Save).ToList();
        DataCache.DeleteSpecificIDsFromCache(excersisesToRefresh);
        ui.transform.GetChild(2).gameObject.SetActive(false);
        WakeQuizManager(subject_Save, topic_Save, difficulty_Save, false);
    }

    /// <summary>
    /// Ändert die angezeigten Felder im Drop-Down Menü anhand des Fachs.
    /// </summary>
    public void InputSubjectTopicChange()
    {
        string subjectName = input_Subject.GetComponent<TMP_Dropdown>().options[input_Subject.GetComponent<TMP_Dropdown>().value].text;
        Subject subject = DataCache.AllSubjects.FirstOrDefault(s => s.SubjectName == subjectName);
        foreach(Exercise ex in subject.Exercises)
        {
            if(input_Topic.GetComponent<TMP_Dropdown>().options.FirstOrDefault(x => x.text == ex.ExerciseTopic) == null)
            {
                input_Topic.GetComponent<TMP_Dropdown>().options.Add(new TMP_Dropdown.OptionData(ex.ExerciseTopic));
            }
        }
        input_Topic.GetComponent<TMP_Dropdown>().options.Add(new TMP_Dropdown.OptionData("  "));

    }

    /// <summary>
    /// Passt die Erfahrung des Spielers an, wenn die Frage richtig  beantwortet wurde und schaltet die Bewegung wieder frei.
    /// </summary>
    /// <param name="isCorrect">Boolean, ob die Frage richtig beantwortet wurde.</param>
    public void Respond(bool isCorrect)
    {
        image = ui.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        save = image.color;
        DataCache.CurrentPlayer.UpdateXP(exercise.Difficulty, isCorrect);
        DataCache.SaveAmountCorrectAnsweredQuestion(exercise, isCorrect);
        ui.transform.GetChild(0).gameObject.SetActive(false);
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
        

        Subject subject = DataCache.AllSubjects.FirstOrDefault(s => s.Exercises.Contains(exercise));
        if (!highGrass)
        {
            WakeQuizManager(subject.SubjectName, exercise.ExerciseTopic, exercise.Difficulty, highGrass);
        }
        IsQuizManagerActive = !highGrass;
    }

    /// <summary>
    /// Setzt die Farbe der UI zurück.
    /// </summary>
    void ResetColor()
    {
        image.color = save;   
    }

    /// <summary>
    /// Schaltet die QuizManager UI aus.
    /// </summary>
    public void Cancel()
    {
        ui.transform.GetChild(0).gameObject.SetActive(false);
        ui.transform.GetChild(1).gameObject.SetActive(false);
        ui.transform.GetChild(2).gameObject.SetActive(false);
        player.GetComponent<PlayerController>().resumeMovement();
        input_Subject.GetComponent<TMP_Dropdown>().options.Clear();
        IsQuizManagerActive = false;
    }

    /// <summary>
    /// Gibt die Daten, die im ExtendedQuizManager eingegeben wurden weiter.
    /// </summary>
    public void Confirm()
    {
        string subjectName = input_Subject.GetComponent<TMP_Dropdown>().options[input_Subject.GetComponent<TMP_Dropdown>().value].text;
        string topic = input_Topic.GetComponent<TMP_Dropdown>().options[input_Topic.GetComponent<TMP_Dropdown>().value].text;
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
        Subject subject = DataCache.AllSubjects.FirstOrDefault(s => s.SubjectName == subjectName);
        exercise = ExerciseController.GetRandomSuitableExercise(subject, topic, difficulty);

        questionText.GetComponent<Text>().text = exercise.ExerciseText;

        var answers = exercise.GetShuffledAnswers();
        foreach (GameObject button in abuttons)
        {
            if (answers.Count() > 0)
            {
                button.SetActive(true);
                var currentAnswer = answers.FirstOrDefault();
                button.transform.GetChild(0).GetComponent<Text>().text = currentAnswer.Text;
                answers.Remove(currentAnswer);
            }
            else if (answers.Count() == 0)
            {
                button.SetActive(false);
            }
        }
        MainMenu.QuestionCache.AddElement(exercise.ID);

        Debug.Log(exercise.ExerciseText);
    }
    #endregion
}
