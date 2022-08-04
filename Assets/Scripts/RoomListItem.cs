using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    RoomInfo info;

    public void  SetUp(RoomInfo info1)
    {
        info = info1;
        text.text = info1.Name;
    }

    public void OnClick()
    {

        ServerManager.instance.JoinListRoom(info);
        

    }


   
}
