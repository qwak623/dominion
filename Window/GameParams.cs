using GameCore.Cards;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Window
{
    class GameParams
    {
        public List<Card> Cards = new List<Card>();
        public AIType AIType;
        const string path = "..\\..\\GameParams.txt";

        private GameParams() { }

        public static GameParams Load()
        {
            using (var reader = new StreamReader(path))
            {
                var par = new GameParams();
                Enum.TryParse(reader.ReadLine(), out AIType aiType);
                par.AIType = aiType;

                while (!reader.EndOfStream)
                {
                    Enum.TryParse(reader.ReadLine(), out CardType cardType);
                    par.Cards.Add(Card.Get(cardType));
                }

                return par;
            }
        }

        public void Save()
        {
            using (var writer = new StreamWriter(path))
            {
                writer.WriteLine(AIType);
                foreach (var card in Cards)
                    writer.WriteLine(card.Type);
            }
        }
    }

    public enum AIType { Provincial }
}
