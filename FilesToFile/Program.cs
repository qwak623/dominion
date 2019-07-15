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
            var manager = new SimpleManager(directoryPath, "Fives_");
            var dict = new Dictionary<string, BuyAgenda>();
            List<BuyAgenda> list = new List<BuyAgenda>();
            int i = 0, k = 0;
            foreach (var item in manager)
                list.Add(item);

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

            WriteLine(i);
            ReadLine();
            // loading files

            //    DirectoryInfo dir = new DirectoryInfo($"..{sep}..{sep}..{sep}AI{sep}Provincial{sep}data{sep}kingdomsTens");
            //    FileInfo[] files = dir.GetFiles("kingdom_*.txt");

            //DirectoryInfo dir = new DirectoryInfo($"..{sep}..{sep}..{sep}AI{sep}Provincial{sep}data{sep}kingdoms");
            //FileInfo[] files = dir.GetFiles("Fives_*.txt");
            //foreach (var file in files)
            //    file.Delete();

            //foreach (var agenda in list.OrderBy(l => l.Id))
            //    man.Save(agenda.Id.Split('_').Select(a => int.Parse(a)), agenda);

            //var tens = new Tens(directoryPath);

            //foreach (var file in files)
            //{
            //    var id = file.Name
            //            .Remove(file.Name.Length - 4)
            //            .Substring(20).TrimStart(new char[] { '7', '_' });

            //    using (var reader = new StreamReader(file.OpenRead()))
            //    {
            //        reader.ReadLine();
            //        var prov = int.Parse(reader.ReadLine());
            //        var duch = int.Parse(reader.ReadLine());
            //        var est = int.Parse(reader.ReadLine());
            //        var buyMenuStr = "";

            //        while (!reader.EndOfStream)
            //        {
            //            var line = reader.ReadLine().Split();
            //            Enum.TryParse(line[0], out CardType type);
            //            buyMenuStr += $",{type} ({(int)type}) {int.Parse(line[1])}";
            //        }


            //        var agenda = BuyAgenda.FromString($"{id}:{prov};{duch};{est};{buyMenuStr.Trim(',')}");
            //        var cards = id.Split('_').Select(c => int.Parse(c));

            //        if (tens.Load(cards) != null)
            //            x++;
            //        else
            //            tens.Save(cards, agenda);

            //        dict.Add(id, $"{id}:{prov};{duch};{est};{buyMenuStr.Trim(',')}");
            //    }
            //}

            //Console.WriteLine("Old agendas " + x);
            //Console.WriteLine("All agendas " + tens.Count());

            //foreach (var item in dict)
            //{
            //    var agenda = tens.Load(item.Key.Split('_').Select(c => int.Parse(c)));
            //    if (agenda.ToString(item.Key) != item.Value)
            //        Console.WriteLine(item.Key);
            //}
        }
    }
}
