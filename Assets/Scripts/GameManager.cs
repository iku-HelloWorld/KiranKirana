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

    public GameObject waitingPanel;
    public TextMeshProUGUI waitingText;

    public GameObject answerPanel;
    public TextMeshProUGUI answerText;

    public GameObject waitingAreas;
    //public GameObject barrier;

    public bool rightA=false;
    public bool rightB = false;
     [SerializeField]public bool answerReveal;
    private Collider[] barrierColliders;

    

    [SerializeField] float questionTimer = 10.0f;
    [SerializeField] float answeringTimer = 5.0f;
    [SerializeField] float answerRevealTimer = 10.0f;

    [SerializeField] float goTime = 5.0f;

    public GameObject[] bridges;
    private bool questionTimerBool = true;
    private bool answeringTimerBool = true;
    private bool goTimeBool;
    


   [SerializeField] int questionindex = 0;
  

    private void Start()
    {
        GameObject[] barrier = GameObject.FindGameObjectsWithTag("Barrier");
        barrierColliders = new Collider[barrier.Length];
        for(int i = 0; i < barrierColliders.Length; i++)
        {
            barrierColliders[i] = barrier[i].GetComponent<Collider>();
        }
        goTimeBool = true;
        SetActivePanel(questionPanel.name);

    }
    private void Update()
    {
        List<Quizquestion> quizquestions = ListOlustur();

        CheckOption(quizquestions);
       

        QuestionPhase();

        answer();

        answerRevealPhase();
        
    }

    private void updatePosition()
    {
        FindObjectOfType<MyPlayer>().gameObject.transform.position=waitingAreas.gameObject.transform.GetChild(questionindex-1).position;
    }

    private void QuestionPhase()
    {
        if (questionTimerBool && answerReveal == false)
        {
            SetActivePanel(questionPanel.name);
            EnableCollider(barrierColliders, true);

            soruSayac.SetActive(true);
            soruSayac.GetComponent<TextMeshProUGUI>().text = "Düþünme aþamasý kalan süre:" + (int)questionTimer;
            answeringTimer = 3.0f;
            questionTimer -= Time.deltaTime;
        }
    }
    private void answer()
    {
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
                answerReveal = true;
                EnableCollider(barrierColliders, true);
                questionTimer = 5.0f;
                answeringTimerBool = false;
                
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 20);
                
            }


        }




    }


    private void answerRevealPhase()
    {
        if (answerReveal)
        {
            //enablescript(true);
            cevapSayac.SetActive(false);
            SetActivePanel(answerPanel.name);
            answerRevealTimer -= Time.deltaTime;
            

            if (answerRevealTimer <= 0)
            {
                questionindex++;
                questionTimerBool = true;
                answerReveal = false;
                answerRevealTimer = 3.0f;
                updatePosition();
                // enablescript(false);




            }



        }

    }

    private void enablescript(bool enable)
    {
        foreach (GameObject bridges in bridges)
        {
            bridges.GetComponent<colorChange>().enabled = enable;
        }
    }





    //private void WaitingQuestion()
    //{
    //    if (goTimeBool)
    //    {
    //        goTime -= Time.deltaTime;
    //        SetActivePanel(waitingPanel.name);
    //        waitingText.text = "Lütfen soru alanýna ilerleyiniz kalan süre " + (int)goTime;

    //        if (goTime <= 0)
    //        {
    //            goTimeBool = false;
    //        }
    //    }
    //}

    private void CheckOption(List<Quizquestion> quizquestions)
    {
        if (quizquestions[questionindex].trueOption == "A")
        {
            
            rightA = true;
            rightB = false;
        }

        else if (quizquestions[questionindex].trueOption == "B")
        {
            rightB = true;
            rightA = false;
            
        }
    }

    private List<Quizquestion> ListOlustur()
    {
        List<Quizquestion> quizquestions = new List<Quizquestion>();
        quizquestions.Add(new Quizquestion("Türkiyenin baþkenti neresidir", "Ýstanbul", "Ankara", "B"));
        quizquestions.Add(new Quizquestion("Ýstanbul kaç yýlýnda fethedilmiþtir", "1453", "1456", "B"));
        quizquestions.Add(new Quizquestion("Özkan Harundan daha iyi cs oynar", "Doðru", "Doðru", "A"));

        

        question.text = quizquestions[questionindex].question;
        answerText.text = "Doðru cevap: " + quizquestions[questionindex].trueOption;
        option1.text = quizquestions[questionindex].option1;
        option2.text = quizquestions[questionindex].option2;

        


        return quizquestions;
    }

   
    void EnableCollider(Collider[] cd, bool enable)
    {
        for (int i = 0; i < cd.Length; i++)
        {
            cd[i].enabled = enable;
        }
    }

    public void SetActivePanel(string activePanel)      // Set Active Panel Method
    {
        questionPanel.SetActive(activePanel.Equals(questionPanel.name));
        waitingPanel.SetActive(activePanel.Equals(waitingPanel.name));
        answerPanel.SetActive(activePanel.Equals(answerPanel.name));
        
    }



}




    


    



