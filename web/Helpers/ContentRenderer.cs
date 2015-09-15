using System.Xml.Linq;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using Web.Models;

namespace Web.Helpers
{
    [TargetElement("content", Attributes = "page")]
    public class ContentRenderer : TagHelper
    {
        public Page Page { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.Add("class", "content");
            output.TagMode = TagMode.StartTagAndEndTag;

            var content = XDocument.Parse("<root>" + Page.Body + "</root>");
            var images = content.Descendants("img");
            foreach (var image in images)
            {
                ResponsiveImage.updateTag(image);
            }

            output.Content.SetContent(string.Join("", content.Root.ToString().Replace("<root>", "").Replace("</root>", "")));
        }
    }
}