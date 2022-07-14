using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schd
{
    class Result
    {
        public int processID;
        public int startP;
        public int arriveTime;
        public int burstTime;
        public int waitingTime;
        public int turnaroundTime;
        public int responseTime;

        public Result(int processID, int startP, int arriveTime, int burstTime, int waitingTime,int turnaroundTime,int responseTime)
        {
            this.processID = processID;
            this.startP = startP;
            this.arriveTime = arriveTime;
            this.burstTime = burstTime;
            this.waitingTime = waitingTime;
            this.turnaroundTime = turnaroundTime;
            this.responseTime = responseTime;
        }
    }
}
