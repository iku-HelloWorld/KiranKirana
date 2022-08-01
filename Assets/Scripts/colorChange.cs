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
    }

    private void OnCollisionEnter(Collision collision)
    {
        answered = true;
        if ((collision.gameObject.tag == "Player" && trueA && transform.gameObject.tag == "A") || (collision.gameObject.tag == "Player" && trueB && transform.gameObject.tag == "B"))
        {

            transform.GetChild(0).GetComponent<Light>().color = Color.green;

        }
        if((collision.gameObject.tag == "Player" && trueA && transform.gameObject.tag == "B")|| collision.gameObject.tag == "Player" && trueB && transform.gameObject.tag == "A")
        {
            transform.GetChild(0).GetComponent<Light>().color = Color.red;
        }




    }
}

