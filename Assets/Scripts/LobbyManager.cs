using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JoinLobby()
    { PhotonNetwork.JoinRandomRoom(); }

    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene("Table");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions newRoom = new RoomOptions();
        newRoom.MaxPlayers = 6;
        newRoom.IsOpen = true;
        newRoom.IsVisible = true;

        PhotonNetwork.CreateRoom(null, newRoom);
    }
}
