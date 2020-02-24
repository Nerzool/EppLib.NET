using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace EppLib.Extensions.Afilias.DomainCreate
{
    public class AssociationDomainCreateExtension : AssociationExtensionBase
    {
        public ContactType Type { get; set; }
        public string Id { get; set; }
        public string Pw { get; set; }

        public override XmlNode ToXml(XmlDocument doc)
        {
            var root = CreateElement(doc, "association:create");

            var contact = AddXmlElement(doc, root, "association:contact", null);

            contact.SetAttribute("type", Type.ToString());

            AddXmlElement(doc, contact, "association:id", Id);

            var autinfo = AddXmlElement(doc, contact, "association:authInfo", null);

            AddXmlElement(doc, autinfo, "association:pw", Pw);

            return root;
        }
    }
}
