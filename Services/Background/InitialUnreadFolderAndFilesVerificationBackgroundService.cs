using Serilog;

namespace Laboratory_of_Inventions_RabbitMQ.Services.Background
{
    public class InitialUnreadFolderAndFilesVerificationBackgroundService : BackgroundService
    {
        private readonly ILogger<FileWatcherBackgroundService> _logger;
        private readonly string _unreadXmlFolderPath;
        private readonly string _readXmlFolderPath;
        public InitialUnreadFolderAndFilesVerificationBackgroundService(ILogger<FileWatcherBackgroundService> logger, IConfiguration configuration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _readXmlFolderPath = configuration["AppSettings:ReadXmlPath"] ?? throw new ArgumentNullException(nameof(configuration));
            _unreadXmlFolderPath = configuration["AppSettings:UnreadXmlPath"] ?? throw new ArgumentNullException(nameof(configuration));
        }
        private void CreateOrCheckFolder(string path)
        {
            bool IsExistsDirectory = Directory.Exists(path);
            if (!IsExistsDirectory)
                Directory.CreateDirectory(path);
        }
        private void CheckUnreadFolder()
        {
            List<string> fileArray = Directory.GetFiles(_unreadXmlFolderPath).ToList();
            bool IsFolderContainFiles = fileArray.Count > 0;
            if (IsFolderContainFiles)
            {
                MoveUnreadFiles(fileArray);
            }
        }
        private void MoveUnreadFiles(List<string> fileArray)
        {
            foreach (string file in fileArray)
            {

                // move file to readXml folder
                File.Move(file, _unreadXmlFolderPath + "~" + Path.GetFileName(file), true);
                // returning the file back 
                //File.Move(_readXmlFolderPath + Path.GetFileNameWithoutExtension(file) + Path.GetExtension(file), file);
            }
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation("Start InitialUnreadFolderAndFilesVerificationService.");
                CreateOrCheckFolder(_readXmlFolderPath);
                CreateOrCheckFolder(_unreadXmlFolderPath);
                // Delay to start the FileWatcherService
                await Task.Delay(200);
                CheckUnreadFolder();
                _logger.LogInformation("Stop InitialUnreadFolderAndFilesVerificationService.");
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}
