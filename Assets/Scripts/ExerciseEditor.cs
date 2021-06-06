using PokAEmon.BackgroundWorkers;
using PokAEmon.Controllers;
using PokAEmon.Enums;
using PokAEmon.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExerciseEditor : MonoBehaviour
{
    #region Unity Variables

    public GameObject SubjectInput;
    public GameObject TopicInput;
    public GameObject ExerciseTextInput;
    public GameObject DifficultyDropDown;
    public GameObject AnswerPair1;
    public GameObject AnswerPair2;
    public GameObject AnswerPair3;
    #endregion

    #region Variables

    public static Exercise EditedExercise;
    public static string SubjectName;
    public static bool isNewExercise;
    #endregion

    #region Unity Methods
    void Start()
    {
        FillInputsByExercise();
        PreFillEasyAnswers();
    }
    #endregion

    #region Methods

    /// <summary>
    /// Füllt die Textboxen bei der Bearbeitung einer Aufgabe, mit den aktuellen Daten der ausgewählten Aufgabe.
    /// </summary>
    public void FillInputsByExercise()
    {
        if (EditedExercise != null)
        {
            SubjectInput.GetComponent<InputField>().text = SubjectName;
            TopicInput.GetComponent<InputField>().text = EditedExercise.ExerciseTopic;
            ExerciseTextInput.GetComponent<InputField>().text = EditedExercise.ExerciseText;
            if (EditedExercise.Answers != null && EditedExercise.Answers.Count > 0)
            {
                switch (EditedExercise.Difficulty)
                {
                    case PokAEmon.Enums.Difficulty.Easy:
                        DifficultyDropDown.GetComponent<Dropdown>().value = 0;
                        AnswerPair1.transform.GetChild(0).transform.GetChild(0).GetComponent<InputField>().text = EditedExercise.Answers[0].Text;
                        AnswerPair1.transform.GetChild(0).transform.GetChild(1).GetComponent<Toggle>().isOn = EditedExercise.Answers[0].IsCorrect;
                        AnswerPair1.transform.GetChild(1).transform.GetChild(0).GetComponent<InputField>().text = EditedExercise.Answers[1].Text;
                        AnswerPair1.transform.GetChild(1).transform.GetChild(1).GetComponent<Toggle>().isOn = EditedExercise.Answers[1].IsCorrect;
                        break;
                    case PokAEmon.Enums.Difficulty.Medium:
                        DifficultyDropDown.GetComponent<Dropdown>().value = 1;
                        AnswerPair1.transform.GetChild(0).transform.GetChild(0).GetComponent<InputField>().text = EditedExercise.Answers[0].Text;
                        AnswerPair1.transform.GetChild(0).transform.GetChild(1).GetComponent<Toggle>().isOn = EditedExercise.Answers[0].IsCorrect;
                        AnswerPair1.transform.GetChild(1).transform.GetChild(0).GetComponent<InputField>().text = EditedExercise.Answers[1].Text;
                        AnswerPair1.transform.GetChild(1).transform.GetChild(1).GetComponent<Toggle>().isOn = EditedExercise.Answers[1].IsCorrect;
                        AnswerPair2.transform.GetChild(0).transform.GetChild(0).GetComponent<InputField>().text = EditedExercise.Answers[2].Text;
                        AnswerPair2.transform.GetChild(0).transform.GetChild(1).GetComponent<Toggle>().isOn = EditedExercise.Answers[2].IsCorrect;
                        AnswerPair2.transform.GetChild(1).transform.GetChild(0).GetComponent<InputField>().text = EditedExercise.Answers[3].Text;
                        AnswerPair2.transform.GetChild(1).transform.GetChild(1).GetComponent<Toggle>().isOn = EditedExercise.Answers[3].IsCorrect;
                        break;
                    case PokAEmon.Enums.Difficulty.Hard:
                        DifficultyDropDown.GetComponent<Dropdown>().value = 2;
                        AnswerPair1.transform.GetChild(0).transform.GetChild(0).GetComponent<InputField>().text = EditedExercise.Answers[0].Text;
                        AnswerPair1.transform.GetChild(0).transform.GetChild(1).GetComponent<Toggle>().isOn = EditedExercise.Answers[0].IsCorrect;
                        AnswerPair1.transform.GetChild(1).transform.GetChild(0).GetComponent<InputField>().text = EditedExercise.Answers[1].Text;
                        AnswerPair1.transform.GetChild(1).transform.GetChild(1).GetComponent<Toggle>().isOn = EditedExercise.Answers[1].IsCorrect;
                        AnswerPair2.transform.GetChild(0).transform.GetChild(0).GetComponent<InputField>().text = EditedExercise.Answers[2].Text;
                        AnswerPair2.transform.GetChild(0).transform.GetChild(1).GetComponent<Toggle>().isOn = EditedExercise.Answers[2].IsCorrect;
                        AnswerPair2.transform.GetChild(1).transform.GetChild(0).GetComponent<InputField>().text = EditedExercise.Answers[3].Text;
                        AnswerPair2.transform.GetChild(1).transform.GetChild(1).GetComponent<Toggle>().isOn = EditedExercise.Answers[3].IsCorrect;
                        AnswerPair3.transform.GetChild(0).transform.GetChild(0).GetComponent<InputField>().text = EditedExercise.Answers[4].Text;
                        AnswerPair3.transform.GetChild(0).transform.GetChild(1).GetComponent<Toggle>().isOn = EditedExercise.Answers[4].IsCorrect;
                        AnswerPair3.transform.GetChild(1).transform.GetChild(0).GetComponent<InputField>().text = EditedExercise.Answers[5].Text;
                        AnswerPair3.transform.GetChild(1).transform.GetChild(1).GetComponent<Toggle>().isOn = EditedExercise.Answers[5].IsCorrect;
                        break;
                    default:
                        break;
                }
            }

        }
        else EditedExercise = new Exercise();
    }

    /// <summary>
    /// Zeigt anhand der gewählten Schwierigkeit Eingabemöglichkeiten für die Antworten an.
    /// </summary>
    /// <param name="difficulty">Die ausgewählte Schwierigkeit.</param>
    public void OnDifficultySelect()
    {
        int difficulty = DifficultyDropDown.GetComponent<Dropdown>().value;
        switch (difficulty)
        {
            case 0:
                EditedExercise.Difficulty = Difficulty.Easy;
                AnswerPair1.SetActive(true);
                AnswerPair2.SetActive(false);
                AnswerPair3.SetActive(false);
                break;
            case 1:
                EditedExercise.Difficulty = Difficulty.Medium;
                AnswerPair1.SetActive(true);
                AnswerPair2.SetActive(true);
                AnswerPair3.SetActive(false);
                break;
            case 2:
                EditedExercise.Difficulty = Difficulty.Hard;
                AnswerPair1.SetActive(true);
                AnswerPair2.SetActive(true);
                AnswerPair3.SetActive(true);
                break;
            default:
                EditedExercise.Difficulty = Difficulty.Easy;
                AnswerPair1.SetActive(true);
                AnswerPair2.SetActive(false);
                AnswerPair3.SetActive(false);
                break;
        }
        PreFillEasyAnswers();
    }

    /// <summary>
    /// Wechselt die Szene zur Menu-Szene mit aktiviertem Editor-Menü.
    /// </summary>
    public void ReturnToEditorMenu()
    {
        MainMenu.returnedFromEditor = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    /// <summary>
    /// Speichert den aktuellen Stand der Aufgabe mit den eingegebenen Daten.
    /// </summary>
    public void SaveExercise()
    {
        string subjectname = SubjectInput.GetComponent<InputField>().text;
        string topic = TopicInput.GetComponent<InputField>().text;
        string text = ExerciseTextInput.GetComponent<InputField>().text;
        List<Answer> answers = CreateAnswerList();
        if (!string.IsNullOrEmpty(subjectname) && !string.IsNullOrEmpty(topic) && !string.IsNullOrEmpty(text) && answers != null)
        {
            if (isNewExercise)
            {
                if (!DataCache.AllSubjects.Any(s => s.SubjectName == subjectname))
                    DataCache.AllSubjects.Add(new Subject(subjectname));

                DataCache.AllSubjects.FirstOrDefault(s => s.SubjectName == subjectname).CreateExercise(text, topic, EditedExercise.Difficulty, answers);
            }
            else
            {
                if (!DataCache.AllSubjects.Any(s => s.SubjectName == subjectname))
                    DataCache.AllSubjects.Add(new Subject(subjectname, new List<Exercise>()));
                DataCache.AllSubjects.FirstOrDefault(s => s.SubjectName == subjectname).Exercises.FirstOrDefault(ex => ex.ID == EditedExercise.ID).EditExercise(text, topic, EditedExercise.Difficulty, answers);
            }
            ReturnToEditorMenu();
        }
    }

    /// <summary>
    /// Erstellt eine Liste an Antworten für eine Aufgabe.
    /// </summary>
    /// <returns>Eine Liste mit Antworten.</returns>
    private List<Answer> CreateAnswerList()
    {
        List<Answer> res = new List<Answer>();

        res.Add(new Answer(AnswerPair1.transform.GetChild(0).transform.GetChild(0).GetComponent<InputField>().text, AnswerPair1.transform.GetChild(0).transform.GetChild(1).GetComponent<Toggle>().isOn));
        res.Add(new Answer(AnswerPair1.transform.GetChild(1).transform.GetChild(0).GetComponent<InputField>().text, AnswerPair1.transform.GetChild(1).transform.GetChild(1).GetComponent<Toggle>().isOn));
        res.Add(new Answer(AnswerPair2.transform.GetChild(0).transform.GetChild(0).GetComponent<InputField>().text, AnswerPair2.transform.GetChild(0).transform.GetChild(1).GetComponent<Toggle>().isOn));
        res.Add(new Answer(AnswerPair2.transform.GetChild(1).transform.GetChild(0).GetComponent<InputField>().text, AnswerPair2.transform.GetChild(1).transform.GetChild(1).GetComponent<Toggle>().isOn));
        res.Add(new Answer(AnswerPair3.transform.GetChild(0).transform.GetChild(0).GetComponent<InputField>().text, AnswerPair3.transform.GetChild(0).transform.GetChild(1).GetComponent<Toggle>().isOn));
        res.Add(new Answer(AnswerPair3.transform.GetChild(1).transform.GetChild(0).GetComponent<InputField>().text, AnswerPair3.transform.GetChild(1).transform.GetChild(1).GetComponent<Toggle>().isOn));
        int amountCorrectAnswers = 0;
        foreach (Answer a in res)
        {
            if (a.IsCorrect)
            {
                amountCorrectAnswers++;
            }
            if (string.IsNullOrEmpty(a.Text))
            {
                res.Remove(a);
            }
        }
        if (amountCorrectAnswers != 0 && amountCorrectAnswers < res.Count)
        {
            return res;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Befüllt die ersten Antworten im Editor mit Wahr/Falsch bei einfacher Schwierigkeit
    /// </summary>
    private void PreFillEasyAnswers()
    {
        if (EditedExercise.Difficulty == Difficulty.Easy)
        {
            AnswerPair1.transform.GetChild(0).transform.GetChild(0).GetComponent<InputField>().text = "Wahr";
            AnswerPair1.transform.GetChild(0).transform.GetChild(0).GetComponent<InputField>().interactable = false;
            AnswerPair1.transform.GetChild(1).transform.GetChild(0).GetComponent<InputField>().text = "Falsch";
            AnswerPair1.transform.GetChild(1).transform.GetChild(0).GetComponent<InputField>().interactable = false;
        }
        else
        {
            AnswerPair1.transform.GetChild(0).transform.GetChild(0).GetComponent<InputField>().text = "";
            AnswerPair1.transform.GetChild(0).transform.GetChild(0).GetComponent<InputField>().interactable = true;
            AnswerPair1.transform.GetChild(1).transform.GetChild(0).GetComponent<InputField>().text = "";
            AnswerPair1.transform.GetChild(1).transform.GetChild(0).GetComponent<InputField>().interactable = true;
        }
    }

    #endregion
}
