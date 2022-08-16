using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class teleportSc : MonoBehaviourPunCallbacks
{

    int questionIndex;
    PhotonView pw;
  
    // Start is called before the first frame update
    void Start()
    {
        pw = transform.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        questionIndex = FindObjectOfType<GameManager>().questionindex;
        Debug.Log("soru indexi" + questionIndex);
    }
    
   public void teleportCharacter()
    {
       
            transform.position = GameObject.FindGameObjectWithTag("waitingArea").transform.GetChild(questionIndex).position;
        
        
    }


    
}
