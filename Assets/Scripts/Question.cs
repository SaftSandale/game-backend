using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : Interactable
{
    #region Unity Variables
    public QuizManager quizManager;
    #endregion

    #region Overwritten Methods
    /// <summary>
    /// Zeigt die UI für Aufgaben an.
    /// </summary>
    public override void interact()
    {
        quizManager.wakeQuizManager();
    }
    #endregion
}
