using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_12.Classes
{
    public class XXXDiskInfo
    {
        private DriveInfo driveInfo;
        public XXXDiskInfo(DriveInfo driveInfo)
        {
            this.driveInfo = driveInfo;
        }
        public string getFreeSpace()
        {
            return "Free space: " + driveInfo.TotalFreeSpace.ToString();
        }

        public string getFileSystem()
        {
            return "File system: " + driveInfo.DriveFormat;
        }
        public static string[] getFullInfo()
        {
            var list = new List<string>();
            foreach (var item in DriveInfo.GetDrives())
            {
                list.Add("Name: " + item.Name + "\n");
                list.Add("Total size: " + item.TotalSize + "\n");
                list.Add("Free space: " + item.TotalFreeSpace + "\n");
                list.Add("Mark: " + item.VolumeLabel + "\n");
            }
            return list.ToArray();
        }
    }
}