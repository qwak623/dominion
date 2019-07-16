using GameCore.Cards;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Window
{
    class GameParams
    {
        public List<Card> Cards = new List<Card>();
        public AIType AIType;
        public string User1Name = "Human";
        public string User2Name = "Friend";
        const string path = "..\\..\\GameParams.txt";

        private GameParams() { }

        public static GameParams Load()
        {
            var par = new GameParams();

            try
            {
                using (var reader = new StreamReader(path))
                {
                    Enum.TryParse(reader.ReadLine().Split('=')[1], out AIType aiType);
                    par.AIType = aiType;
                    par.User1Name = reader.ReadLine().Split('=')[1];
                    par.User2Name = reader.ReadLine().Split('=')[1];

                    foreach (var card in reader.ReadLine().Split('=')[1].Split(','))
                    {
                        Enum.TryParse(card, out CardType cardType);
                        par.Cards.Add(Card.Get(cardType));
                    }
                }
            }
            catch (Exception)
            {
                par.AIType = AIType.Tens;
                par.User1Name = "Human";
                par.User2Name = "Friend";
                par.Cards = PresetGames.Get(Games.FirstGame);
            }

            return par;
        }

        public void Save()
        {
            using (var writer = new StreamWriter(path))
            {
                writer.WriteLine($"AIType={AIType}");
                writer.WriteLine($"User1Name={User1Name}");
                writer.WriteLine($"User2Name={User2Name}");
                writer.WriteLine("Cards=" + Cards.Select(c => c.Type.ToString()).Aggregate((a, b) => a + "," + b));
            }
        }
    }

    public enum AIType { Tens, Fives, Threes }
}
