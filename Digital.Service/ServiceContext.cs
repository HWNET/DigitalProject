using Digital.Common.WCF.ContextPropagation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Service
{
    public class ServiceContext
    {
         private static Dictionary<string, ServiceHost> Services { get; set; }

         static ServiceContext()
        {
            Services = new Dictionary<string, ServiceHost>();
        }

        //private static ServiceHost hostTAS;
        public static void StartService<TService>()
        {
            string key = typeof(TService).ToString();
            if (!Services.ContainsKey(key))
            {
                var hostTAS = new ServiceHost(typeof(TService));
                hostTAS.Open();
                Services.Add(key, hostTAS);
            }

        }

        //private static ServiceHost hostTAS;
        public static void StartService(object singletonInstance, params Uri[] baseAddresse)
        {
            string key = singletonInstance.GetType().ToString();//typeof(TService).ToString();
            if (!Services.ContainsKey(key))
            {
                var hostTAS = new ServiceHost(singletonInstance);
                foreach (ServiceEndpoint ep in hostTAS.Description.Endpoints)
                {
                    ep.Behaviors.Add(new ContextPropagationBehavior());
                }
                hostTAS.Open();
                Services.Add(key, hostTAS);
            }

        }

        public static void StopService()
        {
            try
            {
                foreach (var item in Services.Values)
                {
                    item.Close();
                }
            }
            finally
            {
                Services.Clear();
            }

        }
    }
}
