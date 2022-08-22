using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public GameObject question;

    public GameObject option1;
    public GameObject option2;

    public GameObject soruSayac;
    public GameObject cevapSayac;

    public GameObject questionPanel;

    Canvas gameController;

    public GameObject answerPanel;
    public GameObject answerText;
   

    public GameObject waitingAreas;
    //public GameObject barrier;

    public bool rightA= false;
    public bool rightB = false;
    public bool transitionBool = false;
    [SerializeField]public bool answerReveal;
    private Collider[] barrierColliders;

    

    [SerializeField] float questionTimer;
    [SerializeField] public float answeringTimer;
    [SerializeField] public float answerRevealTimer;
   

  
    private bool questionTimerBool = true;
    private bool answeringTimerBool = true;
  
    


   [SerializeField] public int questionindex = 0;
  
    [PunRPC]
    public void EnableControllerCanvas()
    {
        gameController = GameObject.FindGameObjectWithTag("Controller").GetComponent<Canvas>();
        gameController.enabled = true;

    }

    private void Start()
    {
        GameObject[] barrier = GameObject.FindGameObjectsWithTag("Barrier");
        
        barrierColliders = new Collider[barrier.Length];
        for(int i = 0; i < barrierColliders.Length; i++)
        {
            barrierColliders[i] = barrier[i].GetComponent<Collider>();
        }
        
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

    

    private void QuestionPhase()
    {
        teleportSc[] _players = FindObjectsOfType<teleportSc>();
        foreach (teleportSc player in _players)
        {
            player.gameObject.GetComponent<CapsuleCollider>().enabled = true;
        }

        if (questionTimerBool && answerReveal == false)  //questiontimerbool false olduðu için set active panel çalýþmayý býrakýyor.
        {
            SetActivePanel(questionPanel.name);
            EnableCollider(barrierColliders, true);

            soruSayac.SetActive(true);
            

            soruSayac.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = (int)questionTimer+" saniye";

            
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
            cevapSayac.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = (int)answeringTimer + " saniye";
            if (answeringTimer <= 0)
            {
                //GameObject.FindGameObjectWithTag("Player").GetComponent<deathHandler>().enabled = true;
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
        if (answerReveal&&transitionBool==false)
        {
           
            cevapSayac.SetActive(false);
            SetActivePanel(answerPanel.name);
            answerRevealTimer -= Time.deltaTime;


            if (0 < answerRevealTimer&&answerRevealTimer<=2)
            {
                transitionBool = true;
                
            }

            
            if (answerRevealTimer <= 0.2)
            {

                answeringTimer = 5.0f;
                questionindex++;
                questionTimerBool = true;
                answerReveal = false;
                answerRevealTimer = 4.0f;
                Debug.Log("ea");
                //FindObjectOfType<teleportSc>().teleportCharacter();
                transitionBool = false;
                FindObjectOfType<transitionSc>().transitionClose();

                //FindObjectOfType<transitionSc>().gameObject.SetActive(false);

            }



        }

        if (transitionBool)
        {
            //FindObjectOfType<transitionSc>().gameObject.SetActive(true);

            answerRevealTimer -= Time.deltaTime;
            if (answerRevealTimer <= 0)
            {
                transitionBool = false;
            }
        }

    }

   




    
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

        

        question.GetComponent<TextMeshProUGUI>().text = quizquestions[questionindex].question;
        answerText.GetComponent<TextMeshProUGUI>().text = "Doðru cevap: " + quizquestions[questionindex].trueOption;
        option1.GetComponent<TextMeshProUGUI>().text = quizquestions[questionindex].option1;
        option2.GetComponent<TextMeshProUGUI>().text = quizquestions[questionindex].option2;

        


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
       
        answerPanel.SetActive(activePanel.Equals(answerPanel.name));
        //transitionPanel.SetActive(activePanel.Equals(transitionPanel.name));
        
    }


}











