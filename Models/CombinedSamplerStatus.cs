using Microsoft.Identity.Client;
using System.Xml.Serialization;

namespace Laboratory_of_Inventions_RabbitMQ.Models
{
    [Serializable]
    [XmlRoot("CombinedSamplerStatus")]
    public class CombinedSamplerStatus
    {
        public CombinedSamplerStatus() { }
        public EModuleState ModuleState;
        public bool IsBusy;
        public bool IsReady;
        public bool IsError;
        public bool KeyLock;
        public int Status;
        public string Vial;
        public int Volume;
        public int MaximumInjectionVolume;
        public string RackL;
        public string RackR;
        public int RackInf;
        public bool Buzzer;
    }
}
