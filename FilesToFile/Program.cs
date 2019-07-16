using AI.Evolution;
using AI.Model;
using AI.Provincial;
using GameCore;
using GameCore.Cards;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using static System.Console;

namespace FilesToFile
{
    /// <summary>
    /// This program is converter from old agenda store to new.
    /// </summary>
    class Program
    {
        static readonly char sep = Path.DirectorySeparatorChar;
        static string directoryPath = $"..{sep}..{sep}..{sep}AI{sep}Provincial{sep}data{sep}kingdoms{sep}";

        static void Main(string[] args)
        {
            DirectoryInfo dir = new DirectoryInfo($"..{sep}..{sep}..{sep}AI{sep}Provincial{sep}data{sep}kingdoms");
            FileInfo[] files = dir.GetFiles("Tenss_*.txt");
            var dict = new Dictionary<string, BuyAgenda>();

            int i = 0;

            i = foreachFiles(files, dict, i);
            WriteLine("Tenss " + i);
            i = 0;
            //files = dir.GetFiles("Fives_*.txt");
            //i = foreachFiles(files, dict, i);
            //WriteLine("Fives " + i);

            WriteLine(dict.Count);

            var manager = new SimpleManager(directoryPath, "FinalTens_");
            foreach (var item in dict.OrderBy(a => a.Key))
            {
                try
                {
                    manager.Save(item.Key.ToCardList(), item.Value);
                }
                catch
                {
                    WriteLine(item.Value.ToString(item.Key));
                }
            }

            //int i = 0;
            //var manager = new SimpleManager(directoryPath, "AFThrees_");
            //WriteLine(manager.Count());
            //foreach (var item in manager)
            //    dict[item.Id] = item;

            //var writer = new StreamWriter(directoryPath + "missingThrees.txt");

            //foreach (var line in File.ReadAllLines(directoryPath + "threes.txt"))
            //{
            //    var str = line.Replace(' ', '_');
            //    if (!dict.ContainsKey(str))
            //    {
            //        writer.WriteLine(str.Replace('_', ' '));
            //        i++;
            //    }
            //}
            //WriteLine(i);

            //i = 0;
            //foreach (var item in manager)
            //{
            //    {
            //        if (item.Provinces < 1 ||
            //            item.Duchies < 1 ||
            //            item.Estates < 0 ||
            //            item.BuyMenu.Count < 4 //||
            //                                   //item.BuyMenu.Select(m => m.Card).Contains(CardType.Estate)
            //            )
            //        {
            //            i++;
            //            writer.WriteLine(item.Id.Replace('_', ' '));
            //        }
            //    }

            //}

            //writer.Close();

            WriteLine(i);
            ReadLine();


            //var manager = new SimpleManager(directoryPath, "Fives_");
            //var dict = new Dictionary<string, BuyAgenda>();
            //List<BuyAgenda> list = new List<BuyAgenda>();
            //int i = 0, k = 0;
            //foreach (var item in manager)
            //    list.Add(item);

            //using (var writer = new StreamWriter($"{directoryPath}badFives.txt"))
            //{
            //    foreach (var item in list)
            //    {
            //        if (item.Provinces < 2 ||
            //            item.Duchies < 1 ||
            //            item.Estates < 0 ||
            //            item.BuyMenu.Count < 3 //||
            //                                   //item.BuyMenu.Select(m => m.Card).Contains(CardType.Estate)
            //            )
            //        {
            //            i++;
            //            writer.WriteLine(item.Id);
            //        }
            //    }
            //}

            // turnaj stejnych inteligenci
            //foreach (var item in list.OrderBy(a => a.Id))
            //{
            //    if (item == null)
            //        continue;
            //    var cards = item.Id.ToCardList();
            //    if (!dict.ContainsKey(item.Id))
            //        dict[item.Id] = item;
            //    else
            //    {
            //        WriteLine(item.Id);
            //        var agendas = new List<BuyAgendaTournament.Tuple>
            //        {
            //            new BuyAgendaTournament.Tuple{ Agenda = dict[item.Id], Cards = cards },
            //            new BuyAgendaTournament.Tuple{ Agenda = item, Cards = cards }
            //        };
            //        agendas.Tournament(cards, 50);
            //        if (agendas[0].Wins < agendas[1].Wins)
            //            dict[item.Id] = item;
            //        WriteLine(agendas[0].Wins - agendas[1].Wins);
            //        i++;
            //    }
            //    k++;
            //}

            // ukladani zpetne
            //foreach (var item in dict)
            //    manager.Save(item.Key.ToCardList(), item.Value);

            // loading files
        }

        private static int foreachFiles(FileInfo[] files, Dictionary<string, BuyAgenda> dict, int i)
        {
            foreach (var file in files)
            {
                using (var reader = file.OpenText())
                {
                    while (!reader.EndOfStream)
                    {
                        try
                        {
                            var line = reader.ReadLine();
                            var agenda = BuyAgenda.FromString(line);
                            if (dict.ContainsKey(agenda.Id))
                            {
                                WriteLine(agenda.Id);
                                var cards = agenda.Id.ToCardList();
                                var agendas = new List<BuyAgendaTournament.Tuple>
                                    {
                                        new BuyAgendaTournament.Tuple{ Agenda = dict[agenda.Id] },
                                        new BuyAgendaTournament.Tuple{ Agenda = agenda }
                                    };
                                agendas.Tournament(cards, 50);
                                if (agendas[0].Wins < agendas[1].Wins)
                                    dict[agenda.Id] = agenda;
                                WriteLine(agendas[0].Wins - agendas[1].Wins);
                                i++;
                            }
                            else
                                dict[agenda.Id] = agenda;
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }

            return i;
        }
    }
}
