using System.Collections.Generic;
using TaskQueueProject;

namespace ActionRunnerProject
{
    public class ActionRunner
    {
        public static void RunAndWaitAll(List<TaskDelegate> tasks)
        {
            TaskQueue taskQueue = new TaskQueue(5);
            tasks.ForEach(taskQueue.EnqueueTask);
            taskQueue.ExecuteAndStop();
        }
    }
}