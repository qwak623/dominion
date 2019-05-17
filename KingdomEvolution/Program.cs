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
            //           var rnd = new ThreadSafeRandom();

            //           var cards = PresetGames.Get(Games.SizeDistortion).AddRequiredCards().ToList();
            //           var honza = BuyAgenda.Load(cards, "honza");

            ////           var evolution = new Evolution(new Params { Kingdom = cards }, honza, new Logger()); 
            //           var evolution = new Evolution(new Params { Kingdom = cards }); 
            //           evolution.Run();

            var rnd = new ThreadSafeRandom();

            for (int i = 0; i < 100; i++)
            {
                List<Card> cards = null;
                try
                {
                    // get random 10 cards
                    cards = Enumerable.Range((int)CardType.Adventurer, 25)
                        .Select(t => ((t, r: rnd.NextDouble())))
                        .OrderBy(a => a.r)
                        .Take(10)
                        .Select(((int type, double) a) => Card.Get((CardType)a.type))
                        .ToList()
                        .AddRequiredCards();

                    var kingdomName = cards.OrderBy(p => p.Type).Select(p => (int)p.Type).Aggregate("kingdom", (a, b) => a + " " + b);
                    WriteLine($"kingdom {i}: {kingdomName}");
                    var evolution = new Evolution(new Params { Kingdom = cards }, new Logger());
                    evolution.Run();

                }
                catch (Exception e)
                {
                    WriteLine(e.Message);
                }
            }
        }

        class Logger : ILogger
        {
            public void Log(string str) => WriteLine(str);
        }
    }
}
