namespace WindowsFormsApplication3
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grpSetting = new System.Windows.Forms.GroupBox();
            this.cmbPortName = new System.Windows.Forms.ComboBox();
            this.grpSend = new System.Windows.Forms.GroupBox();
            this.sndTextBox = new System.Windows.Forms.TextBox();
            this.sndButton = new System.Windows.Forms.Button();
            this.grpRecv = new System.Windows.Forms.GroupBox();
            this.Table = new System.Windows.Forms.DataGridView();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dat1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dat2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Batt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mo_A = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mo_B = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mo_C = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AD_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AD_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AD_3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AD_4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.I2C = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rcvTextBox = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.export = new System.Windows.Forms.Button();
            this.clear = new System.Windows.Forms.Button();
            this.stop = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.cloocaTextBox = new System.Windows.Forms.TextBox();
            this.genButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.grpSetting.SuspendLayout();
            this.grpSend.SuspendLayout();
            this.grpRecv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Table)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSetting
            // 
            this.grpSetting.Controls.Add(this.cmbPortName);
            this.grpSetting.Location = new System.Drawing.Point(21, 20);
            this.grpSetting.Margin = new System.Windows.Forms.Padding(4);
            this.grpSetting.Name = "grpSetting";
            this.grpSetting.Padding = new System.Windows.Forms.Padding(4);
            this.grpSetting.Size = new System.Drawing.Size(238, 60);
            this.grpSetting.TabIndex = 0;
            this.grpSetting.TabStop = false;
            this.grpSetting.Text = "シリアルポート設定";
            // 
            // cmbPortName
            // 
            this.cmbPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPortName.FormattingEnabled = true;
            this.cmbPortName.Location = new System.Drawing.Point(38, 25);
            this.cmbPortName.Margin = new System.Windows.Forms.Padding(4);
            this.cmbPortName.Name = "cmbPortName";
            this.cmbPortName.Size = new System.Drawing.Size(127, 23);
            this.cmbPortName.TabIndex = 0;
            // 
            // grpSend
            // 
            this.grpSend.Controls.Add(this.sndTextBox);
            this.grpSend.Controls.Add(this.sndButton);
            this.grpSend.Location = new System.Drawing.Point(11, 210);
            this.grpSend.Margin = new System.Windows.Forms.Padding(4);
            this.grpSend.Name = "grpSend";
            this.grpSend.Padding = new System.Windows.Forms.Padding(4);
            this.grpSend.Size = new System.Drawing.Size(1307, 130);
            this.grpSend.TabIndex = 1;
            this.grpSend.TabStop = false;
            this.grpSend.Text = "送信配列(JSON)";
            this.grpSend.Enter += new System.EventHandler(this.grpSend_Enter);
            // 
            // sndTextBox
            // 
            this.sndTextBox.Location = new System.Drawing.Point(8, 32);
            this.sndTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.sndTextBox.Multiline = true;
            this.sndTextBox.Name = "sndTextBox";
            this.sndTextBox.Size = new System.Drawing.Size(1179, 91);
            this.sndTextBox.TabIndex = 1;
            // 
            // sndButton
            // 
            this.sndButton.Location = new System.Drawing.Point(1191, 93);
            this.sndButton.Margin = new System.Windows.Forms.Padding(4);
            this.sndButton.Name = "sndButton";
            this.sndButton.Size = new System.Drawing.Size(108, 30);
            this.sndButton.TabIndex = 0;
            this.sndButton.Text = "送信";
            this.sndButton.UseVisualStyleBackColor = true;
            this.sndButton.Click += new System.EventHandler(this.sndButton_Click);
            // 
            // grpRecv
            // 
            this.grpRecv.Controls.Add(this.Table);
            this.grpRecv.Controls.Add(this.rcvTextBox);
            this.grpRecv.Location = new System.Drawing.Point(11, 348);
            this.grpRecv.Margin = new System.Windows.Forms.Padding(4);
            this.grpRecv.Name = "grpRecv";
            this.grpRecv.Padding = new System.Windows.Forms.Padding(4);
            this.grpRecv.Size = new System.Drawing.Size(1340, 444);
            this.grpRecv.TabIndex = 2;
            this.grpRecv.TabStop = false;
            this.grpRecv.Text = "ログ";
            this.grpRecv.Enter += new System.EventHandler(this.grpRecv_Enter);
            // 
            // Table
            // 
            this.Table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Table.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Time,
            this.Dat1,
            this.Dat2,
            this.Batt,
            this.Mo_A,
            this.Mo_B,
            this.Mo_C,
            this.AD_1,
            this.AD_2,
            this.AD_3,
            this.AD_4,
            this.I2C});
            this.Table.Location = new System.Drawing.Point(20, 50);
            this.Table.Name = "Table";
            this.Table.RowTemplate.Height = 24;
            this.Table.Size = new System.Drawing.Size(1266, 385);
            this.Table.TabIndex = 5;
            this.Table.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Time
            // 
            this.Time.HeaderText = "Time";
            this.Time.Name = "Time";
            // 
            // Dat1
            // 
            this.Dat1.HeaderText = "Dat1";
            this.Dat1.Name = "Dat1";
            // 
            // Dat2
            // 
            this.Dat2.HeaderText = "Dat2";
            this.Dat2.Name = "Dat2";
            // 
            // Batt
            // 
            this.Batt.HeaderText = "Batt";
            this.Batt.Name = "Batt";
            // 
            // Mo_A
            // 
            this.Mo_A.HeaderText = "Mo_A";
            this.Mo_A.Name = "Mo_A";
            // 
            // Mo_B
            // 
            this.Mo_B.HeaderText = "Mo_B";
            this.Mo_B.Name = "Mo_B";
            // 
            // Mo_C
            // 
            this.Mo_C.HeaderText = "Mo_C";
            this.Mo_C.Name = "Mo_C";
            // 
            // AD_1
            // 
            this.AD_1.HeaderText = "AD_1";
            this.AD_1.Name = "AD_1";
            // 
            // AD_2
            // 
            this.AD_2.HeaderText = "AD_2";
            this.AD_2.Name = "AD_2";
            // 
            // AD_3
            // 
            this.AD_3.HeaderText = "AD_3";
            this.AD_3.Name = "AD_3";
            // 
            // AD_4
            // 
            this.AD_4.HeaderText = "AD_4";
            this.AD_4.Name = "AD_4";
            // 
            // I2C
            // 
            this.I2C.HeaderText = "I2C";
            this.I2C.Name = "I2C";
            // 
            // rcvTextBox
            // 
            this.rcvTextBox.Location = new System.Drawing.Point(11, 20);
            this.rcvTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.rcvTextBox.Multiline = true;
            this.rcvTextBox.Name = "rcvTextBox";
            this.rcvTextBox.Size = new System.Drawing.Size(1163, 10);
            this.rcvTextBox.TabIndex = 0;
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(641, 40);
            this.connectButton.Margin = new System.Windows.Forms.Padding(4);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(149, 30);
            this.connectButton.TabIndex = 3;
            this.connectButton.Text = "接続";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // export
            // 
            this.export.Location = new System.Drawing.Point(1107, 809);
            this.export.Name = "export";
            this.export.Size = new System.Drawing.Size(149, 31);
            this.export.TabIndex = 4;
            this.export.Text = "ログ出力";
            this.export.UseVisualStyleBackColor = true;
            this.export.Click += new System.EventHandler(this.export_Click);
            // 
            // clear
            // 
            this.clear.Location = new System.Drawing.Point(701, 809);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(104, 31);
            this.clear.TabIndex = 5;
            this.clear.Text = "ログクリア";
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // stop
            // 
            this.stop.Location = new System.Drawing.Point(901, 809);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(107, 31);
            this.stop.TabIndex = 6;
            this.stop.Text = "一時停止";
            this.stop.UseVisualStyleBackColor = true;
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbType);
            this.groupBox1.Location = new System.Drawing.Point(302, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(236, 60);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "送信配列の型";
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(18, 21);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(179, 23);
            this.cmbType.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.clearButton);
            this.groupBox2.Controls.Add(this.cloocaTextBox);
            this.groupBox2.Controls.Add(this.genButton);
            this.groupBox2.Location = new System.Drawing.Point(13, 88);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1305, 115);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "clooca";
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(1198, 70);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 3;
            this.clearButton.Text = "clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // cloocaTextBox
            // 
            this.cloocaTextBox.Location = new System.Drawing.Point(18, 21);
            this.cloocaTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.cloocaTextBox.Multiline = true;
            this.cloocaTextBox.Name = "cloocaTextBox";
            this.cloocaTextBox.Size = new System.Drawing.Size(1130, 87);
            this.cloocaTextBox.TabIndex = 2;
            // 
            // genButton
            // 
            this.genButton.Location = new System.Drawing.Point(1198, 21);
            this.genButton.Name = "genButton";
            this.genButton.Size = new System.Drawing.Size(86, 31);
            this.genButton.TabIndex = 0;
            this.genButton.Text = "genarate";
            this.genButton.UseVisualStyleBackColor = true;
            this.genButton.Click += new System.EventHandler(this.genButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(582, 855);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(309, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "(c)2012 KAWASAKI Toshiya All Right Reserved.";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(923, 8);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(223, 60);
            this.startButton.TabIndex = 11;
            this.startButton.Text = "START!";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 879);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.stop);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.export);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.grpRecv);
            this.Controls.Add(this.grpSend);
            this.Controls.Add(this.grpSetting);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "NXT Testing tool by IKKK";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpSetting.ResumeLayout(false);
            this.grpSend.ResumeLayout(false);
            this.grpSend.PerformLayout();
            this.grpRecv.ResumeLayout(false);
            this.grpRecv.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Table)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSetting;
        private System.Windows.Forms.ComboBox cmbPortName;
        private System.Windows.Forms.GroupBox grpSend;
        private System.Windows.Forms.TextBox sndTextBox;
        private System.Windows.Forms.Button sndButton;
        private System.Windows.Forms.GroupBox grpRecv;
        private System.Windows.Forms.TextBox rcvTextBox;
        private System.Windows.Forms.Button connectButton;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button export;
        private System.Windows.Forms.DataGridView Table;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dat1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dat2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Batt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mo_A;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mo_B;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mo_C;
        private System.Windows.Forms.DataGridViewTextBoxColumn AD_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn AD_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn AD_3;
        private System.Windows.Forms.DataGridViewTextBoxColumn AD_4;
        private System.Windows.Forms.DataGridViewTextBoxColumn I2C;
        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.Button stop;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox cloocaTextBox;
        private System.Windows.Forms.Button genButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button startButton;
    }
}

