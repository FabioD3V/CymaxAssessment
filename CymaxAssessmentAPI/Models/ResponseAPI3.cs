using System.Xml.Serialization;

namespace CymaxAssessmentAPI.Models
{
    [XmlRoot(ElementName = "Response")]
	public class ResponseAPI3
	{
		[XmlElement(ElementName = "quote")]
		public string Quote { get; set; }
	}
}
