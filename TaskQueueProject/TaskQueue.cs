using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;


namespace TaskQueueProject
{
    public delegate void TaskDelegate();

    public class TaskQueue
    {
        private bool _running = true;
        
        private readonly ConcurrentQueue<TaskDelegate> _taskPool = new();

        private readonly List<Thread> _threads = new();

        public TaskQueue(int threadCount)
        {
            for (var i = 0; i < threadCount; i++)
            {
                Thread thread = new Thread(() =>
                {
                    WriteThreadMessage("Started");
                    while (_running || !_taskPool.IsEmpty)
                    {
                        if (!_taskPool.TryDequeue(out var task)) continue;
                        WriteThreadMessage("Got task");
                        task();
                        WriteThreadMessage("Task executed");
                    }
                    WriteThreadMessage("Going down");
                });
                thread.Start();
                _threads.Add(thread);
            }
        }

        public static void WriteThreadMessage(string message)
        {
            Console.WriteLine("Thread #" + Thread.CurrentThread.ManagedThreadId + ": " + message);
        }

        public void EnqueueTask(TaskDelegate task)
        {
            _taskPool.Enqueue(task);
        }

        public void ExecuteAndStop()
        {
            _running = false;
            _threads.ForEach(thread => thread.Join());
        }
    }
}