using Laboratory_of_Inventions_RabbitMQ.Models;

namespace Laboratory_of_Inventions_RabbitMQ.Services
{
    public class TaskService<T>
    {
        public static Task<T> StartNewTask(Func<T> func)
        {
            return Task<T>.Factory.StartNew(func);
        }
        public static Task<InstrumentStatus?> StartNewTask(string filename)
        {
            return Task<InstrumentStatus?>.Factory.StartNew(() => FileParserService.ReadAndParseXml(filename));
        }
    }
}
