using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Digitial.WindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            //T4 地址
            //E:\Program Files\Microsoft Visual Studio 12.0\Common7\IDE\Extensions\Microsoft\Web\Mvc\Scaffolding\Templates\MvcView
            //view template path
            //Install-Package Rabbit.Bootstrap.MVC
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 

                new Service1() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
