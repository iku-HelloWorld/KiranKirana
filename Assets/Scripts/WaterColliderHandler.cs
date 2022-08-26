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

    [SerializeField] GameObject initialCameraPos;
    GameObject camGameObject;

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
            camGameObject = Instantiate(initialCameraPos, new Vector3(-27.3999996f, 62.5999985f, -37.4000015f), new Quaternion(0.0502322726f, 0.280282021f, -0.0146889426f, 0.958489954f));
        }

    }

    

}
