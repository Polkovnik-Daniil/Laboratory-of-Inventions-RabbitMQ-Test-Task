using System.Xml.Serialization;

namespace Laboratory_of_Inventions_RabbitMQ.Models
{
    [Serializable]
    [XmlRoot("CombinedOvenStatus")]
    public class CombinedOvenStatus
    {
        public CombinedOvenStatus() { }
        public EModuleState ModuleState;
        public bool IsBusy;
        public bool IsReady;
        public bool IsError;
        public bool KeyLock;
        public bool UseTemperatureControl;
        public bool OvenOn;
        public double Temperature_Actual;
        public double Temperature_Room;
        public int MaximumTemperatureLimit;
        public int Valve_Position;
        public int Valve_Rotations;
        public bool Buzzer;        
    }
}