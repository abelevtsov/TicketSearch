using System;
using System.Net.Mail;

namespace FlyTicketsSearch
{
    public class Program
    {
        public static void Main(params string[] args)
        {
            Console.WriteLine("Run search");
            DoSearch();
            Console.ReadKey();
        }

        private async static void DoSearch()
        {
            var searchEngine = new S7();
            var result = await searchEngine.Search().ConfigureAwait(true);
            searchEngine.Dispose();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Цена на билет до *** снизилась!!!\nНовая цена {0} Руб.", result);
            Notify(result);
        }

        private  static void Notify(int cost)
        {
            var from = "***@***.ru";
            var to = "***@***.ru";
            var message = new MailMessage(from, to)
            {
                Subject = "Цена снизилась!",
                Body = string.Format("Цена на билет до *** снизилась!!!\nНовая цена {0} Руб.", cost)
            };

            message.CC.Add(new MailAddress(to));

            var client = new SmtpClient("")
            {
                UseDefaultCredentials = true
            };

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in Notify: {0}", ex);
            }
        }
    }
}
