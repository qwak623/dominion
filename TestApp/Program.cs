using AI.Evolution;
using AI.Model;
using AI.Provincial;
using GameCore;
using GameCore.Cards;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utils;
using static System.Console;

namespace TestApp
{
    class Program
    {
        static readonly char sep = Path.DirectorySeparatorChar;
        static string directoryPath = $"..{sep}..{sep}..{sep}AI{sep}Provincial{sep}data{sep}kingdoms{sep}";

        static void Main(string[] args)
        {
            List<int> numbers = new List<int>{ -1, 2, 4, 7, 10 };

            foreach (var item in new Subsets(numbers, 2))
                WriteLine(item.Aggregate("", (e, f) => e + " " + f.ToString()));

            List<Card> cards = PresetGames.Get(Games.BigMoney);

            string first = "Tens";
            var firstAgenda = new Tens(directoryPath).LoadBest(cards);
            User getFirst() => new ProvincialAI(firstAgenda, first);
            string second = "Fives";
            var secondAgenda = new Fives(directoryPath).LoadBest(cards);
            User getSecond() => new ProvincialAI(secondAgenda, second);

            Game game = new Game(new User[] { getFirst(), getSecond() }, cards.GetKingdom(2), new MyLogger());
            var task = game.Play();
            var results = task.Result;

            ReadLine();
        }

        class MyLogger : ILogger
        {
            public void Log(string str) => WriteLine(str);
        }
    }
}
