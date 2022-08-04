 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class CameraMovement : MonoBehaviour
{
    public float Yaxis;
    public float Xaxis;
    public float RotationSensivity = 5f;

    
    float RotationMin = -40f;
    float RotationMax = 60f;


    Transform target;
    PhotonView pw;
    Camera mainCamera;
    

    
    FixedTouchField touchField;
 
    bool enableMobileInputs;

    private void Start()
    {
        touchField = FindObjectOfType<FixedTouchField>();

        enableMobileInputs = GetComponent<MyPlayer>().enableMobileInputs;
        

        pw = GetComponent<PhotonView>();

       
        
        mainCamera = Camera.main;
        mainCamera.enabled = true;


        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PhotonView>().IsMine)
            {
                target = gameObject.transform.GetChild(0).GetComponent<Transform>();

                break;
            }
        }



    }

    // Update is called once per frame
    void LateUpdate()
    {
        

        if (pw.IsMine)
        {
            HandleTouchMovement();
        }

    }

    private void HandleTouchMovement()
    {
        if (enableMobileInputs)
        {
            RotationSensivity = 0.5f;
            Yaxis += touchField.TouchDist.x * RotationSensivity;
            Xaxis -= touchField.TouchDist.y * RotationSensivity;


        }
        else
        {
            Yaxis += Input.GetAxis("Mouse X") * RotationSensivity;
            Xaxis -= Input.GetAxis("Mouse Y") * RotationSensivity;
        }




        Xaxis = Mathf.Clamp(Xaxis, RotationMin, RotationMax);

        Vector3 targetRotation = new Vector3(Xaxis, Yaxis);
        mainCamera.transform.eulerAngles = targetRotation;

        mainCamera.transform.position = target.position - mainCamera.transform.forward * 5f;
    }
}
