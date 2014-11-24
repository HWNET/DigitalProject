using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Common.WCF.ContextPropagation
{
    public class ContextPropagationBehaviorElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(ContextPropagationBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new ContextPropagationBehavior();
        }
    }
}
