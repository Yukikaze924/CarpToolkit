using System;

namespace CarpToolkit.Models
{
    public class AppConfig
    {
        public App App { get; set; }
        public Host Host { get; set; }
    }


    /// <summary>
    /// 程序常量配置部分
    /// </summary>
    public class App
    {
        public string name { get; set; } = "CarpToolkit";
        public string fullname { get; set; } = "Carp Toolkit";
        public string organization { get; set; } = "Carp.org";
        public Default Default { get; set; } = new Default();
    }
    public class Default
    {
        public int width { get; set; } = 1114;
        public int height { get; set; } = 700;
        public string storage_path { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    }


    /// <summary>
    /// 主机通讯部分
    /// </summary>
    public class Host
    {
        public Host(string? domain, string? address, Port? port)
        {
            this.domain = domain;
            this.address = address;
            this.port = port;
        }

        public string? domain { get; set; }
        public string? address { get; set; }
        public Port? port { get; set; }
    }
    public class Port
    {
        public int? website { get; set; } = 80;
        public int? api { get; set; } = 8000;
        public int? database { get; set; } = 3306;
    }
}
