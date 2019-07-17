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

namespace PseudoTournament
{
    class Program
    {
        const int gameCount = 5000;
        static readonly char sep = Path.DirectorySeparatorChar;
        static string directoryPath = $"..{sep}..{sep}..{sep}AI{sep}Provincial{sep}data{sep}kingdoms{sep}";

        static void Main(string[] args)
        {
            int count = 100;
            var tens = new SimpleManager(directoryPath, "Tens_");
            var fives = new CachedManager(directoryPath, 5, "Fives_");
            var threes = new CachedManager(directoryPath, 3, "Threes_");

            using (var writer = new StreamWriter(directoryPath + "pseudoTensVsFives.txt"))
            {
                int subsetSize = 5;
                for (int i = 0; i < count; i++)
                {
                    var cards = tens.RandomKingdom();
                    double subsetPower = 252;
                    int wins = 0;
                    
                    foreach (var five in new Subsets(cards.Where(c => c.Type > CardType.Curse).Select(c => (int)c.Type).ToList(), subsetSize))
                    {
                        var sCards = five.Select(x => Card.Get((CardType)x)).ToList();

                        var agendas = new List<BuyAgendaTournament.Tuple>();
                        agendas.Add(new BuyAgendaTournament.Tuple { Agenda = tens.LoadBest(cards), Id = "tens" });
                        var agenda = fives.Load(sCards);
                        if (agenda != null)
                            agendas.Add(new BuyAgendaTournament.Tuple { Agenda = agenda, Wins = 0, Id = sCards.ToId() });

                        agendas.Tournament(cards, 100);
                        if (agendas[1].Wins > agendas[0].Wins)
                            wins++;
                    }

                    string line = cards.Select(c => $"{c.Type}({(int)c.Type})").Aggregate((a, b) => $"{a},{b}");
                    line = $"{line} {wins} {subsetPower} {wins/subsetPower}";
                    WriteLine(line);
                    writer.WriteLine(line);

                    //agendas.ShowResults(new MyLogger());
                }
                writer.Close();
            }

            WriteLine("Tens vs Threes");

            using (var writer = new StreamWriter(directoryPath + "pseudoTensVsThrees.txt"))
            {
                int subsetSize = 3;
                for (int i = 0; i < count; i++)
                {
                    var cards = tens.RandomKingdom();
                    double subsetPower = 120;
                    int wins = 0;

                    foreach (var three in new Subsets(cards.Where(c => c.Type > CardType.Curse).Select(c => (int)c.Type).ToList(), subsetSize))
                    {
                        var sCards = three.Select(x => Card.Get((CardType)x)).ToList();

                        var agendas = new List<BuyAgendaTournament.Tuple>();
                        agendas.Add(new BuyAgendaTournament.Tuple { Agenda = tens.LoadBest(cards), Id = "tens" });
                        var agenda = threes.Load(sCards);
                        if (agenda != null)
                            agendas.Add(new BuyAgendaTournament.Tuple { Agenda = agenda, Wins = 0, Id = sCards.ToId() });

                        agendas.Tournament(cards, 100);
                        if (agendas[1].Wins > agendas[0].Wins)
                            wins++;
                    }

                    string line = cards.Select(c => $"{c.Type}({(int)c.Type})").Aggregate((a, b) => $"{a},{b}");
                    line = $"{line} {wins} {subsetPower} {wins / subsetPower}";
                    WriteLine(line);
                    writer.WriteLine(line);

                    //agendas.ShowResults(new MyLogger());
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
