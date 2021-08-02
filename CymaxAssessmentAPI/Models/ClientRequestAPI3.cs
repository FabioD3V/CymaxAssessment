using CymaxAssessmentAPI.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CymaxAssessmentAPI.Models
{
    [DataContract]
    public class ClientRequestAPI3 : IBaseClientRequest
    {
        [DataMember]
        [XmlElement("source")]
        public string Source { get; set; } = string.Empty;

        [DataMember]
        [XmlElement("destination")]
        public string Destination { get; set; } = string.Empty;

        [DataMember]
        [XmlElement("packages")]
        public Packages[] Packages { get; set; } = new Packages[] { };

        public BaseClientRequest GetMappedRequest()
        {
            List<int> packageArray = new List<int>();
            foreach (var pkg in Packages)
            {
                packageArray.Add(pkg.Package);
            }

            return new BaseClientRequest()
            {
                SourceAddress = Source,
                DestinationAddress = Destination,
                CartonDimensions = packageArray.ToArray()
            };
        }
    }
    
    public class Packages
    {
        [DataMember]
        [XmlElement("package")]
        public int Package { get; set; }
    }
}
