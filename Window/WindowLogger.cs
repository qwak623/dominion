using GameCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Window
{
    class WindowLogger : Logger
    {
        Action<string> log;
        StreamWriter writer = new StreamWriter("log.txt");
        public WindowLogger(Action<string> log)
        {
            this.log = log;
        }

        public override void Log(string str)
        {
            log(str + "\n");
            writer.WriteLine(str);
            writer.Flush();
        }
    }
}
