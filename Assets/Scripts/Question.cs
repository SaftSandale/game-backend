using LearningGame.Backend.Controllers;
using LearningGame.Backend.Enums;
using LearningGame.Backend.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void interact()
    {
        Subject subject = new Subject("AE");
        var topic = "OOP";
        var difficulty = Difficulty.Easy;
        var exerciseController = new ExerciseController();
        var exercise = ExerciseController.GetRandomSuitableExercise(subject, topic, difficulty);

        Debug.Log(exercise.ExerciseText);
    }

    public override string getDescription()
    {
        return "What is ITS?";
    }
}
