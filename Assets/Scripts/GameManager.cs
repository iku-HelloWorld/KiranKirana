using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI question;

    public TextMeshProUGUI option1;
    public TextMeshProUGUI option2;

    public GameObject soruSayac;
    public GameObject cevapSayac;

    public bool rightA=false;
    public bool rightB = false;



    public GameObject A;
    public GameObject B;

    [SerializeField] float questionTimer = 10.0f;
    [SerializeField] float answeringTimer = 5.0f;

    private bool questionTimerBool = true;
    private bool answeringTimerBool = true;
    bool nextQuestion = false;


    int questionindex = 0;
    [SerializeField] int timer = 5;

    private void Start()
    {



    }
    private void Update()
    {
            List<Quizquestion> quizquestions = new List<Quizquestion>();
            quizquestions.Add(new Quizquestion("Ankaranýn baþkenti neresidir", "Ýstanbul", "Ankara", "A"));
            quizquestions.Add(new Quizquestion("Ýstanbul kaç yýlýnda fethedilmiþtir", "1453", "1456", "A"));
        quizquestions.Add(new Quizquestion("Özkan Harundan daha iyi cs oynar", "Doðru", "Doðru","A"));

        question.text = quizquestions[questionindex].question;
        option1.text = quizquestions[questionindex].option1;
        option2.text= quizquestions[questionindex].option2;

        if (quizquestions[questionindex].trueOption == "A")
        {
            rightA = true;
            Debug.Log("Doðru cevap a ");

        }
        else if(quizquestions[questionindex].trueOption == "B")
        {
            rightB = true;
            Debug.Log("Doðru cevap b");

        }



        if (questionTimerBool)
        {
            soruSayac.SetActive(true);
            soruSayac.GetComponent<TextMeshProUGUI>().text = "Düþünme aþamasý kalan süre:" + (int)questionTimer;
            answeringTimer = 3.0f;
            questionTimer -= Time.deltaTime;
        }
       

        if (questionTimer <= 0)
        {
            
            answeringPhase();
        }


        

        }

    private void answeringPhase()
    {
        answeringTimerBool = true;
        questionTimerBool = false;
        soruSayac.SetActive(false);
        if (answeringTimerBool)
        {
            answeringTimer -= Time.deltaTime;
            cevapSayac.SetActive(true);
            cevapSayac.GetComponent<TextMeshProUGUI>().text = "Cevaplamak için kalan süre" + (int)answeringTimer;
            if (answeringTimer <= 0)
            {
                questionTimer = 5.0f;
                answeringTimerBool = false;
                questionTimerBool = true;
                questionindex++;
                cevapSayac.SetActive(false);
            }


        }
        

        

    }

    //private void OnCollisionEnter(Collision collision)  //GAMEMANAGER COLLISION !!!!!! PLAYER SCRIPTININ ICINDEN PUBLIC DEGER ILE ALINACAK
    //{
    //    if (collision.gameObject.tag == "A")
    //    {
    //        Debug.Log("hit a");
    //        rightA = true;
    //    }
    //    else
    //    {
    //        rightB = true;
    //    }
    //}


}




    


    



