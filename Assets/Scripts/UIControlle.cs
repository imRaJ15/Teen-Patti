using System.Collections.Generic;
using UnityEngine;

public class UIControlle : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] _player1Cards, _player2Cards, _player3Cards, _player4Cards;

    private Player _player;
    private Dictionary<string, Sprite> cardSprites = new Dictionary<string, Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        InitializeCardSprites();

        _player = GameObject.Find("PlayerScript").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        AssignSpriteToCards();
    }


    private void AssignSpriteToCards()
    {
        if (_player != null)
        {
            _player1Cards[0].sprite = cardSprites[_player.Player1Cards[0]];
            _player1Cards[1].sprite = cardSprites[_player.Player1Cards[1]];
            _player1Cards[2].sprite = cardSprites[_player.Player1Cards[2]];

            _player2Cards[0].sprite = cardSprites[_player.Player2Cards[0]];
            _player2Cards[1].sprite = cardSprites[_player.Player2Cards[1]];
            _player2Cards[2].sprite = cardSprites[_player.Player2Cards[2]];

            _player3Cards[0].sprite = cardSprites[_player.Player3Cards[0]];
            _player3Cards[1].sprite = cardSprites[_player.Player3Cards[1]];
            _player3Cards[2].sprite = cardSprites[_player.Player3Cards[2]];

            _player4Cards[0].sprite = cardSprites[_player.Player4Cards[0]];
            _player4Cards[1].sprite = cardSprites[_player.Player4Cards[1]];
            _player4Cards[2].sprite = cardSprites[_player.Player4Cards[2]];
        }
        else { Debug.LogWarning("Player Scripts is not Found"); }
    }

    private void InitializeCardSprites()
    {
        cardSprites["Ace of Spades"] = Resources.Load<Sprite>("Cards/Ace of Spades");
        cardSprites["Two of Spades"] = Resources.Load<Sprite>("Cards/Two of Spades");
        cardSprites["Three of Spades"] = Resources.Load<Sprite>("Cards/Three of Spades");
        cardSprites["Four of Spades"] = Resources.Load<Sprite>("Cards/Four of Spades");
        cardSprites["Five of Spades"] = Resources.Load<Sprite>("Cards/Five of Spades");
        cardSprites["Six of Spades"] = Resources.Load<Sprite>("Cards/Six of Spades");
        cardSprites["Seven of Spades"] = Resources.Load<Sprite>("Cards/Seven of Spades");
        cardSprites["Eight of Spades"] = Resources.Load<Sprite>("Cards/Eight of Spades");
        cardSprites["Nine of Spades"] = Resources.Load<Sprite>("Cards/Nine of Spades");
        cardSprites["Ten of Spades"] = Resources.Load<Sprite>("Cards/Ten of Spades");
        cardSprites["Jack of Spades"] = Resources.Load<Sprite>("Cards/Jack of Spades");
        cardSprites["Queen of Spades"] = Resources.Load<Sprite>("Cards/Queen of Spades");
        cardSprites["King of Spades"] = Resources.Load<Sprite>("Cards/King of Spades");

        cardSprites["Ace of Hearts"] = Resources.Load<Sprite>("Cards/Ace of Hearts");
        cardSprites["Two of Hearts"] = Resources.Load<Sprite>("Cards/Two of Hearts");
        cardSprites["Three of Hearts"] = Resources.Load<Sprite>("Cards/Three of Hearts");
        cardSprites["Four of Hearts"] = Resources.Load<Sprite>("Cards/Four of Hearts");
        cardSprites["Five of Hearts"] = Resources.Load<Sprite>("Cards/Five of Hearts");
        cardSprites["Six of Hearts"] = Resources.Load<Sprite>("Cards/Six of Hearts");
        cardSprites["Seven of Hearts"] = Resources.Load<Sprite>("Cards/Seven of Hearts");
        cardSprites["Eight of Hearts"] = Resources.Load<Sprite>("Cards/Eight of Hearts");
        cardSprites["Nine of Hearts"] = Resources.Load<Sprite>("Cards/Nine of Hearts");
        cardSprites["Ten of Hearts"] = Resources.Load<Sprite>("Cards/Ten of Hearts");
        cardSprites["Jack of Hearts"] = Resources.Load<Sprite>("Cards/Jack of Hearts");
        cardSprites["Queen of Hearts"] = Resources.Load<Sprite>("Cards/Queen of Hearts");
        cardSprites["King of Hearts"] = Resources.Load<Sprite>("Cards/King of Hearts");

        cardSprites["Ace of Clubs"] = Resources.Load<Sprite>("Cards/Ace of Clubs");
        cardSprites["Two of Clubs"] = Resources.Load<Sprite>("Cards/Two of Clubs");
        cardSprites["Three of Clubs"] = Resources.Load<Sprite>("Cards/Three of Clubs");
        cardSprites["Four of Clubs"] = Resources.Load<Sprite>("Cards/Four of Clubs");
        cardSprites["Five of Clubs"] = Resources.Load<Sprite>("Cards/Five of Clubs");
        cardSprites["Six of Clubs"] = Resources.Load<Sprite>("Cards/Six of Clubs");
        cardSprites["Seven of Clubs"] = Resources.Load<Sprite>("Cards/Seven of Clubs");
        cardSprites["Eight of Clubs"] = Resources.Load<Sprite>("Cards/Eight of Clubs");
        cardSprites["Nine of Clubs"] = Resources.Load<Sprite>("Cards/Nine of Clubs");
        cardSprites["Ten of Clubs"] = Resources.Load<Sprite>("Cards/Ten of Clubs");
        cardSprites["Jack of Clubs"] = Resources.Load<Sprite>("Cards/Jack of Clubs");
        cardSprites["Queen of Clubs"] = Resources.Load<Sprite>("Cards/Queen of Clubs");
        cardSprites["King of Clubs"] = Resources.Load<Sprite>("Cards/King of Clubs");

        cardSprites["Ace of Diamonds"] = Resources.Load<Sprite>("Cards/Ace of Diamonds");
        cardSprites["Two of Diamonds"] = Resources.Load<Sprite>("Cards/Two of Diamonds");
        cardSprites["Three of Diamonds"] = Resources.Load<Sprite>("Cards/Three of Diamonds");
        cardSprites["Four of Diamonds"] = Resources.Load<Sprite>("Cards/Four of Diamonds");
        cardSprites["Five of Diamonds"] = Resources.Load<Sprite>("Cards/Five of Diamonds");
        cardSprites["Six of Diamonds"] = Resources.Load<Sprite>("Cards/Six of Diamonds");
        cardSprites["Seven of Diamonds"] = Resources.Load<Sprite>("Cards/Seven of Diamonds");
        cardSprites["Eight of Diamonds"] = Resources.Load<Sprite>("Cards/Eight of Diamonds");
        cardSprites["Nine of Diamonds"] = Resources.Load<Sprite>("Cards/Nine of Diamonds");
        cardSprites["Ten of Diamonds"] = Resources.Load<Sprite>("Cards/Ten of Diamonds");
        cardSprites["Jack of Diamonds"] = Resources.Load<Sprite>("Cards/Jack of Diamonds");
        cardSprites["Queen of Diamonds"] = Resources.Load<Sprite>("Cards/Queen of Diamonds");
        cardSprites["King of Diamonds"] = Resources.Load<Sprite>("Cards/King of Diamonds");
    }
}
