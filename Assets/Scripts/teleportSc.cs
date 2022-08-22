using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class teleportSc : MonoBehaviourPunCallbacks
{

    int questionIndex;
    bool mustDie;

    PhotonView pw;
    float answerRevealTimer;

    List<GameObject> waitingAreas;

    
  
    // Start is called before the first frame update
    void Start()
    {
        
        pw = transform.GetComponent<PhotonView>();
        questionIndex = FindObjectOfType<GameManager>().questionindex;

    }

    // Update is called once per frame
    void Update()
    {

        if(questionIndex != FindObjectOfType<GameManager>().questionindex)
        {
            questionIndex = FindObjectOfType<GameManager>().questionindex;

        }
        
        



        answerRevealTimer = FindObjectOfType<GameManager>().answerRevealTimer;
        mustDie = FindObjectOfType<deathHandler>().mustDie;
        Debug.Log("soru indexi" + questionIndex);


        

    }


   
   public void teleportCharacter()
    {
           
        
            transform.position = GameObject.FindGameObjectWithTag("waitingArea").transform.GetChild(questionIndex + 1).position;
        
            

    }

    public void destroyCharacter()
    {
        
            PhotonNetwork.Destroy(gameObject);
        
        
    }

    
}
