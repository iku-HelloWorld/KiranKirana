using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class RoomBrowser : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform content;
    [SerializeField] GameObject roomListing;









   /* public GameObject buttonprefab;
    public GameObject buttonParent;

        
    private void OnEnable() 
    {
        for(int i = 0; i< 7;i++)
        {
            int roomNum = i;
            GameObject newButton = Instantiate(buttonprefab, buttonParent.transform);
            //newButton.GetComponent<RoomButton>().
            //newButton.GetComponent<Button>().onClick.AddListener() => SelectRoom(ServerManager.Instance.) 
        }

    }

    private void SelectRoom(string roomName)
    {
        Debug.Log(roomName);
    }
*/
}