using System;

namespace WebCrawl.ConsoleApplication
{
    public class ConsoleWrapper
    {
        public virtual void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public virtual string ReadMessage()
        {
            return Console.ReadLine();
        }
    }
}
