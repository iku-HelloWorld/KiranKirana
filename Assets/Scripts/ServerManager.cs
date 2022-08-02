using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class ServerManager : MonoBehaviourPunCallbacks
{

    [SerializeField] TextMeshProUGUI playerList;
    [SerializeField] TMP_InputField nameText;
    [SerializeField] GameObject loginScreen;
    [SerializeField] GameObject lobbyScreen;
    [SerializeField] GameObject CreateOrJoin;
    [SerializeField] GameObject joinRoom;
    [SerializeField] GameObject CustomScreen;
    [SerializeField] TMP_InputField roomName;
    [SerializeField] TMP_InputField roomcapacity;
    [SerializeField] TMP_InputField roomJoinInput;
    private bool isReady = false;
    [SerializeField] Canvas cnvas;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        SetActivePanel(loginScreen.name);
        
       // OnJoinedLobby();        
       
    }


    public void SetActivePanel(string activePanel)
    {
        loginScreen.SetActive(activePanel.Equals(loginScreen.name));
        lobbyScreen.SetActive(activePanel.Equals(lobbyScreen.name));
        CreateOrJoin.SetActive(activePanel.Equals(CreateOrJoin.name));
        joinRoom.SetActive(activePanel.Equals(joinRoom.name));
        CustomScreen.SetActive(activePanel.Equals(CustomScreen.name));

    }

 bool test = true;

    void Update()
    {

        
       
           
            PlayerListText();
            
       
        

        

    }

    public void JoinCustomRoom()
    {
        PhotonNetwork.JoinRoom(roomJoinInput.text);
        SetActivePanel(lobbyScreen.name);
    }



    
    

    public void SetNickname(string name)
    {
       // PhotonNetwork.NickName = nameText.text;
       PhotonNetwork.LocalPlayer.NickName = nameText.text;
       Debug.Log(PhotonNetwork.NickName);
       PhotonNetwork.JoinLobby();
        
    }


    private void PlayerListText()
    {
        playerList.text ="";
    
        foreach(Player p in PhotonNetwork.PlayerList)
        {
            playerList.text += p.NickName + "- \n"; 
              Debug.Log("hello");
        }         
        
    }

    public override void OnJoinedLobby()
    {       
        Debug.Log("Connected to Lobby");
        SetActivePanel(CreateOrJoin.name);
       //PhotonNetwork.JoinOrCreateRoom("ODA 1", new RoomOptions { MaxPlayers = 15, IsOpen = true, IsVisible = true }, TypedLobby.Default);
    }  
    public override void OnJoinedRoom()
    {
        Debug.Log("Odaya Girildi.");     
        GameObject oyuncu = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);      
       // PlayerListText();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected");      
    }
    public override void OnConnectedToMaster()
    {

        Debug.Log("Server'e Bağlanıldı.");
        
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


    public void JoinRandomRoom ()
    {
        OnConnectedToMaster();
        PhotonNetwork.JoinOrCreateRoom("ODA 1", new RoomOptions { MaxPlayers = 15, IsOpen = true, IsVisible = true }, TypedLobby.Default);
        SetActivePanel(lobbyScreen.name);
        isReady = true;
    }
    public void JoinRoom()
    {
        SetActivePanel(joinRoom.name);   
    }
    public void CreateRoom()
    {
        SetActivePanel(CustomScreen.name);
    }

    public void PlayerReady()
    { 
        isReady = !isReady;
    }


    public void CreateCustomRoom()
    {
        

        PhotonNetwork.CreateRoom(roomName.text,new RoomOptions{ MaxPlayers = 15, IsOpen = true, IsVisible = true}, TypedLobby.Default);
        SetActivePanel(lobbyScreen.name);
    }


}


