using System.Linq;
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
                ResponsiveImage.UpdateTag(image);

            var contact = content.Descendants("contact").FirstOrDefault();
            if (contact != null)
                ContactInfo.addContactInfo(context, contact);

            output.Content.SetContent(content.Root.ToString().Replace("<root>", "").Replace("</root>", ""));
        }
    }
}