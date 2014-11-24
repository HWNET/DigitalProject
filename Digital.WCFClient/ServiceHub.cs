using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Digital.WCFClient
{
    public enum ServiceBinding
    {
        Tcp,
        BasicHttp,
        PollingDuplexHttp,
    }

    public static class ServiceHub
    {
        #region Common Service
        /// <summary>
        /// Create the first plant common service client, port is 4502
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serverIP"></param>
        /// <returns></returns>
        public static T GetCommonServiceClient<T>(string serverIP) where T : ICommunicationObject
        {
            var clientType = typeof(T);
            var clientName = clientType.Name.Replace("Client", "");
            object serviceClient;

            TcpTransportBindingElement tt = new TcpTransportBindingElement();
            tt.MaxBufferSize = 10485760 * 4;
            tt.MaxReceivedMessageSize = 10485760 * 4;


            dynamic binding = new CustomBinding(new BinaryMessageEncodingBindingElement(), tt)
            {
                ReceiveTimeout = new TimeSpan(0, 20, 0),
                SendTimeout = new TimeSpan(0, 10, 0),
                OpenTimeout = new TimeSpan(0, 10, 0),
                CloseTimeout = new TimeSpan(0, 10, 0)
            };
            var endpointAddress = new EndpointAddress(String.Format("net.tcp://{0}:{1}/{2}", serverIP, "4502", clientName));
            serviceClient = Activator.CreateInstance(typeof(T), binding, endpointAddress);
            return (T)serviceClient;
        }

        /// <summary>
        /// Create all plant common service client
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serverIP"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public static T GetCommonServiceClient<T>(string serverIP, string port) where T : ICommunicationObject
        {
            var clientType = typeof(T);
            var clientName = clientType.Name.Replace("Client", "");
            object serviceClient;
            TcpTransportBindingElement tt = new TcpTransportBindingElement();
            tt.MaxBufferSize = 10485760 * 4;
            tt.MaxReceivedMessageSize = 10485760 * 4;

            dynamic binding = new CustomBinding(new BinaryMessageEncodingBindingElement(), tt)
            {
                ReceiveTimeout = new TimeSpan(0, 20, 0),
                SendTimeout = new TimeSpan(0, 10, 0),
                OpenTimeout = new TimeSpan(0, 10, 0),
                CloseTimeout = new TimeSpan(0, 10, 0)
            };
            var endpointAddress = new EndpointAddress(String.Format("net.tcp://{0}:{1}/{2}", serverIP, port, clientName));
            serviceClient = Activator.CreateInstance(typeof(T), binding, endpointAddress);
            return (T)serviceClient;
        }

        /// <summary>
        /// Create all plant common service client,and include call back function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serverIP"></param>
        /// <param name="port"></param>
        /// <param name="callbackContext"></param>
        /// <returns></returns>
        public static T GetCommonServiceClient<T>(string serverIP, string port, InstanceContext callbackContext) where T : ICommunicationObject
        {
            try
            {
                var clientType = typeof(T);
                var clientName = clientType.Name.Replace("Client", "");
                object serviceClient;
                TcpTransportBindingElement tt = new TcpTransportBindingElement();
                tt.MaxBufferSize = 10485760 * 4;
                tt.MaxReceivedMessageSize = 10485760 * 4;

                dynamic binding = new CustomBinding(new BinaryMessageEncodingBindingElement(), tt)
                {
                    ReceiveTimeout = new TimeSpan(0, 20, 0),
                    SendTimeout = new TimeSpan(0, 10, 0),
                    OpenTimeout = new TimeSpan(0, 10, 0),
                    CloseTimeout = new TimeSpan(0, 10, 0)
                };
                var endpointAddress = new EndpointAddress(String.Format("net.tcp://{0}:{1}/{2}", serverIP, port, clientName));
                serviceClient = Activator.CreateInstance(typeof(T), callbackContext, binding, endpointAddress);
                return (T)serviceClient;
            }
            catch (Exception ex)
            {
                throw new Exception("Client side of AAM Service has error: " + ex.Message);
            }
        }

       
        #endregion

        

        

       
      
    }
}
