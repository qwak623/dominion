using GameCore;
using System;
using System.Threading.Tasks;
using System.Linq;

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

        public void Run()
        {
            SetUp();

            for (int gen = 0; gen < par.Generations; gen++)
            {
                var sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                // evolution step
                Evaluate();
                sw.Stop();
                var e = sw.Elapsed;
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
            //for (int i = 0; i < pool.Length; i++)
            Parallel.For(0, pool.Length, i => 
                pool[i].Fitness = pool[i].Agenda.Evaluate(leaders, par.Kingdom, par.MinGames, par.MaxGames));
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
            // first five members without changes
            for (int i = 0; i < leaders.Length; i++)
                pool[i] = (leaders[i], 0);

            // for each new member in pool
            for (int i = leaders.Length; i < pool.Length; i++)
            {
                var agenda = leaders[rnd.Next(leaders.Length)].Clone();

                // at least one mutations always happens
                mutations[rnd.Next(mutations.Length)].Mutate(agenda, par.Kingdom);

                // sometimes there will be more mutations
                while (rnd.NextDouble() < par.Mutate)
                    mutations[rnd.Next(mutations.Length)].Mutate(agenda, par.Kingdom);

                if (agenda.BuyMenu.Any(m => m.Number == 0))
                    agenda = agenda;

                pool[i] = (agenda, 0);
            }
        }
    }
}
