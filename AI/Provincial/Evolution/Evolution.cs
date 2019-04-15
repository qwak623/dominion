using AI.Shared;
using GameCore.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Provincial.Evolution
{
    public class Evolution
    {
        Params par;

        BuyAgenda[] leaders;
        (BuyAgenda Agenda, double Fitness)[] pool;
        readonly Mutation[] mutations; // todo asi by taky melo byt z venku nejak

        readonly ThreadSafeRandom rnd = new ThreadSafeRandom();

        public Evolution(Params par)
        {
            this.par = par;

            // todo neco s mutation probability takto je to asi blbost...
            mutations = new Mutation[5];
            mutations[0] = new ReplaceSupplyCardMutation();
            mutations[1] = new ModifyPurchaseCountMutation();
            mutations[2] = new SwapSupplyCardsMutation();
            mutations[3] = new VictoryCardPurchaseMutation();
            mutations[4] = new AddCardMutation();
        }

        public void Run(int generations)
        {
            SetUp();

            for (int gen = 0; gen < generations; gen++)
            {
                // evolution step
                Evaluate();
                SetNewLeaders();
                GenerateNewPool();
            }

            leaders[0].Save(par.Kingdom);
        }

        public void SetUp()
        {
            leaders = new BuyAgenda[par.LeaderCount];
            for (int i = 0; i < leaders.Length; i++)
                leaders[i] = BuyAgenda.GetRandom(par.Kingdom);
            pool = new (BuyAgenda, double)[par.PoolCount];
            for (int i = 0; i < pool.Length; i++)
                pool[i].Agenda = BuyAgenda.GetRandom(par.Kingdom);
        }

        void Evaluate()
        {
            // todo parallel
            //    Parallel.For(0, pool.Length, i => 
            for (int i = 0; i < pool.Length; i++)
                pool[i].Fitness = pool[i].Agenda.Evaluate(leaders, par.Kingdom, par.MinGames, par.MaxGames);
        }

        void SetNewLeaders()
        {
            // TODO on tam dela neco jako ze pocita pouzivanost karet a tak nejak zajistuje diverzitu leaders
            Array.Sort(pool, (a, b) => -a.Fitness.CompareTo(b.Fitness));
            for (int i = 0; i < leaders.Length; i++)
                leaders[i] = pool[i].Agenda;
        }

        void GenerateNewPool()
        {
            // for each new member in pool
            for (int i = 0; i < pool.Length; i++)
            {
                var agenda = leaders[rnd.Next(leaders.Length)].Clone();

                // at least one mutations always happens
                mutations[rnd.Next(mutations.Length)].Mutate(agenda, par.Kingdom);

                // sometimes there will be more mutations
                while (rnd.NextDouble() < par.Mutate)
                    mutations[rnd.Next(mutations.Length)].Mutate(agenda, par.Kingdom);

                pool[i] = (agenda, 0);
            }
        }
    }
}
