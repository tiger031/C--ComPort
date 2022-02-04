using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComPort
{
    public partial class Form1 : Form
    {
        string dataOut;
        string sendWidth;
        string dataIn;

       
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            comboBox1.Items.AddRange(ports);
            checkBox1.Checked = false;
            serialPort1.DtrEnable = false;
            checkBox2.Checked = false;
            serialPort1.RtsEnable = false;
            button3.Enabled = false;
            checkBox5.Checked = false ;
            checkBox6.Checked = true;
            sendWidth = "Write";
            button1.Enabled = true;
            button2.Enabled = false;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = comboBox1.Text;
                serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);
                serialPort1.DataBits = Convert.ToInt32(comboBox3.Text);
                serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), comboBox4.Text);
                serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), comboBox5.Text);

                serialPort1.Open();
                progressBar1.Value = 100;
                button1.Enabled = false;
                button2.Enabled = true;
                label8.Text = "ON";
            }catch (Exception err)
            {
                MessageBox.Show(err.Message,"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button1.Enabled = true;
                button2.Enabled = false;
                label8.Text = "OFF";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                progressBar1.Value = 50;
                progressBar1.Value = 0;
                button1.Enabled = true;
                button2.Enabled = false;
                label8.Text = "OFF";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOut = textBox1.Text;
                if (sendWidth == "WriteLine")
                {
                    serialPort1.WriteLine(dataOut);
                }else if(sendWidth == "Write"){
                    serialPort1.Write(dataOut);
                }
            
                
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Enabled)
            {
                serialPort1.DtrEnable = true;
            }else
            {
                serialPort1.DtrEnable = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                serialPort1.RtsEnable = true;
            }
            else
            {
                serialPort1.RtsEnable = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != ""){
                textBox1.Clear();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int dataOUTlength = textBox1.TextLength;
            label7.Text = string.Format("{0:00}", dataOUTlength);
            if (checkBox4.Checked)
            {
                textBox1.Text = textBox1.Text.Replace(Environment.NewLine, "");
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                button3.Enabled = true;
            }else
            {
                button3.Enabled = false;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (checkBox4.Checked)
            {
                if(e.KeyCode == Keys.Enter)
                {
                    if (serialPort1.IsOpen)
                    {
                        dataOut = textBox1.Text;
                        if (sendWidth == "WriteLine")
                        {
                            serialPort1.WriteLine(dataOut);
                        }
                        else if (sendWidth == "Write")
                        {
                            serialPort1.Write(dataOut);
                        }

                        
                    }
                }
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                sendWidth = "WriteLine";
                checkBox6.Checked = false;
                checkBox5.Checked = true;

            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                sendWidth = "Write";
                checkBox6.Checked = true;
                checkBox5.Checked = false;
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            dataIn = serialPort1.ReadExisting();
            this.Invoke(new EventHandler(ShowData));
        }

        private void ShowData(object sender, EventArgs e)
        {
            textBox2.Text = dataIn;
        }
    }
}
