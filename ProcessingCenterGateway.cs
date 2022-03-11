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
        public static void DeleteCustomerProfileById(string customerProfileId) {

        }

        public static void DeletePaymentProfileById(string customerProfileId, string paymentProfileId) {

        }

        public static string CreateCustomerProfileByCustomerCD(string customerCD)
        {
            return customerCD + "CCPID";
        }

        public static Dictionary<string, string> GetSomeCardDetailsByToken(string token)
        {
            //This should be request to Processing Center to retrive the Card Data by token
            //since this is not an option for this demo we hide the data in token itself
            string[] dataFromToken = token.Split('-');
            return new Dictionary<string, string> {
                {"Token", token },
                {"ExpDate", dataFromToken[1].ToString()},                                                   //Here ms from Unix Epoch in UTC
                {"LastFour",  dataFromToken[2]},
                {"CardType", dataFromToken[0].Length > 3 ? dataFromToken[0].Substring(0,3) : "OTH" }
            };
        }

    }
}
