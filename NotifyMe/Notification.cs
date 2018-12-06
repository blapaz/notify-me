using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace NotifyMe
{
    class Notification
    {
        public string to;
        public string from;
        public string password;
        public string subject;
        public string message;

        public Notification()
        {
            subject = GetMessageSubject();
            message = GetMessageBody();

            string cfgFile = Path.Combine(Directory.GetCurrentDirectory(), "notify.cfg");
            string[] lines = File.ReadAllLines(cfgFile);

            foreach (string line in lines)
            {
                string[] spl = line.Split("=");
                string key = spl[0].ToLower().Trim();
                string val = spl[1].Trim();

                switch (key)
                {
                    case "to": to = val;
                        break;
                    case "from": from = val;
                        break;
                    case "password": password = val;
                        break;
                    case "subject":
                        subject = val;
                        break;
                    case "message":
                        message = val;
                        break;
                    default:
                        break;
                }
            }
        }

        public void Send()
        {
            NetworkCredential credentials = new NetworkCredential(from, password);

            MailMessage mail = new MailMessage()
            {
                From = new MailAddress(from),
                Subject = subject,
                Body = message
            };

            mail.To.Add(new MailAddress(to));

            SmtpClient client = new SmtpClient()
            {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                Credentials = credentials
            };

            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in sending email: " + ex.Message);
                Console.ReadKey();
                return;
            }
        }

        string GetMessageSubject()
        {
            string msg = "NotifyMe Has Started!";
            return msg;
        }

        string GetMessageBody()
        {
            string msg = "NotifyMe has started @ ip " + GetPublicIP();
            return msg;
        }

        string GetPublicIP()
        {
            string url = "http://checkip.dyndns.org";
            WebRequest req = WebRequest.Create(url);
            WebResponse resp = req.GetResponse();
            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string response = sr.ReadToEnd().Trim();
            string[] a = response.Split(':');
            string a2 = a[1].Substring(1);
            string[] a3 = a2.Split('<');
            string a4 = a3[0];
            return a4;
        }
    }
}
