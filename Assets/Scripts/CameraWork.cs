 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWork : MonoBehaviour
{
    public float Yaxis;
    public float Xaxis;
    public float RotationSensivity = 8f;


    public float RotationMax

    public Transform target;

    // Update is called once per frame
    void Update()
    {
        Yaxis += Input.GetAxis("Mouse X") * RotationSensivity;
        Xaxis -= Input.GetAxis("Mouse Y") * RotationSensivity;

        Vector3 targetRotation = new Vector3(Xaxis, Yaxis);
        transform.eulerAngles = targetRotation;

        transform.position = target.position - transform.forward * 30f;
        
    }
}
