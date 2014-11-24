using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Common.WCF.ErrorHandlers
{
    public class ExceptionShieldingErrorHandler : IErrorHandler
    {
        #region Fields
        //private NLogHelper Logger { get; set; }
        //private readonly ILog _logger = LogManager.GetLogger(typeof(ExceptionShieldingErrorHandler));
        #endregion

        #region Constructor
        //NLogHelper _logger
        public ExceptionShieldingErrorHandler()
        {
           // Logger = _logger;
        }
        #endregion

        #region IErrorHandler Members
        public bool HandleError(Exception error)
        {
            if (error != null)
            {
                // Log the error
                string message = BuildErrorLog(error, "ExceptionShieldingErrorHandler");
                //string.Format("ExceptionShieldingErrorHandler catched: Message[{0}]; StackTrace[{1}].", error.Message, error.StackTrace);
                //Logger.WriteInfo(message);
            }

            return true;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            EnsureMessage(ref fault, version);

            try
            {
                ProcessException(error, ref fault);
            }
            catch (Exception ex)
            {
                ProcessException(ex, ref fault);
            }
        }

        private string BuildErrorLog(Exception error, string classInfo)
        {
            if (error == null) return string.Empty;

            Exception errorMirror = error;

            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("{0} catched:", classInfo));
            sb.Append(Environment.NewLine);
            sb.Append(string.Format("Exception: Message [{0}]; StackTrace[{1}]", errorMirror.Message, errorMirror.StackTrace));

            if (errorMirror.InnerException != null)
            {
                errorMirror = errorMirror.InnerException;
                sb.Append(Environment.NewLine);
                sb.Append(string.Format("First InnerException: Message [{0}]; StackTrace[{1}]", errorMirror.Message, errorMirror.StackTrace));

                if (errorMirror.InnerException != null)
                {
                    errorMirror = errorMirror.InnerException;
                    sb.Append(Environment.NewLine);
                    sb.Append(string.Format("Second InnerException: Message [{0}]; StackTrace[{1}]", errorMirror.Message, errorMirror.StackTrace));
                }
            }

            return sb.ToString();
        }
        #endregion

        #region Methods
        private void EnsureMessage(ref Message message, MessageVersion defaultVersion)
        {
            if (message == null)
            {
                message = Message.CreateMessage(defaultVersion ?? MessageVersion.Default, "");
            }
        }

        private void ProcessException(Exception originalException, ref Message fault)
        {
            if (IsFaultException(originalException)) return;

            MessageFault messageFault = BuildMessageFault(originalException);
            //fault = Message.CreateMessage(fault.Version, messageFault, GetFaultAction());
        }

        private MessageFault BuildMessageFault(Exception exception)
        {
            FaultException faultException = new FaultException(exception.Message);
            return faultException.CreateMessageFault();
        }

        private bool IsFaultException(Exception exception)
        {
            return typeof(FaultException).IsInstanceOfType(exception);
        }

        private string GetFaultAction()
        {
            string operationAction = string.Empty;
            if (OperationContext.Current != null)
                operationAction = OperationContext.Current.RequestContext.RequestMessage.Headers.Action;
            return operationAction;
        }
        #endregion
    }
}
