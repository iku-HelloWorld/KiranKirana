using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quizquestion
{
    public string question;
    public string option1;
    public string option2;

    public string trueOption;

    public Quizquestion(string newquestion,string newoption1,string newoption2,string newtrueOption)
    {
        question = newquestion;
        option1 = newoption1;
        option2 = newoption2;
        trueOption = newtrueOption;
    }

   
}
