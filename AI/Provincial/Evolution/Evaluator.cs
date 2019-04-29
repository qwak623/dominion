using GameCore;
using AI.Provincial.PlayAgenda;
using System;
using System.Collections.Generic;
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

            //foreach (var leader in leaders)
            Parallel.ForEach(leaders, leader =>
            {
                int wins = 0;
                int gameIndex;
                bool significantDifferenceFound = false;
                for (gameIndex = 0; gameIndex < maxGames && !significantDifferenceFound; gameIndex++)
                {
                    Kingdom kingdom = k.GetKingdom(true);
                    User[] users = { new ProvincialAI(agenda), new ProvincialAI(leader) };

                    var game = new Game(users, kingdom);
                    var task = game.Play();
                    var result = task.Result;

                    wins += result.Score[0].CompareTo(result.Score[1]);
                    // todo funguje jen u dvou hracu zatim

                    if (gameIndex >= minGames && gameIndex % 200 == 0)
                    {
                        double errorMargin = 2.0 / Math.Sqrt(gameIndex + 1);
                        double spread = Math.Abs(wins / (double)gameIndex);
                        significantDifferenceFound = (errorMargin <= spread);
                    }
                }

                // TODO
                lock (obj)
                fitness += wins / (double)gameIndex;
            //}
            });
            return fitness;
        }
    }
}
