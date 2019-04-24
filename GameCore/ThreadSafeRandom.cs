using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{

    public class ThreadSafeRandom
    {
        private static readonly Random _global = new Random(0);
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

        public int Next() => _local.Next();

        public int Next(int max) => (int)((uint)_local.Next()) % max;

        public double NextDouble() => _local.NextDouble();
    }
}