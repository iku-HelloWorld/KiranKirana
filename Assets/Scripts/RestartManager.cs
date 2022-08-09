using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RestartManager : MonoBehaviour
{

    GameObject playerObject;
    PhotonView pw;
    GameObject lobbyCanvas;

    private void Awake()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PhotonView>().IsMine)
            {
                playerObject = player.gameObject;
                pw = player.GetComponent<PhotonView>();

                break;
            }
        }
    }

    public void DisconnectAndRestart()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Destroy(playerObject);
        lobbyCanvas = GameObject.Find("LobbyCanvas");
        lobbyCanvas.gameObject.SetActive(true);
       

    }
}
