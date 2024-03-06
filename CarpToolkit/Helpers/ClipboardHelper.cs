using Avalonia;
using Avalonia.Controls;
using System.Threading.Tasks;

namespace CarpToolkit.Helpers
{
    public static class ClipboardHelper
    {
        private static TopLevel Owner { get; set; }

        public static Task SetTextAsync(string text) =>
            Owner.Clipboard.SetTextAsync(text);

        /**
         *  ClipboardHelper initialization should be done in a View.cs
         */
        public static void init(Visual visual)
        {
            Owner = TopLevel.GetTopLevel(visual);
        }
    }
}
