using PokAEmon.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateList : MonoBehaviour
{
	public GameObject prefab; // This is our prefab object that will be exposed in the inspector

	public List<Subject> Subjects;

	public GameObject EditModeSelectionWindow;

	void Start()
	{
		Subjects = PokAEmon.BackgroundWorkers.Cache.LoadAllSubjectsFromJson();
		Populate();
	}

	void Update()
	{

	}

	void Populate()
	{
		GameObject newObj; // Create GameObject instance

		List<Tuple<string, string, string>> exerciseList = new List<Tuple<string, string, string>>();
		foreach (Subject subject in Subjects)
        {
			foreach (Exercise exercise in subject.Exercises)
				exerciseList.Add(new Tuple<string, string, string>(subject.SubjectName, exercise.ExerciseTopic, exercise.ExerciseText));
        }
		for (int i = 0; i < exerciseList.Count; i++)
		{
			// Create new instances of our prefab until we've created as many as we specified
			newObj = (GameObject)Instantiate(prefab, transform);

			// Randomize the color of our image
			newObj.GetComponent<Button>().onClick.AddListener(delegate () { EditModeSelectionWindow.SetActive(true); });
			newObj.transform.GetChild(0).GetComponent<Text>().text = exerciseList[i].Item1;
			newObj.transform.GetChild(1).GetComponent<Text>().text = exerciseList[i].Item2;
			newObj.transform.GetChild(2).GetComponent<Text>().text = exerciseList[i].Item3;
		}

	}
}