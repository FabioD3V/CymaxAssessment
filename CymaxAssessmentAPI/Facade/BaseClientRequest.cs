namespace CymaxAssessmentAPI.Facade
{
    public class BaseClientRequest
    {
        public string SourceAddress { get; set; }
        public string DestinationAddress { get; set; }
        public int[] CartonDimensions { get; set; }
    }
}
