using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Photon.Pun.Demo.PunBasics;
using UnityEngine.SceneManagement;

public class ServerManager : MonoBehaviourPunCallbacks
{
    public static ServerManager instance;
    ExitGames.Client.Photon.Hashtable props;

    [SerializeField] GameObject quizcanv;

    [SerializeField] GameObject loginScreen;
    [SerializeField] GameObject lobbyScreen;
    [SerializeField] GameObject CreateOrJoinScreen;             // Panels
    [SerializeField] GameObject joinRoomScreen;
    [SerializeField] GameObject CustomScreen;
    //[SerializeField] Canvas inputCanvas;
    [SerializeField] Canvas loseCanvas;

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

    PhotonView pw;
    PhotonView qpw;

    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;


    void Start()
    {
         PhotonView pw = PhotonView.Get(this);
        //loseCanvas.enabled = false;
        //inputCanvas.enabled = false;
        PhotonNetwork.ConnectUsingSettings();       // Connect To Server
        SetActivePanel(loginScreen.name);                 // Define Active Panel 
       // buttonText = this.gameObject.transform.GetChild(0).gameObject;
       

       props = new ExitGames.Client.Photon.Hashtable
    {
        {"status", false}       
    };

    }

    void InputController()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<Canvas>().enabled = true;
        //inputCanvas.GetComponent<Canvas>().enabled = true;
        FindObjectOfType<transitionSc>().enabled = true;

    }

    

    public void IsRoomReady()
    {   
        foreach(Player  p in PhotonNetwork.PlayerList)
        {
            if(p.CustomProperties.ContainsKey("status") == true)
            {
                continue;
            }
            else{
                Debug.Log("haz??r olamyan oyuncular var");
                return;
            }


        }

        Debug.Log("herkes haz??r");
        


    }



    public void Status()
    {
        props["status"] = !(bool)props["status"];
        if((bool)props["status"])
        {
            Debug.Log("oyuncu haz??r");
        }
        else{
            Debug.Log("Haz??r de??il");
        }
        PhotonNetwork.SetPlayerCustomProperties(props);
        
       

       
       
    }

     void Awake() {
        instance = this;    
    }

    void Update()
    {     
            PlayerListText();               // bunu Update den ????karman??n yolunu bul                              

    }

    //Player Input Methods

    public void SetNickname(string name)        // Set User Nickname by User Input
    {
        // PhotonNetwork.NickName = nameText.text;
        PhotonNetwork.LocalPlayer.NickName = nicknameText.text;
        Debug.Log(PhotonNetwork.LocalPlayer.NickName);
        PhotonNetwork.JoinLobby();

    }

    public void Startscene()
    {

        foreach (Player p in PhotonNetwork.PlayerList)
        {   
            Debug.Log(p.NickName);
            
            //PhotonNetwork.LocalPlayer.CustomProperties();
            bool isready = Convert.ToBoolean(p.CustomProperties["status"]);
                
            if(!isready)
            {   
                
                Debug.Log("haz??r  olmayan oyuncular var" + p.NickName);
                return;
            }
        }

      //  quizcanv.SetActive(true);
        // Debug.Log("herkes haz??r");


        
       //pw.RPC("Startgm", RpcTarget.All, 10, false);
       PhotonNetwork.Instantiate("quizCanvas", new Vector3(1.700012f, 41.59998f, 25.4f), Quaternion.identity);
    //   PhotonNetwork.Instantiate("Player", new Vector3(-10.2600002f, 47.0600014f, -22.8600006f), Quaternion.identity);
        GetComponent<PhotonView>().RPC("Startgm", RpcTarget.All );
        //  pw.RPC("Startgm", RpcTarget.All);


        //FindObjectOfType<GameManager>().enabled = true;
        //cnvas.enabled = false;
        //   inputCanvas.enabled = true;
        //PhotonNetwork.Instantiate("Player", new Vector3(-10.2600002f, 47.0600014f, -22.8600006f), Quaternion.identity);



        if (FindObjectOfType<GameManager>().enabled)
        {
            InputController();

        }

    }

    #region Create Or join 

    
   public void CreateRoom()                  // Method For User Input Button
    {
        OnConnectedToMaster();
        SetActivePanel(CustomScreen.name);
    }   

    public void CreateCustomRoom()              // Create Room with properties by using User Inputs.
    {
        if(string.IsNullOrEmpty(customRoomName.text))
        {
                    OnConnectedToMaster();
            return;
            
        }
       //bcustomRoomcapacity.text;
        Byte.TryParse(customRoomcapacity.text, out byte capacity);
       //  byte capacity = (byte)customRoomcapacity;
        PhotonNetwork.CreateRoom(customRoomName.text, new RoomOptions { MaxPlayers = capacity, IsOpen = true, IsVisible = true, CustomRoomProperties = props }, TypedLobby.Default);
        OnConnectedToMaster();
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
       
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 10, CustomRoomProperties = props };
        OnConnectedToMaster();
      //  PhotonNetwork.JoinOrCreateRoom("ODA 1", new RoomOptions { MaxPlayers = 15, IsOpen = true, IsVisible = true, CustomRoomProperties = props }, TypedLobby.Default);
           //PhotonNetwork.JoinRandomOrCreateRoom()
        PhotonNetwork.JoinRandomOrCreateRoom(props, 15, MatchmakingMode.FillRoom, TypedLobby.Default, sqlLobbyFilter:null, roomName: null, roomOps);
     //   PhotonNetwork.JoinRandomOrCreateRoom(roomOps);
        SetActivePanel(lobbyScreen.name);
        
    }
    
    #endregion

    private void PlayerListText() // Print Connected Players in the Room. 
    {
     
        
        playerList.text = "";
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            
                     playerList.text +=  p.NickName + "-" + p.CustomProperties["status"] +"\n"; 
            
           
                    // playerList.text +=  p.NickName + "-" + "     Haz??r " +"\n";      
        }
    }


    

    public void JoinListRoom(RoomInfo info) 
    {
        PhotonNetwork.JoinRoom(info.Name);
        cnvas.enabled = false;
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




    #region BackButtons

    public void BackCOJButton()
    {   
        SetActivePanel(CreateOrJoinScreen.name);
    }

    public void BackJoinButton()
    {
        SetActivePanel(joinRoomScreen.name);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.JoinLobby();

    }

    public void BackLogin()
    {
        SetActivePanel(loginScreen.name);
        PhotonNetwork.LeaveLobby();
    }

    public void LeaveGame()
    {
        Application.Quit();
    }

    #endregion



   
    // Override Methods


      #region  Override methods

    public override void OnJoinedLobby()
    {
        Debug.Log("Connected to Lobby");
        SetActivePanel(CreateOrJoinScreen.name);
        
        //PhotonNetwork.CreateRoom("b", new RoomOptions { MaxPlayers = 15, IsOpen = true, IsVisible = true }, TypedLobby.Default);
        /*PhotonNetwork.CreateRoom("at", new RoomOptions { MaxPlayers = 15, IsOpen = true, IsVisible = true }, TypedLobby.Default);
        PhotonNetwork.CreateRoom("ads", new RoomOptions { MaxPlayers = 15, IsOpen = true, IsVisible = true }, TypedLobby.Default);
        PhotonNetwork.CreateRoom("dada", new RoomOptions { MaxPlayers = 15, IsOpen = true, IsVisible = true }, TypedLobby.Default);
        PhotonNetwork.CreateRoom("customRoomName.text", new RoomOptions { MaxPlayers = 15, IsOpen = true, IsVisible = true }, TypedLobby.Default);
        PhotonNetwork.CreateRoom("asdasd", new RoomOptions { MaxPlayers = 15, IsOpen = true, IsVisible = true }, TypedLobby.Default);
        PhotonNetwork.CreateRoom("adadfafafa", new RoomOptions { MaxPlayers = 15, IsOpen = true, IsVisible = true }, TypedLobby.Default);
        PhotonNetwork.CreateRoom("daafsafafas", new RoomOptions { MaxPlayers = 15, IsOpen = true, IsVisible = true }, TypedLobby.Default);*/
    }     
    public override void OnJoinedRoom()
    {
        props.TryGetValue("status", out object playerStatus);
        props["status"] = !(bool)playerStatus;
        Debug.Log("Odaya Girildi.");     
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        
        //  PhotonNetwork.Instantiate("Player", new Vector3(-10.2600002f, 47.0600014f, -22.8600006f), Quaternion.identity);



    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected");      
    }
    public override void OnConnectedToMaster()
    {

        Debug.Log("Server'e Ba??lan??ld??.");
        
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
        SetActivePanel(loginScreen.name);
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
         Debug.Log("Joinning has Failed" + message + "-" + returnCode);
         SetActivePanel(loginScreen.name);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("The room could not be created." + message + " - " + returnCode);
        SetActivePanel(loginScreen.name);
    }



    
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
            
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            Debug.Log("lisstt");
            Instantiate(roomListItemPrefab,roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
            
        }
        
    }


    #endregion


    [PunRPC]

    void Startgm()
    {
         PhotonNetwork.Instantiate("Walking", new Vector3(-8.86f, 45.28f, -22.8600006f), Quaternion.identity);
        Debug.Log("herkes haz??r");
      /*  quizcanv.GetComponent<GameManager>().enabled = true;
       // PhotonView.FindObjectOfType<GameManager>().enabled = true;
        cnvas.enabled = false;
       //().enabled = true;
        inputCanvas.enabled = true;*/
        PhotonView.Find(8).transform.gameObject.SetActive(false);
        PhotonView.Find(7).transform.gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<Canvas>().enabled = true;
        // qpw = PhotonView.Find(id);
        //qpw.transform.gameObject.SetActive(false);
        //GameObject.Find("LobbyScreen").gameObject.SetActive(false);


    }
       
}

