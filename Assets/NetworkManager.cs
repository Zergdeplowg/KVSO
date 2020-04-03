using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

	// Use this for initialization

    public GameObject StandbyCamera;
    private SpawnSpot[] spawnSpots;

    public bool offlineMode = false;


    void Start ()
	{
	    spawnSpots = GameObject.FindObjectsOfType<SpawnSpot>();
        Connect();
	}

    void Connect()
    {
        if (offlineMode)
        {
            PhotonNetwork.offlineMode = true;
            OnJoinedLobby();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings("KvsO 0.0.1");
        }
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("OnPhotonRandomJoinFailed");
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        SpawnMyPlayer();
    }

    void SpawnMyPlayer()
    {
        SpawnSpot mySpawnSpot = spawnSpots[Random.Range(0, spawnSpots.Length)];
        GameObject myPlayer = (GameObject)PhotonNetwork.Instantiate("First Person Controller",mySpawnSpot.transform.position,mySpawnSpot.transform.rotation,0);
        //((MonoBehaviour)myPlayer.GetComponent("FPSInputController")).enabled = true;
        ((MonoBehaviour)myPlayer.GetComponent("MouseLook")).enabled = true;
        ((MonoBehaviour)myPlayer.GetComponent("PlayerMovement")).enabled = true;
        ((MonoBehaviour)myPlayer.GetComponent("NetworkCharacter")).enabled = true;
        ((MonoBehaviour)myPlayer.GetComponent("PlayerShooting")).enabled = true;
        myPlayer.gameObject.transform.Find("Main Camera").gameObject.SetActive(true);
        StandbyCamera.SetActive(false);
    }
}
