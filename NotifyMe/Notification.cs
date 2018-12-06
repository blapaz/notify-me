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
        public string To { get; set; }
        public string From { get; set; }
        public string Password { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }

        public Notification()
        {
            Subject = GetMessageSubject();
            Message = GetMessageBody();

            string cfgFile = Path.Combine(Directory.GetCurrentDirectory(), "notify.cfg");

            if (File.Exists(cfgFile))
            {
                string[] lines = File.ReadAllLines(cfgFile);

                foreach (string line in lines)
                {
                    string[] spl = line.Split("=");
                    string key = spl[0].ToLower().Trim();
                    string val = spl[1].Trim();

                    switch (key)
                    {
                        case "to":
                            To = val;
                            break;
                        case "from":
                            From = val;
                            break;
                        case "password":
                            Password = val;
                            break;
                        case "subject":
                            Subject = val;
                            break;
                        case "message":
                            Message = val;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void Send()
        {
            NetworkCredential credentials = new NetworkCredential(From, Password);

            MailMessage mail = new MailMessage()
            {
                From = new MailAddress(From),
                Subject = Subject,
                Body = Message
            };

            mail.To.Add(new MailAddress(To));

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
