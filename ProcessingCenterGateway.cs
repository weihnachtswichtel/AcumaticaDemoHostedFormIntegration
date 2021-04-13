using PX.CCProcessingBase.Interfaces.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookielessHostedForm
{
    public static class ProcessingCenterGateway
    {
        public static CustomerData GetCustomerProfileById(string customerProfileId)
        {
            return new CustomerData()
            {
                CustomerCD = customerProfileId.Substring(0, customerProfileId.IndexOf("CCPID")),
            };
        }
    }
}
