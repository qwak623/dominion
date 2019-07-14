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
            //var cards = tens.RandomKingdom();

            var cards = PresetGames.Get(Games.FirstGame);
            cards = new int[] { 9, 12, 15, 18, 22, 24, 27, 28, 31, 32 }.Select(c => Card.Get((CardType)c)).ToList();

            WriteLine(cards.Where(c => c.Type > CardType.Curse).Select(c => $"{c.Type}({(int)c.Type})").Aggregate((a, b) => $"{a}, {b}"));

            var agendas = new List<BuyAgendaTournament.Tuple>();
            var manager = new SimpleManager(directoryPath, "TensProgress_");
            foreach (var agenda in manager.Where(a => a.Id == "bigMoney" || a.Id == "9_12_15_18_22_24_27_28_31_32" || a.Id == "badCards"))
                agendas.Add(new BuyAgendaTournament.Tuple { Agenda = agenda });

            agendas.Tournament(cards, 500);
            agendas.ShowResults(new MyLogger());
            ReadLine();
        }
    }

    class MyLogger : ILogger
    {
        public void Log(string str) => WriteLine(str);
    }
}
