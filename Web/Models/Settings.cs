using System.IO;

namespace Web.Models
{
    public static class Settings
    {
        public const string Title = "Steve Desmond";
        public const string Domain = "https://stevedesmond.ca/";
        public const string Description = "Software developer and photographer from Ithaca, NY";
        public const string ImageBase = Domain + "images";
        public const string BasePath = @"C:\www";
        public const string ConnectionString = "Data Source=" + BasePath + @"\db.sqlite";
    }
}
