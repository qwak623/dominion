using GameCore;
using AI.Provincial.PlayAgenda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameCore.Cards;

namespace AI.Provincial.Evolution
{
    static class Evaluator
    {
        public static double Evaluate(this BuyAgenda agenda, BuyAgenda[] leaders, List<Card> k, int minGames, int maxGames)
        {
            double fitness = 0;
            object obj = new object();

            // TODO parallel
            //Parallel.ForEach(leaders, leader =>
            foreach (var leader in leaders)
            {
                int wins = 0;
                int gameIndex;
                bool significantDifferenceFound = false;
                for (gameIndex = 0; gameIndex < maxGames && !significantDifferenceFound; gameIndex++)
                {
                    Kingdom kingdom = k.GetKingdom(true);
                    User[] users = { new ProvincialAI(agenda), new ProvincialAI(leader) };

                    var game = new Game(users, kingdom, new FakeLogger());
                    var task = game.Play();
                    var result = task.Result;

                    // todo funguje jen u dvou hracu zatim
                    wins += result.Score[0].CompareTo(result.Score[1]);

                    if (gameIndex >= minGames && gameIndex % 200 == 0)
                    {
                        double errorMargin = 2.0 / Math.Sqrt(gameIndex + 1);
                        double spread = Math.Abs(wins / (double)gameIndex);
                        significantDifferenceFound = (errorMargin <= spread);
                    }
                }

                lock (obj)
                    fitness += wins / (double)gameIndex;
            }
            //});
            return fitness;
        }
    }

    class FakeLogger : Logger
    {
        public override void Log(string str) => System.Diagnostics.Debug.WriteLine(str);
    }
}
