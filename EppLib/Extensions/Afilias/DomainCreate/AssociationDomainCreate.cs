using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace EppLib.Extensions.Afilias.DomainCreate
{
    public class AssociationDomainCreate : Entities.DomainCreate
    {
        public ContactType Type { get; set; }
        public string Id { get; set; }
        public string Pw { get; set; }

        public AssociationDomainCreate(string domainName, string registrantContactId)
            : base(domainName, registrantContactId) { }

        public override XmlDocument ToXml()
        {
            var associationExtenstion = new AssociationDomainCreateExtension
            {
                Type = Type,
                Id = Id,
                Pw = Pw,
            };

            Extensions.Clear();
            Extensions.Add(associationExtenstion);

            return base.ToXml();
        }
    }
}
