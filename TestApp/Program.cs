using AI.Provincial.Evolution;
using AI.Provincial.PlayAgenda;
using GameCore;
using GameCore.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Card> cards = PresetGames.Get(Games.SizeDistortion).AddRequiredCards();

            User getFirst() => new ProvincialAI(BuyAgenda.Load(cards, "kaca"), "Kaca");
            User getSecond() => new ProvincialAI(BuyAgenda.Load(cards, "honza"), "Honza");

            Game game = new Game(new User[] { getFirst(), getSecond() }, cards.GetKingdom(2), new MyLogger());
            var task = game.Play();
            var results = task.Result;

            ReadLine();
        }

        class MyLogger : ILogger
        {
            public void Log(string str) => WriteLine(str);
        }
    }
}
