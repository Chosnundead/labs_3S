using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Concurrent;
using System.Threading;

namespace lab_15.Classes
{
    static public class Lab
    {
        static public void task_1()
        {
            var task = new Task(() =>
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                const int n = 16661;
                var list = new List<int>();
                for (int i = 1; i <= n; i++)
                {
                    list.Add(i);
                }
                for (int item = 2; item <= n; item++)
                {
                    for (int i = 2; i <= n; i++)
                    {
                        if ((i * item) > n)
                        {
                            break;
                        }
                        list.Remove(i * item);
                        // Console.WriteLine(i * item);
                    }
                }
                foreach (var item in list)
                {
                    Console.WriteLine(item);
                }
                stopwatch.Stop();
                Console.WriteLine($"Task took {stopwatch.ElapsedMilliseconds} ms.");
            });
            task.Start();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Id: {task.Id}\nIsCompleted: {task.IsCompleted}\nStatus: {task.Status}");
            Console.ResetColor();
            task.Wait();
        }
        static public void task_2()
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            var task = new Task(() =>
            {
                var list = new List<int>();
                for (int i = 0; i < 100000; i++)
                {
                    list.Add(i);
                    list[i] *= 2;
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Операция остановленна.");
                        return;
                    }
                    Thread.Sleep(100);
                }
            }, token);
            task.Start();
            Thread.Sleep(1000);
            cancelTokenSource.Cancel();
            Thread.Sleep(200);
            cancelTokenSource.Dispose(); // освобождаем ресурсы
            // task.Wait();
        }
        static public void task_3()
        {
            var task_1 = new Task<int>(() =>
            {
                return 1;
            });
            var task_2 = new Task<int>(() =>
            {
                Thread.Sleep(1000);
                return 1;
            });
            var task_3 = new Task<int>(() =>
            {
                Thread.Sleep(2000);
                return 1;
            });
            task_1.Start();
            task_2.Start();
            task_3.Start();
            Console.WriteLine($"Сумма трёх асинхронных единиц: {task_1.Result + task_2.Result + task_3.Result}");
        }
        static public void task_4()
        {
            Console.WriteLine($"\ntask_1:");
            Task task1 = new Task(() =>
            {
                Console.WriteLine($"Первая задача.");
            });
            // задача продолжения - task2 выполняется после task1
            Task task2 = task1.ContinueWith((Task t) =>
            {
                Console.WriteLine($"Вторая задача.");
            });
            task1.Start();
            // ждем окончания второй задачи
            task2.Wait();
            Console.WriteLine($"\ntask_2:");
            task1 = new Task(() =>
            {
                Console.WriteLine($"Первая задача.");
            });
            // задача продолжения - task2 выполняется после task1
            task2 = new Task(() =>
            {
                Console.WriteLine($"Вторая задача.");
            });
            task1.GetAwaiter().OnCompleted(() =>
            {
                task2.Start();
            });
            task1.Start();
            task2.Wait();
        }
        static public void task_5()
        {
            List<int> test = new List<int>();
            for (int i = 0; i < 100000; i++)
            {
                test.Add(i);
            }
            List<int> list_1 = new List<int>();
            List<int> list_2 = new List<int>();
            Parallel.Invoke(
                () =>
                {
                    Parallel.For(1, 100000, (int n) => { list_1.Add(n); });
                    Thread.Sleep(1000);
                    Console.WriteLine("Parallel.For Завершен.");
                },
                () =>
                {
                    Parallel.ForEach<int>(test, (int n) => { list_2.Add(n); });
                    Thread.Sleep(2000);
                    Console.WriteLine("Parallel.ForEach Завершен.");
                }
            );
        }
        static public void task_6()
        {
            Parallel.Invoke(
                () =>
                {
                    for (int i = 0; i < 1000; i += 2)
                    {
                        Console.WriteLine(i);
                    }
                },
                () =>
                {
                    for (int i = 1; i < 1000; i += 2)
                    {
                        Console.WriteLine(i);
                    }
                }
            );
        }
        static public void task_7()
        {
            using (BlockingCollection<string> bc = new BlockingCollection<string>())
            {
                // Spin up a Task to populate the BlockingCollection
                Task t1 = Task.Run(() =>
                {
                    bc.Add("Нож");
                    Thread.Sleep(new Random().Next(50, 200));
                    bc.Add("Пистолет");
                    Thread.Sleep(new Random().Next(50, 200));
                    bc.Add("Люгерные партроны");
                    Thread.Sleep(new Random().Next(50, 200));
                    bc.Add("Рация");
                    Thread.Sleep(new Random().Next(50, 200));
                    bc.Add("Бронижелет");
                    bc.CompleteAdding();
                });

                // Spin up a Task to consume the BlockingCollection
                Task t2 = Task.Run(() =>
                {
                    try
                    {
                        // Consume consume the BlockingCollection
                        while (true)
                        {
                            Console.WriteLine("\nНа складе:");
                            foreach (var item in bc)
                            {
                                Console.WriteLine(item);
                            }
                            Thread.Sleep(200);
                            Console.WriteLine($"\n{bc.Take()} был куплен!");
                        };
                    }
                    catch (InvalidOperationException)
                    {
                        // An InvalidOperationException means that Take() was called on a completed collection
                        Console.WriteLine("\n\nВсе товары куплены!");
                    }
                });

                Task.WhenAll(t1, t2).Wait();
            }
        }
        static public void task_8()
        {
            async Task myTaskAsync()
            {
                await Task.Delay(666);
                for (int i = 0; i < 666; i++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("666");
                    Console.ResetColor();
                    Console.Write("Неужели это конец?");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("666\n");
                    Console.ResetColor();
                }
            }
            myTaskAsync().Wait();
        }
    }
}