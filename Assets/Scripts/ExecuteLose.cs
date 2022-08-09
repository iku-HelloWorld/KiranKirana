using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun.UtilityScripts;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;


public class ExecuteLose : MonoBehaviourPunCallbacks
{
    PhotonView pw;
    public static GameObject controllerCanvas;

    public static GameObject returnCanvas;


    private void Start()
    {
        pw = GetComponent<PhotonView>();
        controllerCanvas = GameObject.Find("Controller");
        returnCanvas = GameObject.Find("SpectateAndPlayAgain");
        returnCanvas.SetActive(false);
                
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Water" && pw.IsMine)
        {
            controllerCanvas.SetActive(false);
            Debug.Log("suya degdi");

            returnCanvas.SetActive(true);
            

        }
    }

}
