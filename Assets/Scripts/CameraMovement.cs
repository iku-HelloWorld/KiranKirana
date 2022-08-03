 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun.Demo.PunBasics;
using Photon.Pun;


public class CameraMovement : MonoBehaviour
{
    public float Yaxis;
    public float Xaxis;
    public float RotationSensivity = 5f;

    float smoothTime = 0.12f;
    float RotationMin = -40f;
    float RotationMax = 60f;


    Transform target;
    PhotonView pw;
    

    [SerializeField] GameObject player;
    public FixedTouchField touchField;
 
    bool enableMobileInputs;

    private void Start()
    {
        enableMobileInputs = player.GetComponent<MyPlayer>().enableMobileInputs;
        //target = player.transform.Find("CameraTarget").transform;

        pw = GetComponent<PhotonView>();

        CameraWork cameraWork = gameObject.GetComponent<CameraWork>();
        //target = player.transform.Find("CameraTarget").transform;

        if (pw.IsMine)
        {
            target = player.transform.Find("CameraTarget").transform;

        }
        else
        {
            Debug.Log("CameraNotFound");
        }


         
    }

    // Update is called once per frame
    void LateUpdate()
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
        transform.eulerAngles = targetRotation;

        transform.position = target.position - transform.forward * 5f;
        
    }
}
