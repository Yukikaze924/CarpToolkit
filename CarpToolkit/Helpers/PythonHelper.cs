using Avalonia.Controls.Documents;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpToolkit.Helpers
{
    public class PythonHelper
    {
        public static ScriptEngine Engine { get; set; }

        public static void init()
        {
            Engine = Python.CreateEngine();
        }

        public static dynamic ExecutePythonFrom(string path)
        {
            return Engine.ExecuteFile(path);
        }
    }
}
