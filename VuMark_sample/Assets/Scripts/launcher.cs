using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launcher : Photon.PunBehaviour {
    #region Public Variables
    public PhotonLogLevel logLevel = PhotonLogLevel.Informational;
    public byte MaxPlayersPerRoom = 10;
    public GameObject loadmanager;
    //public GameObject controlPanel;
    //public GameObject progressLabel;
    #endregion
    int counter = 0;
    #region Private Variables

    /// <summary>
    /// this is the game version of the client
    /// </summary>

    string gameVersion = "1";
    bool isConnecting;
    //bool isAndroid = true;
    bool isPC = false;
    bool isHolo = true;
    #endregion

    //callback when unity want to interact with game objct
    #region MonoBehaviour CallBacks
    private void Awake()
    {
        PhotonNetwork.autoJoinLobby = false; //we don't want to automatically join lobby, instead we join in a random room

        PhotonNetwork.automaticallySyncScene = false; //this can make sure we can use photonNetwork.loadlevel() method to let all clients in the same room run the same level

        PhotonNetwork.logLevel = logLevel;
    }
    // Use this for initialization
    void Start()
    {
        //now we connect by clicking the button
        Connect();
        //progressLabel.SetActive(false);
        //controlPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (counter  == 100 && isConnecting)
        {
            counter = 0;
           // Debug.Log("people in this room is " + PhotonNetwork.room.PlayerCount);
        }
        counter++;
    }
    #endregion


    #region public method
    public void Connect()
    {
        //progressLabel.SetActive(true);
        //controlPanel.SetActive(false);
        if (PhotonNetwork.connected)
        {
            //when the network is connected, we join a random room
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings(gameVersion);
        }

        isConnecting = true;
    }
    #endregion

    #region Photon.PunBehaviour callBacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("DemoAnimator/Launcher: OnConnectedToMaster() was called by PUN");
        //base.OnConnectedToMaster();
        if (isConnecting)
        {
            Debug.Log("we join random room");
            PhotonNetwork.JoinRandomRoom(); //if there is a room, join it. If not create one.
        }
    }

    public override void OnDisconnectedFromPhoton()
    {
        //progressLabel.SetActive(false);
        //controlPanel.SetActive(true);
        Debug.Log("DemoAnimator/Launcher: onDisconnectedFromPhoton() was called by PUN");
        //base.OnDisconnectedFromPhoton();
    }

    //deal with join room method
    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        //base.OnPhotonCreateRoomFailed(codeAndMsg);
        Debug.Log("DemoAnimator/Launcher:OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
        //we will create a new room if the join room fail
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
    }

    public override void OnJoinedRoom()
    {
        //base.OnJoinedRoom();
        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room");

        Debug.Log("the player in the room is " + PhotonNetwork.room.PlayerCount);
        //if (isAndroid)
        //    PhotonNetwork.LoadLevel("Room_1");
        if (isHolo)
        {
            //PhotonNetwork.LoadLevel("2-Hololens"); //first approach, use photon to load scene, but I am not sure whether loadmanager has some special setting
            //the other way is to set load manager to active;
            //GameObject loadmanager = GameObject.Find("loadManager");
            var loadScript = loadmanager.GetComponent<LoadingScreen>();
            loadScript.enabled = true;
        }
        else if (isPC)
            PhotonNetwork.LoadLevel("3-LPsensor");
    }
    #endregion

}
