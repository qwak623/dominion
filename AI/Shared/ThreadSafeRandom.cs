using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Shared
{

    public class ThreadSafeRandom
    {
        // todo všechny randomy prevest na thread safe random, aby bylo mozne deterministicky pouzivat seed
        private static readonly Random _global = new Random();
        [ThreadStatic] private static Random _local;

        public ThreadSafeRandom()
        {
            if (_local == null)
            {
                lock (_global)
                {
                    if (_local == null)
                    {
                        int seed = _global.Next();
                        _local = new Random(seed);
                    }
                }
            }
        }

        public int Next()
        {
            return _local.Next();
        }

        public int Next(int max)
        {
            return (int)((uint)_local.Next()) % max;
        }

        public double NextDouble()
        {
            return _local.NextDouble();
        }
    }
}