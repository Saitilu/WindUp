using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomFilled : MonoBehaviour
{
    void Update()
    {
        if (PhotonNetwork.CurrentRoom.MaxPlayers == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            PhotonNetwork.LoadLevel("Game");
        }
    }
}
