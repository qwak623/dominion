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
        const int gameCount = 1;
        static void Main(string[] args)
        {
            List<Card> cards = PresetGames.Get(Games.FirstGame).Concat(PresetGames.VictoryAndTreasures()).ToList();

            for (int i = 0; i < gameCount; i++)
            {
                //var militial = new MilitialAI();
                var evolved = new ProvincialAI(BuyAgenda.Load(cards), "Evolved");
                //var random = new AI.Provincial.PlayAgenda.ProvincialAI(AI.Provincial.Evolution.BuyAgenda.GetRandom(cards));
                var villageSmithy = new ProvincialAI(BuyAgenda.Load(cards, "village_smithy"), "Villager");

                Game game = new Game(new User[] { evolved, villageSmithy }, cards.GetKingdom(true), new MyLogger());
                var task = game.Play();
                var results = task.Result;
            }

            ReadLine();
        }

        class MyLogger : ILogger
        {
            public void Log(string str) => WriteLine(str);
        }
    }
}
