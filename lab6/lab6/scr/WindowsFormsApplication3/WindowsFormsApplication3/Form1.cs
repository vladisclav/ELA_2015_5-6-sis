using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CalculatorRemote;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        String request="";
        bool operation = false;
        bool equal = false;
        Calculator calc;
        public Form1()
        {
            InitializeComponent();
            HttpChannel channel = new HttpChannel();
            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownClientType(Type.GetType("CalculatorRemote.Calculator, ClassLibrary1"), "http://localhost:8080/calcURI");
            calc = new Calculator();
        }

        private void OpButton_click(object sender, EventArgs e)
        {
            Button temp = (Button)sender;
            if (temp.Text == "C")
            {
                request = "";
                answerBox.Text = "0";
                operation = false;
                return;
            }
            if (request.Length == 0 && (temp.Text=="*"||temp.Text=="+"||temp.Text=="-"||temp.Text=="/"))
            {
                request = "0";
                request += temp.Text;
                operation = true;
                return;
            }
            else if (temp.Text == "*" || temp.Text == "+" || temp.Text == "-" || temp.Text == "/")
            {
                if(operation){
                    
                    request = request.Remove(request.Length - 1);
                }
                if (equal) request = answerBox.Text;
                request += temp.Text;
                operation = true;
                equal = false;
                return;
            }
            else
            {
                if (equal) request = "";
                if (request.Length == 0) answerBox.Text = "";
                request += temp.Text;
                if (operation) answerBox.Text = temp.Text;
                else answerBox.Text += temp.Text;
                operation = false;
                equal = false;
                return;
            }
        }

        private void equalClick(object sender, EventArgs e)
        {
            if (operation)
            {
                request = request.Remove(request.Length - 1);
                operation = false;
            }
            if (request.Length != 0)
            {
                answerBox.Text = calc.getResult(request);
                equal = true;
            }
        }
    }
}
