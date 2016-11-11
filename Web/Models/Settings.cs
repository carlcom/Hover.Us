namespace Web.Models
{
    public static class Settings
    {
        public const string Title = "Steve Desmond Software Development";
        public const string Domain = "https://stevedesmond.ca";
        public const string Location = "Ithaca, NY";
        public const string Description = "Independent contractor and software consultant based in " + Location + "; working remotely with teams and businesses everywhere";
        public const string ImageBase = Domain + "/images";
        public const string ProfileImage = ImageBase + "/steve-0300.jpg";
        public const string ConnectionString = @"Data Source=D:\www\db.sqlite";
        public const string EmailFromAndTo = "hi@stevedesmond.ca";
        public const string EmailServer = "smtp.googlemail.com";
        public const string EmailUser = "steve@vtsv.ca";
        public const string EmailSubject = "Contact Form Submission";
    }
}