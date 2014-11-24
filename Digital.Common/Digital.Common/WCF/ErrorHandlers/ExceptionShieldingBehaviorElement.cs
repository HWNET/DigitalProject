using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Common.WCF.ErrorHandlers
{
    public class ExceptionShieldingBehaviorElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(ExceptionShieldingBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new ExceptionShieldingBehavior();
        }
    }
}
