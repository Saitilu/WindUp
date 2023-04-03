using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class RoomMatch : MonoBehaviourPunCallbacks
{
    private void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(Random.Range(0, 100).ToString(), roomOptions, null);
    }

    public void QuickMatch()
    {
        PhotonNetwork.JoinRandomRoom();
    }


    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("JoinFailed");
        CreateRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined");
        // joined a room successfully
        PhotonNetwork.LoadLevel("Waiting");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        PhotonNetwork.JoinRandomRoom();
    }
}

