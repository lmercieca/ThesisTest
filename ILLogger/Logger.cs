using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ILLogger
{
    public class Logger
    {
        static string ASSEMBLY_DIRECTORY = @"C:\Development\Thesis\trunk\Mutation\TestProject\TestingILRewrite";
        static ConcurrentDictionary<int, int> counters = new ConcurrentDictionary<int, int>();

        static Stopwatch stopWatch = null;

        static object lck = new object();

        public static void IncrementiveLogEntry(string message)
        {
            if (stopWatch == null)
            {
                stopWatch = new Stopwatch();
                stopWatch.Start();
            }

            int ThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
            int CallStackIndex = 1;

            if (counters.ContainsKey(ThreadID))
            {
                CallStackIndex = counters[ThreadID] + 1;
            }


            string processedMessage = " message: " + message + "timer: " + stopWatch.ElapsedTicks + " index:" + CallStackIndex;
            WriteMessage(processedMessage);

            counters.AddOrUpdate(ThreadID, 1, (key, oldValue) => oldValue + 1);
        }

        public static void DecrementaveLogEntry(string message)
        {
            if (stopWatch == null)
            {
                stopWatch = new Stopwatch();
                stopWatch.Start();
            }

            int ThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
            int CallStackIndex = 1;

            if (counters.ContainsKey(ThreadID))
            {
                CallStackIndex = counters[ThreadID] - 1;
            }

            string processedMessage = " message:" + message + "timer: " + stopWatch.ElapsedTicks + " index:" + CallStackIndex;

            WriteMessage(processedMessage);

            counters.AddOrUpdate(ThreadID, 1, (key, oldValue) => oldValue - 1);
        }

        public static void WriteMessage(string Message)
        {
            Message = "Thread: " + System.Threading.Thread.CurrentThread.ManagedThreadId + Message;
            lock (lck)
            {
                Console.WriteLine(Message);
                string path = Path.Combine(ASSEMBLY_DIRECTORY + "\\logger" + System.Threading.Thread.CurrentThread.ManagedThreadId + ".log");

                using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(Message);
                    }
                }
            }
        }
    }
}
