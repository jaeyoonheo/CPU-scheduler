using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
namespace Schd
{
    class ReadyQueueElement
    {
        public int processID;
        public int arriveTime;
        public int burstTime;
        public int waitingTime;
        public int priority;
        public int remainBurst;
        public int executeNum;
        public int responseTime;

        public ReadyQueueElement(int processID,int arriveTime, int burstTime, int waitingTime, int priority, int remainBurst,int executeNum,int responseTime)
        {
            this.processID = processID;
            this.arriveTime = arriveTime;
            this.burstTime = burstTime;
            this.waitingTime = waitingTime;
            this.priority = priority;
            this.remainBurst = remainBurst;
            this.executeNum = executeNum;
            this.responseTime = responseTime;
        }
    }

    class SchedulingAlgorithm
    {
        public static List<Result> Run(List<Process> jobList, List<Result> resultList)
        {
            int currentProcess = 0;
            int cpuTime = 0;
            int cpuDone = 0;

            int runTime = 0;

            List<ReadyQueueElement> readyQueue = new List<ReadyQueueElement>();
            do
            {
                while(jobList.Count != 0)
                {
                    Process frontJob = jobList.ElementAt(0);
                    if (frontJob.arriveTime == runTime)
                    {
                        readyQueue.Add(new ReadyQueueElement(frontJob.processID, frontJob.arriveTime, frontJob.burstTime, 0, frontJob.priority,0,0,0));
                        jobList.RemoveAt(0);
                    }
                    else
                    {
                        break;
                    }
                }

                if (currentProcess == 0)
                {
                    if (readyQueue.Count != 0)
                    {
                        ReadyQueueElement rq = readyQueue.ElementAt(0);
                        resultList.Add(new Result(rq.processID, runTime, rq.arriveTime, rq.burstTime, rq.waitingTime, rq.waitingTime + rq.burstTime,rq.waitingTime));
                        cpuDone = rq.burstTime;
                        cpuTime = 0;
                        currentProcess = rq.processID;
                        readyQueue.RemoveAt(0);

                    }
                }
                else
                {
                    if (cpuTime == cpuDone)
                    {
                        currentProcess = 0;
                        continue;
                    }
                }

                cpuTime++;
                runTime++;

                for(int i = 0; i < readyQueue.Count; i++)
                {
                    readyQueue.ElementAt(i).waitingTime++;
                }

            } while (jobList.Count != 0 || readyQueue.Count != 0 || currentProcess != 0);

            return resultList;
        }

        public static List<Result> SJF(List<Process> jobList, List<Result> resultList)
        {
            int currentProcess = 0;
            int cpuTime = 0;
            int cpuDone = 0;

            int runTime = 0;

            List<ReadyQueueElement> readyQueue = new List<ReadyQueueElement>();
            do
            {
                while (jobList.Count != 0)
                {
                    Process frontJob = jobList.ElementAt(0);
                    if (frontJob.arriveTime == runTime)
                    {
                        readyQueue.Add(new ReadyQueueElement(frontJob.processID, frontJob.arriveTime, frontJob.burstTime, 0, frontJob.priority,0,0,0));
                        jobList.RemoveAt(0);
                    }
                    else
                    {
                        break;
                    }
                }

                readyQueue.Sort(delegate (ReadyQueueElement p1, ReadyQueueElement p2)
                {
                    if (p1.burstTime > p2.burstTime) return 1;
                    else if (p1.burstTime < p2.burstTime) return -1;
                    else
                    {
                        return p1.processID.CompareTo(p2.processID);
                    }
                });

                if (currentProcess == 0)
                {
                    if (readyQueue.Count != 0)
                    {
                        ReadyQueueElement rq = readyQueue.ElementAt(0);
                        resultList.Add(new Result(rq.processID, runTime, rq.arriveTime, rq.burstTime, rq.waitingTime, rq.waitingTime + rq.burstTime,rq.waitingTime));
                        cpuDone = rq.burstTime;
                        cpuTime = 0;
                        currentProcess = rq.processID;
                        readyQueue.RemoveAt(0);

                    }
                }
                else
                {
                    if (cpuTime == cpuDone)
                    {
                        currentProcess = 0;
                        continue;
                    }
                }

                cpuTime++;
                runTime++;

                for (int i = 0; i < readyQueue.Count; i++)
                {
                    readyQueue.ElementAt(i).waitingTime++;
                }

            } while (jobList.Count != 0 || readyQueue.Count != 0 || currentProcess != 0);

            return resultList;
        }

