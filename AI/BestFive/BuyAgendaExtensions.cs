using AI.Evolution;
using AI.Provincial;
using GameCore;
using GameCore.Cards;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace AI.BestFive
{
    public static class BuyAgendaExtensions
    {
        public static BuyAgenda LoadBestFives(List<Card> k, string folder, ILogger logger = null)
        {
            k = k.OrderBy(c => c.Type).ToList();

            // loading all fives
            var sw = new Stopwatch();
            sw.Start();

            var agendas = new List<Tuple>();
            foreach (var five in new Subsets(k.Where(c => c.Type > CardType.Curse).Select(c => (int)c.Type).ToList(), 5))
            {
                var cards = five.Select(i => Card.Get((CardType)i)).ToList();

                var agenda = BuyAgenda.Load(cards.AddRequiredCards(), folder);
                if (agenda != null)
                    agendas.Add(new Tuple { Agenda = agenda, Wins = 0, Cards = cards });
            }
            //agendas.Add(new Tuple { Agenda = BuyAgenda.Load(k, "kingdomsTens"), Wins = 0, Cards = k });

            sw.Stop();
            logger?.Log($"Loading time: {sw.Elapsed.TotalMilliseconds}ms");

            Console.WriteLine($"Agendas count: {agendas.Count}");

            // tournament

            // first round
            sw.Restart();
            Tournament(k, agendas, 1);
            sw.Stop();
            logger?.Log($"Tournament first round time: {sw.Elapsed.TotalMilliseconds}ms");

            ShowResults(agendas, logger);

            // second round
            sw.Restart();
            agendas = agendas.OrderByDescending(a => a.Wins).Take(100).ToList();
            Tournament(k, agendas, 2);
            sw.Stop();
            logger?.Log($"Tournament second round time: {sw.Elapsed.TotalMilliseconds}ms");

            ShowResults(agendas, logger);

            // third round
            sw.Restart();
            agendas = agendas.OrderByDescending(a => a.Wins).Take(25).ToList();
            Tournament(k, agendas, 20);
            sw.Stop();
            logger?.Log($"Tournament third round time: {sw.Elapsed.TotalMilliseconds}ms");

            ShowResults(agendas, logger);

            // final round
            sw.Restart();
            agendas = agendas.OrderByDescending(a => a.Wins).Take(5).ToList();
            Tournament(k, agendas, 500);
            sw.Stop();
            logger?.Log($"Tournament final round time: {sw.Elapsed.TotalMilliseconds}ms");

            ShowResults(agendas, logger);

            return agendas.OrderByDescending(x => x.Wins).FirstOrDefault().Agenda;
        }

        private static void ShowResults(List<Tuple> agendas, ILogger logger)
        {
            foreach (var item in agendas.OrderBy(x => x.Wins))
                logger?.Log(item.Wins + ": " + item.Cards
                    .OrderBy(i => i.Type)
                    .Select(c => $"{c.Type}({(int)c.Type})")
                    .Aggregate((a, b) => $"{a}, {b}"));
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

                            if (result.Score[0] > result.Score[1])
                            {
                                lock (agendas[i])
                                {
                                    var tuple = agendas[i];
                                    tuple.Wins++;
                                    agendas[i] = tuple;
                                }
                            }
                            else
                            {
                                lock (agendas[j])
                                {
                                    var tuple = agendas[j];
                                    tuple.Wins++;
                                    agendas[j] = tuple;
                                }
                            }
                        });
                    }

                }
            }
        }

        class Tuple
        {
            public BuyAgenda Agenda;
            public int Wins;
            public List<Card> Cards;
        }
    }
}
