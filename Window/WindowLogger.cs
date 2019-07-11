using GameCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Window
{
    class WindowLogger : ILogger
    {
        Action<string> log;
        public WindowLogger(Action<string> log) => this.log = log;
        public void Log(string str) => log(str + "\n");
    }
}