        public static List<Result> SRTF(List<Process> jobList, List<Result> resultList)
        {
            ReadyQueueElement currentProcess = null;
            int cpuTime = 0;
            int cpuDone = 0;

            int runTime = 0;

            List<ReadyQueueElement> readyQueue = new List<ReadyQueueElement>();
            List<Result> resultList2 = new List<Result>();

            do
            {
                while (jobList.Count != 0)
                {
                    Process frontJob = jobList.ElementAt(0);
                    if (frontJob.arriveTime == runTime)
                    {
                        readyQueue.Add(new ReadyQueueElement(frontJob.processID, frontJob.arriveTime, frontJob.burstTime, 0, frontJob.priority,frontJob.burstTime,0,-1));
                        jobList.RemoveAt(0);
                    }
                    else
                    {
                        break;
                    }
                }

                readyQueue.Sort(delegate (ReadyQueueElement p1, ReadyQueueElement p2)
                {
                    if (p1.remainBurst > p2.remainBurst) return 1;
                    else if (p1.remainBurst < p2.remainBurst) return -1;
                    else
                    {
                        return p1.processID.CompareTo(p2.processID);
                    }
                });

                if (currentProcess == null)
                {
                    if (readyQueue.Count != 0)
                    {
                        currentProcess = readyQueue.ElementAt(0);
                        cpuDone = currentProcess.remainBurst;
                        cpuTime = 0;
                        readyQueue.RemoveAt(0);
                    }
                }
                else
                {
                    if (cpuTime == cpuDone)
                    {
                        if (currentProcess.responseTime < 0) currentProcess.responseTime = currentProcess.waitingTime;
                        resultList.Add(new Result(currentProcess.processID, runTime - cpuTime, currentProcess.arriveTime, cpuTime, currentProcess.waitingTime, currentProcess.waitingTime + currentProcess.burstTime,0));
                        resultList2.Add(new Result(currentProcess.processID, runTime, currentProcess.arriveTime, currentProcess.burstTime, currentProcess.waitingTime, currentProcess.waitingTime + currentProcess.burstTime,currentProcess.responseTime));
                        currentProcess = null;
                        continue;
                    }
                    else if (readyQueue.Count != 0)
                    {
                        if (currentProcess.remainBurst > readyQueue.ElementAt(0).remainBurst)
                        {
                            if (currentProcess.responseTime < 0) currentProcess.responseTime = currentProcess.waitingTime;                           
                            resultList.Add(new Result(currentProcess.processID, runTime - cpuTime, currentProcess.arriveTime, cpuTime, currentProcess.waitingTime, currentProcess.waitingTime + currentProcess.burstTime,0));
                            readyQueue.Add(currentProcess);
                            currentProcess = readyQueue.ElementAt(0);
                            cpuDone = currentProcess.remainBurst;
                            readyQueue.RemoveAt(0);
                            cpuTime = 0;
                        }
                    }
                    
                }

                cpuTime++;
                runTime++;
                if (currentProcess != null) currentProcess.remainBurst--;

                for (int i = 0; i < readyQueue.Count; i++)
                {
                    readyQueue.ElementAt(i).waitingTime++;
                }

            } while (jobList.Count != 0 || readyQueue.Count != 0 || currentProcess != null);

            return resultList2;
        }

