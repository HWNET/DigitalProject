using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Digital.Common.WCF.ContextPropagation
{
    public class ContextReceiver : ICallContextInitializer
    {
        //private NLogHelper logger = NLogHelper.CreateAAMServiceLogger();
        public void AfterInvoke(object correlationState)
        {
        }

        public object BeforeInvoke(InstanceContext instanceContext, IClientChannel channel, Message message)
        {
            if (message != null && message.Headers != null)
            {
                var a = message.Headers.Action;
                Console.WriteLine(a);
                //logger.WriteInfo(a);

            }
            return null;
        }
    }
}
