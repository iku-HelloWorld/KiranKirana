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

    [SerializeField] GameObject loginScreen;
    [SerializeField] GameObject lobbyScreen;
    [SerializeField] GameObject CreateOrJoin;
    [SerializeField] GameObject joinRoom;
 
    [SerializeField] Canvas cnvas;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        SetActivePanel(loginScreen.name);
        OnConnectedToMaster(); 
       // OnJoinedLobby();        
    }


    public void SetActivePanel(string activePanel)
    {
        loginScreen.SetActive(activePanel.Equals(loginScreen.name));
        lobbyScreen.SetActive(activePanel.Equals(lobbyScreen.name));
        CreateOrJoin.SetActive(activePanel.Equals(CreateOrJoin.name));
        joinRoom.SetActive(activePanel.Equals(joinRoom.name));

    }



    void Update()
    {
        

    }


    /*public void SetActivePanel(string activePanel)
    {
        GirisPanel.SetActive(activePanel.Equals(GirisPanel.name));
        SecimPanel.SetActive(activePanel.Equals(SecimPanel.name));
        OdaOlusturPanel.SetActive(activePanel.Equals(OdaOlusturPanel.name));       
        OdalistePanel.SetActive(activePanel.Equals(OdalistePanel.name));
        OdaicPanel.SetActive(activePanel.Equals(OdaicPanel.name));      
    }*/

    public void SetNickname(string name)
    {
       // PhotonNetwork.NickName = nameText.text;
       PhotonNetwork.LocalPlayer.NickName = nameText.text;
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
        SetActivePanel(CreateOrJoin.name);
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


    public void JoinRoom()
    {
        SetActivePanel(joinRoom.name);
    }


}


