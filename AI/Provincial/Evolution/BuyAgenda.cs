﻿
using GameCore.Cards;
using GameCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AI.Provincial.Evolution
{
    public class BuyAgenda
    {
        const string path = "..\\..\\..\\AI\\Provincial\\data\\kingdoms\\";
        public List<(CardType Card, int Number)> BuyMenu = new List<(CardType, int)>();

        public int Colonies;
        public int Provinces;
        public int Duchies;
        public int Estates;

        private BuyAgenda() { }

        public static BuyAgenda GetRandom(List<Card> k)
        {
            var agenda = new BuyAgenda();
            var rnd = new ThreadSafeRandom();

            // i dont really care if the possitive mod is correct, i just need the number between 1 and 10
            var randomKingdom = k.Where(c => c.Type != CardType.Curse || c.Type != CardType.Copper)
                                 .OrderBy(c => rnd.Next())
                                 .Take(9)
                                 .Select(c => (c.Type, rnd.Next(10) + 1));

            int i = 0;
            foreach (var item in randomKingdom)
            {
                agenda.BuyMenu.Add(item);
                if (i == 2)
                    agenda.BuyMenu.Add((CardType.Gold, 99));
                if (i == 7)
                    agenda.BuyMenu.Add((CardType.Silver, 10));
                i++;
            }

            // todo podle poctu hracu
            // todo vyresit kolonie
            // todo a myslim ze se podle tohoto stejne vůbec nerozhoduje
            agenda.Colonies = rnd.Next(8);
            agenda.Provinces = rnd.Next(8);
            agenda.Duchies = rnd.Next(8);
            agenda.Estates = rnd.Next(8);

            return agenda;
        }

        public BuyAgenda Clone()
        {
            return new BuyAgenda
            {
                Colonies = this.Colonies,
                Provinces = this.Provinces,
                Duchies = this.Duchies,
                Estates = this.Estates,
                BuyMenu = BuyMenu.ToList()
            };
        }

        public void Save(List<Card> k)
        {
            var filename = k.OrderBy(c => c.Type).Select(c => (int)c.Type).Aggregate("kingdom", (a, b) => a + "_" + b);
            using (var writer = new StreamWriter($"{path}{filename}.txt"))
            {
                writer.WriteLine(Colonies);
                writer.WriteLine(Provinces);
                writer.WriteLine(Duchies);
                writer.WriteLine(Estates);
                foreach (var item in BuyMenu)
                    writer.WriteLine($"{item.Card.ToString()} {item.Number}");
            }
        }

        public static BuyAgenda Load(List<Card> k, string filename = null)
        {
            filename = filename ?? k.OrderBy(p => p.Type).Select(p => (int)p.Type).Aggregate("kingdom", (a, b) => a + "_" + b);

            try
            {
                using (var reader = new StreamReader($"{path}{filename}.txt"))
                {
                    var agenda = new BuyAgenda();
                    agenda.Colonies = int.Parse(reader.ReadLine());
                    agenda.Provinces = int.Parse(reader.ReadLine());
                    agenda.Duchies = int.Parse(reader.ReadLine());
                    agenda.Estates = int.Parse(reader.ReadLine());
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine().Split();
                        Enum.TryParse(line[0], out CardType type);

                        agenda.BuyMenu.Add((type, int.Parse(line[1])));
                    }

                    return agenda;
                }
            }
            catch(IOException)
            {
                // if this kingdom was not evolved yed
                // todo log? dependency injection
                return null;
            }
        }
    }
}