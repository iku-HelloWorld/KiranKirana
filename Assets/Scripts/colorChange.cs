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
        trueB = FindObjectOfType<GameManager>().rightB;
        if (trueA)
        {
            Debug.Log("A doğru");
        }
        if (trueB)
        {
            Debug.Log("B doğru");

        }


    }

    private void OnTriggerEnter(Collider other)
    {
       

        if ((other.gameObject.tag == "Player" && (trueA && transform.gameObject.tag == "A") ) || (other.gameObject.tag == "Player" &&( trueB && transform.gameObject.tag == "B")))
        {


            Debug.Log("Cevap doğru");
        }
        else if (other.gameObject.tag == "Player" && (trueA && transform.gameObject.tag == "B") || other.gameObject.tag == "Player" && (trueB && transform.gameObject.tag == "A"))
        {
            Debug.Log("cevap yanlış");
            transform.GetChild(0).gameObject.SetActive(false);

            ;
        }

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
