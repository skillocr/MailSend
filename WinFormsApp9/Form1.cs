using System;
using System.Windows.Forms;
using System.Threading;

namespace WinFormsApp9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void ThreadMail_sub(object obj)
        {
            mailsend.SubMail temp = (mailsend.SubMail)obj;

            if (mailsend.SendMail(temp.from, temp.sqm, temp.name, temp.to, temp.subject, temp.body))
            this.Invoke((MethodInvoker)delegate ()
            {
                Button_Send_Mail.Enabled = true;
            });
        }

        private void Button_Send_Mail_Click(object sender, EventArgs e)
        {
            Button_Send_Mail.Enabled = false;
            mailsend.SubMail  submail = new mailsend.SubMail();
            submail.from =    textBox1.Text;
            submail.sqm =     textBox2.Text;
            submail.name =    textBox3.Text;
            submail.to =      textBox4.Text;
            submail.subject = textBox5.Text;
            submail.body =    textBox6.Text;

            Thread thread_sMail = new Thread(new ParameterizedThreadStart(ThreadMail_sub));
            thread_sMail.Start((object)submail);
        }
    }
}
