using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class colorChange : MonoBehaviourPunCallbacks
{

    PhotonView pw;
    bool trueA;
    bool trueB;
    bool answerReveal;

    GameObject brokenGlass;
    GameObject solidGlass;


    bool answered = true;
    [SerializeField] ParticleSystem confetti;
    void Start()
    {
        /* pw = GetComponent<PhotonView>();
         solidGlass = GameObject.Find("solidGlass");
         solidGlass.SetActive(true);*/

        brokenGlass = GameObject.Find("brokenGlass");
        brokenGlass.SetActive(true);
        brokenGlass.GetComponent<Animator>().SetBool("WrongAnswer", true);
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

    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Player" && (trueA && transform.gameObject.tag == "A")) || (other.gameObject.tag == "Player" && (trueB && transform.gameObject.tag == "B")))
        {


            Debug.Log("Cevap doğru ");
                confetti.Play();
            


        }
        else if (other.gameObject.tag == "Player" && (trueA && transform.gameObject.tag == "B") || other.gameObject.tag == "Player" && (trueB && transform.gameObject.tag == "A"))
        {
            Debug.Log("cevap yanlış");

            pw.RPC("GameMechanic", RpcTarget.All);

        }


    }

    [PunRPC]
    void GameMechanic()
    {
        Destroy(gameObject);
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
