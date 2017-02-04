using System.Linq;
using System.Xml.Linq;

namespace Web.Helpers
{
    public static class SVGExtensions
    {
        public static string CleanSVG(this string original)
        {
            var xml = XDocument.Parse(original).Root;

            xml.Attributes().ToList().Where(attributesShouldBeRemoved).Remove();
            xml.Descendants().Where(elementShouldBeRemoved).Remove();
            foreach (var style in xml.Descendants().Attributes().Where(a => a.Name.LocalName == "style"))
                style.Value = "fill:#fff";

            return xml.ToString();
        }

        private static bool elementShouldBeRemoved(XElement e)
        {
            return e.Name.NamespaceName.Contains("sodipodi")
            || e.Name.LocalName == "defs"
            || e.Name.LocalName == "metadata";
        }

        private static bool attributesShouldBeRemoved(XAttribute a)
        {
            return a.Name.NamespaceName.Contains("xmlns")
                || a.Name.NamespaceName.Contains("sodipodi")
                || a.Name.NamespaceName.Contains("inkscape")
                || a.Name.LocalName.Contains("sodipodi")
                || a.Name.LocalName.Contains("inkscape")
                || a.Name == "width"
                || a.Name == "height";
        }
    }
}