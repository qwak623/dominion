using GameCore.Cards;
using System.Collections.Generic;

namespace AI.Provincial.Evolution
{
    public class Params
    {
        public List<Card> Kingdom;
        public int MinGames = 50;
        public int MaxGames = 100;
        public int LeaderCount = 5;
        public int PoolCount = 100;
        public int Generations = 10;
        public double Mutate = 0.5;
    }
}
