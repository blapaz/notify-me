using System;

namespace NotifyMe
{
    class Program
    {
        static void Main(string[] args)
        {
            Notification n = new Notification();
            n.Send();
        }
    }
}
