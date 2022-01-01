using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using ActionRunnerProject;
using AssemblyReaderProject;
using NativeBufferProject;
using Mutex = MutexProject.Mutex;

namespace TaskQueueProject
{
    static class Program
    {
        static void Main(string[] args)
        {
            switch (Int32.Parse(args[0]))
            {
                case 1:
                    Task1();
                    break;
                case 2:
                    Task2();
                    break;
                case 3:
                    Task3();
                    break;
                case 4:
                    Task4();
                    break;
                case 5:
                    Task5();
                    break;
            }
        }

        static void Task1()
        {
            TaskQueue taskQueue = new TaskQueue(2);
            taskQueue.EnqueueTask(() => { TaskQueue.WriteThreadMessage("Task 1 executing..."); });
            taskQueue.EnqueueTask(() => { TaskQueue.WriteThreadMessage("Task 2 executing..."); });
            taskQueue.EnqueueTask(() => { TaskQueue.WriteThreadMessage("Task 3 executing..."); });
            taskQueue.EnqueueTask(() => { TaskQueue.WriteThreadMessage("Task 4 executing..."); });
            taskQueue.EnqueueTask(() => { TaskQueue.WriteThreadMessage("Task 5 executing..."); });
            taskQueue.ExecuteAndStop();
        }

        private static void Task2()
        {
            Mutex mutex = new Mutex();
            Thread thread1 = new Thread(() =>
            {
                mutex.Lock();
                Console.WriteLine("Hello from thread 1");
                mutex.Unlock();
            });
            Thread thread2 = new Thread(() =>
            {
                mutex.Lock();
                Console.WriteLine("Hello from thread 2");
                mutex.Unlock();
            });
            thread1.Start();
            thread2.Start();
        }

        private static void Task3()
        {
            ActionRunner.RunAndWaitAll(new List<TaskDelegate>
            {
                () => TaskQueue.WriteThreadMessage(new Random().Next() + ""),
                () => TaskQueue.WriteThreadMessage(new Random().Next() + ""),
                () => TaskQueue.WriteThreadMessage(new Random().Next() + ""),
                () => TaskQueue.WriteThreadMessage(new Random().Next() + ""),
                () => TaskQueue.WriteThreadMessage(new Random().Next() + ""),
                () => TaskQueue.WriteThreadMessage(new Random().Next() + ""),
                () => TaskQueue.WriteThreadMessage(new Random().Next() + ""),
                () => TaskQueue.WriteThreadMessage(new Random().Next() + ""),
                () => TaskQueue.WriteThreadMessage(new Random().Next() + ""),
                () => TaskQueue.WriteThreadMessage(new Random().Next() + ""),
                () => TaskQueue.WriteThreadMessage(new Random().Next() + ""),
                () => TaskQueue.WriteThreadMessage(new Random().Next() + ""),
                () => TaskQueue.WriteThreadMessage(new Random().Next() + ""),
                () => TaskQueue.WriteThreadMessage(new Random().Next() + ""),
                () => TaskQueue.WriteThreadMessage(new Random().Next() + ""),
                () => TaskQueue.WriteThreadMessage(new Random().Next() + ""),
                () => TaskQueue.WriteThreadMessage(new Random().Next() + ""),
                () => TaskQueue.WriteThreadMessage(new Random().Next() + ""),
                () => TaskQueue.WriteThreadMessage(new Random().Next() + ""),
                () => TaskQueue.WriteThreadMessage(new Random().Next() + ""),
                () => TaskQueue.WriteThreadMessage(new Random().Next() + ""),
            });
        }

        private static void Task4()
        {
            const int byteCount = 1000;
            var buffer = new NativeBuffer(byteCount);
            for (var i = 0; i < byteCount; i++)
            {
                Console.Write((char) Marshal.ReadByte(buffer.Handle + i));
            }

            Console.WriteLine();
            buffer.Dispose();
        }

        private static void Task5()
        {
          AssemblyReader
                .GetPublicTypes(
                    "E:\\University\\SSP\\AssemblyBrowserLab\\AssemblyBrowserLib\\bin\\Debug\\net5.0\\AssemblyBrowserLib.dll"
                )
                .Select(type => type)
                .OrderBy(type => type.Namespace)
                .ThenBy(type => type.FullName)
                .ToList()
                .ForEach(Console.WriteLine);
        }
    }
}