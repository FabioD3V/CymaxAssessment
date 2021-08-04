using CymaxAssessmentAPI.BaseModels;

namespace CymaxAssessmentAPI.Models
{
    public class ClientRequestAPI2 : IBaseClientRequest
    {
        public string Consignee { get; set; }
        public string Consignor { get; set; }
        public int[] Cartons { get; set; }

        public BaseClientRequest GetMappedRequest()
        {
            return new BaseClientRequest()
            {
                SourceAddress = Consignee,
                DestinationAddress = Consignor,
                CartonDimensions = Cartons
            };
        }
    }
}
