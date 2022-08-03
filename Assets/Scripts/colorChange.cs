using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorChange : MonoBehaviour
{
    bool trueA;
    bool trueB;

    bool answered = true;



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        trueA = FindObjectOfType<GameManager>().rightA;
        Debug.Log(trueA);
        trueB = FindObjectOfType<GameManager>().rightB;
    }

    private void OnTriggerEnter(Collider other)
    {

        if ((other.gameObject.tag == "Player" && trueA && transform.gameObject.tag == "A") || (other.gameObject.tag == "Player" && trueB && transform.gameObject.tag == "B"))
        {

            Debug.Log("Cevap doðru");

        }
        else
        {
            /*transform.GetChild(0).GetComponent<Light>().color = Color.red*/
            ;
        }

    }


    //private void OnCollisionEnter(Collision collision)
    //{

    //    answered = true;

    //    if ((collision.gameObject.tag == "Player" && trueA && transform.gameObject.tag == "A") || (collision.gameObject.tag == "Player" && trueB && transform.gameObject.tag == "B"))
    //    {

    //        Debug.Log("Cevap doðru");

    //    }
    //    else
    //    {
    //        /*transform.GetChild(0).GetComponent<Light>().color = Color.red*/;
    //    }




    //}
}
