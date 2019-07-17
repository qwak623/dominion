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
            var tens = new SimpleManager(directoryPath, "Tens_");
            var cards = tens.RandomKingdom();
            var fives = new CachedManager(directoryPath, 5, "Fives_");
            var threes = new CachedManager(directoryPath, 3, "Threes_");

            var agendas = new List<BuyAgendaTournament.Tuple>();
            agendas.Add(new BuyAgendaTournament.Tuple { Agenda = tens.LoadBest(cards), Id = "tens" });
            agendas.Add(new BuyAgendaTournament.Tuple { Agenda = fives.LoadBest(cards), Id = "fives" });
            agendas.Add(new BuyAgendaTournament.Tuple { Agenda = threes.LoadBest(cards), Id = "threes" });

            agendas.Tournament(cards, gameCount);

            agendas.ShowResults(new MyLogger());

            ReadLine();
        }
    }

    class MyLogger : ILogger
    {
        public void Log(string str) => WriteLine(str);
    }
}
