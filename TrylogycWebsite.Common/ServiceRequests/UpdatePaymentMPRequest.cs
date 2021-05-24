using CommonTrylogycWebsite.ServiceRequests;
using System;
using System.Collections.Generic;
using System.Text;
using TrylogycWebsite.Common.ServiceRequests.Interfaces;

namespace TrylogycWebsite.Common.ServiceRequests
{
    public class UpdatePaymentMPRequest : BaseRequest,IUpdatePaymentMPRequest
    {
        #region propiedades 
        public string preferenceId { get; set; }
        public int estado { get; set; }
        public string collection { get; set; }
        public string merchantOrder { get; set; }

        public override bool IsValid()
        {
            return true;
        }
        #endregion
    }
}
