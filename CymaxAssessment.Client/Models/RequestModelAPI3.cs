using System.Collections.Generic;
using System.Xml.Serialization;

namespace CymaxAssessment.Client.Models
{
    [XmlRoot(ElementName = "ClientRequestAPI3")]
    public class RequestModelAPI3
    {
        [XmlElement("source")]
        public string Source { get; set; }

        [XmlElement("destination")]
        public string Destination { get; set; }

        [XmlElement("packages")]
        public Packages Packages { get; set; }
    }

    [XmlRoot(ElementName = "packages")]
    public class Packages
    {        
        [XmlElement("package")]
        public List<int> Package { get; set; }
    }
}
