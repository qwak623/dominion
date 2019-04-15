using GameCore.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Provincial.Evolution
{
    public class Params
    {
        public List<Card> Kingdom;
        public int MinGames = 1;
        public int MaxGames = 1;
        public int LeaderCount = 5;
        public int PoolCount = 5;
        public double Mutate = 0.2;
    }
}
