using System.Linq;
using System.Xml.Linq;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using Web.Models;

namespace Web.Helpers
{
    [TargetElement("contact", Attributes = "for")]
    public class ContactInfo : TagHelper
    {
        public string For { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.Add("class", "contact");
            output.TagMode = TagMode.StartTagAndEndTag;

            var page = new DB().Pages.First(p => p.Category.Matches("Resume") && p.URL.Matches("Contact"));
            var content = XDocument.Parse(page.Body);

            output.Content.SetContent(string.Join("", content.ToString()));
        }

        public static void addContactInfo(TagHelperContext context, XElement contact)
        {
            var contactOutput = new TagHelperOutput("contact", new TagHelperAttributeList());
            new ContactInfo().Process(context, contactOutput);
            contact.Name = "div";
            contact.Add(new XAttribute("class", "contact"));
            contact.Add(XElement.Parse(contactOutput.Content.GetContent()));
        }
    }
}