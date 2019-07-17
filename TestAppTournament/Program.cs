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
            int count = 100;

            using (var writer = new StreamWriter(directoryPath + "tournamentTensVsFives.txt"))
            {
                for (int i = 0; i < count; i++)
                {
                    //var fivess = new SimpleManager(directoryPath, "Fivess_");
                    //var cards = fivess.RandomKingdom();
                    //var fives = new SimpleManager(directoryPath, "Fives_");

                    var tens = new SimpleManager(directoryPath, "Tens_");
                    var cards = tens.RandomKingdom();
                    var fives = new CachedManager(directoryPath, 5, "Fives_");
                    //var threes = new CachedManager(directoryPath, 3, "Threes_");

                    //cards = PresetGames.Get(Games.FirstGame);
                    //cards = new int[] { 9, 12, 15, 18, 22, 24, 27, 28, 31, 32 }.Select(c => Card.Get((CardType)c)).ToList();

                    //WriteLine(cards.Where(c => c.Type > CardType.Curse).Select(c => $"{c.Type}({(int)c.Type})").Aggregate((a, b) => $"{a}, {b}"));

                    //WriteLine(fives.LoadBest(cards).Id);

                    var agendas = new List<BuyAgendaTournament.Tuple>();
                    agendas.Add(new BuyAgendaTournament.Tuple { Agenda = tens.LoadBest(cards), Id = "tens" });
                    agendas.Add(new BuyAgendaTournament.Tuple { Agenda = fives.LoadBest(cards), Id = "fives" });
                    //agendas.Add(new BuyAgendaTournament.Tuple { Agenda = threes.LoadBest(cards), Id = "threes" });

                    //var manager = new SimpleManager(directoryPath, "TensProgress_");
                    //foreach (var agenda in manager.Where(a => a.Id == "bigMoney" || a.Id == "9_12_15_18_22_24_27_28_31_32" || a.Id == "badCards"))
                    //    agendas.Add(new BuyAgendaTournament.Tuple { Agenda = agenda });

                    agendas.Tournament(cards, 5000);

                    string line = cards.Select(c => $"{c.Type}({(int)c.Type})").Aggregate((a, b) => $"{a},{b}");
                    line = $"{line} {agendas[0].Wins} {agendas[1].Wins} {agendas[1].Wins - agendas[0].Wins}";
                    WriteLine(line);
                    writer.WriteLine(line);

                    //agendas.ShowResults(new MyLogger());
                }
                writer.Close();
            }

            WriteLine("Tens vs Threes");

            using (var writer = new StreamWriter(directoryPath + "tournamentTensVsThrees.txt"))
            {
                for (int i = 0; i < count; i++)
                {
                    var tens = new SimpleManager(directoryPath, "Tens_");
                    var cards = tens.RandomKingdom();
                    var threes = new CachedManager(directoryPath, 3, "Threes_");

                    //WriteLine(cards.Where(c => c.Type > CardType.Curse).Select(c => $"{c.Type}({(int)c.Type})").Aggregate((a, b) => $"{a}, {b}"));


                    var agendas = new List<BuyAgendaTournament.Tuple>();
                    agendas.Add(new BuyAgendaTournament.Tuple { Agenda = tens.LoadBest(cards), Id = "tens" });
                    //agendas.Add(new BuyAgendaTournament.Tuple { Agenda = fives.LoadBest(cards), Id = "fives" });
                    agendas.Add(new BuyAgendaTournament.Tuple { Agenda = threes.LoadBest(cards), Id = "threes" });


                    agendas.Tournament(cards, 5000);

                    string line = cards.Select(c => $"{c.Type}({(int)c.Type})").Aggregate((a, b) => $"{a},{b}");
                    line = $"{line} {agendas[0].Wins} {agendas[1].Wins} {agendas[1].Wins - agendas[0].Wins}";
                    WriteLine(line);
                    writer.WriteLine(line);

                }
                writer.Close();
            }

            WriteLine("Threes vs Fives");

            using (var writer = new StreamWriter(directoryPath + "tournamentThreesVsFives.txt"))
            {
                for (int i = 0; i < count; i++)
                {
                    //var tens = new SimpleManager(directoryPath, "Tens_");
                    //var cards = tens.RandomKingdom();
                    var cards = Enumerable.Range((int)CardType.Adventurer, 25)
                        .Select(t => ((t, r: new ThreadSafeRandom().NextDouble())))
                        .OrderBy(a => a.r)
                        .Take(10)
                        .Select(((int type, double) a) => Card.Get((CardType)a.type))
                        .OrderBy(a => a.Price)
                        .ThenBy(a => a.Name)
                        .ToList();

                    var fives = new CachedManager(directoryPath, 5, "Fives_");
                    var threes = new CachedManager(directoryPath, 3, "Threes_");

                    //WriteLine(cards.Where(c => c.Type > CardType.Curse).Select(c => $"{c.Type}({(int)c.Type})").Aggregate((a, b) => $"{a}, {b}"));


                    var agendas = new List<BuyAgendaTournament.Tuple>();
                    agendas.Add(new BuyAgendaTournament.Tuple { Agenda = threes.LoadBest(cards), Id = "threes" });
                    //agendas.Add(new BuyAgendaTournament.Tuple { Agenda = fives.LoadBest(cards), Id = "fives" });
                    agendas.Add(new BuyAgendaTournament.Tuple { Agenda = fives.LoadBest(cards), Id = "fives" });


                    agendas.Tournament(cards, 5000);

                    string line = cards.Select(c => $"{c.Type}({(int)c.Type})").Aggregate((a, b) => $"{a},{b}");
                    line = $"{line} {agendas[0].Wins} {agendas[1].Wins} {agendas[1].Wins - agendas[0].Wins}";
                    WriteLine(line);
                    writer.WriteLine(line);

                }
                writer.Close();
            }

            ReadLine();
        }
    }

    class MyLogger : ILogger
    {
        public void Log(string str) => WriteLine(str);
    }
}
