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
        const int gameCount = 5000;
        static void Main(string[] args)
        {
            List<Card> cards = PresetGames.Get(Games.FirstGame).Concat(PresetGames.VictoryAndTreasures()).ToList();
            int result = 0;

            for (int i = 0; i < gameCount; i++)
            {
                //var militial = new MilitialAI();
                var evolved = new ProvincialAI(BuyAgenda.Load(cards), "Short");
                //var random = new AI.Provincial.PlayAgenda.ProvincialAI(AI.Provincial.Evolution.BuyAgenda.GetRandom(cards));
                var villageSmithy = new ProvincialAI(BuyAgenda.Load(cards, "overnight"), "Long");

                Game game = new Game(new User[] { evolved, villageSmithy }, cards.GetKingdom(true));
                var task = game.Play();
                var results = task.Result;
                result += results.Score[0].CompareTo(results.Score[1]);
            }

            for (int i = 0; i < gameCount; i++)
            {
                //var militial = new MilitialAI();
                var evolved = new ProvincialAI(BuyAgenda.Load(cards), "Short");
                //var random = new AI.Provincial.PlayAgenda.ProvincialAI(AI.Provincial.Evolution.BuyAgenda.GetRandom(cards));
                var villageSmithy = new ProvincialAI(BuyAgenda.Load(cards, "overnight"), "Long");

                Game game = new Game(new User[] { villageSmithy, evolved }, cards.GetKingdom(true));
                var task = game.Play();
                var results = task.Result;
                result += results.Score[1].CompareTo(results.Score[0]);
            }

            WriteLine(result);
            ReadLine();
        }
    }
}
