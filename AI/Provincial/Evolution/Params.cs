using GameCore;
using GameCore.Cards;
using System.Collections.Generic;

namespace AI.Provincial.Evolution
{
    public class Params
    {
        public List<Card> Kingdom;
        internal MutationSelector MutationSelector = new MutationSelector();

        public int MinGames = 50;
        public int MaxGames = 100;
        public int LeaderCount = 10;
        public int PoolCount = 50;
        public int Generations = 1000; 
        public double Mutate = 0.5;
    }

    internal class MutationSelector
    {
        public readonly List<(Mutation Mutation, double Probability)> mutations = new List<(Mutation, double)>();
        ThreadSafeRandom rnd = new ThreadSafeRandom();

        public MutationSelector()
        {
            // todo neco s mutation probability takto je to asi blbost...
            mutations.Add((new RemoveCardMutation(), 0.02));
            mutations.Add((new AddCardMutation(), 0.04));
            mutations.Add((new ReplaceSupplyCardMutation(), 0.04));
            mutations.Add((new ModifyPurchaseCountMutation(), 0.3));
            mutations.Add((new SwapSupplyCardsMutation(), 0.3));
            mutations.Add((new VictoryCardPurchaseMutation(), 0.3));
        }

        /// <summary>
        /// Selects RemoveCardMutation if buyMenuCount > 20.
        /// Otherwise returns mutation based on preset probability.
        /// </summary>
        /// <param name="buyMenuCount"></param>
        /// <returns></returns>
        public Mutation SelectMutation(int buyMenuCount)
        {
            if (buyMenuCount > 20)
                return mutations[0].Mutation;
            double number = rnd.NextDouble();

            for (int i = 0; i < mutations.Count; number -= mutations[i++].Probability)
                if (number < mutations[i].Probability)
                    return mutations[i].Mutation;

            return null;
        }
    }
}
