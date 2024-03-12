using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Media;

namespace CarpToolkit.Helpers
{
    public class SystemHelper
    {
        public static List<string> ProcessorList = [];

        public static double TotalMemory;

        public static List<string>? VideoCardList = [];

        public static List<double>? VideoCardRAMList = [];

        public static int CurrentVolume;

        public static void init()
        {
            ManagementObjectSearcher Win32_Processor = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            if (Win32_Processor == null) return;
            foreach (ManagementObject obj in Win32_Processor.Get())
            {
                ProcessorList.Add(obj["Name"].ToString());
            }

            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in allDrives)
            {
                if (drive.DriveType == DriveType.Fixed)
                {
                    double val1 = drive.TotalSize / (1024 * 1024 * 1024); // 转换为GB
                }
            }

            ManagementObjectSearcher Win32_ComputerSystem = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
            if (Win32_ComputerSystem == null) return;
            foreach (ManagementObject obj in Win32_ComputerSystem.Get())
            {
                ulong totalMemory = Convert.ToUInt64(obj["TotalPhysicalMemory"]);
                TotalMemory = totalMemory / (1024 * 1024 * 1024); // 转换为GB
            }

            ManagementObjectSearcher Win32_VideoController = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
            if (Win32_VideoController == null) return;
            foreach (ManagementObject videoCard in Win32_VideoController.Get())
            {
                VideoCardList?.Add((string)videoCard["Name"]);

                long adapterRAMBytes = Convert.ToInt64(videoCard["AdapterRAM"]);
                VideoCardRAMList?.Add((double)adapterRAMBytes / (1024 * 1024 * 1024));
            }

        }
    }
}
