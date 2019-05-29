using System;

namespace Utils
{
    public class ThreadSafeRandom
    {
        private static readonly Random _global = new Random();
        [ThreadStatic] private static Random _local;

        public ThreadSafeRandom()
        {
            //_local = _global;
            //return; // todo smazat

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