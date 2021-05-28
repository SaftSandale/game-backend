using PokAEmon.Model;
using PokAEmon.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class Area : MonoBehaviour
{

    public PolygonCollider2D col;
    public string subject;
    public string topic;
    public Difficulty difficulty;
    public QuizManager quizManager;
    private float chance;
    private float rd;
    private IEnumerator CR;

    // Start is called before the first frame update
    void Start()
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
    IEnumerator MeasureTime()
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

    void IncreaseChance()
    {
        rd = Random.Range(0f, 1f);
        if (chance <= 0.25)
        {
            chance = chance + 1f / 200;
        }
    }

    void WakeQuizManagerByChance()
    {
        if (rd < chance)
        {
            quizManager.wakeQuizManager(subject, topic, difficulty, true);
            chance = 0f;
        }
    }
}
