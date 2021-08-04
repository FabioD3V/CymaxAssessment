using System.Text.Json.Serialization;

namespace CymaxAssessment.Client.Models
{
    public class RequestModelAPI2
    {
        [JsonPropertyName("consignee")]
        public string Consignee { get; set; }

        [JsonPropertyName("consignor")]
        public string Consignor { get; set; }

        [JsonPropertyName("cartons")]
        public int[] Cartons { get; set; }
    }
}
