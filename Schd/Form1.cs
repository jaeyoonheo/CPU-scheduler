using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Remoting.Contexts;

namespace Schd
{
    public partial class Scheduling : Form
    {
        string[] readText;
        private bool readFile = false;
        List<Process> pList, pView;
        List<Result> resultList;


        public Scheduling()
        {
            InitializeComponent();
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            pView.Clear();
            pList.Clear();

            //파일 오픈
            string path =  SelectFilePath();
            if (path == null) return;
    
            readText = File.ReadAllLines(path);
            
            //토큰 분리
            for (int i = 0; i < readText.Length; i++)
            {
                string[] token = readText[i].Split(' ');
                Process p = new Process(int.Parse(token[1]), int.Parse(token[2]), int.Parse(token[3]), int.Parse(token[4]));
                pList.Add(p);
            }

            //Grid에 process 출력
            dataGridView1.Rows.Clear();
            string[] row = { "", "", "", "" };
            foreach (Process p in pList)
            {
                row[0] = p.processID.ToString();
                row[1] = p.arriveTime.ToString();
                row[2] = p.burstTime.ToString();
                row[3] = p.priority.ToString();

                dataGridView1.Rows.Add(row);
            }

            //arriveTime으로 정렬
            pList.Sort(delegate(Process x, Process y)
            {
                if (x.arriveTime > y.arriveTime) return 1;
                else if (x.arriveTime < y.arriveTime) return -1;
                else
                {
                    return x.processID.CompareTo(y.processID);
                }
                //return x.arriveTime.CompareTo(y.arriveTime);
            });

            readFile = true;
        }

        private string SelectFilePath()
        {
            openFileDialog1.Filter = "텍스트파일|*.txt";
            return (openFileDialog1.ShowDialog() == DialogResult.OK) ? openFileDialog1.FileName : null;
        }

