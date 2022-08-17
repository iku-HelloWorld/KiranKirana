using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class colorChange : MonoBehaviourPunCallbacks
{

    PhotonView pw;
    bool trueA;
    bool trueB;
    bool trueAnswer;
    bool falseAnswer;
    bool answerReveal;

    AudioSource audioSource;
    [SerializeField]AudioClip glassBreak;
    [SerializeField] AudioClip winSound;

    GameObject brokenGlass;
    GameObject solidGlass;


    bool answered = true;
    [SerializeField] ParticleSystem confetti;
    void Start()
    {
        pw = GetComponent<PhotonView>();
        //solidGlass = GameObject.Find("solidGlass");
        //solidGlass.SetActive(false);

        //brokenGlass = GameObject.Find("brokenGlass");
        //brokenGlass.SetActive(true);

        audioSource = transform.GetComponent<AudioSource>();

        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        

            trueA = FindObjectOfType<GameManager>().rightA;
            trueB = FindObjectOfType<GameManager>().rightB;
            if (trueA)
            {
                Debug.Log("A doğru");
            }
            if (trueB)
            {
                Debug.Log("B doğru");

            }




            answerReveal = FindObjectOfType<GameManager>().answerReveal;

            if (answerReveal)
            {
                if (trueAnswer)
                {

                   confetti.Play();
                   audioSource.PlayOneShot(winSound);
                   trueAnswer = false;
                    

                }

                if (falseAnswer)
                {
                    pw.RPC("GameMechanic", RpcTarget.All);
                    falseAnswer = false;
                }

            }
        

    }
   
    private void OnCollisionEnter(Collision other)
    {

        

            if ((other.gameObject.tag == "Player" && (trueA && transform.gameObject.tag == "A")) || (other.gameObject.tag == "Player" && (trueB && transform.gameObject.tag == "B")))
            {

            trueAnswer = true;
            falseAnswer = false;
            Debug.Log("Cevap doğru ");
             



            }
            else if (other.gameObject.tag == "Player" && (trueA && transform.gameObject.tag == "B") || other.gameObject.tag == "Player" && (trueB && transform.gameObject.tag == "A"))
            {
                Debug.Log("cevap yanlış");
            falseAnswer = true;
            trueAnswer = false;

                

            

        }
    }

    [PunRPC]
    void GameMechanic()
    {
        //solidGlass.SetActive(false);
        //brokenGlass.SetActive(true);

        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.GetComponent<Animator>().SetBool("WrongAnswer", true);
        transform.gameObject.GetComponent<BoxCollider>().enabled = false;
        audioSource.PlayOneShot(glassBreak);
        //brokenGlass.GetComponent<Animator>().SetBool("WrongAnswer", true);


    }


    //private void OnCollisionEnter(Collision collision)
    //{

    //    answered = true;

    //    if ((collision.gameObject.tag == "Player" && trueA && transform.gameObject.tag == "A") || (collision.gameObject.tag == "Player" && trueB && transform.gameObject.tag == "B"))
    //    {

    //        Debug.Log("Cevap do�ru");

    //    }
    //    else
    //    {
    //        /*transform.GetChild(0).GetComponent<Light>().color = Color.red*/;
    //    }




    //}
}
