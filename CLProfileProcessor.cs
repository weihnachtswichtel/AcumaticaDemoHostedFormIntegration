using Newtonsoft.Json.Linq;
using PX.CCProcessingBase.Interfaces.V2;
using PX.Common;
using System;
using System.Collections.Generic;

namespace CookielessHostedForm
{
    public class CLProfileProcessor : ICCProfileProcessor
    {
        private IEnumerable<SettingsValue> settingValues;

        public CLProfileProcessor(IEnumerable<SettingsValue> settingValues)
        {
            this.settingValues = settingValues;
        }

        public string CreateCustomerProfile(CustomerData customerData)
        {
            return customerData.CustomerCD + "CCPID";                      //use single static one for simplicity
        }

        public string CreatePaymentProfile(string customerProfileId, CreditCardData cardData)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCustomerProfile(string customerProfileId)
        {
            throw new System.NotImplementedException();
        }

        public void DeletePaymentProfile(string customerProfileId, string paymentProfileId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CustomerData> GetAllCustomerProfiles()
        {
            throw new System.NotImplementedException();
        }

        private Dictionary<string, string> GetSomeCardDetailsByToken(string token){
            //This should be request to Processing Center to retrive the Card Data by token
            //since this is not an option for this demo we hide the data in token itself
            string[] dataFromToken = token.Split('-');
            return new Dictionary<string, string> {
                {"Token", token },
                {"ExpDate", dataFromToken[1].ToString()},                                                   //Here ms from Unix Epoch in UTC
                {"LastFour",  dataFromToken[2]}
            };
        }
        

        public IEnumerable<CreditCardData> GetAllPaymentProfiles(string customerProfileId)
        {
            string token, cpid, trantype;
            List<CreditCardData> ccdList = new List<CreditCardData>();
            string CLResponseToken = PXContext.Session["CLResponseToken"] as string;
            if (string.IsNullOrEmpty(CLResponseToken)) return null;                                         //will throw an exception (nothing retrieved in Hosted Form)

            var responseDetails = JObject.Parse(CLResponseToken);
            token    = responseDetails["Token"].ToString();
            trantype = responseDetails["Type"].ToString();                                                //This parameter passed in HF previously to verify action called (here CreateOnly)
            cpid    = responseDetails["CPID"].ToString();                                                 //This just another peice of data, may be for verification to compare with customerProfileId

            Dictionary<string, string> responseFromProcessingCenter = GetSomeCardDetailsByToken(token);
            if (responseFromProcessingCenter == null) return null;                                          //will throw an exception (no such card found in the Processing Center)

            CreditCardData ccd = new CreditCardData()
            {
                PaymentProfileID = responseFromProcessingCenter["Token"],
                CardExpirationDate = new DateTime(1970, 1, 1).AddMilliseconds(double.Parse(responseFromProcessingCenter["ExpDate"])),
                CardNumber = responseFromProcessingCenter["LastFour"],
            };
            ccdList.Add(ccd);
            return ccdList;
        }

        public CustomerData GetCustomerProfile(string customerProfileId)
        {
            return new CustomerData() {
                CustomerCD = customerProfileId.Substring(0, customerProfileId.IndexOf("CCPID")),
            };
        }

        public CreditCardData GetPaymentProfile(string customerProfileId, string paymentProfileId)
        {
            Dictionary<string, string> responseFromProcessingCenter = GetSomeCardDetailsByToken(paymentProfileId);

            return new CreditCardData()
            {
                PaymentProfileID = responseFromProcessingCenter["Token"],
                CardNumber = responseFromProcessingCenter["LastFour"],
                CardExpirationDate = new DateTime(1970, 1, 1).AddMilliseconds(double.Parse(responseFromProcessingCenter["ExpDate"])),
            };
        }

        public void UpdateCustomerProfile(CustomerData customerData)
        {
            throw new System.NotImplementedException();
        }

        public void UpdatePaymentProfile(string customerProfileId, CreditCardData cardData)
        {
            throw new System.NotImplementedException();
        }
    }
}