        private void Run_Click(object sender, EventArgs e)
        {
            if (!readFile) return;

            double taSum = 0;

            Process[] temp = pList.ToArray();
            List<Result> resultList2 = new List<Result>();
            resultList.Clear();
       

            //스케쥴러 실행
            switch (selectSchd.SelectedIndex)
            {
                case 0:
                    resultList2 = SchedulingAlgorithm.Run(pList, resultList);
                    break;
                case 1:
                    resultList2 = SchedulingAlgorithm.SJF(pList, resultList);
                    break;
                case 2:
                    resultList2 = SchedulingAlgorithm.SRTF(pList, resultList);
                    break;
                case 3:
                    resultList2 = SchedulingAlgorithm.Non_PRI(pList, resultList);
                    break;
                case 4:
                    resultList2 = SchedulingAlgorithm.Pre_PRI(pList, resultList);
                    break;
                case 5:
                    resultList2 = SchedulingAlgorithm.HRRN(pList, resultList);
                    break;
                case 6:
                    resultList2 = SchedulingAlgorithm.RR(pList, resultList,Convert.ToInt32(quantum.Text));
                    break;
                case 7:
                    resultList2 = SchedulingAlgorithm.MLQ(pList, resultList, Convert.ToInt32(quantum.Text));
                    break;
                case 8:
                    resultList2 = SchedulingAlgorithm.MLFQ(pList, resultList, Convert.ToInt32(quantum.Text),Convert.ToInt32(feedbackNum.Text));
                    break;
                default:
                    resultList2 = SchedulingAlgorithm.Run(pList, resultList);
                    break;
            }

            //결과출력
            dataGridView2.Rows.Clear();
            string[] row = { "", "", "", "", "","" };

            double watingTime = 0.0;
            foreach (Result r in resultList2)
            {
                row[0] = r.processID.ToString();
                row[1] = r.burstTime.ToString();
                row[2] = r.waitingTime.ToString();
                row[3] = r.turnaroundTime.ToString();
                row[4] = r.responseTime.ToString();
                watingTime += r.waitingTime;

                taSum += r.turnaroundTime;

                dataGridView2.Rows.Add(row);
            }

            TRTime.Text = "전체 실행시간: " + (resultList[resultList.Count - 1].startP + resultList[resultList.Count - 1].burstTime).ToString();
            avgRT.Text = "평균 대기시간: " + (watingTime / resultList2.Count).ToString();
            contextSwitch.Text = "문맥 교환: " + (resultList.Count-1).ToString();
            TaTimeAvr.Text = "평균 소요시간: " + (taSum/resultList2.Count).ToString();
            ThroughPut.Text = "단위시간 당 처리 프로세스: " + ((double)(resultList2.Count)/(double)(resultList[resultList.Count - 1].startP + resultList[resultList.Count - 1].burstTime)).ToString();
            panel1.Invalidate();

            pList.AddRange(temp);
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            int startPosition = 10;
            double waitingTime = 0.0;

            int resultListPosition = 0;
            foreach (Result r in resultList)
            {
                e.Graphics.DrawString("p" + r.processID.ToString(), Font, Brushes.Black, startPosition + (r.startP * 10), resultListPosition);
                e.Graphics.DrawRectangle(Pens.Red, startPosition + (r.startP * 10), resultListPosition + 20, r.burstTime * 10, 30);
                e.Graphics.DrawString(r.burstTime.ToString(), Font, Brushes.Black, startPosition + (r.startP * 10), resultListPosition + 60);
                e.Graphics.DrawString(r.waitingTime.ToString(), Font, Brushes.Black, startPosition + (r.startP * 10), resultListPosition + 80);
                waitingTime += (double)r.waitingTime;
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectSchd.SelectedIndex >= 6)
            {
                quantumLabel.Visible = true;
                quantum.Visible = true;
                if (selectSchd.SelectedIndex == 8)
                {
                    feedbackLabel.Visible = true;
                    feedbackNum.Visible = true;
                }
                else
                {
                    feedbackLabel.Visible = false;
                    feedbackNum.Visible = false;
                }
            }
            else
            {
                quantumLabel.Visible = false;
                quantum.Visible = false;
                feedbackLabel.Visible = false;
                feedbackNum.Visible = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pList = new List<Process>();
            pView = new List<Process>();
            resultList = new List<Result>();

            string[] numSchd = {"FCFS","SJF","SRTF","Nonpreemptive Priority","Preemptive Priority","HRRN","RR","Multi-Level Queue","Multi-Level Feedback Queue"};
            selectSchd.Items.AddRange(numSchd);
            selectSchd.SelectedIndex = 0;

            quantumLabel.Visible = false;
            quantum.Visible = false;
            quantum.Text = "4";

            feedbackLabel.Visible = false;
            feedbackNum.Visible = false;
            feedbackNum.Text = "2";

            //입력창
            DataGridViewTextBoxColumn processColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn arriveTimeColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn burstTimeColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn priorityColumn = new DataGridViewTextBoxColumn();

            processColumn.HeaderText = "프로세스";
            processColumn.Name = "process";
            arriveTimeColumn.HeaderText = "도착시간";
            arriveTimeColumn.Name = "arriveTime";
            burstTimeColumn.HeaderText = "실행시간";
            burstTimeColumn.Name = "burstTime";
            priorityColumn.HeaderText = "우선순위";
            priorityColumn.Name = "priority";

            dataGridView1.Columns.Add(processColumn);
            dataGridView1.Columns.Add(arriveTimeColumn);
            dataGridView1.Columns.Add(burstTimeColumn);
            dataGridView1.Columns.Add(priorityColumn);          



            //결과창
            DataGridViewTextBoxColumn resultProcessColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn resultBurstTimeColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn resultWaitingTimeColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn resultTurnaroundTimeColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn resultResponseTimeColumn = new DataGridViewTextBoxColumn();


            resultProcessColumn.HeaderText = "프로세스";
            resultProcessColumn.Name = "process";
            resultBurstTimeColumn.HeaderText = "실행시간";
            resultBurstTimeColumn.Name = "resultBurstTimeColumn";
            resultWaitingTimeColumn.HeaderText = "대기시간";
            resultWaitingTimeColumn.Name = "waitingTime";
            resultTurnaroundTimeColumn.HeaderText = "소요시간";
            resultTurnaroundTimeColumn.Name = "turnaroundTime";
            resultResponseTimeColumn.HeaderText = "응답시간";
            resultResponseTimeColumn.Name = "responseTime";

            dataGridView2.Columns.Add(resultProcessColumn);
            dataGridView2.Columns.Add(resultBurstTimeColumn);
            dataGridView2.Columns.Add(resultWaitingTimeColumn);
            dataGridView2.Columns.Add(resultTurnaroundTimeColumn);
            dataGridView2.Columns.Add(resultResponseTimeColumn);
        }
    }
}
