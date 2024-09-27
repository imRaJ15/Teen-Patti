using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Deck _deck;
    private List<string> player1Cards = new List<string>();
    private List<string> player2Cards = new List<string>();
    private List<string> player3Cards = new List<string>();
    private List<string> player4Cards = new List<string>();
    [SerializeField] private int[] player1Ranks, player2Ranks,player3Ranks, player4Ranks;
    [SerializeField] private string[] player1Suits, player2Suits, player3Suits, player4Suits;

    [SerializeField] private List<string> allPlayersHands = new List<string>();

    private bool _isPlayer1Folded, _isPlayer2Folded, _isPlayer3Folded, _isPlayer4Folded;

    private Dictionary<string, int> _rankToNumber = new Dictionary<string, int>()
    {
        { "Ace", 1 },
        { "Two", 2 },
        { "Three", 3 },
        { "Four", 4 },
        { "Five", 5 },
        { "Six", 6 },
        { "Seven", 7 },
        { "Eight", 8 },
        { "Nine", 9 },
        { "Ten", 10 },
        { "Jack", 11 },
        { "Queen", 12 },
        { "King", 13 }
    };

    public List<string> Player1Cards
    {
        get { return player1Cards; }
        set { player1Cards = value; }
    }

    public List<string> Player2Cards
    {
        get { return player2Cards; }
        set { player2Cards = value; }
    }

    public List<string> Player3Cards
    {
        get { return player3Cards; }
        set { player3Cards = value; }
    }

    public List<string> Player4Cards
    {
        get { return player4Cards; }
        set { player4Cards = value; }
    }

    [SerializeField] private List<(string playerName, int cardRank, int rankValue)> playersHands = new List<(string, int, int)>();

    public List<(string, int, int)> PlayersHands
    {
        get { return playersHands; } 
        set { playersHands = value; }
    }

    private void Start()
    {
        DealCardToPlayer();
        _isPlayer1Folded = false;
        _isPlayer2Folded = true;
        _isPlayer3Folded = true;
        _isPlayer4Folded = false;
        MakePlayersHands();
    }

    public void DealCardToPlayer()
    {
        _deck = GameObject.Find("Floor").GetComponent<Deck>();
        int numberOfCards = 3;
        player1Cards.Clear();
        player2Cards.Clear();
        player3Cards.Clear();
        player4Cards.Clear();

        for (int i = 0; i < numberOfCards; i++)
        {
            if (_deck.CardsDeck.Count > 0)
            {
                player1Cards.Add((_deck.CardsDeck[0]));
                _deck.CardsDeck.RemoveAt(0);
            }

            if (_deck.CardsDeck.Count > 0)
            {
                player2Cards.Add(_deck.CardsDeck[0]);
                _deck.CardsDeck.RemoveAt(0);
            }
            if (_deck.CardsDeck.Count > 0)
            {
                player3Cards.Add(_deck.CardsDeck[0]);
                _deck.CardsDeck.RemoveAt(0);
            }

            if (_deck.CardsDeck.Count > 0)
            {
                player4Cards.Add(_deck.CardsDeck[0]);
                _deck.CardsDeck.RemoveAt(0);
            }
        }
        FillPlayersRankAndSuit();
    }

    private void FillPlayersRankAndSuit()
    {
        if (player1Cards.Count > 0)
        {
            for (int i = 0; i < player1Cards.Count; i++)
            {
                player1Ranks[i] = RankFinder(player1Cards[i]);
                player1Suits[i] = SuitFinder(player1Cards[i]);
            }
        }

        if (player2Cards.Count > 0)
        {
            for (int i = 0; i < player2Cards.Count; i++)
            {
                player2Ranks[i] = RankFinder(player2Cards[i]);
                player2Suits[i] = SuitFinder(player2Cards[i]);
            }
        }

        if (player3Cards.Count > 0)
        {
            for (int i = 0; i < player3Cards.Count; i++)
            {
                player3Ranks[i] = RankFinder(player3Cards[i]);
                player3Suits[i] = SuitFinder(player3Cards[i]);
            }
        }

        if (player4Cards.Count > 0)
        {
            for (int i = 0; i < player4Cards.Count; i++)
            {
                player4Ranks[i] = RankFinder(player4Cards[i]);
                player4Suits[i] = SuitFinder(player4Cards[i]);
            }
        }
    }

    private int RankFinder(string card)
    {
        string[] part = card.Split(' ');

        if (_rankToNumber.ContainsKey(part[0]))
        { return _rankToNumber[part[0]]; }
        else { return 0; }
    }
    private string SuitFinder(string card)
    {
        string[] part = card.Split(' ');
        return part[2];
    }

    public int PlayerOneHand()
    {
        if (!_isPlayer1Folded)
        {
            if (IsThreeOfKind(player1Ranks))
            { return 6; }
            else if (IsRon(player1Ranks) && IsColor(player1Suits))
            { return 5; }
            else if (IsRon(player1Ranks))
            { return 4; }
            else if (IsColor(player1Suits))
            { return 3; }
            else if (IsTwoPair(player1Ranks))
            { return 2; }
            else { return 1; }
        }
        else return -1;
    }

    public int PlayerTwoHand()
    {
        if (!_isPlayer2Folded)
        {
            if (IsThreeOfKind(player2Ranks))
            { return 6; }
            else if (IsRon(player2Ranks) && IsColor(player2Suits))
            { return 5; }
            else if (IsRon(player2Ranks))
            { return 4; }
            else if (IsColor(player2Suits))
            { return 3; }
            else if (IsTwoPair(player2Ranks))
            { return 2; }
            else { return 1; }
        }
        else return -1;
    }

    public int PlayerThreeHand()
    {
        if (!_isPlayer3Folded)
        {
            if (IsThreeOfKind(player3Ranks))
            { return 6; }
            else if (IsRon(player3Ranks) && IsColor(player3Suits))
            { return 5; }
            else if (IsRon(player3Ranks))
            { return 4; }
            else if (IsColor(player3Suits))
            { return 3; }
            else if (IsTwoPair(player3Ranks))
            { return 2; }
            else { return 1; }
        }
        else return -1;
    }

    public int PlayerFourHand()
    {
        if (!_isPlayer4Folded)
        {
            if (IsThreeOfKind(player4Ranks))
            { return 6; }
            else if (IsRon(player4Ranks) && IsColor(player4Suits))
            { return 5; }
            else if (IsRon(player4Ranks))
            { return 4; }
            else if (IsColor(player4Suits))
            { return 3; }
            else if (IsTwoPair(player4Ranks))
            { return 2; }
            else { return 1; }
        }
        else { return -1; }
    }

    public int PlayerOneHighCard()
    { return (PlayerHighCard(player1Ranks)); }

    public int PlayerTwoHighCard()
    { return (PlayerHighCard(player2Ranks)); }

    public int PlayerThreeHighCard()
    { return (PlayerHighCard(player3Ranks)); }

    public int PlayerFourHighCard()
    { return (PlayerHighCard(player4Ranks)); }

    private bool IsColor(string[] suits)
    {
        return (suits[0] == suits[1] && suits[1] == suits[2]);
    }

    private bool IsThreeOfKind(int[] rank)
    {
        return (rank[0] == rank[1] && rank[1] == rank[2]);
    }

    private bool IsRon(int[] rank)
    {
        return ((PlayerHighCard(rank) - PlayerLowCard(rank)) == 2
            && rank[0] != rank[1] && rank[1] != rank[2]);
    }

    private bool IsTwoPair(int[] rank)
    {
        return (rank[0] == rank[1] || rank[1] == rank[2] || rank[0] == rank[2]);
    }

    private int PlayerHighCard(int[] rank)
    {
        int n = Mathf.Max(rank[0], rank[1], rank[2]);
        return n;
    }

    private int PlayerLowCard(int[] rank)
    {
        int n = Mathf.Min(rank[0], rank[1], rank[2]);
        return n;
    }

    public void MakePlayersHands()
    {
        playersHands.Add(("Player1", PlayerOneHand(), PlayerOneHighCard()));
        playersHands.Add(("Player2", PlayerTwoHand(), PlayerTwoHighCard()));
        playersHands.Add(("Player3", PlayerThreeHand(), PlayerThreeHighCard()));
        playersHands.Add(("Player4", PlayerFourHand(), PlayerFourHighCard()));
    }
}

