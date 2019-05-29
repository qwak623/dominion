using AI.Evolution;
using AI.Provincial;
using GameCore;
using GameCore.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Window
{
    public class AIResult
    {
        public User GetUser(List<Card> k, string folder, string name = null) => new ProvincialAI(BuyAgenda.Load(k, folder, name) ?? BuyAgenda.GetRandom(k));
    }
}
