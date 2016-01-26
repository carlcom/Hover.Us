using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace LiveSwitcher
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var xml = XDocument.Load("web.config");
            var root = xml.Root;
            var server = root?.Element("system.webServer");
            var rewrite = server?.Element("rewrite");
            var rules = rewrite?.Element("rules");
            var rule = rules?.Elements("rule").First(r => r.Attribute("name").Value == "Reverse Proxy");
            var action = rule?.Element("action");
            var url = action?.Attribute("url");
            if (url == null) return;
            if (args.Length >= 1)
            {
                url.SetValue("http://127.0.0.1:" + args[0] + "/{R:1}");
                xml.Save(new FileStream("web.config", FileMode.OpenOrCreate));
            }
            Console.WriteLine(url.Value);
        }
    }
}