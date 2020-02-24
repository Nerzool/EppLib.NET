using EppLib.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EppLib.Extensions.Afilias
{
    public abstract class AssociationExtensionBase : EppExtension
    {
        private string _ns = "urn:afilias:params:xml:ns:association-1.0 association-1.0.xsd";

        protected override string Namespace
        {
            get { return _ns; }
            set { _ns = value; }
        }
    }
}
