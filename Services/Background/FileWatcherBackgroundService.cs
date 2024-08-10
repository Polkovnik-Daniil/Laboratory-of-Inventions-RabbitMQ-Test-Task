using Laboratory_of_Inventions_RabbitMQ.Models;

namespace Laboratory_of_Inventions_RabbitMQ.Services.Background
{
    public class FileWatcherBackgroundService : BackgroundService
    {
        private readonly FileSystemWatcher _watcher;
        private readonly ILogger<FileWatcherBackgroundService> _logger;
        private readonly string _unreadXmlPathFolder;
        private readonly string _readXmlPathFolder;
        public FileWatcherBackgroundService(ILogger<FileWatcherBackgroundService> logger, IConfiguration configuration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _readXmlPathFolder = configuration["AppSettings:ReadXmlPath"] ?? throw new ArgumentNullException(nameof(configuration));
            _unreadXmlPathFolder = configuration["AppSettings:UnreadXmlPath"] ?? throw new ArgumentNullException(nameof(configuration));
            _watcher = new FileSystemWatcher(_unreadXmlPathFolder) ?? throw new ArgumentNullException(nameof(_unreadXmlPathFolder));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Start FileWatcherService.");
            _watcher.Created += StartTask;
            _watcher.Renamed += StartTask;
            _watcher.Changed += StartTask;
            _watcher.Deleted += (o, e) => _logger.LogInformation($"{DateTime.Now} {e.ChangeType}: {e.FullPath}");
            _watcher.Error += StartTaskError;
            _watcher.EnableRaisingEvents = true;   // включаем события
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
            await Task.CompletedTask;
        }
        // TODO: перенести в отдельный сервис
        private async void StartTask(object sender, FileSystemEventArgs e)
        {
            try
            {
                _logger.LogInformation($"{DateTime.Now} {e.ChangeType}: {e.FullPath}");
                // Check to xml extension
                bool IsXmlExtension = Path.GetExtension(e.FullPath).Equals(".xml");
                if (!IsXmlExtension)
                {
                    _logger.LogError("The file has an invalid extension");
                    File.Delete(e.FullPath);
                    throw new Exception("The file has an invalid extension");
                }
                // Complete main task
                var task = await TaskService<InstrumentStatus?>.StartNewTask(() => FileParserService.ReadAndParseXml(e.FullPath));
                // change the location of the read file
                string pathTo = _readXmlPathFolder + Path.GetFileName(e.FullPath);
                File.Move(e.FullPath, pathTo);
                // TODO:
                // Send to RabbitMQ

                return;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return;
            }
        }
        private void StartTaskError(object sender, ErrorEventArgs e)
        {
            _logger.LogError($"{DateTime.Now} Error: {e.GetException().Message}");
        }
    }
}