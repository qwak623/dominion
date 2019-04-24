using AI.Provincial.Evolution;
using AI.Provincial.PlayAgenda;
using GameCore;
using GameCore.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Window
{
    public class AIParams
    {
        public User User { private get; set; }
        public User GetUser(List<Card> k) => new ProvincialAI(BuyAgenda.Load(k) ?? BuyAgenda.GetRandom(k));
    }
}
