using LibClientServer;
using LibRemotingObj;
using System;

using System.Windows.Forms;

namespace ClientObject
{
    public partial class FormClient : Form, IClientObserver
    {
        ModelRemoteObject mModelRemoteObject;

        public void Invoke(object obj)
        {
            if (obj is string)
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        //string msg = (string)obj;
                        this.textBox1.Text = (string)obj;
                        return;
                    });
                }else
                {
                    this.textBox1.Text = (string)obj;
                }
               // string msg = (string)obj;
               // this.textBox1.Text = msg;
                // MessageBox.Show(msg);
            }
        }

        public FormClient()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                CManagerClientServer.getInstance().connectClientToServer();
                mModelRemoteObject = CManagerClientServer.getInstance().createRemotingObj();
                MessageBox.Show("Connected.");

                mModelRemoteObject.setClient(this);

                this.btnConnect.Enabled = false;
                this.btnCommand.Enabled = true;
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (mModelRemoteObject != null)
                {
                    mModelRemoteObject.sendMessageToHost(textBox1.Text);
                }
            }
        }

        private void btnCommand_Click(object sender, EventArgs e)
        {
            if (mModelRemoteObject != null)
            {            
                if (btnCommand.Text == "Start")
                {
                    mModelRemoteObject.sendMessageToHost("Start");
                    btnCommand.Text = "Stop";
                }
                else
                {
                    mModelRemoteObject.sendMessageToHost("Stop");
                    btnCommand.Text = "Start";
                }
            }
        }
    }
}
