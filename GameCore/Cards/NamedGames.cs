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
        public static class Base
        {
            public static List<Card> AllCards1stEdition()
            {
                return new List<Card>
                {
                    Bureaucrat.Get(),
                    Cellar.Get(),
                    CouncilRoom.Get(),
                    Feast.Get(),
                    Festival.Get(),
                    Gardens.Get(),
                    Chancellor.Get(),
                    Chapel.Get(),
                    Laboratory.Get(),
                    Library.Get(),
                    Market.Get(),
                    Militia.Get(),
                    Mine.Get(),
                    Moat.Get(),
                    Moneylender.Get(),
                    Remodel.Get(),
                    Smithy.Get(),
                    Spy.Get(),
                    Thief.Get(),
                    ThroneRoom.Get(),
                    Village.Get(),
                    Witch.Get(),
                    Woodcutter.Get(),
                    Workshop.Get(),
                };
            }

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
