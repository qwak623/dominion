using System.Collections.Generic;

namespace GameCore
{
    public class Pile
    {
        Stack<Card> cards;

        public int Count => cards.Count;
        public int CardId => cards.Peek() != null ? cards.Peek().Id : -1; 
        public string CardName => cards.Peek() != null ? cards.Peek().Name : "Pile is empty";
        public int CardPrice => cards.Peek() != null ? cards.Peek().Price : 0;
        public Card Card => cards.Peek();
        public Card GainCard() => cards.Pop();

        public Pile(Card card, int count)
        {
            cards = new Stack<Card>();
            for (int i = 0; i < count; i++)
                cards.Push(card.Get());
        }

        public override string ToString()
        {
            return $"{CardId} {CardName} ${CardPrice}";
        }
    }
}
