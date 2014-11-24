using Digital.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.ServiceHost
{
    class Program
    {
        //private CancellationTokenSource _tokenSource;

        //private CancellationToken _token;

        static void Main(string[] args)
        {
            var service = new Digital.Service.Implements.Service();
            try
            {
                Console.WriteLine("Start Digital Service......");
               
                service.Init();
                Console.WriteLine("Digital Service running.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Run Common Service failed,Error:{0}", ex.ToString()));

            }
          
            try
            {
                Console.WriteLine("Start Common Service......");
                ServiceContext.StartService(service, null);
                Console.WriteLine("Common Service running.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Run Common Service failed,Error:{0}", ex.ToString()));

            }
            Console.Beep(4000, 1000);

            Console.ReadLine();
        }
    }
}
