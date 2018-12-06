using CommandLine;
using System;

namespace NotifyMe
{
    class Program
    {
        public class Options
        {
            [Option('t', "to", Required = false, HelpText = "Set the email address to send to.")]
            public string To { get; set; }

            [Option('f', "from", Required = false, HelpText = "Set the email address to send from.")]
            public string From { get; set; }

            [Option('p', "password", Required = false, HelpText = "Set the password for the email address sending from.")]
            public string Password { get; set; }

            [Option('s', "subject", Required = false, HelpText = "Overwrite the default subject of the email notification.")]
            public string Subject { get; set; }

            [Option('m', "message", Required = false, HelpText = "Overwrite the default message body of the email notification.")]
            public string Message { get; set; }
        }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o =>
                {
                    Notification n = new Notification();
                    if (o.To != null)       n.To = o.To;
                    if (o.From != null)     n.From = o.From;
                    if (o.Password != null) n.Password = o.Password;
                    if (o.Subject != null)  n.Subject = o.Subject;
                    if (o.Message != null)  n.Message = o.Message;
                    n.Send();
                });
        }
    }
}
