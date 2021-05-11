using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using PokAEmon.Controllers;

public class AnswerPress : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;

    public void answerQuestion()
    {
        var pressedButton = EventSystem.current.currentSelectedGameObject;
        var pressedButtonText = pressedButton.transform.GetChild(0).GetComponent<Text>().text;
        isCorrect = ExerciseController.CheckIfAnwerIsCorrect(quizManager.exercise, pressedButtonText);
        quizManager.GetComponent<QuizManager>().respond(isCorrect);
    }
}
