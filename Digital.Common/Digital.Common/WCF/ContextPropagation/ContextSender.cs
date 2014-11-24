using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Digital.Common.WCF.ContextPropagation
{
    public class ContextSender : IClientMessageInspector
    {
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            //MessageHeader<Runtime> contextHeader = new MessageHeader<Runtime>(Runtime.Current);
            //request.Headers.Add(contextHeader.GetUntypedHeader(Runtime.ContextHeaderLocalName, Runtime.ContextHeaderNamespace));
            return null;
        }
    }
}
