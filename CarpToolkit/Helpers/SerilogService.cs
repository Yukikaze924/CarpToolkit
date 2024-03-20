using Serilog;
using Serilog.Core;

namespace CarpToolkit.Helpers
{
    public class SerilogService
    {
        public static Logger logger = new LoggerConfiguration().WriteTo.File(@"CarpToolkit.log").CreateLogger();
    }
}
