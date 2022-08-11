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

    int counter;
    
    List<Transform> targets;

    public Canvas loseCanvas;
    public Canvas controllerCanvas;
    public Canvas LoginCanvas;
    public Canvas quitCanvas;

    void Start()
    {
        counter = 0;
        m_MainCamera = Camera.main;
        m_MainCamera.enabled = true;

        GetAllPlayersInGame();
            
        }
    void Update()
    {

        //GetAllPlayersInGame();

    }

    public void GetAllPlayersInGame()
    {
        
        players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            //targets.Add(player.transform.GetChild(0).transform);
        }
        AssignCameraToTarget();
        
    }

    public void ChangeCameraIndex()
    {
        GetAllPlayersInGame();
        counter++;
    }

    void AssignCameraToTarget()
    {
        m_MainCamera.transform.SetPositionAndRotation(new Vector3(-27.3999996f, 62.5999985f, -37.4000015f), new Quaternion(0.0502322726f, 0.280282021f, -0.0146889426f, 0.958489954f));
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
