using System.Text.Json.Serialization;

namespace CymaxAssessment.Client.Models
{
    public class RequestModelAPI1
    {
        [JsonPropertyName("contactAddress")]
        public string ContactAddress { get; set; }

        [JsonPropertyName("warehouseAddress")]
        public string WarehouseAddress { get; set; }

        [JsonPropertyName("packageDimensions")]
        public int[] PackageDimensions { get; set; }
    }
}
