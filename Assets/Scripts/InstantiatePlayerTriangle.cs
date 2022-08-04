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

        triangle.transform.position = transform.position + Vector3.up * 5.5f;
        triangle.transform.rotation = Quaternion.Euler(0, 0, 180);
        triangle.transform.localScale = new Vector3(0.05f, 0.2f, 0.05f);

    }


}
