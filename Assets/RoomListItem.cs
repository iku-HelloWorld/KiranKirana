using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    RoomInfo info1;

    public void  SetUp(RoomInfo info)
    {
        info1 = info;
        text.text = info.Name;
    }

    public void OnClick()
    {

    }


   
}
