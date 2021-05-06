using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class QuestionAnswer
{
    public string question;
    public string[] answers;
    public int correctAnswer;
    public enum difficulty
    {
        Easy,
        Medium,
        Hard
    }
    public enum questionType
    {
        SingleChoice,
        MultipleChoice
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
