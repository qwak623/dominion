using GameCore.Cards.Base;
using GameCore.Cards.GeneralCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Cards
{
    public static class Kingdom
    {
        public static List<Pile> TheFirstGame(bool two)
        {
            return new List<Pile>
            {
                new Pile(Cellar.Get(), 10),
                new Pile(Moat.Get(), 10),
                new Pile(Village.Get(), 10),
                new Pile(Woodcutter.Get(), 10),
                new Pile(Workshop.Get(), 10),
                new Pile(Militia.Get(), 10),
                new Pile(Remodel.Get(), 10),
                new Pile(Smithy.Get(), 10),
                new Pile(Market.Get(), 10),
                new Pile(Mine.Get(), 10),
                new Pile(Laboratory.Get(), 10),
            }.Concat(VictoryAndTreasures(two)).ToList();
        }

        private static List<Pile> VictoryAndTreasures(bool two)
        {
            return new List<Pile>
            {
                new Pile(Copper.Get(), 60),
                new Pile(Silver.Get(), 40),
                new Pile(Gold.Get(), 30),
                new Pile(Estate.Get(), two ? 8 : 12),
                new Pile(Duchy.Get(), two ? 8 : 12),
                new Pile(Province.Get(), two ? 8 : 12)
            };
        }
    }
}
