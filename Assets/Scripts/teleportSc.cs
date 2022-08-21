using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class teleportSc : MonoBehaviourPunCallbacks
{

    int questionIndex;
    PhotonView pw;
    float answerRevealTimer;
  
    // Start is called before the first frame update
    void Start()
    {
        pw = transform.GetComponent<PhotonView>();
       
    }

    // Update is called once per frame
    void Update()
    {
        questionIndex = FindObjectOfType<GameManager>().questionindex;
        answerRevealTimer = FindObjectOfType<GameManager>().answerRevealTimer;
        Debug.Log("soru indexi" + questionIndex);
       
       
    }
    [PunRPC]
   public void teleportCharacter()
    {
            
            transform.position = GameObject.FindGameObjectWithTag("waitingArea").transform.GetChild(questionIndex).position;

    }


    
}