        public static List<Result> Non_PRI(List<Process> jobList, List<Result> resultList)
        {
            int currentProcess = 0;
            int cpuTime = 0;
            int cpuDone = 0;

            int runTime = 0;

            List<ReadyQueueElement> readyQueue = new List<ReadyQueueElement>();
            do
            {
                while (jobList.Count != 0)
                {
                    Process frontJob = jobList.ElementAt(0);
                    if (frontJob.arriveTime == runTime)
                    {
                        readyQueue.Add(new ReadyQueueElement(frontJob.processID, frontJob.arriveTime, frontJob.burstTime, 0, frontJob.priority, 0, 0,0));
                        jobList.RemoveAt(0);
                    }
                    else
                    {
                        break;
                    }
                }

                readyQueue.Sort(delegate (ReadyQueueElement p1, ReadyQueueElement p2)
                {
                    if (p1.priority > p2.priority) return 1;
                    else if (p1.priority < p2.priority) return -1;
                    else
                    {
                        return p1.processID.CompareTo(p2.processID);
                    }
                });

                if (currentProcess == 0)
                {
                    if (readyQueue.Count != 0)
                    {
                        ReadyQueueElement rq = readyQueue.ElementAt(0);
                        resultList.Add(new Result(rq.processID, runTime, rq.arriveTime, rq.burstTime, rq.waitingTime, rq.waitingTime + rq.burstTime,rq.waitingTime));
                        cpuDone = rq.burstTime;
                        cpuTime = 0;
                        currentProcess = rq.processID;
                        readyQueue.RemoveAt(0);

                    }
                }
                else
                {
                    if (cpuTime == cpuDone)
                    {
                        currentProcess = 0;
                        continue;
                    }
                }

                cpuTime++;
                runTime++;

                for (int i = 0; i < readyQueue.Count; i++)
                {
                    readyQueue.ElementAt(i).waitingTime++;
                }

            } while (jobList.Count != 0 || readyQueue.Count != 0 || currentProcess != 0);

            return resultList;
        }

        public static List<Result> Pre_PRI(List<Process> jobList, List<Result> resultList)
        {
            ReadyQueueElement currentProcess = null;
            int cpuTime = 0;
            int cpuDone = 0;

            int runTime = 0;

            List<ReadyQueueElement> readyQueue = new List<ReadyQueueElement>();
            List<Result> resultList2 = new List<Result>();

            do
            {
                while (jobList.Count != 0)
                {
                    Process frontJob = jobList.ElementAt(0);
                    if (frontJob.arriveTime == runTime)
                    {
                        readyQueue.Add(new ReadyQueueElement(frontJob.processID, frontJob.arriveTime, frontJob.burstTime, 0, frontJob.priority, frontJob.burstTime, 0,-1));
                        jobList.RemoveAt(0);
                    }
                    else
                    {
                        break;
                    }
                }

                readyQueue.Sort(delegate (ReadyQueueElement p1, ReadyQueueElement p2)
                {
                    if (p1.priority > p2.priority) return 1;
                    else if (p1.priority < p2.priority) return -1;
                    else
                    {
                        return p1.processID.CompareTo(p2.processID);
                    }
                });

                if (currentProcess == null)
                {
                    if (readyQueue.Count != 0)
                    {
                        currentProcess = readyQueue.ElementAt(0);
                        cpuDone = currentProcess.remainBurst;
                        cpuTime = 0;
                        readyQueue.RemoveAt(0);
                    }
                }
                else
                {
                    if (cpuTime == cpuDone)
                    {
                        if (currentProcess.responseTime < 0) currentProcess.responseTime = currentProcess.waitingTime;
                        resultList.Add(new Result(currentProcess.processID, runTime - cpuTime, currentProcess.arriveTime, cpuTime, currentProcess.waitingTime, currentProcess.waitingTime + currentProcess.burstTime,0));
                        resultList2.Add(new Result(currentProcess.processID, runTime, currentProcess.arriveTime, currentProcess.burstTime, currentProcess.waitingTime, currentProcess.waitingTime + currentProcess.burstTime,currentProcess.responseTime));
                        currentProcess = null;
                        continue;
                    }
                    else if (readyQueue.Count != 0)
                    {
                        if (currentProcess.priority > readyQueue.ElementAt(0).priority)
                        {
                            if (currentProcess.responseTime < 0) currentProcess.responseTime = currentProcess.waitingTime;
                            resultList.Add(new Result(currentProcess.processID, runTime - cpuTime, currentProcess.arriveTime, cpuTime, currentProcess.waitingTime, currentProcess.waitingTime + currentProcess.burstTime,0));
                            readyQueue.Add(currentProcess);
                            currentProcess = readyQueue.ElementAt(0);
                            cpuDone = currentProcess.remainBurst;
                            readyQueue.RemoveAt(0);
                            cpuTime = 0;
                        }
                    }

                }

                cpuTime++;
                runTime++;
                if (currentProcess != null) currentProcess.remainBurst--;

                for (int i = 0; i < readyQueue.Count; i++)
                {
                    readyQueue.ElementAt(i).waitingTime++;
                }

            } while (jobList.Count != 0 || readyQueue.Count != 0 || currentProcess != null);

            return resultList2;
        }

