using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerPress : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;

    public void answerQuestion()
    {
        quizManager.GetComponent<QuizManager>().respond(isCorrect);
    }
}
