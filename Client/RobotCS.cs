﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text.RegularExpressions;

namespace Client
{
    public partial class RobotCS : Form
    {
        public RobotCS(dynamic args)
        {
            InitializeComponent();
            useAs = args;
        }

        HelperClass hc = new HelperClass();
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        static byte[] _buffer = new byte[1024];
        static Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static Socket _toServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Dictionary<string, Socket> _socketDict = new Dictionary<string, Socket>();
        internal int port, attempts = 0;
        internal string useAs, myIP;
        
        private void RobotCS_Load(object sender, EventArgs e)
        {
            this.Text = gbxRobot.Text = useAs;
            myIP = GetIPAddress();
            tbxIPRobot.Text /*= "169.254.162.201"*/ = GetIPAddress();
            tbxPortRobot.Text = "8686";
            tbxIPBS.Text = GetIPAddress() /*= "192.168.165.10"*/;
            tbxPortBS.Text = "8686";
        }

        private void btnOpenServer_Click(object sender, EventArgs e)
        {
            if ((!string.IsNullOrWhiteSpace(tbxIPRobot.Text)) && (!string.IsNullOrWhiteSpace(tbxPortRobot.Text)))
                new Thread(obj => SetupServer(tbxPortRobot.Text)).Start();
        }

        delegate void addCommandCallback(string text);

        private void addCommand(string text)
        {
            try
            {
                if (this.tbxStatus.InvokeRequired)
                {
                    addCommandCallback d = new addCommandCallback(addCommand);
                    this.Invoke(d, new object[] { text });
                }
                else
                    this.tbxStatus.Text += text + Environment.NewLine;
            }
            catch (Exception e)
            {
                addCommand("# Error set text tbxStatus \n\n" + e);
            }
        }

        string socketToIP(Socket socket)
        {
            return (socket.RemoteEndPoint.ToString().Split(':'))[0];
        }

        string socketToName(Socket socket)
        {
            dynamic[] arr = { "BaseStation", "RefereeBox", "Robot1", "Robot2", "Robot3" };
            for (int i = 0; i < arr.Length; i++)
                if ((_socketDict.ContainsKey(arr[i])) && (_socketDict[arr[i]].RemoteEndPoint == socket.RemoteEndPoint))
                    return arr[i];
            return socket.RemoteEndPoint.ToString();
        }

        void SetupServer(dynamic port)
        {
            try
            {
                if ((!string.IsNullOrWhiteSpace(tbxIPRobot.Text)) && (!string.IsNullOrWhiteSpace(tbxPortRobot.Text)))
                {
                    addCommand("# Setting up server...");
                    addCommand("# Open for IP : " + tbxIPRobot.Text + " (" + this.Text + ") \t Port : " + port);
                    //hc.SetText(this, lblConnectionBS, "Open");
                    _serverSocket.Bind(new IPEndPoint(IPAddress.Any, this.port = int.Parse(port)));
                    _serverSocket.Listen(1);
                    _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
                }
            }
            catch (Exception e)
            {
                addCommand("# FAILED to open server connection \n\n" + e);
            }

        }

