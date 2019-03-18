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
        List<Pile> piles;

        public Kingdom (List<Pile> piles)
        {
            this.piles = piles;
        }

        public static Kingdom TheFirstGame()
        {
            return new Kingdom(new List<Pile>
            {
                new Pile(Cellar.Get()),
                new Pile(Moat.Get()),
                new Pile(Village.Get()),
                new Pile(Woodcutter.Get()),
                new Pile(Workshop.Get()),
                new Pile(Militia.Get()),
                new Pile(Remodel.Get()),
                new Pile(Smithy.Get()),
                new Pile(Market.Get()),
                new Pile(Mine.Get()),
            }.Concat(VictoryAndTreasures()).ToList());
        }

        private static List<Pile> VictoryAndTreasures()
        {
            return new List<Pile>
            {
                new Pile(Copper.Get()),
                new Pile(Silver.Get()),
                new Pile(Gold.Get()),
                new Pile(Estate.Get()),
                new Pile(Duchy.Get()),
                new Pile(Province.Get())
            };
        }

        public void Reset(bool two)
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

        public Pile this[int index] => piles[index];

        public IEnumerator<Pile> GetEnumerator()
        {
            foreach (var pile in piles)
                yield return pile;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
