using AI.Model;
using AI.Provincial;
using GameCore;
using GameCore.Cards;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Utils;
using static System.Console;

namespace TestApp
{
    class Program
    {
        const int gameCount = 5000;
        static readonly char sep = Path.DirectorySeparatorChar;
        static string directoryPath = $"..{sep}..{sep}..{sep}AI{sep}Provincial{sep}data{sep}kingdoms{sep}";

        static void Main(string[] args)
        {
            var tens = new Tens(directoryPath);
            //var cards = tens.RandomKingdom();

            var cards = PresetGames.Get(Games.FirstGame);

            WriteLine(cards.Where(c => c.Type > CardType.Curse).Select(c => $"{c.Type}({(int)c.Type})").Aggregate((a, b) => $"{a}, {b}"));

            //var sw = new Stopwatch();
            //sw.Start();

            //string first = "Tens";
            //var firstAgenda = new Tens(directoryPath).LoadBest(cards);
            //User getFirst() => new ProvincialAI(firstAgenda, first);

            //string second = "Fives";
            //var secondAgenda = new Fives(directoryPath).LoadBest(cards, new MyLogger());
            //User getSecond() => new ProvincialAI(secondAgenda, second);

            //sw.Stop();
            //WriteLine($"Setting time: {sw.Elapsed.TotalSeconds}s");

            //User getFirst() => new ProvincialAI(BuyAgenda.Load(cards, first), first);
            //User getSecond() => new ProvincialAI(BuyAgenda.Load(cards, second), second);

            var agendas = new List<Tuple>();
            var manager = new Tens(directoryPath, "TensProgress_");
            foreach (var agenda in manager)
                agendas.Add(new Tuple { Agenda = agenda });

            Tournament(cards, agendas, 500);
            ShowResults(agendas, new MyLogger());
            ReadLine();
        }

        private static void ShowResults(List<Tuple> agendas, ILogger logger)
        {
            foreach (var item in agendas.OrderBy(x => x.Wins))
                logger?.Log(item.ToString());
            logger?.Log("");
        }

        private static void Tournament(List<Card> k, List<Tuple> agendas, int games)
        {
            agendas.ForEach(a => a.Wins = 0);

            for (int i = 0; i < agendas.Count; i++)
            {
                for (int j = 0; j < agendas.Count; j++)
                {
                    if (i != j)
                    {
                        Parallel.For(0, games, _ =>
                        //for (int c = 0; c < games; c++)
                        {
                            // first game
                            User[] users = { new ProvincialAI(agendas[i].Agenda), new ProvincialAI(agendas[j].Agenda) };
                            Kingdom kingdom = k.GetKingdom(users.Length);

                            var game = new Game(users, kingdom);
                            var task = game.Play();
                            var result = task.Result;

                            if (result.PlayerIsWinner(0))
                                IncWins(agendas, i);
                            if (result.PlayerIsWinner(1))
                                IncWins(agendas, j);
                        });
                        //}
                    }

                }
            }
        }

        private static void IncWins(List<Tuple> agendas, int i)
        {
            lock (agendas[i])
            {
                var tuple = agendas[i];
                tuple.Wins++;
                agendas[i] = tuple;
            }
        }

        public class Tuple
        {
            public BuyAgenda Agenda;
            public int Wins;

            public override string ToString() => Wins + ": " + Agenda.Id;
        }
    }

    class MyLogger : ILogger
    {
        public void Log(string str) => WriteLine(str);
    }
}
