using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class ServerManager : MonoBehaviourPunCallbacks
{
     
    [SerializeField] GameObject loginScreen;
    [SerializeField] GameObject lobbyScreen;
    [SerializeField] GameObject CreateOrJoinScreen;             // Panels
    [SerializeField] GameObject joinRoomScreen;
    [SerializeField] GameObject CustomScreen;



    [SerializeField] TextMeshProUGUI playerList; // Player List Text

    // User Inputs
    [SerializeField] TMP_InputField nicknameText;   // LocalPlayer Nickname
    [SerializeField] TMP_InputField customRoomName;   // Custom Room Name Input       
    [SerializeField] TMP_InputField customRoomcapacity;       // Custom Room Capacity  
    [SerializeField] TMP_InputField customJoinRoomName;     // Custom Room Name which User wants to join.
    private bool isReady = false;
    [SerializeField] Canvas cnvas;

    //***********************************
    [SerializeField] GameObject buttonText;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;


    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();       // Connect To Server
        SetActivePanel(loginScreen.name);                 // Define Active Panel 
       // buttonText = this.gameObject.transform.GetChild(0).gameObject;
       
    }

    void Update()
    {     
            PlayerListText();               // bunu Update den çıkarmanın yolunu bul                              
    }

    //Player Input Methods

    public void SetNickname(string name)        // Set User Nickname by User Input
    {
        // PhotonNetwork.NickName = nameText.text;
        PhotonNetwork.LocalPlayer.NickName = nicknameText.text;
        Debug.Log(PhotonNetwork.LocalPlayer.NickName);
        PhotonNetwork.JoinLobby();

    }








    #region Create Or join 

    
    public void CreateRoom()                  // Method For User Input Button
    {
        SetActivePanel(CustomScreen.name);
    }    

    public void CreateCustomRoom()              // Create Room with properties by using User Inputs.
    {
        PhotonNetwork.CreateRoom(customRoomName.text, new RoomOptions { MaxPlayers = 15, IsOpen = true, IsVisible = true }, TypedLobby.Default);
        SetActivePanel(lobbyScreen.name);
    }

    
    public void JoinRoom()      // Method For User Input Button
    {
        SetActivePanel(joinRoomScreen.name);
    }
    
    public void JoinCustomRoom()        // Join Custom Room by using User Input
    {
        PhotonNetwork.JoinRoom(customJoinRoomName.text);       
        SetActivePanel(lobbyScreen.name);
    }
    public void JoinRandomRoom()        // Join Random Room Method
    {
        OnConnectedToMaster();
        PhotonNetwork.JoinOrCreateRoom("ODA 1", new RoomOptions { MaxPlayers = 15, IsOpen = true, IsVisible = true }, TypedLobby.Default);
        cnvas.enabled = false;
        
    }
    
    #endregion

    private void PlayerListText() // Print Connected Players in the Room. 
    {
        playerList.text = "";
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            playerList.text += p.NickName + "- \n";
            Debug.Log("hello");
        }
    }

    
    public void PlayerReady()   // Player Ready Status 
    {
        isReady = !isReady;
    }

    public void SetActivePanel(string activePanel)      // Set Active Panel Method
    {
        loginScreen.SetActive(activePanel.Equals(loginScreen.name));
        lobbyScreen.SetActive(activePanel.Equals(lobbyScreen.name));
        CreateOrJoinScreen.SetActive(activePanel.Equals(CreateOrJoinScreen.name));
        joinRoomScreen.SetActive(activePanel.Equals(joinRoomScreen.name));
        CustomScreen.SetActive(activePanel.Equals(CustomScreen.name));
    }


   
    // Override Methods

    public override void OnJoinedLobby()
    {
        Debug.Log("Connected to Lobby");
        SetActivePanel(CreateOrJoinScreen.name);
       
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
/*
     public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {

        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
            
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            Instantiate(roomListItemPrefab,roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
            
        }
        
    }
*/

   

}


