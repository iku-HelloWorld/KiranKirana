using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class InstantiatePlayerTriangle : MonoBehaviour
{
    public GameObject TriangleShape;

    GameObject triangle;


    private void Start()
    {
        triangle = Instantiate(TriangleShape, Vector3.zero,Quaternion.identity);
        
    }


    private void LateUpdate()
    {

        triangle.transform.position = transform.position + Vector3.up * 3;

    }


}
