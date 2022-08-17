using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transitionSc : MonoBehaviour
{

    bool transitionBool;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transitionBool = FindObjectOfType<GameManager>().transitionBool;
        if (transitionBool)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        
    }
}
