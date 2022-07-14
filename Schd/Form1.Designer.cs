namespace Schd
{
    partial class Scheduling
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.OpenFile = new System.Windows.Forms.Button();
            this.Run = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.TRTime = new System.Windows.Forms.Label();
            this.avgRT = new System.Windows.Forms.Label();
            this.TaTimeAvr = new System.Windows.Forms.Label();
            this.contextSwitch = new System.Windows.Forms.Label();
            this.selectSchd = new System.Windows.Forms.ComboBox();
            this.quantumLabel = new System.Windows.Forms.Label();
            this.quantum = new System.Windows.Forms.TextBox();
            this.ThroughPut = new System.Windows.Forms.Label();
            this.feedbackNum = new System.Windows.Forms.TextBox();
            this.feedbackLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenFile
            // 
            this.OpenFile.Location = new System.Drawing.Point(12, 547);
            this.OpenFile.Name = "OpenFile";
            this.OpenFile.Size = new System.Drawing.Size(75, 23);
            this.OpenFile.TabIndex = 0;
            this.OpenFile.Text = "OpenFile";
            this.OpenFile.UseVisualStyleBackColor = true;
            this.OpenFile.Click += new System.EventHandler(this.OpenFile_Click);
            // 
            // Run
            // 
            this.Run.Location = new System.Drawing.Point(95, 547);
            this.Run.Name = "Run";
            this.Run.Size = new System.Drawing.Size(75, 23);
            this.Run.TabIndex = 1;
            this.Run.Text = "Run";
            this.Run.UseVisualStyleBackColor = true;
            this.Run.Click += new System.EventHandler(this.Run_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(24, 438);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(580, 103);
            this.panel1.TabIndex = 3;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(24, 44);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(580, 139);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "입력";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 206);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "출력";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(24, 221);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(580, 136);
            this.dataGridView2.TabIndex = 7;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // TRTime
            // 
            this.TRTime.AutoSize = true;
            this.TRTime.Location = new System.Drawing.Point(24, 369);
            this.TRTime.Name = "TRTime";
            this.TRTime.Size = new System.Drawing.Size(89, 12);
            this.TRTime.TabIndex = 8;
            this.TRTime.Text = "전체 실행시간 :";
            // 
            // avgRT
            // 
            this.avgRT.AutoSize = true;
            this.avgRT.Location = new System.Drawing.Point(24, 390);
            this.avgRT.Name = "avgRT";
            this.avgRT.Size = new System.Drawing.Size(89, 12);
            this.avgRT.TabIndex = 9;
            this.avgRT.Text = "평균 대기시간 :";
            // 
            // TaTimeAvr
            // 
            this.TaTimeAvr.AutoSize = true;
            this.TaTimeAvr.Location = new System.Drawing.Point(354, 390);
            this.TaTimeAvr.Name = "TaTimeAvr";
            this.TaTimeAvr.Size = new System.Drawing.Size(97, 12);
            this.TaTimeAvr.TabIndex = 11;
            this.TaTimeAvr.Text = "평균 소요 시간 : ";
            // 
            // contextSwitch
            // 
            this.contextSwitch.AutoSize = true;
            this.contextSwitch.Location = new System.Drawing.Point(354, 369);
            this.contextSwitch.Name = "contextSwitch";
            this.contextSwitch.Size = new System.Drawing.Size(69, 12);
            this.contextSwitch.TabIndex = 12;
            this.contextSwitch.Text = "문맥 교환 : ";
            // 
            // selectSchd
            // 
            this.selectSchd.FormattingEnabled = true;
            this.selectSchd.Location = new System.Drawing.Point(413, 18);
            this.selectSchd.Name = "selectSchd";
            this.selectSchd.Size = new System.Drawing.Size(191, 20);
            this.selectSchd.TabIndex = 13;
            this.selectSchd.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // quantumLabel
            // 
            this.quantumLabel.AutoSize = true;
            this.quantumLabel.Location = new System.Drawing.Point(514, 554);
            this.quantumLabel.Name = "quantumLabel";
            this.quantumLabel.Size = new System.Drawing.Size(53, 12);
            this.quantumLabel.TabIndex = 14;
            this.quantumLabel.Text = "시분할 : ";
            // 
            // quantum
            // 
            this.quantum.Location = new System.Drawing.Point(569, 549);
            this.quantum.Name = "quantum";
            this.quantum.Size = new System.Drawing.Size(34, 21);
            this.quantum.TabIndex = 15;
            // 
            // ThroughPut
            // 
            this.ThroughPut.AutoSize = true;
            this.ThroughPut.Location = new System.Drawing.Point(24, 411);
            this.ThroughPut.Name = "ThroughPut";
            this.ThroughPut.Size = new System.Drawing.Size(157, 12);
            this.ThroughPut.TabIndex = 17;
            this.ThroughPut.Text = "단위시간 당 처리 프로세스 :";
            // 
            // feedbackNum
            // 
            this.feedbackNum.Location = new System.Drawing.Point(466, 549);
            this.feedbackNum.Name = "feedbackNum";
            this.feedbackNum.Size = new System.Drawing.Size(34, 21);
            this.feedbackNum.TabIndex = 19;
            // 
            // feedbackLabel
            // 
            this.feedbackLabel.AutoSize = true;
            this.feedbackLabel.Location = new System.Drawing.Point(395, 554);
            this.feedbackLabel.Name = "feedbackLabel";
            this.feedbackLabel.Size = new System.Drawing.Size(69, 12);
            this.feedbackLabel.TabIndex = 18;
            this.feedbackLabel.Text = "강등 기준 : ";
            // 
            // Scheduling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 575);
            this.Controls.Add(this.feedbackNum);
            this.Controls.Add(this.feedbackLabel);
            this.Controls.Add(this.ThroughPut);
            this.Controls.Add(this.quantum);
            this.Controls.Add(this.quantumLabel);
            this.Controls.Add(this.selectSchd);
            this.Controls.Add(this.contextSwitch);
            this.Controls.Add(this.TaTimeAvr);
            this.Controls.Add(this.avgRT);
            this.Controls.Add(this.TRTime);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Run);
            this.Controls.Add(this.OpenFile);
            this.Name = "Scheduling";
            this.Text = "Scheduling";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenFile;
        private System.Windows.Forms.Button Run;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label TRTime;
        private System.Windows.Forms.Label avgRT;
        private System.Windows.Forms.Label TaTimeAvr;
        private System.Windows.Forms.Label contextSwitch;
        private System.Windows.Forms.ComboBox selectSchd;
        private System.Windows.Forms.Label quantumLabel;
        private System.Windows.Forms.TextBox quantum;
        private System.Windows.Forms.Label ThroughPut;
        private System.Windows.Forms.TextBox feedbackNum;
        private System.Windows.Forms.Label feedbackLabel;
    }
}

