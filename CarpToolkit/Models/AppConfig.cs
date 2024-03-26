namespace CarpToolkit.Models
{
    public class AppConfig
    {
        public Host? Host { get; set; }
    }

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
        public Port(int? website, int? api, int? database)
        {
            this.website = website;
            this.api = api;
            this.database = database;
        }

        public int? website { get; set; }
        public int? api { get; set; }
        public int? database { get; set; }
    }
}
