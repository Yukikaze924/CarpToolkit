using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpToolkit.Models
{
    public class Settings
    {
        public bool IsPaneOpen { get; set; }
        public string AppTheme { get; set; }

        public Settings(bool val1, string val2)
        {
            IsPaneOpen = val1;
            AppTheme = val2;
        }
    }
}
