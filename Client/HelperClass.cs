﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class HelperClass
    {
        delegate void SetTextCallback(Form f, Control ctrl, string text);
        delegate void SetLocationCallback(Form f, Control ctrl, Point point);
        /// Set text property of various controls

        public void SetText(Form form, Control ctrl, string text)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (ctrl.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                form.Invoke(d, new object[] { form, ctrl, text });
            }
            else
                ctrl.Text = text;
        }

        public void SetLocation(Form form, Control ctrl, Point point)
        {
            //MessageBox.Show("lalala 1");
            if (ctrl.InvokeRequired)
            {
                SetLocationCallback d = new SetLocationCallback(SetLocation);
                form.Invoke(d, new object[] { form, ctrl, point });
            }
            else
                ctrl.Location = point;
        }
    }
}
