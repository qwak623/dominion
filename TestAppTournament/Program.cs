using AI.Evolution;
using AI.Provincial;
using GameCore;
using GameCore.Cards;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Utils;
using static System.Console;

namespace TestApp
{
    class Program
    {
        const int gameCount = 5000;
        static void Main(string[] args)
        {
            List<Card> cards = null;
            //cards = PresetGames.Get(Games.VillageSquare).AddRequiredCards();

            DirectoryInfo dir = new DirectoryInfo("..\\..\\..\\AI\\Provincial\\data\\kingdomsTens");
            FileInfo[] files = dir.GetFiles("kingdom_*.txt");

            var rnd = new ThreadSafeRandom();
            int fileIndex = rnd.Next(files.Length);

            try
            {
                    cards = files[fileIndex].Name
                    .Remove(files[fileIndex].Name.Length - 4)
                    .Substring(8).Split('_')
                    .Select(a => int.Parse(a))
                    .Where(a => a > 7)
                    .Select(a => Card.Get((CardType)a))
                    .AddRequiredCards();
            }
            catch (Exception)
            {
            }

            WriteLine(cards.Where(c => c.Type > CardType.Curse).Select(c => $"{c.Type}({(int)c.Type})").Aggregate((a, b) => $"{a}, {b}"));

            var sw = new Stopwatch();
            sw.Start();

            string first = "Tens";
            var firstAgenda = BuyAgenda.Load(cards, "kingdomsTens");
            User getFirst() => new ProvincialAI(firstAgenda, first);
            string second = "Fives";
            var secondAgenda = AI.BestFive.BuyAgendaExtensions.LoadBestFives(cards, "kingdomsFives");
            User getSecond() => new ProvincialAI(secondAgenda, second);

            sw.Stop();
            WriteLine($"Setting time: {sw.Elapsed.TotalSeconds}s");


            //User getFirst() => new ProvincialAI(BuyAgenda.Load(cards, first), first);
            //User getSecond() => new ProvincialAI(BuyAgenda.Load(cards, second), second);

            int[] result = new int[2];
            for (int i = 0; i < gameCount; i++)
            {
                Game game = new Game(new User[] { getFirst(), getSecond() }, cards.GetKingdom(2));
                var task = game.Play();
                var results = task.Result;
                if (results.Score[0] > results.Score[1])
                    result[0]++;
                if (results.Score[0] < results.Score[1])
                    result[1]++;
            }

            for (int i = 0; i < gameCount; i++)
            {
                Game game = new Game(new User[] { getSecond(), getFirst() }, cards.GetKingdom(2));
                var task = game.Play();
                var results = task.Result;
                if (results.Score[0] > results.Score[1])
                    result[1]++;
                if (results.Score[0] < results.Score[1])
                    result[0]++;
            }

            WriteLine();

            WriteLine($"{first} agenda:");
            WriteLine(firstAgenda.Colonies);
            WriteLine(firstAgenda.Provinces);
            WriteLine(firstAgenda.Duchies);
            WriteLine(firstAgenda.Estates);
            firstAgenda.BuyMenu.ForEach(item => WriteLine($"{item.Card.ToString()} {item.Number}"));

            WriteLine();

            WriteLine($"{second} agenda:");
            WriteLine(secondAgenda.Colonies);
            WriteLine(secondAgenda.Provinces);
            WriteLine(secondAgenda.Duchies);
            WriteLine(secondAgenda.Estates);
            secondAgenda.BuyMenu.ForEach(item => WriteLine($"{item.Card.ToString()} {item.Number}"));

            WriteLine();

            WriteLine($"{first}: {result[0]}");
            WriteLine($"{second}: {result[1]}");
            ReadLine();
        }
    }
}
