using AI.Provincial.Evolution;
using GameCore;
using GameCore.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Eva
{
    class Program
    {
        static void Main(string[] args)
        {
            var rnd = new ThreadSafeRandom();

            var cards = PresetGames.Get(Games.SizeDistortion).AddRequiredCards().ToList();
            var honza = BuyAgenda.Load(cards, "honza");

            var evolution = new Evolution(new Params { Kingdom = cards }, honza, new Logger()); // todo neco s timto
            evolution.Run();


            //var rnd = new ThreadSafeRandom();

            //for (int i = 0; i < 50; i++)
            //{
            //    List<Card> cards = null;
            //    try
            //    {
            //        // get random 10 cards
            //        cards = Enumerable.Range((int)CardType.Adventurer, 25)
            //            .Select(t => ((t, r: rnd.NextDouble())))
            //            .OrderBy(a => a.r)
            //            .Take(10)
            //            .Select(((int type, double) a) => Card.Get((CardType)a.type))
            //            .ToList();

            //        // concat with general victories and treasures
            //        cards = cards.Concat(PresetGames.VictoryAndTreasures())
            //            .Concat(cards.Select(c => c.RequiredCards).Where(c => c != null).Distinct())
            //            .ToList();

            //        var evolution = new AI.Provincial.Evolution.Evolution(new Params { Kingdom = cards }); // todo neco s timto
            //        evolution.Run();

            //        var kingdomName = cards.OrderBy(p => p.Type).Select(p => (int)p.Type).Aggregate("kingdom ", (a, b) => a + " " + b);
            //        WriteLine(kingdomName);
            //    }
            //    catch (Exception e)
            //    {
            //        var kingdomName = cards?.OrderBy(p => p.Type).Select(p => (int)p.Type).Aggregate("kingdom ", (a, b) => a + " " + b);
            //        WriteLine(kingdomName);
            //        WriteLine(e.Message);
            //        WriteLine(e.StackTrace);
            //    }
            //}


        }

        class Logger : ILogger
        {
            public void Log(string str) => WriteLine(str);
        }
    }
}
