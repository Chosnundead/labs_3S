using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_12.Classes
{
    public class XXXLog
    {
        private string fileName = "xxxlogfile.txt";
        private string path = @"Files\";
        private FileInfo? fileInfo;

        public XXXLog(string logFileName)
        {
            fileName = logFileName;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            fileInfo = new FileInfo($"{path}{fileName}");
            if (!fileInfo.Exists)
            {
                fileInfo.Delete();
                fileInfo.Create().Close();
            }
        }
        public XXXLog(string logFileName, string logPath)
        {
            fileName = logFileName;
            path = logPath;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            fileInfo = new FileInfo($"{path}{fileName}");
            if (!fileInfo.Exists)
            {
                fileInfo.Delete();
                fileInfo.Create().Close();
            }
        }
        public XXXLog()
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            fileInfo = new FileInfo($"{path}{fileName}");
            if (!fileInfo.Exists)
            {
                fileInfo.Delete();
                fileInfo.Create().Close();
            }
        }

        public static string getFullDate()
        {
            return $"{DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year} {DateTime.Now.TimeOfDay}";
        }

        public void writeLog(string text)
        {
            File.AppendAllText(fileInfo.FullName, XXXLog.getFullDate() + ": " + text);
        }
        public void writelnLog(string text)
        {
            File.AppendAllText(fileInfo.FullName, XXXLog.getFullDate() + ": " + text + "\n");
        }
        public void writeArrayLog(string[] arr)
        {
            foreach (var item in arr)
            {
                File.AppendAllText(fileInfo.FullName, XXXLog.getFullDate() + ": " + item);
            }
        }
    }
}