        public static List<Result> HRRN(List<Process> jobList, List<Result> resultList)
        {
            int currentProcess = 0;
            int cpuTime = 0;
            int cpuDone = 0;

            int runTime = 0;

            List<ReadyQueueElement> readyQueue = new List<ReadyQueueElement>();
            do
            {
                while (jobList.Count != 0)
                {
                    Process frontJob = jobList.ElementAt(0);
                    if (frontJob.arriveTime == runTime)
                    {
                        readyQueue.Add(new ReadyQueueElement(frontJob.processID, frontJob.arriveTime, frontJob.burstTime, 0, frontJob.priority, 0, 0,0));
                        jobList.RemoveAt(0);
                    }
                    else
                    {
                        break;
                    }
                }

                readyQueue.Sort(delegate (ReadyQueueElement p1, ReadyQueueElement p2)
                {
                    double p1_age = (double)(p1.waitingTime + p1.burstTime) / (double)(p1.burstTime);
                    double p2_age = (double)(p2.waitingTime + p2.burstTime) / (double)p2.burstTime;
                    if (p1_age < p2_age) return 1;
                    else if (p1_age > p2_age) return -1;
                    else if (p1.burstTime > p2.burstTime) return 1;
                    else if (p1.burstTime < p2.burstTime) return -1;
                    else
                    {
                        return p1.processID.CompareTo(p2.processID);
                    }
                });

                if (currentProcess == 0)
                {
                    if (readyQueue.Count != 0)
                    {
                        ReadyQueueElement rq = readyQueue.ElementAt(0);
                        resultList.Add(new Result(rq.processID, runTime, rq.arriveTime, rq.burstTime, rq.waitingTime, rq.waitingTime + rq.burstTime,rq.waitingTime));
                        cpuDone = rq.burstTime;
                        cpuTime = 0;
                        currentProcess = rq.processID;
                        readyQueue.RemoveAt(0);

                    }
                }
                else
                {
                    if (cpuTime == cpuDone)
                    {
                        currentProcess = 0;
                        continue;
                    }
                }

                cpuTime++;
                runTime++;

                for (int i = 0; i < readyQueue.Count; i++)
                {
                    readyQueue.ElementAt(i).waitingTime++;
                }

            } while (jobList.Count != 0 || readyQueue.Count != 0 || currentProcess != 0);

            return resultList;
        }

