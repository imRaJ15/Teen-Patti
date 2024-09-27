using Photon.Pun;
using System.Collections;
using UnityEngine;

public class TableManager : MonoBehaviourPunCallbacks
{
    #region Player Join Round Starts and Player Waiting Variables

    [SerializeField] GameObject _button0, _button1, _button2,
        _button3, _button4, _button5, _roundButton, _countDown,
        _firstPlayerWait, _mainWait;

    [SerializeField] Animator _animator;

    private bool _isSeated, _isRoundRunning, _haveToWait, _isCountdownStarts;

    private int _totalSeatedPlayers;

    #endregion

    #region Player's Turn Variables

    int _currentPlayerIndex, _totalTurnRounds;

    bool _isTurnProgress;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        UpdateButtons();
        _totalSeatedPlayers = 0;
        _isRoundRunning = false;
        _haveToWait = false;
        _roundButton.SetActive(false);
        _countDown.SetActive(false);
        _firstPlayerWait.SetActive(false);
        _mainWait.SetActive(false);
        _isCountdownStarts = false;
        _isSeated = false;

        _currentPlayerIndex = 0;
        _totalTurnRounds = 0;
        _isTurnProgress = false;    
    }

    private void Update()
    {
        
    }

    #region Player Join Round Starts and Player Waiting Methods

    private void UpdateButtons()
    {
        int maxPlayer = PhotonNetwork.CurrentRoom.MaxPlayers;

        _button0.SetActive(true);
        _button1.SetActive(true);
        _button2.SetActive(true);
        _button3.SetActive(true);
        _button4.SetActive(true);
        _button5.SetActive(true);

        foreach (var player in PhotonNetwork.PlayerList)
        {
            if (player != null)
            {
                if (player.CustomProperties.ContainsKey("SeatNumber"))
                {
                    int playerNumber = (int)player.CustomProperties["SeatNumber"] - 1;

                    DisableButton(playerNumber);
                }
            }
            else { Debug.Log("Player list not found"); }
        }
    }

    public void OnButtonclick(int seatNumber)
    {
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable
        { { "SeatNumber", seatNumber } });

        DisableButton((seatNumber - 1));

        _isSeated = true;

        photonView.RPC("UpdateButtonsRPC", RpcTarget.All);

        StartCoroutine(DelayedCheckPlayerCount(1.0f));
    }

    IEnumerator DelayedCheckPlayerCount(float delay)
    {
        yield return new WaitForSeconds(delay);
        CheckPlayerCount();
    }

    [PunRPC]
    private void UpdateButtonsRPC()
    { UpdateButtons(); }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        UpdateButtons();
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        UpdateButtons();
    }

    private void CheckPlayerCount()
    {
        int seatedPlayers = 0;

        foreach (var player in PhotonNetwork.PlayerList)
        {
            if (player.CustomProperties.ContainsKey("SeatNumber"))
            { seatedPlayers++; }
        }

        photonView.RPC("TotalSeatedPlayer", RpcTarget.All, seatedPlayers);

        Debug.Log("Players Seated :" + seatedPlayers);

        if (seatedPlayers >= 2)
        { photonView.RPC("CheckRoundConditionRPC", RpcTarget.All); }
        else
        { _firstPlayerWait.SetActive(true); }
    }

    [PunRPC]

    private void TotalSeatedPlayer(int count)
    { _totalSeatedPlayers = count; }

    [PunRPC]
    private void CheckRoundConditionRPC()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (!_isRoundRunning)
            { StartNewRound(); }
            else { photonView.RPC("MakeEveryoneWaitRPC", RpcTarget.All); }
        }
        else
        {
            StartCoroutine(NotifyPlayerRoundCondition());
        }
    }

    [PunRPC]
    private void MakeEveryoneWaitRPC()
    { _haveToWait = true; }

    IEnumerator NotifyPlayerRoundCondition()
    {
        yield return new WaitForSeconds(2.0f);
        if (_haveToWait && !_isRoundRunning)
        { _mainWait.SetActive(true); }
    }

    private void StartNewRound()
    {
        StartCoroutine(StartFreshRound());
        if (!_isCountdownStarts)
        { photonView.RPC("StartCountDownRPC", RpcTarget.All); }
        else { photonView.RPC("StartDelayedCountDownRPC", RpcTarget.Others, AnimationCurrentTime()); }

    }

    [PunRPC]
    private void StartDelayedCountDownRPC(float delayTime)
    {
        _countDown.SetActive(true);
        FastForwardAnimation(delayTime);
        _firstPlayerWait.SetActive(false);
    }

    [PunRPC]
    private void StartCountDownRPC()
    {
        if (_isSeated)
        {
            _isCountdownStarts = true;
            _countDown.SetActive(true);
            _firstPlayerWait.SetActive(false);
        }
    }

    [PunRPC]
    private void StartNewRoundRPC(bool roundCondition)
    {
        _isRoundRunning = roundCondition;
        _countDown.SetActive(false);

        if (PhotonNetwork.IsMasterClient)
        { StartPlayerTurn(_currentPlayerIndex); }
    }

    IEnumerator StartFreshRound()
    {
        yield return new WaitForSeconds(5.0f);
        _isRoundRunning = true;
        photonView.RPC("StartNewRoundRPC", RpcTarget.All, _isRoundRunning);
    }

    private void StopCurrentRound()
    {
        _isRoundRunning = false;

        photonView.RPC("StopCurrentRoundRPC", RpcTarget.All, _isRoundRunning);
    }

    [PunRPC]
    private void StopCurrentRoundRPC(bool roundCondition)
    { _isRoundRunning = roundCondition; }

    private void DisableButton(int s)
    {
        switch (s)
        {
            case 0:
                _button0.SetActive(false);
                break;
            case 1:
                _button1.SetActive(false);
                break;
            case 2:
                _button2.SetActive(false);
                break;
            case 3:
                _button3.SetActive(false);
                break;
            case 4:
                _button4.SetActive(false);
                break;
            case 5:
                _button5.SetActive(false);
                break;
        }
    }

    private void FastForwardAnimation(float timeToSkip)
    {
        float animationLength = 4.75f;

        float normalizeTime = timeToSkip / animationLength;

        _animator.Play("CountDown Anim", 0, normalizeTime);

        _animator.Update(0);
    }

    private float AnimationCurrentTime()
    {
        float animationLength = 4.75f;

        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("CountDown Anim") && PhotonNetwork.IsMasterClient)
        {
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            float normalizedTime = stateInfo.normalizedTime;
            float currentTime = normalizedTime * animationLength;
            return currentTime = currentTime % animationLength;
        }
        else { return 0f; }
    }

    #endregion

    #region Player's Turn Methods

    [PunRPC]

    private void StartPlayerTurn(int playerIndex)
    {
        _currentPlayerIndex = playerIndex;
        _isTurnProgress = true;

        if (PhotonNetwork.LocalPlayer.ActorNumber == (_currentPlayerIndex + 1))
        { PlayerAction(true); }
        else
        { PlayerAction(false); }
    }

    public void EndPlayerTurn()
    {
        _isTurnProgress = false;

        photonView.RPC("NextPlayer", RpcTarget.All);
    }

    [PunRPC]

    private void NextPlayer()
    {
        _currentPlayerIndex = (_currentPlayerIndex + 1) % _totalSeatedPlayers;

        photonView.RPC("StartPlayerTurn", RpcTarget.All, _currentPlayerIndex);

        if (_currentPlayerIndex == 0)
        { _totalTurnRounds++; }
    }

    private void PlayerAction(bool isEnabled)
    {
        if (isEnabled)
        { _roundButton.SetActive(true); }
        else
        { _roundButton.SetActive(false); }
    }

    #endregion
}
