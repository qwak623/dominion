using GameCore.Cards.Base;
using GameCore.Cards.GeneralCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Cards
{
    public static class NamedGames
    {
        public static List<Card> TheFirstGame()
        {
            return new List<Card>
            {
                Cellar.Get(),
                Moat.Get(),
                Village.Get(),
                Woodcutter.Get(),
                Workshop.Get(),
                Militia.Get(),
                Remodel.Get(),
                Smithy.Get(),
                Market.Get(),
                Mine.Get(),
            }.Concat(VictoryAndTreasures()).ToList();
        }

        static List<Card> VictoryAndTreasures()
        {
            return new List<Card>
            {
                Copper.Get(),
                Silver.Get(),
                Gold.Get(),
                Estate.Get(),
                Duchy.Get(),
                Province.Get()
            };
        }
    }
}