        private void AcceptCallback(IAsyncResult AR)
        {
            try
            {
                Socket socket = _serverSocket.EndAccept(AR);
                if (socket.Connected)
                {
                    _socketDict.Add(socket.RemoteEndPoint.ToString(), socket);
                    socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), socket);
                    //new Thread(obj => socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), socket)).Start();
                    _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
                    addCommand("# Success Connected to: " + socketToIP(socket));
                    //MessageBox.Show(_toServerSocketDict.Keys.Where(item => item.StartsWith("192.168.1.107")).ElementAt(0))  
                }
            }
            catch (Exception e)
            {
                addCommand("# FAILED to connected \n\n" + e);
            }
        }

        private void ReceiveCallBack(IAsyncResult AR)
        {
            try
            {
                Socket socket = (Socket)AR.AsyncState;
                int received = socket.EndReceive(AR);
                byte[] dataBuf = new byte[received];
                Array.Copy(_buffer, dataBuf, received);
                string text = Encoding.ASCII.GetString(dataBuf).Trim();
                var _data = text.Split('|');
                addCommand("> " + socketToName(socket) + " : " + _data[0]);
                //new Thread(obj => addCommand("> " + socketToIP(socket) + " : " + _data[0])).Start();

                string respone = ResponeCallback(_data[0], socket);
                if (!string.IsNullOrEmpty(respone))
                {
                    if (_data.Count() == 1)
                        SendCallBack(socket, respone);
                    else
                        sendByHostList(_data[1], respone);
                }
                socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), socket);
                //new Thread(obj => socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), socket)).Start();
            }
            catch (Exception e)
            {
                addCommand("# FAILED to receive message \n\n" + e);
            }
        }

        void SendCallBack(Socket _dstSocket, string txtMessage)
        {
            try
            {
                addCommand("@ " + socketToName(_dstSocket) + " : " + txtMessage);
                byte[] buffer = Encoding.ASCII.GetBytes(txtMessage);
                _dstSocket.Send(buffer);
                _dstSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), _dstSocket);
                //new Thread(obj => _dstSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), _dstSocket)).Start();
            }
            catch (Exception e)
            {
                addCommand("# FAILED to send message \n\n" + e);
            }
        }

        void sendByHostList(dynamic inputHostList, string txtMsg)
        {
            try
            {
                var hostList = inputHostList.Split(',');
                foreach (var _hostList in hostList)
                {
                    try
                    {
                        SendCallBack(_socketDict[_socketDict.Keys.Where(host => host.StartsWith(_hostList)).ElementAtOrDefault(0).ToString()], txtMsg);
                    }
                    catch (Exception)
                    {
                        continue;   // If host not found then Skip
                    }
                }
            }
            catch (Exception e)
            {
                addCommand("# FAILED to send message \n\n" + e);
                MessageBox.Show("host Not Found :<");
            }
        }        

        string ResponeCallback(dynamic text, Socket socket)
        {
            string respone = string.Empty;
            if (Regex.IsMatch(text, @"X:[-]{0,1}[0-9]{1,4},Y:[-]{0,1}[0-9]{1,4}"))
            {
                // If message is data X & Y from encoder
                /// Scale is 1 : 20 
                int[] _posXY = new int[2];
                var posXY = text.Split(',');
                if (posXY.Length == 2) // If data receive only one X & Y
                {
                    _posXY[0] = int.Parse(posXY[0].Split(':')[1]);
                    _posXY[1] = int.Parse(posXY[1].Split(':')[1]);
                }
                else // If data receive multi X & Y (error problems)
                {
                    _posXY[0] = int.Parse(posXY[posXY.Length - 2].Split(':')[2]);
                    _posXY[1] = int.Parse(posXY[posXY.Length - 1].Split(':')[1]);
                }

                hc.SetText(this, tbxX, _posXY[0].ToString());          // On encoder tbx
                hc.SetText(this, tbxY, _posXY[1].ToString());
            }
            else if (Regex.IsMatch(text, @"BaseStation"))
            {
                // If will rename key in socket dictionary
                Socket temp = _socketDict[socket.RemoteEndPoint.ToString()];    // Backup
                _socketDict.Remove(socket.RemoteEndPoint.ToString());           // Remove with old key
                _socketDict.Add(text, temp);                                    // Add with new key
                Dictionary<Control, Control> ctrl = new Dictionary<Control, Control>();
                if (text == "BaseStation")
                    ctrl.Add(tbxIPBS, tbxPortBS);

                hc.SetText(this, (ctrl.Keys.ElementAtOrDefault(0)), socketToIP(socket));
                hc.SetText(this, ctrl[ctrl.Keys.ElementAtOrDefault(0)], this.port.ToString());
            }
            else if ((_socketDict.ContainsKey("RefereeBox")) && (socket.RemoteEndPoint.ToString().Contains(_socketDict["RefereeBox"].RemoteEndPoint.ToString())))
            //else if (true)
            {
                // If socket is Referee Box socket
                switch (text)
                {
                    /// 1. DEFAULT COMMANDS ///
                    case "S": //STOP
                        respone = "STOP";
                        goto broadcast;
                    case "s": //START
                        respone = "START";
                        goto broadcast;
                    case "W": //WELCOME (welcome message)
                        respone = "WELCOME";
                        goto broadcast;
                    case "Z": //RESET (Reset Game)
                        respone = "RESET";
                        goto broadcast;
                    case "U": //TESTMODE_ON (TestMode On)
                        respone = "TESTMODE_ON";
                        break;
                    case "u": //TESTMODE_OFF (TestMode Off)
                        respone = "TESTMODE_OFF";
                        break;

                    /// 2. PENALTY COMMANDS ///
                    case "y": //YELLOW_CARD_MAGENTA	
                        respone = "YELLOW_CARD_MAGENTA";
                        goto broadcast;
                    case "Y": //YELLOW_CARD_CYAN
                        respone = "YELLOW_CARD_CYAN";
                        goto broadcast;
                    case "r": //RED_CARD_MAGENTA
                        respone = "RED_CARD_MAGENTA";
                        goto broadcast;
                    case "R": //RED_CARD_CYAN
                        respone = "RED_CARD_CYAN";
                        goto broadcast;
                    case "b": //DOUBLE_YELLOW_MAGENTA
                        respone = "DOUBLE_YELLOW_MAGENTA";
                        goto broadcast;
                    case "B": //DOUBLE_YELLOW_CYAN
                        respone = "DOUBLE_YELLOW_CYAN";
                        goto broadcast;

                    /// 3. GAME FLOW COMMANDS ///
                    case "1": //FIRST_HALF
                        respone = "FIRST_HALF";
                        goto broadcast;
                    case "2": //SECOND_HALF
                        respone = "SECOND_HALF";
                        goto broadcast;
                    case "3": //FIRST_HALF_OVERTIME
                        respone = "FIRST_HALF_OVERTIME";
                        goto broadcast;
                    case "4": //SECOND_HALF_OVERTIME
                        respone = "SECOND_HALF_OVERTIME";
                        goto broadcast;
                    case "h": //HALF_TIME
                        respone = "HALF_TIME";
                        goto broadcast;
                    case "e": //END_GAME (ends 2nd part, may go into overtime)
                        respone = "END_GAME";
                        goto broadcast;
                    case "z": //GAMEOVER (Game Over)
                        respone = "GAMEOVER";
                        goto broadcast;
                    case "L": //PARKING
                        respone = "PARKING";
                        break;

                    /// 4. GOAL STATUS ///
                    case "a": //GOAL_MAGENTA
                        respone = "GOAL_MAGENTA";
                        goto broadcast;
                    case "A": //GOAL_CYAN
                        respone = "GOAL_CYAN";
                        goto broadcast;
                    case "d": //SUBGOAL_MAGENTA
                        respone = "SUBGOAL_MAGENTA";
                        break;
                    case "D": //SUBGOAL_CYAN
                        respone = "SUBGOAL_CYAN";
                        break;

                    /// 5. GAME FLOW COMMANDS ///
                    case "k": //KICKOFF_MAGENTA
                        respone = "KICKOFF_MAGENTA";
                        goto broadcast;
                    case "K": //KICKOFF_CYAN
                        respone = "KICKOFF_CYAN";
                        goto broadcast;
                    case "f": //FREEKICK_MAGENTA
                        respone = "FREEKICK_MAGENTA";
                        break;
                    case "F": //FREEKICK_CYAN
                        respone = "FREEKICK_CYAN";
                        break;
                    case "g": //KICKOFF_MAGENTA
                        respone = "KICKOFF_MAGENTA";
                        break;
                    case "G": //GOALKICK_CYAN
                        respone = "GOALKICK_CYAN";
                        break;
                    case "t": //THROWN_MAGENTA
                        respone = "THROWN_MAGENTA";
                        break;
                    case "T": //THROWN_CYAN
                        respone = "THROWN_CYAN";
                        break;
                    case "c": //CORNER_MAGENTA
                        respone = "CORNER_MAGENTA";
                        break;
                    case "C": //CORNER_CYAN
                        respone = "CORNER_CYAN";
                        break;

                    /// 6. OTHERS ///
                    case "get_time": //TIME NOW
                        respone = DateTime.Now.ToLongTimeString();
                        break;
                    default:
                        //addCommand("# Invalid Command :<");
                        break;
                }
            }
            else
            {
                // If socket is Robot socket   
                switch (text)
                {
                    /// INFORMATION ///
                    case "B": //Get the ball
                        respone = "Ball on " + socketToName(socket);
                        goto broadcast;

                    /// OTHERS ///
                    case "get_time": //TIME NOW
                        respone = DateTime.Now.ToLongTimeString();
                        break;
                    default:
                        //addCommand("# Invalid Command :<");
                        break;
                }
            }
            goto end;

            broadcast:
            sendByHostList("Robot1,Robot2,Robot3,BaseStation", respone);
            return string.Empty;

            multicast:
            sendByHostList("Robot2,Robot3,BaseStation", respone);
            return string.Empty;

            end:
            return respone;
        }

        ////////////////////////////////////////////////////////////////////////        

        void reqConnect(string ipDst, dynamic port, string keyName)
        {
            addCommand("# Connecting to " + ipDst + " (" + keyName + ") \t Port : " + port);
            try
            {
                attempts++;
                _toServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //_toServerSocket.Connect(IPAddress.Parse(ipDst = "169.254.162.201"), 100);
                _toServerSocket.Connect(IPAddress.Parse(ipDst), int.Parse(port));
                if (_toServerSocket.Connected)
                    addCommand("# Success Connecting to: " + ipDst + " (" + keyName + ") \t Port : " + port);
                //hc.SetText(this, connection, "Connected");
                SendCallBack(_toServerSocket, this.Text);
                _socketDict.Add(keyName, _toServerSocket);
                _toServerSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), _toServerSocket);
                //new Thread(obj => _toServerSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), _toServerSocket)).Start();
            }
            catch (SocketException)
            {
                hc.SetText(this, tbxStatus, string.Empty);
                addCommand("# IP This Device  : " + myIP + " (" + this.Text + ")");
                addCommand("# IP Destination  : " + ipDst + " (" + keyName + ") \t Port : " + port);
                addCommand("# Connection attempts: " + attempts.ToString());
                //hc.SetText(this, connection, "Disconnected");
            }
        }

        private void btnConnectBS_Click(object sender, EventArgs e)
        {
            if ((!String.IsNullOrWhiteSpace(tbxIPBS.Text)) && (!String.IsNullOrWhiteSpace(tbxPortBS.Text)))
                new Thread(objs => reqConnect(tbxIPBS.Text, tbxPortBS.Text, gbxBS.Text)).Start();
        }

        string GetIPAddress()
        {
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    myIP = IP.ToString();
            return myIP;
        }

        void sendFromTextBox()
        {
            var dataMessage = tbxMessage.Text.Trim().Split('|');
            if (dataMessage.Count() == 1) //for to be Client
                SendCallBack(_socketDict["BaseStation"], dataMessage[0]);
            else if (dataMessage.Count() == 2)  //for to be Server
                SendCallBack(_socketDict["BaseStation"], tbxMessage.Text.Trim());
            else
                MessageBox.Show("Incorrect Format!");
            tbxMessage.ResetText();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            sendFromTextBox();
        }

        private void tbxMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                sendFromTextBox();
        }

        private void btnCloseServer_Click(object sender, EventArgs e)
        {
            MessageBox.Show(useAs.ToString());
        }


        //////////////////////////////////////////////////
        ///
        void changeCounter(object sender, KeyEventArgs e)
        {
            var obj = ((dynamic)sender).Name;
            dynamic[,] arr = { { tbxX, tbxY }, { tbxGotoX, tbxGotoY } };
            int n = 0;
            for (int i = 0; i < arr.GetLength(0); i++)
                for (int j = 0; j < arr.GetLength(1); j++)
                    if (arr[i, j].Name == obj)
                        n = i;
            if (e.KeyCode == Keys.Right)
                arr[n, 0].Text = (int.Parse(arr[n, 0].Text) + 1).ToString();
            else if (e.KeyCode == Keys.Left)
                arr[n, 0].Text = (int.Parse(arr[n, 0].Text) - 1).ToString();
            else if (e.KeyCode == Keys.Up)
                arr[n, 1].Text = (int.Parse(arr[n, 1].Text) - 1).ToString();
            else if (e.KeyCode == Keys.Down)
                arr[n, 1].Text = (int.Parse(arr[n, 1].Text) + 1).ToString();
        }

        private void tbxGoto_KeyDown(object sender, KeyEventArgs e)
        {
            changeCounter(sender, e);
            if ((e.KeyCode == Keys.Enter) && (!string.IsNullOrWhiteSpace(tbxGotoX.Text)) && (!string.IsNullOrWhiteSpace(tbxGotoY.Text)))
                 new Thread(obj => GotoLoc(this.Text, tbxX, tbxY, int.Parse(tbxGotoX.Text), int.Parse(tbxGotoY.Text), 20, 20)).Start();
        }
        
        void GotoLoc(string Robot, dynamic encXRobot, dynamic encYRobot, int endX, int endY, int shiftX, int shiftY)
        {
            try
            {
                int startX = int.Parse(encXRobot.Text), startY = int.Parse(encYRobot.Text);
                if (startX > endX)
                    shiftX *= -1;
                if (startY > endY)
                    shiftY *= -1;
                bool[] chk = { true, true };
                while (chk[0] |= chk[1])
                {
                    if (startX != endX)
                    {
                        if (Math.Abs(endX - startX) < Math.Abs(shiftX))     // Shift not corresponding
                            shiftX = (endX - startX);
                        startX += shiftX;   // On process
                    }
                    else
                        chk[0] = false;     // Done
                    if (startY != endY)
                    {
                        if (Math.Abs(endY - startY) < Math.Abs(shiftY))     // Shift not corresponding
                            shiftY = (endY - startY);
                        startY += shiftY;   // On process
                    }
                    else
                        chk[1] = false;     // Done

                    hc.SetText(this, tbxX, startX.ToString());
                    hc.SetText(this, tbxY, startY.ToString());
                    Thread.Sleep(100);    // time per limit
                }
            }
            catch (Exception e)
            {
            }
        }

        private void Connection_byDistinct(object sender, EventArgs e)
        {
            var obj = ((dynamic)sender).Name;
            dynamic[,] arr = { { gbxRobot, tbxIPRobot, tbxPortRobot }, { gbxBS, tbxIPBS, tbxPortBS } };
            int n = 0;
            for (int i = 0; i < arr.GetLength(0); i++)
                for (int j = 0; j < arr.GetLength(1); j++)
                    if (arr[i, j].Name == obj)
                        n = i;
            if ( (!String.IsNullOrWhiteSpace(arr[n, 1].Text)) && (!String.IsNullOrWhiteSpace(arr[n, 2].Text)))
                new Thread(objs => reqConnect(arr[n, 1].Text, arr[n, 2].Text, arr[n, 0].Text)).Start();
        }

        private void Connection_KeyEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Connection_byDistinct(sender, e);
        }

        private void tbxXY_TextChanged(object sender, EventArgs e)
        {
            string dtEncoder = "X:" + tbxX.Text + ",Y:" + tbxY.Text;
            if (_socketDict.ContainsKey("BaseStation"))
                new Thread(obj => SendCallBack(_socketDict["BaseStation"], dtEncoder)).Start();
            else
                MessageBox.Show("Connection to BaseStation is DISCONNECTED :<");
        }

        private void tbxStatus_TextChanged(object sender, EventArgs e)
        {
            try
            {
                tbxStatus.SelectionStart = tbxStatus.Text.Length;
                tbxStatus.ScrollToCaret();
            }
            catch (Exception ex)
            {
                MessageBox.Show("# Error tbxStatus \n\n" + ex);
            }
        }
    }
}
