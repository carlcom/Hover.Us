using System.Xml.Linq;
using Microsoft.AspNet.Razor.TagHelpers;
using Web.Models;

namespace Web.Helpers
{
    [HtmlTargetElement("content", Attributes = "page")]
    public class ContentRenderer : TagHelper
    {
        public Page Page { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.Add("class", "content");
            output.TagMode = TagMode.StartTagAndEndTag;

            var content = XDocument.Parse("<root>" + Page.Body + "</root>");
            var images = content.Descendants("rimg");
            foreach (var image in images)
                ResponsiveImage.UpdateTag(image);

            output.Content.SetHtmlContent(content.Root.ToString().Replace("<root>", "").Replace("</root>", ""));
        }
    }
}