using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class colorChange : MonoBehaviourPunCallbacks
{

    PhotonView pw;
    bool trueA;
    bool trueB;
    public bool trueAnswer;
    public bool falseAnswer;
    bool answerReveal;
    float answerTimer;

    AudioSource audioSource;
    [SerializeField]AudioClip glassBreak;
    [SerializeField] AudioClip winSound;

    GameObject brokenGlass;
    GameObject solidGlass;


    bool transitionBool;
    [SerializeField] ParticleSystem confetti;
    void Start()
    {
        pw = GetComponent<PhotonView>();
      

        audioSource = transform.GetComponent<AudioSource>();

        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        

            trueA = FindObjectOfType<GameManager>().rightA;
            trueB = FindObjectOfType<GameManager>().rightB;
            transitionBool = FindObjectOfType<GameManager>().transitionBool;
            answerTimer= FindObjectOfType<GameManager>().answerRevealTimer;





            answerReveal = FindObjectOfType<GameManager>().answerReveal;

            if (answerReveal)
            {
                if (trueAnswer)
                {
               
                confetti.Play();
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(winSound);
                }
               

               
                   
                    
                }
            else if (falseAnswer)
            {
                pw.RPC("GameMechanic", RpcTarget.All);
                falseAnswer = false;

            }

            if (transitionBool&&trueAnswer)
            {
              
                    FindObjectOfType<transitionSc>().TransitionHandler();
                    //trueAnswer = false;
              

            }

           
            
          

        }


        
       
        

    }
   
    private void OnCollisionEnter(Collision other)
    {
       


            if ((other.gameObject.tag == "Player" && trueA && transform.gameObject.tag == "A") || (other.gameObject.tag == "Player" && trueB && transform.gameObject.tag == "B") )
            {

                trueAnswer = true;
                falseAnswer = false;
                Debug.Log("Cevap doğru ");




            }
            else if (other.gameObject.tag == "Player" && (trueA && transform.gameObject.tag == "B")  || other.gameObject.tag == "Player" && (trueB && transform.gameObject.tag == "A"))
            {
                Debug.Log("cevap yanlış");
                falseAnswer = true;
                trueAnswer = false;



            

        }
    }

    [PunRPC]
    void GameMechanic()
    {
      

        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.GetComponent<Animator>().SetBool("WrongAnswer", true);
        transform.gameObject.GetComponent<BoxCollider>().enabled = false;
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(glassBreak);
        }
       
        

    }





  
}
