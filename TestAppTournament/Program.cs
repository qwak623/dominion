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
        const int gameCount = 500000;
        //static readonly char sep = Path.DirectorySeparatorChar;
        //static string directoryPath = $"..{sep}..{sep}..{sep}AI{sep}Provincial{sep}data{sep}kingdoms{sep}";
        //static string directoryPath = $"..{sep}..{sep}..{sep}AI{sep}";

        static void Main(string[] args)
        {
            string directoryPath = BuyAgenda.DirectoryPath;

            var tens = new SimpleManager(directoryPath, "Tens_");
            var cards = tens.RandomKingdom();
            var fives = new CachedManager(directoryPath, 5, "Fives_");
            var threes = new CachedManager(directoryPath, 3, "Threes_");

            var agendas = new List<BuyAgendaTournament.Tuple>();
         //   agendas.Add(new BuyAgendaTournament.Tuple { Agenda = tens.LoadBest(cards), Id = "tens" });
         //  agendas.Add(new BuyAgendaTournament.Tuple { Agenda = tens.LoadBest(cards), Id = "tens" });
            agendas.Add(new BuyAgendaTournament.Tuple { Agenda = fives.LoadBest(cards), Id = "fives" });
            agendas.Add(new BuyAgendaTournament.Tuple { Agenda = threes.LoadBest(cards), Id = "threes" });

            var sw = new Stopwatch();
            sw.Start();
            agendas.Tournament(cards, gameCount);
            sw.Stop();
            WriteLine(sw.Elapsed.TotalSeconds);

            agendas.ShowResults(new MyLogger());

            ReadLine();
        }
    }

    class MyLogger : ILogger
    {
        public void Log(string str) => WriteLine(str);
    }
}
