using CymaxAssessmentAPI.BaseModels;

namespace CymaxAssessmentAPI.Models
{
    public class ClientRequestAPI1 : IBaseClientRequest
    {
        public string ContactAddress { get; set; }
        public string WarehouseAddress { get; set; }
        public int[] PackageDimensions { get; set; }

        public BaseClientRequest GetMappedRequest()
        {
            return new BaseClientRequest()
            {
                SourceAddress = ContactAddress,
                DestinationAddress = WarehouseAddress,
                CartonDimensions = PackageDimensions
            };
        }
    }
}
