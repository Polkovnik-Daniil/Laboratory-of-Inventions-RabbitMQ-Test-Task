using Laboratory_of_Inventions_RabbitMQ.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laboratory_of_Inventions_RabbitMQ.Database.Entity
{
    [Serializable]
    public class DeviceStatus
    {
        public string ModuleCategoryID { get; set; }
        [NotMapped]
        public int IndexWithinRole { get; set; }
        [NotMapped]
        public RapidControlStatus RapidControlStatus { get; set; }
        public DeviceStatus() { }
    }
}
