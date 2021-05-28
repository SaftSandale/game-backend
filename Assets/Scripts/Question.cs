using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : Interactable
{
    #region Unity Variables
    public QuizManager quizManager;
    public Area area;
    #endregion

    #region Overwritten Methods
    /// <summary>
    /// Zeigt die UI f�r Aufgaben an.
    /// </summary>
    public override void interact()
    {
        quizManager.wakeQuizManager(area.subject, area.topic, area.difficulty, false);
    }
    #endregion
}
