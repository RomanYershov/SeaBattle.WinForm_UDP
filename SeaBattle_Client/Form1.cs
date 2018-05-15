using SeaBattle.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SeaBattle_Client
{
    public partial class Form1 : Form
    {
        private static IPAddress remoteIPAddress;
        private static int remotePort;
        private static int localPort;

        private static List<Label> Field;
        private static List<Ship> Ships;
        private static List<string> MyCoordinates;
        private string targetPos;
        private Label targetLabel;



        public Form1()
        {
            InitializeComponent();
            Field = new List<Label>();
            Ships = new List<Ship>();
            MyCoordinates = new List<string>();
            SaveField();
        }



        private void SaveField()
        {
            for (int i = groupBox1.Controls.Count - 1; i >= 0; i--)
            {
                Label label = groupBox1.Controls[i] as Label;
                Field.Add(label);
            }
        }

        public static void Send(string datagram)
        {
            // Создаем UdpClient
            UdpClient sender = new UdpClient();

            // Создаем endPoint по информации об удаленном хосте
            IPEndPoint endPoint = new IPEndPoint(remoteIPAddress, remotePort);

            try
            {
                // Преобразуем данные в массив байтов
                byte[] bytes = Encoding.UTF8.GetBytes(datagram);

                // Отправляем данные
                sender.Send(bytes, bytes.Length, endPoint);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникло исключение: " + ex.ToString() + "\n  " + ex.Message);
            }
            finally
            {
                // Закрыть соединение
                sender.Close();
            }
        }


        public void Receiver()
        {
            // Создаем UdpClient для чтения входящих данных
            UdpClient receivingUdpClient = new UdpClient(localPort);

            IPEndPoint RemoteIpEndPoint = null;

            try
            {
                while (true)
                {
                    // Ожидание дейтаграммы
                    byte[] receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);


                    // Преобразуем и отображаем данные
                    string returnData = Encoding.UTF8.GetString(receiveBytes);
                    Action action;
                    if (returnData == "yes")
                    {
                        targetLabel.BackColor = Color.Red;
                        action = () =>
                        {
                            targetLabel.Text = "X";

                        };
                        Invoke(action);
                    }
                    else if (returnData == "no")
                    {
                        action = () =>
                        {
                            targetLabel.Text = "X";

                        };
                        Invoke(action);

                    }

                    //Field.FirstOrDefault(x => x.Tag.ToString() == returnData).Text = "X";

                    else IsBingo(returnData);


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникло исключение: " + ex.ToString() + "\n  " + ex.Message);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //Send(richTextBox1.Text);
            //label1.Text += "\nyou: " + richTextBox1.Text + "\n";
            //richTextBox1.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                localPort = Convert.ToInt16("11000");


                remotePort = Convert.ToInt16("10000");


                remoteIPAddress = IPAddress.Parse("127.0.0.1");

                Task task = new Task(Receiver);
                task.Start();

                // tRec = new Thread(new ThreadStart(Receiver));
                // tRec.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникло исключение: " + ex.ToString() + "\n  " + ex.Message);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Label selectedCell = ((Label)sender);
            if (selectedCell.Text == " ")
            {
                selectedCell.BackColor = Color.Gainsboro;
                selectedCell.Text = string.Empty;
                MyCoordinates.Remove(selectedCell.Tag.ToString());
                return;
            }

            if (MyCoordinates.Count >= 15)
            {
                MessageBox.Show("Максимальное кол-во");
                return;
            }
            selectedCell.BackColor = Color.BlanchedAlmond;
            selectedCell.Text = " ";

            MyCoordinates.Add(selectedCell.Tag.ToString());

        }

        private void EnemyFieldCell_Click(object sender, EventArgs e)
        {
            targetLabel = ((Label)sender);
            string targetPos = targetLabel.Tag.ToString();
            //if (MyCoordinates.Contains(pos))
            //{
            //    Field.Where(x => x.Tag.ToString() == pos)
            //        .FirstOrDefault()
            //        .BackColor = Color.Black;
            //    MyCoordinates.Remove(pos);
            //    selectedCell.BackColor = Color.Red;
            //    MessageBox.Show("Попал!!!");
            //    return;
            //}
            //selectedCell.Text = "X";
            Send(targetPos);
        }

        private void IsBingo(string pos)
        {
            targetLabel = Field.Where(x => x.Tag.ToString() == pos)
                      .FirstOrDefault();
            if (MyCoordinates.Contains(pos))
            {
                targetLabel.BackColor = Color.Gray;
                Action action = () =>
                {
                    targetLabel.Text = "X";
                };
                Invoke(action);
                MyCoordinates.Remove(pos);
                Send("yes");
            }
            else
            {
                Action action = () =>
                {
                    targetLabel.Text = "X";
                };
                Invoke(action);
                Send("no");
            }
        }


    }
}
