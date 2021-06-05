using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using PokAEmon.Controllers;

public class AnswerPress : MonoBehaviour
{
    #region Unity Variables
    public QuizManager quizManager;
    #endregion

    #region Variables
    /// <summary>
    /// Speichert, ob angeklickte Antwort korrekt ist.
    /// </summary>
    public bool isCorrect = false;
    #endregion

    #region Methods
    /// <summary>
    /// Ermittelt den Text, der aktuell ausgewählten Antwort und prüft, ob diese korrekt ist.
    /// </summary>
    public void answerQuestion()
    {
        var pressedButton = EventSystem.current.currentSelectedGameObject;
        var pressedButtonText = pressedButton.transform.GetChild(0).GetComponent<Text>().text;
        isCorrect = ExerciseController.CheckIfAnwerIsCorrect(quizManager.exercise, pressedButtonText);
        quizManager.GetComponent<QuizManager>().Respond(isCorrect);
    }
    #endregion
}
