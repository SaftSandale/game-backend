using PokAEmon.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopulateList : MonoBehaviour
{
    #region Unity Variables

    public GameObject prefab;
	public List<Subject> Subjects;
	public GameObject EditModeSelectionWindow;
	public Tuple<string, Exercise> SelectedExercise;
	private List<GameObject> ButtonList = new List<GameObject>();
    #endregion

    #region Unity Methods

    private void Start()
	{
		Subjects = PokAEmon.BackgroundWorkers.DataCache.AllSubjects;
		Populate();
	}
    #endregion

    #region Methods

	/// <summary>
	/// Befüllt Editor UI mit Daten der Aufgaben.
	/// </summary>
    private void Populate()
	{
		GameObject newObj;
		foreach (Subject subject in Subjects)
        {
			foreach (Exercise exercise in subject.Exercises)
            {
				newObj = (GameObject)Instantiate(prefab, transform);

				newObj.GetComponent<Button>().onClick.AddListener(delegate () { EditModeSelectionWindow.SetActive(true); SelectedExercise = new Tuple<string, Exercise>(subject.SubjectName ,exercise); });
				newObj.transform.GetChild(0).GetComponent<Text>().text = subject.SubjectName;
				newObj.transform.GetChild(1).GetComponent<Text>().text = exercise.ExerciseTopic;
				newObj.transform.GetChild(2).GetComponent<Text>().text = exercise.ExerciseText;

				ButtonList.Add(newObj.gameObject);
			}
        }
	}

	/// <summary>
	/// Lädt die Startseite des Editors neu.
	/// </summary>
	public void ReloadScrollView()
    {
		foreach (GameObject obj in ButtonList)
        {
			Destroy(obj.gameObject);
        }
		Subjects = PokAEmon.BackgroundWorkers.DataCache.AllSubjects;
		Populate();
    }

	/// <summary>
	/// Öffnet die UI, um eine Aufgabe zu bearbeiten.
	/// </summary>
	public void EditSelectedExercise()
    {
		ExerciseEditor.SubjectName = SelectedExercise.Item1;
		ExerciseEditor.EditedExercise = SelectedExercise.Item2;
		ExerciseEditor.isNewExercise = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	/// <summary>
	/// Löscht die ausgewählte Aufgabe.
	/// </summary>
	public void DeleteSelectedExercise()
    {
		Subject currentSub = PokAEmon.BackgroundWorkers.DataCache.AllSubjects.FirstOrDefault(s => s.SubjectName == SelectedExercise.Item1);
		currentSub.RemoveExercise(SelectedExercise.Item2);
		EditModeSelectionWindow.SetActive(false);
		ReloadScrollView();
	}
    #endregion
}