using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;


    class mailsend
    {
    public struct SubMail
    {
        public  string from;
        public  string sqm;
        public  string name;
        public  string to;
        public  string subject;
        public  string body;
    };
    public static string GetDomainName(string emailAddress)
        {
            int atIndex = emailAddress.IndexOf('@');
            if (atIndex == -1)
            {
                throw new ArgumentException("Not a valid email address", "emailAddress");
            }
            if (emailAddress.IndexOf('<') > -1 && emailAddress.IndexOf('>') > -1)
            {
                return emailAddress.Substring(atIndex + 1, emailAddress.IndexOf('>') - atIndex);
            }
            else
            {
                return emailAddress.Substring(atIndex + 1);
            }
        }
    ///<summary> 
    ///发送邮件 
    ///</summary> 
    ///<param name="from">登录邮箱</param> 
    ///<param name="sqm">授权码</param> 
    ///<param name="name">显示名称</param> 
    ///<param name="to">发送到这个邮箱地址</param> 
    ///<param name="subject">标题</param> 
    ///<param name="body">内容</param> 
    public static bool SendMail(string from,string sqm,string name, string to, string subject, string body)
        {
            string domainName = GetDomainName(from);
            try
            {
                SmtpClient client = new SmtpClient();
                client.Host = "smtp." + domainName;//smtp服务器
                client.Port = 25;//服务器端口
                client.UseDefaultCredentials = false;
                //client.EnableSsl = false;
                NetworkCredential networkCredential = new NetworkCredential(from, sqm);
                client.Credentials = networkCredential;
                MailMessage mail = new MailMessage();
                mail.To.Add(new MailAddress(to));
                mail.From = new MailAddress(from, name, System.Text.Encoding.UTF8); //邮件的发件人
                mail.Subject = subject;
                mail.Body = body;
                //设置邮件的格式 
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;

                //设置邮件的发送级别
                mail.Priority = MailPriority.Normal;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Timeout = 30000;
                client.Send(mail);
                //System.Diagnostics.Debug.WriteLine("发送成功");
                MessageBox.Show("发送成功");
                return true;
            }
            catch (SmtpException ex)
            {
                //System.Diagnostics.Debug.WriteLine("发送失败:" + ex.StatusCode + "\n\n" + ex.Message + "\n\n" + ex.StackTrace);
                MessageBox.Show("发送失败:" + ex.StatusCode + "\n\n" + ex.Message + "\n\n" + ex.StackTrace);

            }

            return false;
        }
    }

