﻿using System.Collections.Generic;

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
        public Card Card => cards.Count > 0 ? top : null;

        public Card GainCard()
        {
            if (Empty)
                return null;

            top = cards.Pop();
            return top;
        }

        public Pile(Card card, int count = 1)
        {
            cards = new Stack<Card>();
            for (int i = 0; i < count; i++)
                cards.Push(card);
            top = cards.Peek();
        }

        public override string ToString() => $"{Name} ${Price} ({Count})";
    }
}
