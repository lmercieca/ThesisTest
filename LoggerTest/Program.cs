using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerTest
{
    class Program
    {
        static void Main(string[] args)
        {
        //    ILLogger.Logger.IncrementiveLogEntry("test");
        //    ILLogger.Logger.DecrementaveLogEntry("test");

            Random r = new Random();
            Parallel.For(0, 100, i =>
            {
                if (r.Next() % 2 == 0)
                ILLogger.Logger.IncrementiveLogEntry("up");
                else
                    ILLogger.Logger.DecrementaveLogEntry("down");

                if (r.Next() % 2 == 0)
                    ILLogger.Logger.IncrementiveLogEntry("left");
                else

                    ILLogger.Logger.DecrementaveLogEntry("right");

                if (r.Next() % 2 == 0)
                    ILLogger.Logger.IncrementiveLogEntry("5");
                else

                    ILLogger.Logger.DecrementaveLogEntry("6");
            });

        

        }
    }
}
