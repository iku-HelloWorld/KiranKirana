using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class transitionCanvasHandler : MonoBehaviour
{
    PhotonView pw;
    bool trueAnswer;
    GameObject transitionCanvas;
    GameObject playerMY;
    // Start is called before the first frame update
    void Start()
    {
        transitionCanvas = GameObject.FindGameObjectWithTag("transition");
     
    }

  
    void Update()
    {
        

    }

    public void canvasHandling()
    {
       
            transitionCanvas.transform.GetChild(0).gameObject.GetComponent<Canvas>().enabled = true;
        

    }
}
