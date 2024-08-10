using Laboratory_of_Inventions_RabbitMQ.Database.Entity;
using System.Xml.Serialization;

namespace Laboratory_of_Inventions_RabbitMQ.Models
{
    [Serializable]
    public class InstrumentStatus
    {
        public InstrumentStatus() { }
        public string PackageID { get; set; }
        [XmlElement("DeviceStatus")]
        public List<DeviceStatus> DeviceStatus { get; set; } = new List<DeviceStatus>();
    }
}
