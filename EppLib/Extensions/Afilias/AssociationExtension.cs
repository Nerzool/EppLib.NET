using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace EppLib.Extensions.Afilias
{
    public class AssociationExtension : AssociationExtensionBase
    {
        private ContactType _type;
        public string _id;
        public string _pw;

        public AssociationExtension(string id, string pw, ContactType type)
        {
            _id = id;
            _pw = pw;
            _type = type;
        }

        public override XmlNode ToXml(XmlDocument doc)
        {
            var root = CreateElement(doc, "association:create");

            var contact = AddXmlElement(doc, root, "association:contact", null);

            contact.SetAttribute("type", _type.ToString());

            AddXmlElement(doc, contact, "association:id", _id);

            var autinfo = AddXmlElement(doc, contact, "association:authInfo", null);

            AddXmlElement(doc, autinfo, "association:pw", _pw);

            return root;
        }
    }
}
