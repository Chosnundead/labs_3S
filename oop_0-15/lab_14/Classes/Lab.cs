using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace lab_14.Classes
{
    static public class Lab
    {
        static public void task_1()
        {
            Console.WriteLine($"\ntask_1:");
            var processes = Process.GetProcesses();
            foreach (var item in processes)
            {
                //access is denied -> нельзя вывести остальную инфу, логично что не системное приложение не имеет полного доступа к процессам.
                // Console.WriteLine($"{item.Id}: {item.ProcessName}, priority: {item.BasePriority}; startTime: {item.StartTime}; isExited: {item.HasExited}; totalProcessTime: {item.TotalProcessorTime}.");
                Console.WriteLine($"{item.Id}: {item.ProcessName}, priority: {item.BasePriority};");
            }
        }
        static public void task_2()
        {
            Console.WriteLine($"\ntask_2:");
            var domain = AppDomain.CurrentDomain;
            Console.Write($"name: {domain.FriendlyName}; frameworkDetails: {domain.SetupInformation.TargetFrameworkName}; applicationBase: {domain.SetupInformation.ApplicationBase}; assemblies: ");
            foreach (var item in domain.GetAssemblies())
            {
                Console.Write($"{item.FullName}, ");
            }
            // // Secondary AppDomains are not supported on this platform. -> Нельзя создать ещё один домен
            Console.WriteLine($"\nВторая часть задачи: ");
            Console.WriteLine($"var newDomain = AppDomain.CreateDomain(\"newDomain\");");
            Console.WriteLine($"var assembly = newDomain.Load(\"System.Xml\");");
            Console.WriteLine($"AppDomain.Unload(newDomain);");
            // // Создадим новый домен приложения
            // AppDomain newDomain = AppDomain.CreateDomain("newDomain");
            // // Уничтожение домена приложения
            // var assembly = newDomain.Load("lab_11");
            // var typeOfAssembly = assembly.GetType("Time");
            // var methodToExec = typeOfAssembly.GetMethod("info");
            // object obj = Activator.CreateInstance(typeOfAssembly);
            // // Execute the method.
            // methodToExec.Invoke(obj, null);
            // AppDomain.Unload(newDomain);
            // // Use the file name to load the assembly into the current
            // application domain.
            // Assembly a = Assembly.Load("System.Console");
            // // Get the type to use.
            // Type myType = a.GetType("XXX");
            // // Get the method to call.
            // MethodInfo myMethod = myType.GetMethod("WriteLine");
            // // Create an instance.
            // object obj = Activator.CreateInstance(myType);
            // // Execute the method.
            // myMethod.Invoke(obj, new object[] { "sss" });
            // AppDomain.CurrentDomain.Load("System.Xml");
            // var type = Type.GetType("XmlDocument");
            // object xmlDoc = Activator.CreateInstance(type);
            // Console.WriteLine(xmlDoc.ToString());
        }
        static public void task_3()
        {
            Console.Write("n == ");
            int n = Convert.ToInt32(Console.ReadLine());
            File.WriteAllText(@"Files\write.txt", "");
            var thread_1 = new Thread(() =>
            {
                for (int i = 1; i <= n; i++)
                {
                    Console.WriteLine(i);
                    Console.WriteLine($"Отстановка на {i * 5}ms.");
                    Thread.Sleep(i * 5);
                }
            });
            var thread_2 = new Thread(() =>
            {
                for (int i = 1; i <= n; i++)
                {
                    File.AppendAllText(@"Files\write.txt", $"{i}\n");
                }
            });
            thread_1.Start();
            thread_2.Start();
            try
            {
                Thread.Sleep(600);
                Console.WriteLine($"ThreadState: {thread_1.ThreadState}");
                Thread.Sleep(100);
                Console.WriteLine($"Name: {thread_1.Name}");
                Thread.Sleep(100);
                Console.WriteLine($"Priority: {thread_1.Priority}");
                Thread.Sleep(100);
                Console.WriteLine($"ManagedThreadId: {thread_1.ManagedThreadId}");
                Thread.Sleep(100);
                Console.WriteLine("thread_1 Остановлен!");
                thread_1.Interrupt();
            }
            catch (Exception e) { }
        }
        static public void task_4()
        {
            Console.Write("n == ");
            int n = Convert.ToInt32(Console.ReadLine());
            File.WriteAllText(@"Files\numbers.txt", "");
            var thread_1 = new Thread(() =>
            {
                for (int i = 1; i < n; i += 2)
                {
                    Console.WriteLine(i);
                    File.AppendAllText(@"Files\numbers.txt", $"{i}\n");
                    Thread.Sleep(21);
                }
            });
            var thread_2 = new Thread(() =>
            {
                for (int i = 0; i < n; i += 2)
                {
                    Console.WriteLine(i);
                    File.AppendAllText(@"Files\numbers.txt", $"{i}\n");
                    Thread.Sleep(i);
                }
            });
            Console.WriteLine("\ntask_a:");
            thread_1.Priority = ThreadPriority.AboveNormal;
            thread_1.Start();
            thread_2.Start();
            Thread.Sleep(1000);
            try
            {
                thread_1.Interrupt();
                thread_2.Interrupt();
            }
            catch (Exception e) { }
            object locker = new();
            thread_1 = new Thread(() =>
            {
                lock (locker)
                {
                    for (int i = 1; i < n; i += 2)
                    {
                        Console.WriteLine(i);
                        File.AppendAllText(@"Files\numbers.txt", $"{i}\n");
                        Thread.Sleep(21);
                    }
                }
            });
            thread_2 = new Thread(() =>
            {
                lock (locker)
                {
                    for (int i = 0; i < n; i += 2)
                    {
                        Console.WriteLine(i);
                        File.AppendAllText(@"Files\numbers.txt", $"{i}\n");
                        Thread.Sleep(i);
                    }
                }
            });
            Console.WriteLine("\ntask_b_i:");
            thread_1.Start();
            thread_2.Start();
            Thread.Sleep(1000);
            try
            {
                thread_1.Interrupt();
                thread_2.Interrupt();
            }
            catch (Exception e) { }
            Mutex mutexObj = new();
            thread_1 = new Thread(() =>
            {
                for (int i = 1; i < n; i += 2)
                {
                    mutexObj.WaitOne();
                    Console.WriteLine(i);
                    File.AppendAllText(@"Files\numbers.txt", $"{i}\n");
                    Thread.Sleep(21);
                    mutexObj.ReleaseMutex();
                }
            });
            thread_2 = new Thread(() =>
            {
                for (int i = 0; i < n; i += 2)
                {
                    mutexObj.WaitOne();
                    Console.WriteLine(i);
                    File.AppendAllText(@"Files\numbers.txt", $"{i}\n");
                    Thread.Sleep(i);
                    mutexObj.ReleaseMutex();
                }
            });
            Console.WriteLine("\ntask_b_ii:");
            thread_1.Start();
            thread_2.Start();
            Thread.Sleep(1000);
            try
            {
                thread_1.Interrupt();
                thread_2.Interrupt();
            }
            catch (Exception e) { }
        }
        public static void task_5()
        {
            void counter(object obj)
            {
                for (int i = 1; i <= 9; i++)
                {
                    Console.WriteLine($"{i}");
                    Thread.Sleep(1000);
                }
            }
            int num = 0;
            TimerCallback tm = new TimerCallback(counter);
            Timer timer = new Timer(tm, num, 0, 9000);
            Console.ReadKey();
        }
    }
}
