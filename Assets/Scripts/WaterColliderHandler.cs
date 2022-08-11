using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class WaterColliderHandler : MonoBehaviour
{
    PhotonView pw;
    public Canvas loseCanvas;
    public Canvas controllerCanvas;

    private void Start()
    {
        pw = GetComponent<PhotonView>();
        GameObject tempCanvas = GameObject.Find("LoseCanvas");
        GameObject tempControllerCanvas = GameObject.Find("Controller");
        if(tempCanvas != null && tempControllerCanvas != null)
        {
            loseCanvas = tempCanvas.GetComponent<Canvas>();
            controllerCanvas = tempControllerCanvas.GetComponent<Canvas>();


            if(loseCanvas == null)
            {
                Debug.Log("could not locate the lose canvas" + tempCanvas.name);
            }
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Water" && pw.IsMine)
        {
            loseCanvas.enabled = true;
            controllerCanvas.enabled = false;
            gameObject.GetPhotonView().enabled = false;
            gameObject.GetComponent<CameraMovement>().enabled = false;
            GameObject restartMenager = FindObjectOfType<RestartOrSpectate>().gameObject;
            restartMenager.GetComponent<RestartOrSpectate>().enabled = true;
            PhotonNetwork.Destroy(gameObject);
        }

    }

    

}