        public static List<Result> RR(List<Process> jobList, List<Result> resultList, int quantum)
        {
            ReadyQueueElement currentProcess = null;
            int cpuTime = 0;
            int cpuDone = 0;

            int runTime = 0;

            List<ReadyQueueElement> readyQueue = new List<ReadyQueueElement>();
            List<Result> resultList2 = new List<Result>();

            do
            {
                while (jobList.Count != 0)
                {
                    Process frontJob = jobList.ElementAt(0);
                    if (frontJob.arriveTime == runTime)
                    {
                        readyQueue.Add(new ReadyQueueElement(frontJob.processID, frontJob.arriveTime, frontJob.burstTime, 0, frontJob.priority, frontJob.burstTime, 0,-1));
                        jobList.RemoveAt(0);
                    }
                    else
                    {
                        break;
                    }
                }

                if (currentProcess == null)
                {
                    if (readyQueue.Count != 0)
                    {
                        currentProcess = readyQueue.ElementAt(0);
                        cpuDone = currentProcess.remainBurst;
                        cpuTime = 0;
                        readyQueue.RemoveAt(0);
                    }
                }
                else
                {
                    if (cpuTime == cpuDone)
                    {
                        if (currentProcess.responseTime < 0) currentProcess.responseTime = currentProcess.waitingTime;
                        resultList.Add(new Result(currentProcess.processID, runTime-cpuTime, currentProcess.arriveTime, cpuTime, currentProcess.waitingTime, currentProcess.waitingTime + currentProcess.burstTime,0));
                        resultList2.Add(new Result(currentProcess.processID, runTime, currentProcess.arriveTime, currentProcess.burstTime, currentProcess.waitingTime, currentProcess.waitingTime + currentProcess.burstTime,currentProcess.responseTime));
                        currentProcess = null;
                        continue;
                    }
                    else if (cpuTime==quantum)
                    {
                        if (currentProcess.responseTime < 0) currentProcess.responseTime = currentProcess.waitingTime;
                        currentProcess.remainBurst -= quantum;
                        resultList.Add(new Result(currentProcess.processID, runTime - cpuTime, currentProcess.arriveTime, cpuTime, currentProcess.waitingTime, currentProcess.waitingTime + currentProcess.burstTime,0));
                        readyQueue.Add(currentProcess);
                        currentProcess = readyQueue.ElementAt(0);
                        cpuDone = currentProcess.remainBurst;
                        cpuTime = 0;
                        readyQueue.RemoveAt(0);
                    }
                }

                cpuTime++;
                runTime++;

                for (int i = 0; i < readyQueue.Count; i++)
                {
                    readyQueue.ElementAt(i).waitingTime++;
                }

            } while (jobList.Count != 0 || readyQueue.Count != 0 || currentProcess != null);

            return resultList2;
        }

