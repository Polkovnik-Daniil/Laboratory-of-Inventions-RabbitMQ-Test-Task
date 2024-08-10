using System.ComponentModel.Design.Serialization;
using System.Xml.Serialization;

namespace Laboratory_of_Inventions_RabbitMQ.Models
{
    [Serializable]
    [XmlRoot("CombinedPumpStatus")]
    public class CombinedPumpStatus
    {
        public CombinedPumpStatus() { }
        public EModuleState ModuleState;
        public bool IsBusy;
        public bool IsReady;
        public bool IsError;
        public bool KeyLock;
        public string Mode;
        public int Flow;
        public int PercentB;
        public int PercentC;
        public int PercentD;
        public int MinimumPressureLimit;
        public double MaximumPressureLimit;
        public int Pressure;
        public bool PumpOn;
        public int Channel;
    }
}