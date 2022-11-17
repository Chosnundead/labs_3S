using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_12.Classes
{
    public static class XXXFileManager
    {
        public static void task_a()
        {
            var drives = DriveInfo.GetDrives();
            string files = "";
            string dirs = "";
            foreach (var item in drives)
            {
                if (item.Name.ToLower().Contains("d"))
                {
                    foreach (var file in item.RootDirectory.GetFiles())
                    {
                        files += file.Name + "\n";
                    }
                    foreach (var dir in item.RootDirectory.GetDirectories())
                    {
                        dirs += dir.Name + "\\\n";
                    }
                    break;
                }
            }
            string fileManager = dirs + files;
            var dirictoryInfo = new DirectoryInfo("XXXInspect");
            if (!dirictoryInfo.Exists)
            {
                dirictoryInfo.Create();
            }
            File.WriteAllText(@"XXXInspect\XXXInspect.txt", fileManager);
            File.Delete(@"XXXInspect\XXXInspect.txt");
            File.WriteAllText(@"XXXInspect\XXXInspect_copy.txt", fileManager);
        }

        public static void task_b()
        {
            var dirictoryInfo = new DirectoryInfo("XXXFiles");
            if (!dirictoryInfo.Exists)
            {
                dirictoryInfo.Create();
            }
            var dirictoryInfoToCopy = new DirectoryInfo("Files");
            foreach (var item in dirictoryInfoToCopy.GetFiles())
            {
                if (item.Name.Contains(".txt"))
                {
                    try
                    {
                        item.CopyTo($"XXXFiles\\{item.Name}");
                    }
                    catch (Exception e) { }
                }
            }
            var lastDirictoryInfo = new DirectoryInfo("XXXInspect\\XXXFiles");
            lastDirictoryInfo.Create();
            foreach (var item in dirictoryInfo.GetFiles())
            {
                try
                {
                    item.CopyTo($"XXXInspect\\XXXFiles\\{item.Name}");
                }
                catch (Exception e) { }
            }
            dirictoryInfo.Delete(true);
        }

        public static void task_c()
        {
            //to continue
        }
    }
}