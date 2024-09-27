using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<string> cardsDeck = new List<string>();
    private List<string> sufflingDeck = new List<string>();
    private List<string> part1 = new List<string>();
    private List<string> part2 = new List<string>();

    public List<string> CardsDeck
    { 
        get { return cardsDeck; } 
        set { cardsDeck = value; }
    }

    private void Start()
    {
        PerfactCardsDeck();
    }

    public void PerfactCardsDeck()
    {
        int r = Random.Range(0, 5);

        switch (r)
        {
            case 0:
                CardDeckforShuffle();
                ChipSuffle();
                KetteShuffle();
                ChipSuffle();
                ChipSuffle();
                KetteShuffle();
                ChipSuffle();
                cardsDeck.Clear();
                SuffleDecktoMainDeck();
                break;

            case 1:
                CardDeckforShuffle();
                ChipSuffle();
                ChipSuffle();
                KetteShuffle();
                ChipSuffle();
                KetteShuffle();
                ChipSuffle();
                ChipSuffle();
                cardsDeck.Clear();
                SuffleDecktoMainDeck();
                break;

            case 2:
                CardDeckforShuffle();
                KetteShuffle();
                ChipSuffle();
                ChipSuffle();
                cardsDeck.Clear();
                SuffleDecktoMainDeck();
                break;

            case 3:
                CardDeckforShuffle();
                ChipSuffle();
                ChipSuffle();
                ChipSuffle();
                ChipSuffle();
                KetteShuffle();
                ChipSuffle();
                cardsDeck.Clear();
                SuffleDecktoMainDeck();
                break;

            case 4:
                CardDeckforShuffle();
                KetteShuffle();
                KetteShuffle();
                ChipSuffle();
                ChipSuffle();
                ChipSuffle();
                KetteShuffle();
                ChipSuffle();
                ChipSuffle();
                cardsDeck.Clear();
                SuffleDecktoMainDeck();
                break;

            default:
                Debug.LogWarning("There is something wrong with Random Rang");
                break;
        }
    }

    private void InitializeDeck()
    {
        List<Card> list = new List<Card>();

        foreach (Card.Suit suit in System.Enum.GetValues(typeof(Card.Suit)))
        {
            foreach (Card.Rank rank in System.Enum.GetValues(typeof(Card.Rank)))
            {
                list.Add(new Card(suit, rank));
            }
        }

        for (int i = 0; i < list.Count; i++)
        {
            cardsDeck.Add(list[i].ToWords());
        }
    }

    void CardDeckforShuffle()
    {
        InitializeDeck();
        for (int i = 0; i < cardsDeck.Count; i++)
        { sufflingDeck.Add(cardsDeck[i]); }
    }

    private void SuffleDecktoMainDeck()
    {
        for (int i = 0; i < sufflingDeck.Count; i++)
        { cardsDeck.Add(sufflingDeck[i]); }
    }

    private void KetteShuffle()
    {
        DivideDeckInToTwo();

        int maxIndex = Mathf.Max(part1.Count, part2.Count);

        for (int i = 0; i < maxIndex; i++)
        {
            if (i < part1.Count)
            { sufflingDeck.Add(part1[i]); }

            if (i < part2.Count)
            { sufflingDeck.Add(part2[i]); }
        }

        part1.Clear();
        part2.Clear();
    }

    private void ChipSuffle()
    {
        //CardDeckforShuffle();

        DivideDeckInToTwo();

        sufflingDeck.AddRange(part2);

        sufflingDeck.AddRange(part1);

        part1.Clear();
        part2.Clear();
    }

    private void DivideDeckInToTwo()
    {
        float dividingFactor = Random.Range(1.73f, 2.6f);
        int cardDivision = (int)(52 / dividingFactor);

        for (int i = 0; i < cardDivision; i++)
        { part1.Add(sufflingDeck[i]); }

        for (int i = cardDivision; i < 52; i++)
        { part2.Add(sufflingDeck[i]); }

        sufflingDeck.Clear();
    }
}
