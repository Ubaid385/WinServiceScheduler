using Data.Misc;
using Topshelf.Logging;

namespace SampleQuartzApplication
{
    public class Service
    { 
        public Service() { }

        public void Start()
        {
            // Combine the documents path with the folder name
            string folderPath = ConfigReader.getConfig<string>("LogDirectory");

            // Check if the folder already exists
            if (Directory.Exists(folderPath))
            {
                Console.WriteLine("Folder already exists. Skipping creation.");
            }
            else
            {
                // Create the folder
                Directory.CreateDirectory(folderPath);
                Console.WriteLine("Folder created successfully.");
                HostLogger.Get<Service>().Info("Log Folder created successfully.");

                new CronJobInitializer();
            }
        }
        public void Stop()
        {
            // STOP TCP SERVER & CLIENT
            //TCPHandler.Shutdown();
        }
    }
}
