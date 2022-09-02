using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;


public class RestartOrSpectate : MonoBehaviour
{


    Camera m_MainCamera;
    GameObject[] players;

    public int counter=0;
    
    

    public Canvas loseCanvas;
    public Canvas controllerCanvas;
    public Canvas LoginCanvas;
    public Canvas quitCanvas;
    

    GameObject camGameObject;



    void Start()
    {
        camGameObject = GameObject.Find("InitialCamPos").gameObject;
        
       
        m_MainCamera = Camera.main;
        m_MainCamera.enabled = true;

        
       
    
            
     }
    void LateUpdate()
    {
        Debug.Log(counter);
        GetAllPlayersInGame();
        

    }
    
    

    public void GetAllPlayersInGame()
    {

        List<GameObject> targets = new List<GameObject>();
        targets.Add(camGameObject);
        players = GameObject.FindGameObjectsWithTag("Player");

        if (counter > players.Length + 1)
        {
            counter = 0;
        }

        targets.Add(camGameObject);

        if (players != null)
        {
            foreach (GameObject player in players)
            {
                targets.Add(player.transform.GetChild(0).gameObject);
            }

        }
        GameObject[] targetArray = targets.ToArray();
        AssignCameraToTarget(targetArray);
        
    }

    public void ChangeCameraIndex()
    {

        GetAllPlayersInGame();
        counter++;


    }

    public void AssignCameraToTarget(GameObject[] camTargets)
    {
        if(counter > camTargets.Length)
        {
            counter = 0;
        }

        
        m_MainCamera.transform.SetPositionAndRotation(camTargets[counter].transform.position - m_MainCamera.transform.forward * 5f, camTargets[counter].transform.rotation);
        //m_MainCamera.transform.SetPositionAndRotation(new Vector3(-27.3999996f, 62.5999985f, -37.4000015f), new Quaternion(0.0502322726f, 0.280282021f, -0.0146889426f, 0.958489954f));
    }


    public void WatchTheGame()
    {
        
        GameObject tempCanvas = GameObject.Find("LoseCanvas");
        
        if (tempCanvas != null )
        {
            loseCanvas = tempCanvas.GetComponent<Canvas>();
            
            if (loseCanvas == null)
            {
                Debug.Log("could not locate the lose canvas" + tempCanvas.name);
            }
        }
        loseCanvas.enabled = false;
        OpenQuitButtonCanv();
       

    }

    public void OpenQuitButtonCanv()
    {
        GameObject tempQuitCanv = GameObject.Find("ChangeCameraPosCanv");

        if (tempQuitCanv != null)
        {
            quitCanvas = tempQuitCanv.GetComponent<Canvas>();

            if (quitCanvas == null)
            {
                Debug.Log("QuitCanvas is not in the scene");
            }
        }
        quitCanvas.enabled = true;
    }

    public void PlayAgain()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.Disconnect();
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
