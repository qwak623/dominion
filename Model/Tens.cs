using GameCore;
using GameCore.Cards;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Model
{
    public class Tens : BuyAgendaManager
    {
        string directoryPath;
        string prefix;
        ReaderWriterLockSlim rwl = new ReaderWriterLockSlim();

        public Tens(string directoryPath, string prefix = "Tens_")
        {
            this.directoryPath = directoryPath;
            this.prefix = prefix;
        }

        /// <summary>
        /// This method does not saves other kingdoms.
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public override BuyAgenda Load(IEnumerable<int> cards)
        {
            var id = cards.OrderBy(p => p).Select(p => p.ToString()).Aggregate((a, b) => a + "_" + b);

            // im not sure if this is necesarry
            rwl.EnterReadLock();
            try
            {
                using (var reader = new StreamReader($"{directoryPath}{prefix}{cards.First()}.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (line.Split(':')[0] == id)
                            return BuyAgenda.FromString(line);
                    }
                }
            }
            finally
            {
                rwl.ExitReadLock();
            }
            return null;
        }

        public override BuyAgenda LoadBest(List<Card> cards, ILogger logger = null) => Load(cards);

        public override void Save(IEnumerable<int> cards, BuyAgenda agenda)
        {
            rwl.EnterWriteLock();
            try
            {
                var id = cards.OrderBy(p => p).Select(p => p.ToString()).Aggregate((a, b) => a + "_" + b);
                File.AppendAllText($"{directoryPath}{prefix}{(int)cards.First()}.txt", agenda.ToString(id) + Environment.NewLine);
            }
            finally
            {
                rwl.ExitWriteLock();
            }
        }

        public override void Update(BuyAgenda agenda)
        {
            // TODO
            throw new NotImplementedException("Agenda update");
        }
    }
}
