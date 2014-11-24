using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Common.WCF.ErrorHandlers
{
    public class ExceptionShieldingBehavior : IServiceBehavior//, IContractBehavior
    {
        //private NLogHelper _logger = NLogHelper.CreateAAMServiceLogger();
        public ExceptionShieldingBehavior()
        {

        }

        #region IServiceBehavior Members
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
            {
                AddErrorHandler(dispatcher);
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {

        }
        #endregion

        #region IContractBehavior Members
        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {

        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            if (dispatchRuntime == null) throw new ArgumentNullException("dispatchRuntime");
            AddErrorHandler(dispatchRuntime.ChannelDispatcher);
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {

        }
        #endregion

        #region Methods
        private void AddErrorHandler(ChannelDispatcher channelDispatcher)
        {
            if (!ContainsExceptionShieldingErrorHandler(channelDispatcher.ErrorHandlers) &&
                !ContainsMetadataEndpoint(channelDispatcher.Endpoints))
            {
                //IErrorHandler errorHandler = new ExceptionShieldingErrorHandler(_logger);
                //channelDispatcher.ErrorHandlers.Add(errorHandler);
            }
        }

        private bool ContainsExceptionShieldingErrorHandler(Collection<IErrorHandler> handlers)
        {
            return handlers.Any(handler => typeof(ExceptionShieldingErrorHandler).IsInstanceOfType(handler));
        }

        private bool ContainsMetadataEndpoint(SynchronizedCollection<EndpointDispatcher> endpoints)
        {
            string mexContractName = typeof(IMetadataExchange).Name;
            return endpoints.Any(endpoint => endpoint.ContractName == mexContractName);
        }
        #endregion
    }
}
