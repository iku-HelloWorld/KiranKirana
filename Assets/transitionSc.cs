using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class transitionSc : MonoBehaviour
{
    GameObject playerMY;
    bool transitionBool;
    bool trueAnswer;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
                

            if (player.GetComponent<PhotonView>().IsMine)
            {
                playerMY = player;
                break;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

        //transitionBool = FindObjectOfType<GameManager>().transitionBool;
        trueAnswer = FindObjectOfType<colorChange>().trueAnswer;
        Debug.Log("Doðru cevap " + trueAnswer);
    }

    public void TransitionHandler()
    {
        if (playerMY.GetComponent<PhotonView>().IsMine)
        {
            Debug.Log("tansition plez");
            transform.GetChild(0).gameObject.GetComponent<Canvas>().enabled = true;
            Debug.Log("ismine");
        }
        


    }

    public void transitionClose()
    {

        Debug.Log("tansition plez");
        transform.GetChild(0).gameObject.GetComponent<Canvas>().enabled = false;
        

    }

}
