using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;

namespace Web.Helpers
{
    [TargetElement("rimg", Attributes = "base")]
    public class ResponsiveImage : TagHelper
    {
        public string Base { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            output.TagMode = TagMode.SelfClosing;

            var img = new XElement("img", new XAttribute("src", Base + ".jpg"));
            UpdateTag(img);
            output.Attributes["src"] = img.Attribute("src").Value;
            output.Attributes["srcset"] = img.Attribute("srcset").Value;
            output.Attributes["sizes"] = img.Attribute("sizes").Value;
            output.Attributes["title"] = context.AllAttributes["alt"].Value.ToString();
        }

        public static void UpdateTag(XElement image)
        {
            var imgSrc = image.Attribute("src");
            if (!imgSrc.Value.StartsWith(Startup.ImageBase))
            {
                imgSrc.Value = Startup.ImageBase + "/" + imgSrc.Value;
            }
            var img = Path.GetFileNameWithoutExtension(imgSrc.Value).Split('-')[0];

            var widths = new[] { 240, 320, 480, 640, 800, 960, 1280, 1600, 1920, 2400 };
            var srcset = string.Join(", ",
                widths.Select(s => Startup.ImageBase + "/" + img + "-" + (s < 1000 ? "0" : "") + s + ".jpg " + s + "w"));

            var sizes = "(max-width: 767px) 100vw, (max-width: 991px) 720px, (max-width: 1199px) 940px, 1140px";

            image.Add(new XAttribute("srcset", srcset));
            image.Add(new XAttribute("sizes", sizes));
        }
    }
}