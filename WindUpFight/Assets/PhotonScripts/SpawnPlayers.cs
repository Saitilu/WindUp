using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    bool spawned = false;
    [SerializeField] GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GameObject playerInstance;
        if (PhotonNetwork.IsMasterClient && !spawned)
        {
            Debug.Log("Masters");
            playerInstance = PhotonNetwork.Instantiate(playerPrefab.name, new Vector2(-4, -2.4f), Quaternion.identity);
            playerInstance.name = "MyPlayer";
            spawned = true;
        }
        else if (!spawned)
        {
            Debug.Log("Clients");
            playerInstance = PhotonNetwork.Instantiate(playerPrefab.name, new Vector2(4, -2.4f), Quaternion.identity);
            playerInstance.name = "MyPlayer";
            spawned = true;
        }
    }

    private void Update()
    {
        GameObject otherPlayer = GameObject.Find("Player(Clone)");
        if (otherPlayer != null)
        {
            otherPlayer.name = "OtherPlayer";
            Destroy(this);
        }
    }
}   
