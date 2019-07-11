using AI.Model;
using GameCore.Cards;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace BestCards
{
    class Program
    {
        static void Main(string[] args)
        {
            Tuple[] tuples = new Tuple[33 - 8];
            var rnd = new ThreadSafeRandom();
            char sep = Path.DirectorySeparatorChar;
            string directoryPath = $"..{sep}..{sep}..{sep}AI{sep}Provincial{sep}data{sep}kingdoms{sep}";
            var managerFives = new Fives(directoryPath);
            foreach (var cardType in Enumerable.Range((int)CardType.Adventurer, 25))
                tuples[cardType - 8] = new Tuple { CardType = (CardType)cardType };

            for (int i = 0; i < 500; i++)
            {
                var sw = new Stopwatch();
                sw.Start();

                // random Game
                var cards = new List<Card>();
                cards = Enumerable.Range((int)CardType.Adventurer, 25)
                    .Select(t => ((t, r: rnd.NextDouble())))
                    .OrderBy(a => a.r)
                    .Take(10)
                    .Select(((int type, double) a) => Card.Get((CardType)a.type))
                    .OrderBy(a => a.Price)
                    .ThenBy(a => a.Name)
                    .ToList();

                var bestCards = managerFives.LoadBest(cards).Id.Split('_').Select(c => Card.Get((CardType)int.Parse(c))).ToList();

                foreach (var card in cards)
                    tuples[(int)card.Type - 8].TotalGames++;

                foreach (var card in cards)
                    if (bestCards.Contains(card))
                        tuples[(int)card.Type - 8].Chosen++;

                sw.Stop();
                Console.WriteLine(sw.Elapsed.TotalSeconds);
            }

            foreach (var tuple in tuples.OrderBy(t => t.Chosen / (float)t.TotalGames))
                Console.WriteLine(tuple);

            Console.ReadLine();
        }

        class Tuple
        {
            public CardType CardType;
            public int Chosen;
            public int TotalGames;

            public override string ToString() => $"{CardType}: {Chosen}/{TotalGames}";
        }
    }
}
