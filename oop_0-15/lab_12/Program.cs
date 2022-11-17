using System;
using lab_12.Classes;
using System.Text.RegularExpressions;
class Program
{
    static void Main(string[] args)
    {
        var log = new XXXLog();
        var diskInfo = new XXXDiskInfo(DriveInfo.GetDrives().First());
        log.writelnLog(diskInfo.getFreeSpace());
        log.writelnLog(diskInfo.getFileSystem());
        log.writeArrayLog(XXXDiskInfo.getFullInfo());
        var fileInfo = new XXXFileInfo(new FileInfo(@"Files\test.txt"));
        log.writelnLog(fileInfo.getFullPath());
        log.writeArrayLog(fileInfo.getFullName());
        log.writeArrayLog(fileInfo.getFullDate());
        var dirInfo = new XXXDirInfo(new DirectoryInfo(@"."));
        log.writelnLog(dirInfo.getFileQuantity());
        log.writelnLog(dirInfo.getCreationTime());
        log.writelnLog(dirInfo.getDirictoryQuantity());
        log.writeArrayLog(dirInfo.getParrentsDirictories());
        XXXFileManager.task_a();
        XXXFileManager.task_b();
        XXXFileManager.task_c();
        var fileArr = File.ReadAllLines(Path.GetFullPath(@"Files\xxxlogfile.txt"));
        Console.WriteLine($"Количество записей: {fileArr.Length}\n");
        Console.WriteLine($"Записи за этот час:");
        var temp = "";
        foreach (var item in fileArr)
        {
            if (!(new Regex(DateTime.Now.Hour.ToString() + @":\d{2}:\d{2}.\d{7}:").Match(item).Value.Equals("")))
            {
                Console.WriteLine(item);
                temp += item + "\n";
            }
            // if (!(new Regex(@"^\.*" + DateTime.Now.Hour + @":\d{2}:\d{2}.\d{7}:").Match(item).Value.Equals("")))
            // {
            //     Console.WriteLine(item);
            // }
            File.WriteAllText(@"Files\xxxlogfile.txt", temp);
        }
        using (var stream = new FileStream(@"Files\xxxlogfile.txt", FileMode.Open))
        {
            //Пример использования using
            Console.WriteLine("Можно ли читать?: " + stream.CanRead.ToString());
        }

    }
}