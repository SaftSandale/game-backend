using PokAEmon.Enums;
using System.Collections;
using UnityEngine;

/// <summary>
/// Area Klasse definiert Bereiche, in denen der Spieler zuf�llig Aufgaben bekommen kann.
/// </summary>
[RequireComponent(typeof(PolygonCollider2D))]
public class Area : MonoBehaviour
{
    #region Unity Variables
    public PolygonCollider2D col;
    public string subject;
    public string topic;
    public Difficulty difficulty;
    public QuizManager quizManager;
    private float chance;
    private float rd;
    private IEnumerator CR;
    #endregion

    #region Unity Methods
    private void Start()
    {
        CR = MeasureTime();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(CR);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopCoroutine(CR);
            chance = 0;
        }
    }
    #endregion

    #region Methods

    /// <summary>
    /// Ermittelt die vergangene Zeit und erh�ht die Chance eine zuf�llige Aufgabe zu erhalten.
    /// </summary>
    /// <returns></returns>
    private IEnumerator MeasureTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (!quizManager.IsQuizManagerActive)
            { 
                IncreaseChance();
                WakeQuizManagerByChance();
            }
        }
    }

    /// <summary>
    /// Erh�ht die Chance eine zuf�llige Aufgabe zu erhalten.
    /// </summary>
    private void IncreaseChance()
    {
        rd = Random.Range(0f, 1f);
        if (chance <= 0.25)
        {
            chance = chance + 1f / 200;
        }
    }

    /// <summary>
    /// Ruft die Aufgaben UI auf, wenn der Spieler zuf�llig eine Aufgabe erh�lt.
    /// </summary>
    private void WakeQuizManagerByChance()
    {
        if (rd < chance)
        {
            quizManager.WakeQuizManager(subject, topic, difficulty, true);
            chance = 0f;
        }
    }
    #endregion
}
