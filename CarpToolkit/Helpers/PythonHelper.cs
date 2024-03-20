using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace CarpToolkit.Helpers
{
    public class PythonHelper
    {
        public static ScriptEngine Engine;

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
