using System.Xml.Serialization;

namespace Laboratory_of_Inventions_RabbitMQ.Models
{
    [Serializable]
    public class RapidControlStatus
    {
        public RapidControlStatus() { }
        public CombinedSamplerStatus? CombinedSamplerStatus { get; set; }
        public CombinedPumpStatus? CombinedPumpStatus { get; set; }
        public CombinedOvenStatus? CombinedOvenStatus { get; set; }
    }
}
