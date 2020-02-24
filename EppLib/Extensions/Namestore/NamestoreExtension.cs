using System.Xml;

namespace EppLib.Extensions.Namestore
{
    public class NamestoreExtension : NamestoreExtensionBase
    {
        public NamestoreExtension(string subproduct)
        {
            SubProduct = subproduct;
        }

        public string SubProduct;

        public override XmlNode ToXml(XmlDocument doc)
        {
            var root = CreateElement(doc, "namestoreExt");

            if (SubProduct != null)
            {
                AddXmlElement(doc, root, "namestoreExt:subProduct", $"dot{SubProduct}");
            }

            return root;
        }
    }
}
