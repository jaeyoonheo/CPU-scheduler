# CPU Scheduler Simulator

### Description

Operating system lecture term project. Implemented the CPU scheduler performance evaluation tool as a GUI.



### Environment

This code is written on Windows 10, using the Visual Studio 2017 compiler.



### Scheduling Algorithm

#### Creteria

+ CPU utilization 
**(compute time) / (total time)**

+ Throughput 
**(count of process) / (unit time)**

+ Turnaround Time 
**Time from request to completion of process execution**

+ Waiting Time 
**Sum of time a process has waited in the ready queue**

+ Response Time 

  **Time from request to first response**

#### 1. FCFS (First Come First Serve)

>  FCFS is a policy that prioritizes processes in the order in which they arrive, and then works in order. It is the easiest to implement because it processes the processes in the order they arrive in the queue, and it allows all processes to use the CPU under an equal condition, and provides a predictable response time. However, if the process allocated the first CPU burst requires a long processing time, the processes with a relatively short CPU burst that entered the ready queue late have a long waiting time compared to the execution time.

#### 2. SJF (Shortest Job First)

>  SJF is a policy of performing tasks after prioritizing them in the order of the shortest CPU burst. Scheduling is performed in a non-preemptive manner, and the average waiting time is smaller than that of FCFS by processing processes with short bursts first.

#### 3. SRTF (Shortest Remaining Time First)

>  SRTF is an improved policy from the SJF policy to a preemptive one. During execution, if the execution time of a process that has entered the ready queue is less than the remaining execution time of the process, the process that came in later will preempt the CPU. It is the minimum value, but it also needs to be calculated because the execution time of the process must be predicted and sorted.

#### 4. Non-Preemptive Priority

>  In the Priority policy, processes have individual priorities and are sorted according to their priorities. Policies such as FCFS and SJF (SRTF) have different priorities, but basically follow the Priority policy. All Priority policies must consider the starvation phenomenon, in which the CPU is continuously occupied by a process with a higher priority, so that a process with a lower priority is not executed forever. The Non-preemptive Priority policy performs scheduling in a non-preemptive manner according to priority.

#### 5. Preemptive Priority

> Convert to preemptive method from non-preemptive priority

#### 6. HRRN (Highest Response Ratio Next)

>  HRRN uses the aging technique to solve the starvation problem of the priority policy. Aging increases the priority of the process as the waiting time increases, and the calculation formula is as follows.

#### 7. RR (Round Robin)

>  RR operates based on FCFS. In FCFS, the time division constant (q) is applied and the process bursts by q, then sends it to the end of the Ready Queue and executes the next process by q again. This gives the user the effect of processing multiple processes at the same time, but as the value of q is smaller, the number of context switching increases, so the efficiency is lowered. On the other hand, if the value of q is large, there is no difference from FCFS. When there are n processes, all processes must wait 
> (n-1)q for their turn to come. Accordingly, the response speed increases.

#### 8. Multi-Level Queue

>  Multi-Level Queue is a structure in which Ready Queue uses different scheduling policies for each part. Priorities for each partition may exist, and scheduling between them must be additionally implemented.

#### 9. Multi-Level Feedback Queue

>  This policy enables the movement of processes between partitions in a multi-level queue. The implementation should consider the conditions for moving the process between each level and at which level the process will be started.



### Usage

Click the Open File button and select text file

Text file format is like that

```
process 1 1 7 1
process 2 0 10 1
process 3 4 6 1
```

The order is pid, arrival time, execution time, and priority.
