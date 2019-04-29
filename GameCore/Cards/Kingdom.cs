using GameCore.Cards.Base;
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

        public Kingdom (List<Pile> piles, bool two)
        {
            this.piles = piles;
            for (int i = 0; i < piles.Count; i++)
                cardTypeToIndex.Add(piles[i].Card.Type, i);
            Reset(two);
        }

        public KingdomWrapper GetWrapper (int price, bool onlyTreasures = false)
        {
            var w = new KingdomWrapper();
            w.kingdom = this;
            w.price = price;
            w.onlyTreasures = onlyTreasures;
            return w;
        }

        public Pile GetPile(CardType type) => piles[cardTypeToIndex[type]];

        public Pile this[int index] => piles[index];

        public int Count => piles.Count;

        public IEnumerator<Pile> GetEnumerator()
        {
            foreach (var pile in piles)
                yield return pile;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void Reset(bool two)
        {
            for (int i = 0; i < piles.Count; i++)
            {// todo dodelat kolonie a platinu
                // replaces all piles with standart size pile for game start.
                var card = piles[i].Card;
                int count = 10;
                if (card.IsVictory)
                    count = two ? 8 : 12;
                if (card.Type == CardType.Copper)
                    count = 60;
                if (card.Type == CardType.Silver)
                    count = 40;
                if (card.Type == CardType.Gold)
                    count = 30;
                piles[i] = new Pile(card.Get(), count);
            }
        }
    }
}
