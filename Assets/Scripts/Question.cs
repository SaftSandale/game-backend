using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : Interactable
{
    public QuizManager quizManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void interact()
    {
        quizManager.wakeQuizManager();
    }

    public override string getDescription()
    {
        return "What is ITS?";
    }
}
