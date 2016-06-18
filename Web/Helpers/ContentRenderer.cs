using System.Linq;
using System.Xml.Linq;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Web.Models;

namespace Web.Helpers
{
    [HtmlTargetElement("content", Attributes = "page, summarize")]
    public class ContentRenderer : TagHelper
    {
        public Page Page { get; set; }
        public bool Summarize { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.Add("class", "content");
            output.TagMode = TagMode.StartTagAndEndTag;

            var content = XDocument.Parse("<root>" + Page.Body + "</root>");
            if (Summarize)
                content.Root?.ReplaceNodes(content.Root?.Nodes().Take(2));

            var images = content.Descendants("rimg");
            foreach (var image in images)
                ResponsiveImage.UpdateTag(image);

            output.Content.SetHtmlContent(content.Root?.ToString().Replace("<root>", "").Replace("</root>", ""));
        }
    }
}