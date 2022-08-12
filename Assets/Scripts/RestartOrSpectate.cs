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

    

    public Canvas loseCanvas;
    public Canvas controllerCanvas;
    public Canvas LoginCanvas;
    public Canvas quitCanvas;
    public Transform myTransform;

    GameObject camGameObject;



    void Start()
    {
        camGameObject = GameObject.Find("initialCamPos").gameObject;
        
        counter = 0;
        m_MainCamera = Camera.main;
        m_MainCamera.enabled = true;
       
        GetAllPlayersInGame();
            
     }
    void LateUpdate()
    {

        GetAllPlayersInGame();

    }
    List<GameObject> targets = new List<GameObject>();

    public void GetAllPlayersInGame()
    {
        

        players = GameObject.FindGameObjectsWithTag("Player");



        targets[0] = camGameObject;

        if (players != null)
        {
            foreach (GameObject player in players)
            {
                targets.Add(player);
            }

        }
        AssignCameraToTarget(players);
        
    }

    public void ChangeCameraIndex()
    {
        GetAllPlayersInGame();
        counter++;
        Debug.Log(counter);
        
    }

    public void AssignCameraToTarget(GameObject[] camTargets)
    {
        if(counter <= camTargets.Length)
        {
            counter = 0;
        }

        m_MainCamera.transform.SetPositionAndRotation(camTargets[0].transform.position, camTargets[0].transform.rotation);
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