        public static List<Result> MLQ(List<Process> jobList, List<Result> resultList, int quantum)
        {
            ReadyQueueElement currentProcess = null;
            int cpuTime = 0;
            int cpuDone = 0;

            int runTime = 0;

            List<ReadyQueueElement> readyQueue1 = new List<ReadyQueueElement>();
            List<ReadyQueueElement> readyQueue2 = new List<ReadyQueueElement>();
            List<Result> resultList2 = new List<Result>();

            do
            {
                while (jobList.Count != 0)
                {
                    Process frontJob = jobList.ElementAt(0);
                    if (frontJob.arriveTime == runTime)
                    {
                        if (frontJob.priority < 4) readyQueue1.Add(new ReadyQueueElement(frontJob.processID, frontJob.arriveTime, frontJob.burstTime, 0, frontJob.priority, frontJob.burstTime, 0,-1));
                        else readyQueue2.Add(new ReadyQueueElement(frontJob.processID, frontJob.arriveTime, frontJob.burstTime, 0, frontJob.priority, frontJob.burstTime, 0,-1));
                        jobList.RemoveAt(0);
                    }
                    else
                    {
                        break;
                    }
                }

                readyQueue2.Sort(delegate (ReadyQueueElement p1, ReadyQueueElement p2)
                {
                    if (p1.arriveTime > p2.arriveTime) return 1;
                    else if (p1.arriveTime < p2.arriveTime) return -1;
                    else
                    {
                        return p1.processID.CompareTo(p2.processID);
                    }
                });

                if (currentProcess == null)
                {
                    if (readyQueue1.Count != 0)
                    {
                        currentProcess = readyQueue1.ElementAt(0);
                        cpuDone = currentProcess.remainBurst;
                        cpuTime = 0;
                        readyQueue1.RemoveAt(0);
                    }
                    else if(readyQueue2.Count !=0)
                    {
                        currentProcess = readyQueue2.ElementAt(0);
                        cpuDone = currentProcess.remainBurst;
                        cpuTime = 0;
                        readyQueue2.RemoveAt(0);
                    }
                }
                else
                {
                    if(cpuTime==cpuDone)
                    {
                        if (currentProcess.responseTime < 0) currentProcess.responseTime = currentProcess.waitingTime;
                        resultList.Add(new Result(currentProcess.processID, runTime - cpuTime, currentProcess.arriveTime, cpuTime, currentProcess.waitingTime, currentProcess.waitingTime + currentProcess.burstTime,0));
                        resultList2.Add(new Result(currentProcess.processID, runTime, currentProcess.arriveTime, currentProcess.burstTime, currentProcess.waitingTime, currentProcess.waitingTime + currentProcess.burstTime,currentProcess.responseTime));
                        currentProcess = null;
                        continue;
                    }
                    else if (currentProcess.priority >= 4)
                    {
                        if (readyQueue1.Count != 0)
                        {
                            if (currentProcess.responseTime < 0) currentProcess.responseTime = currentProcess.waitingTime;
                            resultList.Add(new Result(currentProcess.processID, runTime - cpuTime, currentProcess.arriveTime, cpuTime, currentProcess.waitingTime, currentProcess.waitingTime + currentProcess.burstTime,0));
                            currentProcess.remainBurst -= cpuTime;
                            readyQueue2.Add(currentProcess);
                            currentProcess = readyQueue1.ElementAt(0);
                            cpuDone = currentProcess.remainBurst;
                            cpuTime = 0;
                            readyQueue1.RemoveAt(0);
                        }
                    }
                    else if(currentProcess.priority<4)
                    {
                        if (cpuTime==quantum)
                        {
                            if (currentProcess.responseTime < 0) currentProcess.responseTime = currentProcess.waitingTime;
                            currentProcess.remainBurst -= quantum;
                            resultList.Add(new Result(currentProcess.processID, runTime - cpuTime, currentProcess.arriveTime, cpuTime, currentProcess.waitingTime, currentProcess.waitingTime + currentProcess.burstTime,0));
                            readyQueue1.Add(currentProcess);
                            currentProcess = readyQueue1.ElementAt(0);
                            cpuDone = currentProcess.remainBurst;
                            cpuTime = 0;
                            readyQueue1.RemoveAt(0);
                        }
                    }
                }

                cpuTime++;
                runTime++;

                for (int i = 0; i < readyQueue1.Count; i++)
                {
                    readyQueue1.ElementAt(i).waitingTime++;
                }
                for (int i = 0; i < readyQueue2.Count; i++)
                {
                    readyQueue2.ElementAt(i).waitingTime++;
                }

            } while (jobList.Count != 0 || readyQueue1.Count != 0 || readyQueue2.Count != 0 || currentProcess != null);

            return resultList2;
        }

