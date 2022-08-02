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

    public GameObject questionPanel;
    
    //public GameObject barrier;

    public bool rightA=false;
    public bool rightB = false;

    private Collider[] barrierColliders;

    public GameObject A;
    public GameObject B;

    [SerializeField] float questionTimer = 10.0f;
    [SerializeField] float answeringTimer = 5.0f;

    [SerializeField] float goTime = 5.0f;

    private bool questionTimerBool = true;
    private bool answeringTimerBool = true;
    private bool goTimeBool;
    


    int questionindex = 0;
  

    private void Start()
    {
        GameObject[] barrier = GameObject.FindGameObjectsWithTag("Barrier");
        barrierColliders = new Collider[barrier.Length];
        for(int i = 0; i < barrierColliders.Length; i++)
        {
            barrierColliders[i] = barrier[i].GetComponent<Collider>();
        }
        goTimeBool = true;
        questionPanel.SetActive(false);

    }
    private void Update()
    {
            List<Quizquestion> quizquestions = new List<Quizquestion>();
            quizquestions.Add(new Quizquestion("Türkiyenin baþkenti neresidir", "Ýstanbul", "Ankara", "A"));
            quizquestions.Add(new Quizquestion("Ýstanbul kaç yýlýnda fethedilmiþtir", "1453", "1456", "A"));
        quizquestions.Add(new Quizquestion("Özkan Harundan daha iyi cs oynar", "Doðru", "Doðru","A"));

        question.text = quizquestions[questionindex].question;
        option1.text = quizquestions[questionindex].option1;
        option2.text= quizquestions[questionindex].option2;



        if (quizquestions[questionindex].trueOption == "A")
        {
            rightA = true;
        }

        if (quizquestions[questionindex].trueOption == "B")
        {
            rightB = true;
        }


        if (goTimeBool)
        {
            goTime -= Time.deltaTime;
            Debug.Log("Lütfen soru alanýna ilerleyiniz kalan süre" + (int)goTime);

            if (goTime <= 0)
            {
                goTimeBool = false;
            }
        }

        


        if (questionTimerBool&&goTimeBool==false)
        {
            EnableCollider(barrierColliders, true);
            questionPanel.SetActive(true);
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
        EnableCollider(barrierColliders, false);
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
                EnableCollider(barrierColliders, true);
                questionPanel.SetActive(false);
                goTimeBool = true;
                goTime = 5.0f;
                questionTimer = 5.0f;
                answeringTimerBool = false;
                questionTimerBool = true;
                questionindex++;
                cevapSayac.SetActive(false);
            }


        }
        

        

    }

    void EnableCollider(Collider[] cd, bool enable)
    {
        for (int i = 0; i < cd.Length; i++)
        {
            cd[i].enabled = enable;
        }
    }





}




    


    



