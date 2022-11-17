using System;
using lab_12.Classes;
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
    }
}