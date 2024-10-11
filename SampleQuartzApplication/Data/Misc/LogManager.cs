using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Data.Misc
{
    public static class LogType
    {
        public static string SUCCESS = "SUCCESS";
        public static string ERROR = "ERROR";
        public static string INFO = "INFO";
    }
    public sealed class LogManager
    {
        private static LogManager? _instance = null;

        public static LogManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LogManager();
                }
                return _instance;
            }
        }
        private static string GetLogFolderPath()
        {
            // Combine the documents path with the folder name
            string folderPath = ConfigReader.getConfig<string>("LogDirectory");

            return folderPath;
        }
        public void createLog(
           string type,
           string content,
           [CallerFilePath] string file = "",
           [CallerLineNumber] int lineNumber = 0,
           [CallerMemberName] string caller = null)
        {
            if (file != null)
            {
                var fileList = file.Split('\\');
                file = fileList.Last();
            }

            string filePath = GetLogFolderPath() + "\\ServiceLog_" + System.DateTime.Today.ToString("yyyy-MM-dd_HH") + ".txt";
            content = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {type}\t{file}_{caller}:{lineNumber} - {content}";

            Console.WriteLine(content);

            try
            {
                // Check if the file exists
                if (!File.Exists(filePath))
                {
                    // If the file doesn't exist, create it
                    using (StreamWriter writer = File.CreateText(filePath))
                    {
                        writer.WriteLine(content);
                    }
                }
                else
                {
                    // If the file exists, append content to it
                    using (StreamWriter writer = File.AppendText(filePath))
                    {
                        writer.WriteLine(content);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
