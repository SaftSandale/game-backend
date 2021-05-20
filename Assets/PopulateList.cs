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
	public GameObject prefab; // This is our prefab object that will be exposed in the inspector

	public List<Subject> Subjects;

	public GameObject EditModeSelectionWindow;

	public Tuple<string, Exercise> SelectedExercise;

	private List<GameObject> ButtonList = new List<GameObject>();

	void Start()
	{
		Subjects = PokAEmon.BackgroundWorkers.Cache.AllSubjects;
		Populate();
	}

	void Update()
	{

	}

	void Populate()
	{
		GameObject newObj; // Create GameObject instance

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

	public void ReloadScrollView()
    {
		foreach (GameObject obj in ButtonList)
        {
			Destroy(obj.gameObject);
        }
		Subjects = PokAEmon.BackgroundWorkers.Cache.AllSubjects;
		Populate();
    }
	public void EditSelectedExercise()
    {
		ExerciseEditor.SubjectName = SelectedExercise.Item1;
		ExerciseEditor.EditedExercise = SelectedExercise.Item2;
		ExerciseEditor.isNewExercise = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
	public void DeleteSelectedExercise()
    {
		Subject currentSub = PokAEmon.BackgroundWorkers.Cache.AllSubjects.FirstOrDefault(s => s.SubjectName == SelectedExercise.Item1);
		currentSub.RemoveExercise(SelectedExercise.Item2);
		EditModeSelectionWindow.SetActive(false);
		ReloadScrollView();
	}
}