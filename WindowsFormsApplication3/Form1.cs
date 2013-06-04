
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using Codeplex.Data;
using System.Net;

using System.Threading;


namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {

        public static uint startTime;  //start time of send log
        public static bool receive_stop; 

        public static int TYPE_U8=0;
        public static int TYPE_S16=1;
        public static int TYPE_S32 = 2;

        String log_str = "Whole Log\nTime,Dat1,Dat2,Batt,Mo_A,Mo_B,Mo_C,AD_1,AD_2,AD_3,AD_4,I2C\n";
        int tbl_cnt = 21;
        public static int LOG_SPAN = 40;

      
        private delegate void Delegate_RcvByteToTextBox(byte[] log);
   

        /****************************************************************************/
        /*!
         *	@brief	コンストラクタ.
         *
         *	@param	なし.
         *
         *	@retval	なし.
         */
        public Form1()
        {
            InitializeComponent();
        }

        /****************************************************************************/
        /*!
         *	
         *
         *	@param	[in]	sender	イベントの送信元のオブジェクト.
         *	@param	[in]	e		イベント情報.
         *
         *	@retval	なし.
         */
        private void Form1_Load(object sender, EventArgs e)
        {
            //get available port name
            string[] PortList = SerialPort.GetPortNames();

            cmbPortName.Items.Clear();

            // add Serial Port Name To cmbBox
            foreach (string PortName in PortList)
            {
                cmbPortName.Items.Add(PortName);
            }
            if (cmbPortName.Items.Count > 0)
            {
                cmbPortName.SelectedIndex = 0;
            }
          

            //add Type of Variables
            cmbType.Items.Add("U8(unsigned char)");
            cmbType.Items.Add("S16(signed short)");
            cmbType.Items.Add("S32(signed int)");
            cmbType.SelectedIndex = 1;
 

            //user cant add row to table
            Table.AllowUserToAddRows = false;
          

            // 送受信用のテキストボックスをクリアする.
            sndTextBox.Clear();
          

            d_password.PasswordChar = '*';
        }

        /****************************************************************************/
        /*!
         *	@brief	ダイアログの終了処理.
         *
         *	@param	[in]	sender	イベントの送信元のオブジェクト.
         *	@param	[in]	e		イベント情報.
         *
         *	@retval	なし.
         */
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //! シリアルポートをオープンしている場合、クローズする.
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
        }



        delegate void connectionFinishedDelegate();
        internal void connectionFinished() {
            //! ボタンの表示を[接続]から[切断]に変える.
            connectButton.Text = "切断";
            connectLoading.Hide();
            connectButton.Enabled = true;
        }


        delegate void ErrorConnectionDelegate();
        internal void ErrorConnection()
        {
            //! ボタンの表示を[接続]から[切断]に変える.
         
            connectLoading.Hide();
            connectButton.Enabled = true;
        }

        delegate void setPortNameDelegate();
        internal void setPortName(){

            connectLoading.Show();
            connectButton.Enabled = false;
            serialPort1.PortName = cmbPortName.SelectedItem.ToString();
        }



        void connect_to_nxt() {
            //! オープンするシリアルポートをコンボボックスから取り出す.
            Invoke(new setPortNameDelegate(setPortName));

            //! ボーレートをコンボボックスから取り出す.

            int BAUDRATE = 38400;
            serialPort1.BaudRate = BAUDRATE;

            //! データビットをセットする. (データビット = 8ビット)
            serialPort1.DataBits = 8;
            // serialPort1.DataBits = 256;
            //! パリティビットをセットする. (パリティビット = なし)
            serialPort1.Parity = Parity.None;

            //! ストップビットをセットする. (ストップビット = 1ビット)
            serialPort1.StopBits = StopBits.One;

            serialPort1.Handshake = Handshake.None;

            //! 文字コードをセットする.
            serialPort1.Encoding = Encoding.Unicode;

            try
            {
                //! シリアルポートをオープンする.
                serialPort1.Open();

                Invoke(new connectionFinishedDelegate(connectionFinished));
             

                receive_stop = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Invoke(new ErrorConnectionDelegate(ErrorConnection));
            }
           
        
        
        
        }

        /****************************************************************************/
        /*!
         *	@brief	[接続]/[切断]ボタンを押したときにシリアルポートのオープン/クローズを行う.
         *
         *	@param	[in]	sender	イベントの送信元のオブジェクト.
         *	@param	[in]	e		イベント情報.
         *
         *	@retval	なし.
         */
        private void connectButton_Click(object sender, EventArgs e)
        {

            if (serialPort1.IsOpen == true)
            {
                //! シリアルポートをクローズする.
                serialPort1.Close();

                //! ボタンの表示を[切断]から[接続]に変える.
                connectButton.Text = "接続";
                receive_stop = true;
                startTime = 0;
            }
            else
            {
                Thread connectThread = new Thread(connect_to_nxt);
                connectThread.Start();
            }
        }

        /****************************************************************************/
        /*!
         *	@brief	[送信]ボタンを押して、データ送信を行う.
         *
         *	@param	[in]	sender	イベントの送信元のオブジェクト.
         *	@param	[in]	e		イベント情報.
         *
         *	@retval	なし.
         */
        private void sndButton_Click(object sender, EventArgs e)
        {
            //! シリアルポートをオープンしていない場合、処理を行わない.
            if (serialPort1.IsOpen == false)
            {
                MessageBox.Show("接続されていません", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //! テキストボックスから、送信するテキストを取り出す.
            String data = sndTextBox.Text;

            //! 送信するテキストがない場合、データ送信は行わない.
            if (string.IsNullOrEmpty(data) == true)
            {
               // return;
            }

            try
            {
                //! シリアルポートからテキストを送信する.
             // serialPort1.Write(data);
                var jsonString = data;


                //送信配列の型を取得
                int sendType=0;
                if (cmbType.SelectedItem.ToString().CompareTo("U8(unsigned char)") == 0)
                {
                    sendType = TYPE_U8;
                }
                else if (cmbType.SelectedItem.ToString().CompareTo("S16(signed short)") == 0)
                {
                    sendType = TYPE_S16;
                }
                else if (cmbType.SelectedItem.ToString().CompareTo("S32(signed int)") == 0)
                {
                    sendType = TYPE_S32;
                }


               
                //JSON解析

                var obj = DynamicJson.Parse(data);

                //型に応じて送る
                if(sendType == TYPE_U8){
                    foreach(var outer in obj){
                        byte[] buf = new byte[34];
                        uint len = 32;
                        buf[0] = (byte)(len & 0xFF);
                        buf[1] = (byte)((len>>8) & 0xFF);
                        int i = 0;

                        foreach(var inner in outer){
                            Console.WriteLine("data{0}:{1}\n",i,inner);
                            buf[i+2] = (byte)inner;
                            i++;
                        }
                        serialPort1.Write(buf, 0, 34);
                        System.Threading.Thread.Sleep(100);
                    }

                 }else if(sendType==TYPE_S16){
                     Console.WriteLine("S16");

                     foreach (var outer in obj)
                     {
                         byte[] buf = new byte[34];

                         uint len = 32;
                         buf[0] = (byte)(len & 0xFF);
                         buf[1] = (byte)((len >> 8) & 0xFF);
                         int i = 0;

                         
                         int ptr = 2;
                         foreach (var inner in outer) {
                            
                                 buf[ptr] = (byte)((short)inner & 0xFF);
                                 buf[ptr + 1] = (byte)(((short)inner >> 8) & 0xFF);
                                 ptr+=2;

                         }

                         Console.WriteLine(BitConverter.ToString(buf));
                         serialPort1.Write(buf, 0, 34);
                         System.Threading.Thread.Sleep(100);
                     }

                 }
                else if (sendType == TYPE_S32)
                {
                    Console.WriteLine("S32");

                    foreach (var outer in obj)
                    {
                        byte[] buf = new byte[34];

                        uint len = 32;
                        buf[0] = (byte)(len & 0xFF);
                        buf[1] = (byte)((len >> 8) & 0xFF);
                        int i = 0;


                        int ptr = 2;
                        foreach (var inner in outer)
                        {

                            buf[ptr] = (byte)((int)inner & 0xFF);
                            buf[ptr + 1] = (byte)(((int)inner >> 8) & 0xFF);
                            buf[ptr + 2] = (byte)(((int)inner >> 16) & 0xFF);
                            buf[ptr + 3] = (byte)(((int)inner >> 24) & 0xFF);
                            ptr += 4;

                        }

                        Console.WriteLine(BitConverter.ToString(buf));
                        serialPort1.Write(buf, 0, 34);
                        System.Threading.Thread.Sleep(100);
                    }

                }
        
                 //! 送信データを入力するテキストボックスをクリアする.
                //sndTextBox.Clear();
                Console.WriteLine("send");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine("error");
            }
        }

        /****************************************************************************/
        /*!
         *	@brief	データ受信が発生したときのイベント処理.
         *
         *	@param	[in]	sender	イベントの送信元のオブジェクト.
         *	@param	[in]	e		イベント情報.
         *
         *	@retval	なし.
         */
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            //! シリアルポートをオープンしていない場合、処理を行わない.
            if (serialPort1.IsOpen == false)
            {
                return;
            }

            try
            {
                //! 受信データを読み込む.
               //string data = serialPort1.ReadExisting();

                byte[] log = new byte[34];
                serialPort1.Read(log,0,34);


                    //! 受信したデータをテキストボックスに書き込む.

                if (!receive_stop)
                {
                    Invoke(new Delegate_RcvByteToTextBox(RcvByteToTextBox), new Object[] { log });
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /****************************************************************************/
        /*!
         *	@brief	受信データをテキストボックスに書き込む.
         *
         *	@param	[in]	data	受信した文字列.
         *
         *	@retval	なし.
         */
   

        private void RcvByteToTextBox(byte[] log) 
        {
            //! 受信データをテキストボックスの最後に追記する.
            if (log != null)
            {

                if (startTime == 0) {
                    startTime = BitConverter.ToUInt32(log, 2);
                }
               uint Time = BitConverter.ToUInt32(log,2) - startTime;
               sbyte Dat1 = (sbyte)log[7];
               sbyte Dat2 = (sbyte)log[6];
               ushort Batt = BitConverter.ToUInt16(log, 8);
               int Mo_A = BitConverter.ToInt32(log, 10);
               int Mo_B = BitConverter.ToInt32(log, 14);
               int Mo_C = BitConverter.ToInt32(log, 18);
                short AD_1 = BitConverter.ToInt16(log, 22);
                short AD_2 = BitConverter.ToInt16(log, 24);
                short AD_3 = BitConverter.ToInt16(log, 26);
                short AD_4 = BitConverter.ToInt16(log, 28);
                int I2C = BitConverter.ToInt32(log, 30);

        

                if (I2C > -1 && I2C < 256)
                {
                    log_str += string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}\n", Time.ToString(), Dat1.ToString(), Dat2.ToString(), Batt.ToString(), Mo_A.ToString(), Mo_B.ToString(), Mo_C.ToString(), AD_1.ToString(), AD_2.ToString(), AD_3.ToString(), AD_4.ToString(), I2C.ToString());
                    if (tbl_cnt > LOG_SPAN )
                    {
                        Table.Rows.Add(Time.ToString(), Dat1.ToString(), Dat2.ToString(), Batt.ToString(), Mo_A.ToString(), Mo_B.ToString(), Mo_C.ToString(), AD_1.ToString(), AD_2.ToString(), AD_3.ToString(), AD_4.ToString(), I2C.ToString());
                        tbl_cnt = 0;
                    }
                    else
                    {
                        tbl_cnt++;
                    }
                    if (Table.RowCount > 5)
                    {
                        Table.FirstDisplayedScrollingRowIndex = Table.RowCount - 3;
                    }

                
                }
              
              
           

            }
        }



        private void grpSend_Enter(object sender, EventArgs e)
        {

        }

        private void grpRecv_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void clear_Click(object sender, EventArgs e)
        {
            int rows = Table.RowCount;
            for (int i = 0; i < rows; i++) {
                Table.Rows.RemoveAt(0);
            }


        }
        private void stop_Click(object sender, EventArgs e)
        {
            if (receive_stop)
            {
                receive_stop = false;
                stop.Text = "一時停止";

            }
            else {
                receive_stop = true;
                stop.Text = "再開";
            }

        }

        private void export_Click(object sender, EventArgs e)
        {

            //SaveFileDialogクラスのインスタンスを作成
            SaveFileDialog sfd = new SaveFileDialog();

            //はじめのファイル名を指定する
            sfd.FileName ="*.csv";
            //はじめに表示されるフォルダを指定する
            sfd.InitialDirectory = @"";
      
            //[ファイルの種類]ではじめに
            //「すべてのファイル」が選択されているようにする
            sfd.FilterIndex = 2;
            //タイトルを設定する
            sfd.Title = "保存先のファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            sfd.RestoreDirectory = true;
            //既に存在するファイル名を指定したとき警告する
            //デフォルトでTrueなので指定する必要はない
            sfd.OverwritePrompt = true;
            //存在しないパスが指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            sfd.CheckPathExists = true;

            //ダイアログを表示する
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき
                //選択されたファイル名を表示する
                Console.WriteLine(sfd.FileName);
                exportCSV(sfd.FileName);
            }

        }

        private void exportCSV(String fp) {

            StreamWriter sw = new StreamWriter(fp, false, System.Text.Encoding.GetEncoding("UTF-8"));

            sw.Write(log_str);
            /*
            String header = "Time,Dat1,Dat2,Batt,Mo_A,Mo_B,Mo_C,AD_1,AD_2,AD_3,AD_4,I2C\n";
            sw.Write(header);

            for (int r = 0; r < Table.Rows.Count; r++) {
                for (int c = 0; c < Table.Columns.Count; c++)
                {

                    String dt = "";
                    if (Table.Rows[r].Cells[c].Value != null) {
                        dt = Table.Rows[r].Cells[c].Value.ToString();
                    }
                    if (c < Table.Columns.Count - 1) {
                        dt += ",";
                    }

                    sw.Write(dt);

                }

                sw.Write("\n");
            
            }
*/
            sw.Close();
    
        }

        private void genButton_Click(object sender, EventArgs e)
        {
            String data = cloocaTextBox.Text;
  
            try
            {
               var obj = DynamicJson.Parse(data);
        
            int packet_no=1;
            String send ="[";
            //header
            send += "["+packet_no+",1,";
            send +=obj["state_num"]+",";
            send += obj["event_num"] + "],";

            packet_no++;

            //matrix

            int count = 0;

            foreach(int element in obj["matrix"]){
                if (count % 14 == 0) {
                    send += "[" + packet_no + ",2";
                    packet_no++;
                }
                
                
                send += "," + element;
                count++;
                if (count % 14 == 0 || count == int.Parse(obj["state_num"] )*int.Parse(obj["event_num"] ))
                {
                    send += "],";
                }
            }
            int sum_of_matrix = count;

          
            count = 0;
            foreach (var element in obj["states"]) {
                

                //state no
                if (count % 14 == 0)
                {
                    send += "[" + packet_no + ",3";
                    packet_no++;
                }
                send += "," + element["id"];
                count++;
                if (count % 14 == 0)
                {
                    send += "],";
                }
                
                //action no
                if (count % 14 == 0)
                {
                    send += "[" + packet_no + ",3";
                    packet_no++;
                }
                send += "," + element["action"];
                count++;

                if (count % 14 == 0)
                {
                    send += "],";
                }

                //value1
                if (count % 14 == 0)
                {
                    send += "[" + packet_no + ",3";
                    packet_no++;
                }
                if (element["value1"] == "") {
                    send += ",0";
                    
                }
                else
                {
                    send += "," + element["value1"];
                }
                count++;

                if (count % 14 == 0)
                {
                    send += "],";
                }


                //value2
                if (count % 14 == 0)
                {
                    send += "[" + packet_no + ",3";
                    packet_no++;
                }
                if (element["value2"] == "")
                {
                    send += ",0";

                }
                else
                {
                    send += "," + element["value2"];
                }
                count++;

                if (count % 14 == 0)
                {
                    send += "],";
                }

                //value3
                if (count % 14 == 0)
                {
                    send += "[" + packet_no + ",3";
                    packet_no++;
                }
                if (element["value3"] == "")
                {
                    send += ",0";

                }
                else
                {
                    send += "," + element["value3"];
                }
                count++;

                if (count % 14 == 0)
                {
                    send += "],";
                }

                //value4
                if (count % 14 == 0)
                {
                    send += "[" + packet_no + ",3";
                    packet_no++;
                }
                if (element["value4"] == "")
                {
                    send += ",0";

                }
                else
                {
                    send += "," + element["value4"];
                }
                count++;

                if (count == (6 * (int.Parse(obj["state_num"])+1)))
                {
                    send += "],";
                }
                else if (count % 14 == 0)
                {
                    send += "],";
                }

                
            }

            int sum_of_states = count;

            send += "[" + packet_no + ",255]";
            send += "]";    
            sndTextBox.Text=send;
            string str = string.Format("sum of matrix{0} ,sum of state{1}", sum_of_matrix, sum_of_states);
           // MessageBox.Show(str);
            Console.WriteLine(str);

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            cloocaTextBox.Text = "";
            sndTextBox.Text = "";
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            //! シリアルポートをオープンしていない場合、処理を行わない.
            if (serialPort1.IsOpen == false)
            {
                //return;
            }
            //! テキストボックスから、送信するテキストを取り出す.
            String data = "[[0719,254,0407]]";

            //! 送信するテキストがない場合、データ送信は行わない.
            if (string.IsNullOrEmpty(data) == true)
            {
                // return;
            }

            try
            {
                //! シリアルポートからテキストを送信する.
                // serialPort1.Write(data);
                var jsonString = data;


                //送信配列の型を取得
                int sendType = 0;
                if (cmbType.SelectedItem.ToString().CompareTo("U8(unsigned char)") == 0)
                {
                    sendType = TYPE_U8;
                }
                else if (cmbType.SelectedItem.ToString().CompareTo("S16(signed short)") == 0)
                {
                    sendType = TYPE_S16;
                }
                else if (cmbType.SelectedItem.ToString().CompareTo("S32(signed int)") == 0)
                {
                    sendType = TYPE_S32;
                }



                //JSON解析

                var obj = DynamicJson.Parse(data);

                //型に応じて送る
                if (sendType == TYPE_U8)
                {
                    foreach (var outer in obj)
                    {
                        byte[] buf = new byte[34];
                        uint len = 32;
                        buf[0] = (byte)(len & 0xFF);
                        buf[1] = (byte)((len >> 8) & 0xFF);
                        int i = 0;

                        foreach (var inner in outer)
                        {
                            Console.WriteLine("data{0}:{1}\n", i, inner);
                            buf[i + 2] = (byte)inner;
                            i++;
                        }
                        serialPort1.Write(buf, 0, 34);
                        //System.Threading.Thread.Sleep(100);
                    }

                }
                else if (sendType == TYPE_S16)
                {
                    Console.WriteLine("S16");

                    foreach (var outer in obj)
                    {
                        byte[] buf = new byte[34];

                        uint len = 32;
                        buf[0] = (byte)(len & 0xFF);
                        buf[1] = (byte)((len >> 8) & 0xFF);
                        int i = 0;


                        int ptr = 2;
                        foreach (var inner in outer)
                        {

                            buf[ptr] = (byte)((short)inner & 0xFF);
                            buf[ptr + 1] = (byte)(((short)inner >> 8) & 0xFF);
                            ptr += 2;

                        }

                        Console.WriteLine(BitConverter.ToString(buf));
                        serialPort1.Write(buf, 0, 34);
                        //System.Threading.Thread.Sleep(100);
                    }

                }
                else if (sendType == TYPE_S32)
                {
                    Console.WriteLine("S32");

                    foreach (var outer in obj)
                    {
                        byte[] buf = new byte[34];

                        uint len = 32;
                        buf[0] = (byte)(len & 0xFF);
                        buf[1] = (byte)((len >> 8) & 0xFF);
                        int i = 0;


                        int ptr = 2;
                        foreach (var inner in outer)
                        {

                            buf[ptr] = (byte)((int)inner & 0xFF);
                            buf[ptr + 1] = (byte)(((int)inner >> 8) & 0xFF);
                            buf[ptr + 2] = (byte)(((int)inner >> 16) & 0xFF);
                            buf[ptr + 3] = (byte)(((int)inner >> 24) & 0xFF);
                            ptr += 4;

                        }

                        Console.WriteLine(BitConverter.ToString(buf));
                        serialPort1.Write(buf, 0, 34);
                        //System.Threading.Thread.Sleep(100);
                    }

                }

         
                Console.WriteLine("sent start signal");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine("error");
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void sndTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        delegate void gotFromCloocaDelegate(string data);

        internal void gotFromClooca(string data) {
            var obj = DynamicJson.Parse(data);
            Console.WriteLine(obj[0]["output"]);
            cloocaTextBox.Text = obj[0]["output"];
            genButton_Click(null, null);

            cloocaLoading.Hide();
            btn_direct.Enabled = true;
        
        }

        delegate void errorLoadingCloocaDelegate();

        internal void errorLoadingClooca() {
            cloocaLoading.Hide();
            btn_direct.Enabled = true;

        }

        delegate void startLoadingCloocaDelegate();

        internal void startLoadingClooca()
        {
            cloocaLoading.Show();
            btn_direct.Enabled = false;

        }
        private void load_from_clooca() {
            string pkey = d_projectkey.Text;
            string branch = d_branch.Text;
            string version = d_version.Text;

            string email = d_email.Text;
            string pwd = d_password.Text;

            if (pkey.Length == 0)
            {
                MessageBox.Show("project keyを入力してください", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (email.Length == 0)
            {
                MessageBox.Show("emailを入力してください", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (pwd.Length == 0)
            {
                MessageBox.Show("パスワードを入力してください", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            String url = "http://www.clooca.com/api/dl/" + pkey + "?return_type=json";
            if (branch.Length != 0) url += "&branch=" + branch;
            if (version.Length != 0) url += "&version=" + version;
            Console.WriteLine(url);

            //basic認証
            WebClient myweb = new WebClient();
            //認証情報
            myweb.Credentials = new NetworkCredential(email, pwd);
            //ダウンロード
            try
            {
                Invoke(new startLoadingCloocaDelegate(startLoadingClooca));
                byte[] pagedata = myweb.DownloadData(url);

                //取得先のサイトに合わせた文字コード設定
                Encoding ec = Encoding.UTF8;//UTF8の例
                // Encoding ec = Encoding.GetEncoding("shift-jis");//シフトGISの例
                Console.WriteLine(ec.GetString(pagedata));

                Invoke(new gotFromCloocaDelegate(gotFromClooca), ec.GetString(pagedata));
               
            }
            catch (WebException exc)
            {
                Console.WriteLine(exc);
                MessageBox.Show(exc.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);;
                Invoke(new errorLoadingCloocaDelegate(errorLoadingClooca));
            }
           
        
        }

        private void btn_direct_Click(object sender, EventArgs e)
        {
          

            Thread cloocaThread = new Thread(load_from_clooca);
           
            cloocaThread.Start();



            
        }

      
    }
}

/* End of file */
