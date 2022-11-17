using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace lab_12.Classes
{
    public class XXXFileInfo
    {
        private FileInfo fileInfo;

        public XXXFileInfo(FileInfo fileInfo)
        {
            this.fileInfo = fileInfo;
        }

        public string getFullPath()
        {
            return fileInfo.FullName;
        }

        public string[] getFullName()
        {
            var regex = new Regex(@"\.(\w*)");
            var list = new List<string>();
            list.Add(fileInfo.Length.ToString() + "\n");
            list.Add(regex.Match(fileInfo.Name).Value + "\n");
            list.Add(fileInfo.Name + "\n");
            return list.ToArray();
        }

        public string[] getFullDate()
        {
            var list = new List<string>();
            list.Add(fileInfo.CreationTimeUtc.ToString() + "\n");
            list.Add(fileInfo.LastWriteTimeUtc.ToString() + "\n");
            return list.ToArray();
        }
    }
}