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
            List<Card> cards = PresetGames.Get(Games.SizeDistortion).AddRequiredCards();

            string first = "kaca";
            string second = "honza";
            User getFirst() => new ProvincialAI(BuyAgenda.Load(cards, first), first);
            User getSecond() => new ProvincialAI(BuyAgenda.Load(cards, second), second);

            int[] result = new int[2];
            for (int i = 0; i < gameCount; i++)
            {
                Game game = new Game(new User[] { getFirst(), getSecond() }, cards.GetKingdom(2));
                var task = game.Play();
                var results = task.Result;
                if (results.Score[0] > results.Score[1])
                    result[0]++;
                if (results.Score[0] < results.Score[1])
                    result[1]++;
            }

            for (int i = 0; i < gameCount; i++)
            {
                Game game = new Game(new User[] { getSecond(), getFirst() }, cards.GetKingdom(2));
                var task = game.Play();
                var results = task.Result;
                if (results.Score[0] > results.Score[1])
                    result[1]++;
                if (results.Score[0] < results.Score[1])
                    result[0]++;
            }

            WriteLine($"{first}: {result[0]}");
            WriteLine($"{second}: {result[1]}");
            ReadLine();
        }
    }
}
