using AI.Evolution;
using AI.Provincial;
using GameCore;
using GameCore.Cards;
using System.Collections.Generic;
using System.Linq;
using Utils;
using static System.Console;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>{ -1, 2, 4, 7, 10 };

            foreach (var item in new Subsets(numbers, 2))
                WriteLine(item.Aggregate("", (e, f) => e + " " + f.ToString()));

            //List<Card> cards = PresetGames.Get(Games.SizeDistortion).AddRequiredCards();

            //User getFirst() => new ProvincialAI(BuyAgenda.Load(cards, "kaca"), "Kaca");
            //User getSecond() => new ProvincialAI(BuyAgenda.Load(cards, "honza"), "Honza");

            //Game game = new Game(new User[] { getFirst(), getSecond() }, cards.GetKingdom(2), new MyLogger());
            //var task = game.Play();
            //var results = task.Result;

            ReadLine();
        }

        class MyLogger : ILogger
        {
            public void Log(string str) => WriteLine(str);
        }
    }
}
