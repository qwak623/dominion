using AI.Evolution;
using AI.Provincial;
using GameCore;
using GameCore.Cards;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Utils;
using static System.Console;

namespace Eva
{
    class Program
    {
        // params
        // -f -s 5 -t 4

        static void Main(string[] args)
        {
            // params
            EvolutionType et = EvolutionType.Tens;
            string folder = "kingdomsTens";
            int startIndex = 0, count = 1, parallelDegreeExt = -1, parallelDegreeInt = -1;
            bool skip = true;

            var rnd = new ThreadSafeRandom();
            char sep = Path.DirectorySeparatorChar;
            string path = $"..{sep}..{sep}..{sep}AI{sep}Provincial{sep}data{sep}kingdomsFives{sep}fives.txt";
            IEnumerator<string> kingdoms = null;

            // params handling
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i][0] != '-')
                    continue;
                // todo ten try moc nefunguje tento radek pada na index outof rangde nebo null poitner
                for (int j = 1; j < args[i].Length; j++)
                {
                    try
                    {
                        switch (args[i][j])
                        {
                            case 'd':
                                et = EvolutionType.Tens;
                                break;
                            case 'f':
                                et = EvolutionType.Fives;
                                folder = "kingdomsFives";
                                break;
                            case 'c':
                                count = int.Parse(args[++i]);
                                break;
                            case 's':
                                startIndex = int.Parse(args[++i]);
                                break;
                            case 't':
                                parallelDegreeExt = 1;
                                parallelDegreeInt = int.Parse(args[++i]);
                                break;
                            case 'n':
                                et = EvolutionType.NamedGames;
                                break;
                            case 'r':
                                skip = false;
                                break;
                            default:
                                break;
                        }
                    }
                    catch
                    {
                        WriteLine($"Parameter {i} failed.");
                    }
                }
            }

            WriteLine($"evolution: {et.ToString()}");

            if (et == EvolutionType.Fives)
            {
                kingdoms = File.ReadAllLines(path).Skip(startIndex).Take(count).GetEnumerator();
                WriteLine($"count: {count}");
                WriteLine($"start index: {startIndex}");
            }
            else
            {
                WriteLine($"kingdom: random");
            }

            for (int i = 0; i < count; i++)
            {
                try
                {
                    switch (et)
                    {
                        case EvolutionType.Tens:
                            {
                                //cards = PresetGames.Get(Games.FirstGame).AddRequiredCards();
                                List<Card> cards = null;
                                
                                // get random 10 cards
                                cards = Enumerable.Range((int)CardType.Adventurer, 25)
                                    .Select(t => ((t, r: rnd.NextDouble())))
                                    .OrderBy(a => a.r)
                                    .Take(10)
                                    .Select(((int type, double) a) => Card.Get((CardType)a.type))
                                    .AddRequiredCards();

                                var kingdomName = cards.OrderBy(p => p.Type).Select(p => (int)p.Type).Aggregate("kingdom", (a, b) => a + " " + b);
                                WriteLine($"kingdom {i}: {kingdomName}");
                                var evolution = new Evolution(new Params
                                {
                                    Kingdom = cards,
                                    Evaluator = new ProvincialEvaluator(),
                                    Folder = folder,
                                    Skip = skip,
                                    ParallelDegreeExt = parallelDegreeExt,
                                    ParallelDegreeInt = parallelDegreeInt,
                                    LeaderCount = 10,
                                    PoolCount = 50,
                                    Generations = 50,
                                }, new Logger());
                                evolution.Run();
                            }
                            break;
                        case EvolutionType.Fives:
                            {
                                List<Card> cards = null;
                                kingdoms.MoveNext();

                                cards = kingdoms.Current.Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries)
                                    .Select(a => Card.Get((CardType)int.Parse(a)))
                                    .AddRequiredCards();


                                var kingdomName = cards.OrderBy(p => p.Type).Select(p => (int)p.Type).Aggregate("kingdom", (a, b) => a + " " + b);
                                WriteLine($"kingdom {i}: {kingdomName}");
                                var evolution = new Evolution(new Params
                                {
                                    Kingdom = cards,
                                    Evaluator = new ProvincialEvaluator(),
                                    Folder = folder,
                                    Skip = skip,
                                    ParallelDegreeExt = parallelDegreeExt,
                                    ParallelDegreeInt = parallelDegreeInt,
                                    LeaderCount = 10,
                                    PoolCount = 50,
                                    Generations = 50,
                                }, new Logger());
                                evolution.Run();
                            }
                            break;
                        case EvolutionType.NamedGames:
                            {
                                //cards = PresetGames.Get(Games.FirstGame).AddRequiredCards();
                                List<List<Card>> games = new List<List<Card>>
                                {
                                    PresetGames.Get(Games.BigMoney),
                                    PresetGames.Get(Games.Interaction),
                                    PresetGames.Get(Games.FirstGame),
                                    PresetGames.Get(Games.SizeDistortion),
                                    PresetGames.Get(Games.ThrashHeap),
                                    PresetGames.Get(Games.VillageSquare),
                                };

                                games.ForEach(cards =>
                                {
                                    var kingdomName = cards.OrderBy(p => p.Type).Select(p => (int)p.Type).Aggregate("kingdom", (a, b) => a + " " + b);
                                    WriteLine($"kingdom {i}: {kingdomName}");
                                    var evolution = new Evolution(new Params
                                    {
                                        Kingdom = cards.AddRequiredCards(),
                                        Evaluator = new ProvincialEvaluator(),
                                        Folder = folder,
                                        Skip = skip,
                                        LeaderCount = 10,
                                        PoolCount = 50,
                                        Generations = 50,
                                    }, new Logger());
                                    evolution.Run();
                                });
                            }
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception e)
                {
                    WriteLine(e.Message);
                }
            }
            ReadLine();
        }

        enum EvolutionType { Tens, Fives, NamedGames }

        class Logger : ILogger
        {
            public void Log(string str) => WriteLine(str);
        }
    }
}
