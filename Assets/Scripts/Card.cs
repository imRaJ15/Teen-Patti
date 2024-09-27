public class Card
{
    public enum Suit { Spades, Hearts, Diamonds, Clubs }
    public enum Rank { Ace = 1, Two = 2, Three = 3, Four = 4, Five = 5,
        Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10, Jack = 11, Queen = 12, King = 13 }

    private Suit cardSuit;
    private Rank cardRank;


    public Card(Suit s, Rank r)
    {
        cardRank = r;
        cardSuit = s;
    }

    public string ToWords()
    {
        return cardRank.ToString() + " of " + cardSuit.ToString();
    }
}
    