        public static List<Result> MLFQ(List<Process> jobList, List<Result> resultList, int quantum, int feedbackNum)
        {
            ReadyQueueElement currentProcess = null;
            int cpuTime = 0;
            int cpuDone = 0;

            int runTime = 0;

            List<ReadyQueueElement> readyQueue1 = new List<ReadyQueueElement>();
            List<ReadyQueueElement> readyQueue2 = new List<ReadyQueueElement>();
            List<Result> resultList2 = new List<Result>();

            do
            {
                while (jobList.Count != 0)
                {
                    Process frontJob = jobList.ElementAt(0);
                    if (frontJob.arriveTime == runTime)
                    {
                        readyQueue1.Add(new ReadyQueueElement(frontJob.processID, frontJob.arriveTime, frontJob.burstTime, 0, frontJob.priority, frontJob.burstTime, 1,-1));
                        jobList.RemoveAt(0);
                    }
                    else
                    {
                        break;
                    }
                }

                if (currentProcess == null)
                {
                    if (readyQueue1.Count != 0)
                    {
                        currentProcess = readyQueue1.ElementAt(0);
                        cpuDone = currentProcess.remainBurst;
                        cpuTime = 0;
                        readyQueue1.RemoveAt(0);
                    }
                    else if (readyQueue2.Count != 0)
                    {
                        currentProcess = readyQueue2.ElementAt(0);
                        cpuDone = currentProcess.remainBurst;
                        cpuTime = 0;
                        readyQueue2.RemoveAt(0);
                    }
                }
                else
                {
                    if (cpuTime == cpuDone)
                    {
                        if (currentProcess.responseTime < 0) currentProcess.responseTime = currentProcess.waitingTime;
                        resultList.Add(new Result(currentProcess.processID, runTime - cpuTime, currentProcess.arriveTime, cpuTime, currentProcess.waitingTime, currentProcess.waitingTime + currentProcess.burstTime,0));
                        resultList2.Add(new Result(currentProcess.processID, runTime, currentProcess.arriveTime, currentProcess.burstTime, currentProcess.waitingTime, currentProcess.waitingTime + currentProcess.burstTime,currentProcess.responseTime));
                        currentProcess = null;
                        continue;
                    }
                    else if (readyQueue1.Count != 0)
                    {
                        if (cpuTime == quantum)
                        {
                            if (currentProcess.responseTime < 0) currentProcess.responseTime = currentProcess.waitingTime;
                            currentProcess.remainBurst -= quantum;
                            resultList.Add(new Result(currentProcess.processID, runTime - cpuTime, currentProcess.arriveTime, cpuTime, currentProcess.waitingTime, currentProcess.waitingTime + currentProcess.burstTime,0));
                            if (currentProcess.executeNum < feedbackNum)
                            {
                                currentProcess.executeNum++;
                                readyQueue1.Add(currentProcess);
                            }
                            else readyQueue2.Add(currentProcess);
                            currentProcess = readyQueue1.ElementAt(0);
                            cpuDone = currentProcess.remainBurst;
                            cpuTime = 0;
                            readyQueue1.RemoveAt(0);
                        }
                    }
                    else if(currentProcess.executeNum<feedbackNum)
                    {
                        while (currentProcess.executeNum < feedbackNum)
                        {
                            if(cpuTime==cpuDone)
                            {
                                if (currentProcess.responseTime < 0) currentProcess.responseTime = currentProcess.waitingTime;
                                resultList.Add(new Result(currentProcess.processID, runTime - cpuTime, currentProcess.arriveTime, cpuTime, currentProcess.waitingTime, currentProcess.waitingTime + currentProcess.burstTime,0));
                                resultList2.Add(new Result(currentProcess.processID, runTime, currentProcess.arriveTime, currentProcess.burstTime, currentProcess.waitingTime, currentProcess.waitingTime + currentProcess.burstTime,currentProcess.responseTime));
                                currentProcess = null;
                                continue;
                            }
                            if (cpuTime == quantum)
                            {
                                if (currentProcess.responseTime < 0) currentProcess.responseTime = currentProcess.waitingTime;
                                currentProcess.executeNum++;
                                currentProcess.remainBurst -= quantum;
                                resultList.Add(new Result(currentProcess.processID, runTime - cpuTime, currentProcess.arriveTime, cpuTime, currentProcess.waitingTime, currentProcess.waitingTime + currentProcess.burstTime,0));
                                cpuDone = currentProcess.remainBurst;
                                cpuTime = 0;
                            }
                            cpuTime++;
                            runTime++;
                        }
                    }
                }

                cpuTime++;
                runTime++;

                for (int i = 0; i < readyQueue1.Count; i++)
                {
                    readyQueue1.ElementAt(i).waitingTime++;
                }
                for (int i = 0; i < readyQueue2.Count; i++)
                {
                    readyQueue2.ElementAt(i).waitingTime++;
                }

            } while (jobList.Count != 0 || readyQueue1.Count != 0 || readyQueue2.Count != 0 || currentProcess != null);

            return resultList2;
        }
    }
}
