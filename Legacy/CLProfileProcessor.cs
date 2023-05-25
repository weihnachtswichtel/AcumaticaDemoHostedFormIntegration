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
            //customerData.CustomerCD + "CCPID" use single static one for simplicity
            return ProcessingCenterGateway.CreateCustomerProfileByCustomerCD(customerData.CustomerCD);
        }

        public string CreatePaymentProfile(string customerProfileId, CreditCardData cardData)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCustomerProfile(string customerProfileId)
        {
            ProcessingCenterGateway.DeleteCustomerProfileById(customerProfileId);
        }

        public void DeletePaymentProfile(string customerProfileId, string paymentProfileId)
        {
            ProcessingCenterGateway.DeletePaymentProfileById(customerProfileId, paymentProfileId);
        }

        public IEnumerable<CustomerData> GetAllCustomerProfiles()
        {
            throw new System.NotImplementedException();
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
            

            Dictionary<string, string> responseFromProcessingCenter = ProcessingCenterGateway.GetSomeCardDetailsByToken(token);
            if (responseFromProcessingCenter == null) return null;                                          //will throw an exception (no such card found in the Processing Center)

            CreditCardData ccd = new CreditCardData()
            {
                PaymentProfileID = responseFromProcessingCenter["Token"],
                CardExpirationDate = new DateTime(1970, 1, 1).AddMilliseconds(double.Parse(responseFromProcessingCenter["ExpDate"])),
                CardNumber = responseFromProcessingCenter["LastFour"],
                CardType = responseFromProcessingCenter["CardType"],                                                        //As Card Type comes from the Processing Center
                CardTypeCode = CLHelper.MapCardType[responseFromProcessingCenter["CardType"]]                               //As Acumatica Internal enum         
        };
            ccdList.Add(ccd);
            return ccdList;
        }

        public CustomerData GetCustomerProfile(string customerProfileId)
        {
            return ProcessingCenterGateway.GetCustomerProfileById(customerProfileId);
        }

        public CreditCardData GetPaymentProfile(string customerProfileId, string paymentProfileId)
        {
            Dictionary<string, string> responseFromProcessingCenter = ProcessingCenterGateway.GetSomeCardDetailsByToken(paymentProfileId);

            return new CreditCardData()
            {
                PaymentProfileID = responseFromProcessingCenter["Token"],
                CardNumber = responseFromProcessingCenter["LastFour"],
                CardExpirationDate = new DateTime(1970, 1, 1).AddMilliseconds(double.Parse(responseFromProcessingCenter["ExpDate"])),
                CardType = responseFromProcessingCenter["CardType"],                                                       //As Card Type comes from the Processing Center
                CardTypeCode = CLHelper.MapCardType[responseFromProcessingCenter["CardType"]]                              //As Acumatica Internal enum   
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