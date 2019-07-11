using AI.Evolution;
using AI.Model;
using AI.Provincial;
using GameCore.Cards;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // loading files

            //    DirectoryInfo dir = new DirectoryInfo($"..{sep}..{sep}..{sep}AI{sep}Provincial{sep}data{sep}kingdomsTens");
            //    FileInfo[] files = dir.GetFiles("kingdom_*.txt");

            var list = new List<BuyAgenda>();

            var man = new Fives(directoryPath);

            int i = 0, c = 0;
            foreach (var item in man)
            {
                if (Card.Get(item.BuyMenu[0].Card).Price == 2)
                {
                    c++;

                    var cards = item.Id.Split('_').Select(a => Card.Get((CardType)int.Parse(a))).ToList();
                    var kingdomName = cards.OrderBy(p => p.Type).Select(p => ((int)p.Type).ToString()).Aggregate((a, b) => a + " " + b);
                    Console.WriteLine($"{kingdomName}");
                
                    //    var evolution = new Evolution(new Params
                //    {
                //        Kingdom = cards,
                //        Evaluator = new ProvincialEvaluator(),
                //        LeaderCount = 10,
                //        PoolCount = 50,
                //        Generations = 50,
                //    });
                //    var agenda = evolution.Run();
                //    list.Add(agenda);
                }
                //else
                //    list.Add(item);
                i++;
            }
            Console.WriteLine(c);
            Console.WriteLine(i);

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

            Console.ReadLine();
        }
    }
}
