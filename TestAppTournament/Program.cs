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
            var array = new List<int[]>();
            for (int i = 0; i < 33; i++)
            {
                array.Add(new int[3]);
                array[i][2] = i;
            }

            var threes43 = new CachedManager(directoryPath, 3, "Threes43_");

            for (int i = 0; i < 100; i++)
            {
                var Cards = Enumerable.Range((int)CardType.Adventurer, 25)
                .Select(t => ((t, r: new ThreadSafeRandom().NextDouble())))
                .OrderBy(a => a.r)
                .Take(10)
                .Select(((int type, double) a) => Card.Get((CardType)a.type))
                .OrderBy(a => a.Price)
                .ThenBy(a => a.Name)
                .ToList();

                foreach (var c in threes43.LoadBest(Cards).Id.ToCardList())
                    array[(int)c.Type][0]++;

                foreach (var c in Cards)
                    array[(int)c.Type][1]++;

            }

            foreach (var c in array.OrderBy(a => (double)a[0] / a[1]))
                WriteLine($"{(CardType)c[2]} {c[0]}/{c[1]}");

            ReadLine();
            return;


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
