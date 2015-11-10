using System;
using System.Xml.Linq;

namespace LiveSwitcher
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var xml = XDocument.Load("web.config");
            var url = xml.Root?.Element("system.webServer")?.Element("rewrite")?.Element("rules")?.Element("rule")?.Element("action")?.Attribute("url");
            if (url != null)
            {
                if (args.Length >= 1)
                {
                    url.SetValue("http://127.0.0.1:" + args[0] + "/{R:1}");
                    xml.Save("web.config");
                }
                Console.WriteLine(url.Value);
            }
        }
    }
}