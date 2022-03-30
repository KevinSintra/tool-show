using System;
using System.Net.Mail;
using System.Text;
using System.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Scheduler
{
    public class SendMailService
    {
        private string _sender = string.Empty;
        private string _to = string.Empty;
        private string _subject = string.Empty;
        private string _body = string.Empty;
        public SendMailService(string sender, string to, string subject, string body)
        {
            this._SetMailInfomation(sender, to, subject, body);
        }
        public void SetMailInfomation(string sender, string to, string subject, string body)
        {
            this._SetMailInfomation(sender, to, subject, body);
        }
        private void _SetMailInfomation(string sender, string to, string subject, string body)
        {
            this._sender = sender ?? ""; this._to = to ?? ""; this._subject = subject ?? ""; this._body = body ?? "";
        }
        public void SendMail()
        {
            #region SmtpClient
            //try
            //{
            //    using (MailMessage Mail = new MailMessage())
            //    {
            //        // 填入  寄件者
            //        Mail.From = new MailAddress(this._sender);

            //        // 填入  收件者
            //        string[] arrToMail = this._to.Split(Convert.ToChar(";"));
            //        foreach (string strTo in arrToMail)
            //            if (strTo.Trim().Length > 0)
            //                Mail.To.Add(new MailAddress(strTo));

            //        // 填入  內容
            //        Mail.BodyEncoding = UTF8Encoding.UTF8;
            //        Mail.Body = this._body;

            //        // 填入  主旨
            //        Mail.Subject = this._subject;

            //        var host = ConfigurationManager.AppSettings["MailHost"].ToString();
            //        var port = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MailHostPort"]);

            //        // SMTP
            //        using (SmtpClient WebMail = new SmtpClient(host, port))
            //        {
            //            WebMail.UseDefaultCredentials = true;
            //            WebMail.Send(Mail);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
            #endregion

            this._AzureSendMail();
        }
        private void _AzureSendMail()
        {
            // 內容
            var msg = new SendGridMessage() {
                From = new EmailAddress(this._sender, "HostService"),
                Subject = this._subject
            };
            msg.AddContent(MimeType.Text, this._body);

            // 填入  收件者
            string[] arrToMail = this._to.Split(Convert.ToChar(";"));
            foreach (string strTo in arrToMail)
                if (strTo.Trim().Length > 0)
                    msg.AddTo(new EmailAddress(strTo));
            
            // 送信
            var client = new SendGridClient("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");
            //try
            //{
                var response = client.SendEmailAsync(msg).Result;
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
        }
        public void SendMail(string sender, string to, string subject, string body)
        {
            this._SetMailInfomation(sender, to, subject, body);
            this.SendMail();
        }
    }
}