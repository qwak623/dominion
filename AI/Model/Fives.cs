using AI.Provincial;
using GameCore;
using GameCore.Cards;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Utils;

namespace AI.Model
{
    public class Fives : BuyAgendaManager
    {
        string directoryPath;
        string prefix;
        Dictionary<string, string>[] files = new Dictionary<string, string>[33];
        object[] locks = new object[33];

        public Fives(string directoryPath, string prefix = "Fives_")
        {
            this.directoryPath = directoryPath;
            this.prefix = prefix;
            for (int i = 0; i < locks.Length; i++)
                locks[i] = new object();
        }

        /// <summary>
        /// This method does saves other kingdoms.
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public override BuyAgenda Load(IEnumerable<int> cards)
        {
            var id = cards.OrderBy(p => p).Select(p => p.ToString()).Aggregate((a, b) => a + "_" + b);
            int i = cards.OrderBy(p => p).First();

            try
            {
                if (files[i] == null)
                    LoadAllAgendas(i);

                lock (locks[i])
                    return BuyAgenda.FromString(files[i][id]);
            }
            catch
            {
                return null;
            }
        }

        void LoadAllAgendas(int i)
        {
            files[i] = new Dictionary<string, string>();

            lock (locks[i])
            {
                using (var reader = new StreamReader($"{directoryPath}{prefix}{i}.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (!string.IsNullOrEmpty(line))
                            files[i].Add(line.Split(':')[0], line);
                    }
                }
            }
        }

        public override void Save(IEnumerable<int> cards, BuyAgenda agenda)
        {
            var id = cards.OrderBy(p => p).Select(p => p.ToString()).Aggregate((a, b) => a + "_" + b);
            int i = cards.OrderBy(p => p).First();

            if (Load(cards) == null)
                lock (locks[i])
                    File.AppendAllText($"{directoryPath}{prefix}{i}.txt", agenda.ToString(id) + Environment.NewLine);
            else
            {
                var dict = new Dictionary<string, string>();
                lock (locks[(int)cards.First()])
                {
                    files[i] = null;
                    foreach (var line in File.ReadAllLines($"{directoryPath}{prefix}{i}.txt").Select(l => l.Split(':')))
                        dict.Add(line[0], $"{line[0]}:{line[1]}");
                    dict[id] = agenda.ToString(id);
                    using (var writer = new StreamWriter($"{directoryPath}{prefix}{i}.txt"))
                        foreach (var a in dict.OrderBy(d => d.Key))
                            writer.WriteLine(a.Value);
                }
            }
        }

        public override BuyAgenda LoadBest(List<Card> k, ILogger logger = null)
        {
            k = k.OrderBy(c => c.Type).ToList();

            // loading all fives
            var sw = new Stopwatch();
            sw.Start();

            var agendas = new List<Tuple>();
            foreach (var five in new Subsets(k.Where(c => c.Type > CardType.Curse).Select(c => (int)c.Type).ToList(), 5))
            {
                var cards = five.Select(i => Card.Get((CardType)i)).ToList();

                var agenda = Load(cards);
                if (agenda != null)
                    agendas.Add(new Tuple { Agenda = agenda, Wins = 0, Cards = cards });
            }

            //agendas.Add(new Tuple { Agenda = new Tens(directoryPath).Load(k), Wins = 0, Cards = k });

            sw.Stop();
            logger?.Log($"Loading time: {sw.Elapsed.TotalMilliseconds}ms");
            logger?.Log($"Agendas count: {agendas.Count}");

            // tournament
            var agendasNextRound = new List<Tuple>();

            // first round
            sw.Restart();
            var group = agendas.Take(16).ToList();
            agendas = agendas.Skip(16).ToList();
            int groupId = 0;
            while (group.Any())
            {
                Tournament(k, group, 5);
                logger?.Log($"group {groupId++}");
                ShowResults(group, logger);

                agendasNextRound.AddRange(group.OrderByDescending(a => a.Wins).Take(4));
                group = agendas.Take(16).ToList();
                agendas = agendas.Skip(16).ToList();
            }

            sw.Stop();
            logger?.Log($"Tournament first round time: {sw.Elapsed.TotalMilliseconds}ms");

            // second round
            sw.Restart();
            group = agendasNextRound.Take(16).ToList();
            agendasNextRound = agendasNextRound.Skip(16).ToList();
            groupId = 0;
            while (group.Any())
            {
                Tournament(k, group, 5);
                logger?.Log($"group {groupId++}");
                ShowResults(group, logger);

                agendas.AddRange(group.OrderByDescending(a => a.Wins).Take(4));
                group = agendasNextRound.Take(16).ToList();
                agendasNextRound = agendasNextRound.Skip(16).ToList();
            }

            sw.Stop();
            logger?.Log($"Tournament second round time: {sw.Elapsed.TotalMilliseconds}ms");

            // third round
            sw.Restart();
            Tournament(k, agendas, 5);
            sw.Stop();
            logger?.Log($"Tournament third round time: {sw.Elapsed.TotalMilliseconds}ms");

            ShowResults(agendas, logger);

            // final round
            sw.Restart();
            agendas = agendas.OrderByDescending(a => a.Wins).Take(5).ToList();
            Tournament(k, agendas, 100);
            sw.Stop();
            logger?.Log($"Tournament final round time: {sw.Elapsed.TotalMilliseconds}ms");

            ShowResults(agendas, logger);

            return agendas.OrderByDescending(x => x.Wins).FirstOrDefault().Agenda;
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

        public override IEnumerator<BuyAgenda> GetEnumerator()
        {
            for (int i = 8; i < 32; i++)
            {
                lock (locks[i])
                {
                    try
                    {
                        if (files[i] == null)
                            LoadAllAgendas(i);
                    }
                    catch
                    {
                        continue;
                    }

                    foreach (var item in files[i])
                        yield return BuyAgenda.FromString(item.Value);
                }
            }

            // free memory
            files = new Dictionary<string, string>[33];
        }

        public class Tuple
        {
            public BuyAgenda Agenda;
            public int Wins;
            public List<Card> Cards;

            public override string ToString()
            {
                return Wins + ": " + Cards
                    .OrderBy(i => i.Type)
                    .Select(c => $"{c.Type}({(int)c.Type})")
                    .Aggregate((a, b) => $"{a}, {b}");
            }
        }
    }
}
