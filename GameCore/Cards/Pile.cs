using System.Collections.Generic;

namespace GameCore.Cards
{
    public class Pile
    {
        Stack<Card> cards;
        Card top;

        public int Count => cards.Count;
        public bool Empty => cards.Count == 0;
        public CardType Type => top.Type;
        public string Name => top.Name;
        public int Price => top.Price;
        public Card Card => cards.Count > 0 ? cards.Peek() : null;

        public Card GainCard()
        {
            top = cards.Pop();
            return top;
        }

        public Pile(Card card, int count = 1)
        {
            cards = new Stack<Card>();
            for (int i = 0; i < count; i++)
                cards.Push(card.Get());
            top = cards.Peek();
        }

        public override string ToString() => $"{Name} ${Price} ({Count})";
    }
}
