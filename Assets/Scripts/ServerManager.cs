using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class ServerManager : MonoBehaviourPunCallbacks
{

    [SerializeField] GameObject playerList;
    [SerializeField] TMP_InputField nameText;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        OnConnectedToMaster(); 
        OnJoinedLobby();        
    }

    void Update()
    {
        

    }

    public void SetNickname(string name)
    {
       // PhotonNetwork.NickName = nameText.text;
       PhotonNetwork.NickName = nameText.text;
       Debug.Log(PhotonNetwork.NickName);

    }


    private void PlayerListText()
    {
        foreach(Player p in PhotonNetwork.PlayerList)
        {
            playerList.transform.Find("Text").GetComponent<TextMeshProUGUI>().text += p.NickName + "- \n"; 
        }

    }

    public override void OnJoinedLobby()
    {       

        Debug.Log("Connected to Lobby");
       //PhotonNetwork.JoinOrCreateRoom("ODA 1", new RoomOptions { MaxPlayers = 15, IsOpen = true, IsVisible = true }, TypedLobby.Default);
    }  


    public override void OnJoinedRoom()
    {
        Debug.Log("Odaya Girildi.");     
        GameObject oyuncu = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);      
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected");      
    }
    public override void OnConnectedToMaster()
    {

        Debug.Log("Server'e Bağlanıldı.");
        PhotonNetwork.JoinLobby();
    }


    public override void OnLeftLobby()
    {
        Debug.Log("Disconnected from Lobby");
    }


    public override void OnLeftRoom()
    {
        Debug.Log("Room Disconnected");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Joining Random room has failed" + message + "-" + returnCode);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
         Debug.Log("Joinning has Failed" + message + "-" + returnCode);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("The room could not be created." + message + " - " + returnCode);
    }


}


