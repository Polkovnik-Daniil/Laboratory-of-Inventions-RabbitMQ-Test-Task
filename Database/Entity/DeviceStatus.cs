using System.ComponentModel.DataAnnotations.Schema;

namespace Laboratory_of_Inventions_RabbitMQ.Database.Entity
{
    public class DeviceStatus
    {
        public string ModuleCategoryID;
        public EModuleState ModuleState;
        [NotMapped]
        public int IndexWithinRole;
        [NotMapped]
        public string RapidControlStatus;
    }
}
