using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject _connectionFailedCanvas;
    
    [SerializeField] int _yourTotalChips;

    //private Player _player;

    //private List<(string, int, int)> playersHands;

    private static GameManager instance;

    private void Awake()
    {
        //_player = GameObject.Find("PlayerScript").GetComponent<Player>();
        //playersHands = _player.PlayersHands;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }

    private void Start()
    {
        //DetermineWinner();
        _connectionFailedCanvas.SetActive(false);
        ConnectToPhoton();
    }

    private void Update()
    {
        
    }

    private void ConnectToPhoton()
    {
        if (!PhotonNetwork.IsConnected) 
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        JoinLobby();
    }

    private void JoinLobby()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Lobby Joined..");
        SceneManager.LoadScene("MainMenu");
    }

    private void IfConnectionFailed()
    {
        if (_connectionFailedCanvas != null)
        {
            _connectionFailedCanvas.SetActive(true);
        }
    }

    public void TryAgainButton()
    {
        ConnectToPhoton();
        _connectionFailedCanvas?.SetActive(false);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected because : " +  cause.ToString());
        IfConnectionFailed();
    }

    /*private void DetermineWinner()
    {
        playersHands.Sort(ComparePlayeByCardRank);
        playersHands.Reverse();
        int hightestRank = playersHands[0].Item2;

        for (int i = 0; i < playersHands.Count; i++)
        {
            var playerhands = playersHands[i];
            Debug.Log($"{playerhands.Item1} : {playerhands.Item2}, {playerhands.Item3}");
        }

        List<(string playerName, int cardRank, int rankValue)> highestCardsPlayer =
            new List<(string, int, int)>();

        foreach (var player in playersHands) 
        {
            if (player.Item2 == hightestRank)
            { highestCardsPlayer.Add(player); }
        }

        if (highestCardsPlayer.Count > 1)
        {
            Debug.Log(TieBraker(highestCardsPlayer) + " is winner");
        }
        else
        {
            var player = highestCardsPlayer;
            Debug.Log($"{player[0].playerName} is Winner");
        }
    }

    private int ComparePlayeByCardRank((string playerName, int cardRank, int rankValue) playerA,
        (string playerName, int cardRank, int rankValue) playerB)
    {
        return playerA.cardRank.CompareTo(playerB.cardRank);
    }

    private int ComparePlayerByRank((string playerName, int cardRank, int rankValue) playerA,
        (string playerName, int cardRank, int rankValue) playerB)
    {
        return playerA.rankValue.CompareTo(playerB.rankValue);
    }

    private string TieBraker(List<(string playerName, int cardRank, int rankValue)> tiedPlayers)
    {
        tiedPlayers.Sort(ComparePlayerByRank);
        tiedPlayers.Reverse();

        int playersHighestRank = tiedPlayers[0].rankValue;

        List<(string playerName, int cardRank, int rankValue)> highestRankPlayers =
           new List<(string, int, int)>();

        foreach ( var player in tiedPlayers ) 
        {
            if (player.rankValue == playersHighestRank)
            {
                highestRankPlayers.Add(player);
            }
        }

        if (highestRankPlayers.Count > 1)
        {
            for ( int i = 0; i < highestRankPlayers.Count; i++ ) 
            {
                return (highestRankPlayers[i].playerName);
            }
        }
        else 
            return highestRankPlayers[0].playerName;

        return null;
    }*/
}