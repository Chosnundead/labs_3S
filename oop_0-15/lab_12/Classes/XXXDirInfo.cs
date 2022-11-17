using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace lab_12.Classes
{
    public class XXXDirInfo
    {
        private DirectoryInfo directoryInfo;

        public XXXDirInfo(DirectoryInfo directoryInfo)
        {
            this.directoryInfo = directoryInfo;
        }

        public string getFileQuantity()
        {
            return directoryInfo.GetFiles().Length.ToString();
        }

        public string getCreationTime()
        {
            return directoryInfo.CreationTimeUtc.ToString();
        }

        public string getDirictoryQuantity()
        {
            return directoryInfo.GetDirectories().Length.ToString();
        }

        public string[] getParrentsDirictories()
        {
            // return new Regex(@"\\.*\\").Match(directoryInfo.FullName).Value.Substring(1, new Regex(@"\\.*\\").Match(directoryInfo.FullName).Value.Length - 2).Split(@"\");
            return new Regex(@"\\.*\\").Match(directoryInfo.FullName).Value.Substring(1, new Regex(@"\\.*\\").Match(directoryInfo.FullName).Value.Length - 2).Replace(@"\", "\n.").Split(".");
        }
    }
}