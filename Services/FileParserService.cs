using Serilog;
using System.Text;
using System.Xml.Linq;
using System.Text.Json;
using System.Xml.Serialization;
using Laboratory_of_Inventions_RabbitMQ.Models;

namespace Laboratory_of_Inventions_RabbitMQ.Services
{
    public class FileParserService
    {
        public static InstrumentStatus? Deserialize(XDocument doc)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(InstrumentStatus));
            using (var reader = doc.Root?.CreateReader())
            {
                return (InstrumentStatus?)xmlSerializer.Deserialize(reader);
            }
        }
        public static T? Deserialize<T>(XDocument doc)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (var reader = doc.Root?.CreateReader())
            {
                return (T?)xmlSerializer.Deserialize(reader);
            }
        }
        public static InstrumentStatus? ReadAndParseXml(string filename)
        {
            try
            {
                StringBuilder contents = new StringBuilder(File.ReadAllText(filename));
                // Commenting on unnecessary Xml declarations
                // Because xml file can`t have more than 1 xml declaration
                contents.Replace("<?", "<!--<?").Replace("?>", "?>-->");
                XDocument doc = XDocument.Parse(contents.ToString());
                InstrumentStatus instrumentStatus = Deserialize(doc);
                // Set another data
                foreach(var elem in instrumentStatus.DeviceStatus)
                {
                    if(elem.RapidControlStatus.CombinedPumpStatus != null)
                    {
                        elem.RapidControlStatus.CombinedPumpStatus.ModuleState = (EModuleState)Enum.GetValues(typeof(EModuleState)).GetValue(new Random().Next(0, 3));
                    }
                    if(elem.RapidControlStatus.CombinedOvenStatus != null)
                    {
                        elem.RapidControlStatus.CombinedOvenStatus.ModuleState = (EModuleState)Enum.GetValues(typeof(EModuleState)).GetValue(new Random().Next(0, 3));
                    }
                    if(elem.RapidControlStatus.CombinedSamplerStatus != null)
                    {
                        elem.RapidControlStatus.CombinedSamplerStatus.ModuleState = (EModuleState)Enum.GetValues(typeof(EModuleState)).GetValue(new Random().Next(0, 3));
                    }
                }
                Log.Information(contents.ToString());
                // Parse to Json
                string json = JsonSerializer.Serialize(instrumentStatus);
                return instrumentStatus;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
        }
    }
}
