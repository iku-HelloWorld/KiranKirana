using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    bool trueA;
    bool trueB;

    public GameObject A;
    public GameObject B;

    private void Start()
    {
        
    }

    private void Update()
    {
        trueA = FindObjectOfType<GameManager>().rightA;
        Debug.Log(trueA);
        trueB = FindObjectOfType<GameManager>().rightB;

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "A"&&trueA)
        {
            
            //collision.gameObject.GetComponent<Renderer>().material.color = Color.green;
            Debug.Log("abe doru");

            A.gameObject.GetComponent<Renderer>().material.color = Color.green;
            B.gameObject.GetComponent<Renderer>().material.color = Color.red;
            
        }
    }


}
