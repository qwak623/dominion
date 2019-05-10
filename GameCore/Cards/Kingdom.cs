﻿using GameCore.Cards.Base;
using GameCore.Cards.GeneralCards;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Cards
{
    public class Kingdom : IEnumerable<Pile>
    {
        public int EmptyPiles;

        List<Pile> piles;
        Dictionary<CardType, int> cardTypeToIndex = new Dictionary<CardType, int>();

        public Kingdom (List<Pile> piles, int players)
        {
            this.piles = piles;
            for (int i = 0; i < piles.Count; i++)
                cardTypeToIndex.Add(piles[i].Card.Type, i); 
            Reset(players);
        }

        public KingdomWrapper GetWrapper (int price, bool onlyTreasures = false)
        {
            var w = new KingdomWrapper();
            w.kingdom = this;
            w.price = price;
            w.onlyTreasures = onlyTreasures;
            return w;
        }

        public Pile GetPile(CardType type)
        {
            // TODO nevim jesli je tohle uplne efektivni, mozna by bylo lepsi nechat padat a zaridit aby nebyl nikdy volan se spatnymi parametry
            // nebo pri vytvareni tam dat i vsechny ostatni karty
            if (cardTypeToIndex.TryGetValue(type, out int index)) 
                return piles[index];
            return null;
        }

        public Pile this[int index] => piles[index];

        public int Count => piles.Count;

        public IEnumerator<Pile> GetEnumerator()
        {
            foreach (var pile in piles)
                yield return pile;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void Reset(int players)
        {
            for (int i = 0; i < piles.Count; i++)
            {// todo dodelat kolonie a platinu
                // replaces all piles with standart size pile for game start.
                var card = piles[i].Card;
                int count = 10;
                if (card.Type == CardType.Curse)
                    count = (players - 1) * 10;
                else if (card.IsVictory)
                    count = players == 2 ? 8 : 12;
                else if (card.Type == CardType.Copper)
                    count = 60;
                else if (card.Type == CardType.Silver)
                    count = 40;
                else if (card.Type == CardType.Gold)
                    count = 30;
                piles[i] = new Pile(card.Get(), count);
            }
        }
    }
}
