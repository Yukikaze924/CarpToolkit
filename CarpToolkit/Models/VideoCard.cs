using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpToolkit.Models
{
    public class VideoCard
    {
        public VideoCard(string videoCardName, double videoCardRAM)
        {
            VideoCardName = videoCardName;
            VideoCardRAM = videoCardRAM;
        }

        public string VideoCardName { get; set; }
        public double VideoCardRAM { get; set; }
    }
}
