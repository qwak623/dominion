using AI.Provincial;
using GameCore;
using System;
using System.Threading.Tasks;
using Utils;

namespace AI.Evolution
{
    public class Evolution
    {
        Params par;
        BuyAgenda[] leaders;
        (BuyAgenda Agenda, double Fitness)[] pool;
        readonly BuyAgenda referenceAgenda;
        readonly ILogger logger;
        readonly ThreadSafeRandom rnd = new ThreadSafeRandom();

        public Evolution(Params par, ILogger logger = null, BuyAgenda referenceAgenda = null)
        {
            this.par = par;
            this.logger = logger;
            this.referenceAgenda = referenceAgenda;
        }

        public void Run()
        {
            SetUp();

            // todo smazat stopwatch
            var sw = new System.Diagnostics.Stopwatch();

            for (int gen = 0; gen < par.Generations; gen++)
            {
                sw.Reset();
                sw.Start();
            
                // evolution step
                GenerateNewPool();
                Evaluate();
                SetNewLeaders();

                sw.Stop();
                var elapsed = sw.Elapsed;

                if (referenceAgenda != null)
                    ComputeFitness(leaders[0], gen);
                logger?.Log($"Generation {gen}: elapsed time {elapsed.TotalSeconds.ToString("0.00")}");
            }

            leaders[0].Save(par.Kingdom, par.Folder);
        }

        public void SetUp()
        {
            leaders = new BuyAgenda[par.LeaderCount];
            for (int i = 0; i < leaders.Length; i++)
                leaders[i] = BuyAgenda.GetRandom(par.Kingdom);
            leaders[0] = BuyAgenda.Load(par.Kingdom, "KingdomTens") ?? BuyAgenda.GetRandom(par.Kingdom);
            if (leaders[0].Loaded)
                logger?.Log("Rewriting kingdom.");
            pool = new (BuyAgenda, double)[par.PoolCount];
        }

        void Evaluate()
        {
            // todo parallel
            //for (int i = 0; i < pool.Length; i++)
            Parallel.For(0, pool.Length, i => 
                pool[i].Fitness = par.Evaluator.Evaluate(pool[i].Agenda, leaders, par.Kingdom, par.MinGames, par.MaxGames));
        }

        void SetNewLeaders()
        {
            // comparing fitness and individual length
            Array.Sort(pool, (a, b) => -2 * a.Fitness.CompareTo(b.Fitness) + a.Agenda.BuyMenu.Count.CompareTo(b.Agenda.BuyMenu.Count));
            int j = 0;
            for (int i = 0; i < leaders.Length;)
            {
                if (IsSimilarToAny(pool[j].Agenda, i))
                    if (j > pool.Length)
                        leaders[i++] = BuyAgenda.GetRandom(par.Kingdom);
                    else
                        j++;
                leaders[i++] = pool[j++].Agenda;
            }
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

        void ComputeFitness(BuyAgenda buyAgenda, int generation)
        {
            User getFirst() => new ProvincialAI(referenceAgenda, "Referencer");
            User getSecond() => new ProvincialAI(buyAgenda, "Leader");
            const int gameCount = 5000;

            int[] result = new int[2];
            for (int i = 0; i < gameCount; i++)
            {
                Game game = new Game(new User[] { getFirst(), getSecond() }, par.Kingdom.GetKingdom(2));
                var task = game.Play();
                var results = task.Result;
                if (results.Score[0] > results.Score[1])
                    result[0]++;
                if (results.Score[0] < results.Score[1])
                    result[1]++;
            }

            for (int i = 0; i < gameCount; i++)
            {
                Game game = new Game(new User[] { getSecond(), getFirst() }, par.Kingdom.GetKingdom(2));
                var task = game.Play();
                var results = task.Result;
                if (results.Score[0] > results.Score[1])
                    result[1]++;
                if (results.Score[0] < results.Score[1])
                    result[0]++;
            }

            //if (result[1] > result[0])
            //   buyAgenda.Save(par.Kingdom, $"gen_{generation}({result[0]}, {result[1]})");
            logger.Log($"Generation {generation}: Referencer {result[0]}, Leader {result[1]}");
        }

        bool IsSimilarToAny(BuyAgenda agenda, int count)
        {
            int aggDistance = 0;
            for (int i = 0; i < count; i++)
                aggDistance += agenda.BuyMenu.CalcLevenshteinDistance(leaders[i].BuyMenu);
            if (aggDistance < count * 1.5f - 1)
                return false;
            return true;
        }
    }
}
