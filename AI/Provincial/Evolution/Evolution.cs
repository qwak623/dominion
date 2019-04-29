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

        readonly ThreadSafeRandom rnd = new ThreadSafeRandom();

        public Evolution(Params par)
        {
            this.par = par;
        }

        public void Run()
        {
            SetUp();

            for (int gen = 0; gen < par.Generations; gen++)
            {
                var sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                // evolution step
                GenerateNewPool();
                Evaluate();
                SetNewLeaders();
                sw.Stop();
                var e = sw.Elapsed;
            }

            leaders[0].Save(par.Kingdom);
        }

        public void SetUp()
        {
            leaders = new BuyAgenda[par.LeaderCount];
            for (int i = 0; i < leaders.Length; i++)
                leaders[i] = BuyAgenda.GetRandom(par.Kingdom);
            pool = new (BuyAgenda, double)[par.PoolCount];
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
            // comparing fitness and individual length

            // TODO
            //Array.Sort(pool, (a, b) => -a.Fitness.CompareTo(b.Fitness));
            Array.Sort(pool, (a, b) => -2 * a.Fitness.CompareTo(b.Fitness) + a.Agenda.BuyMenu.Count.CompareTo(b.Agenda.BuyMenu.Count));
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
                do
                    par.MutationSelector.SelectMutation(agenda.BuyMenu.Count).Mutate(agenda, par.Kingdom);
                while (rnd.NextDouble() < par.Mutate);

                pool[i] = (agenda, 0);
            }
        }
    }
}
