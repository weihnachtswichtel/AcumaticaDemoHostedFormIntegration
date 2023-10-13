using Newtonsoft.Json.Linq;
using PX.CCProcessingBase.Interfaces.V2;
using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CSharp;
using Acumatica.ADPCGateway.Model;
using Acumatica.ADPCGateway;

namespace AcumaticaDummyCreditCardPlugin
{
    public class ADCPProfileProcessor : ICCProfileProcessor
    {
        private IEnumerable<SettingsValue> settingValues;

        public ADCPProfileProcessor(IEnumerable<SettingsValue> settingValues)
        {
            this.settingValues = settingValues;
        }

        //Method called on vaulting of the Credit Card when there is no CustomerPayment Profile created on Acumatica for the customer.
        public string CreateCustomerProfile(CustomerData customerData)
        {
            string result = Requests.GetCreateCustomerProfileByCustomerCD(ADCPHelper.GetPCGredentials(settingValues), customerData.CustomerCD, customerData.CustomerName, customerData.Email);
            return result;
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

        public IEnumerable<CreditCardData> GetAllPaymentProfiles(string customerProfileId)
        {

            CustomerProfile cp = Requests.GetCustomerProfileByCPID(ADCPHelper.GetPCGredentials(settingValues), customerProfileId, true);
            
            List<CreditCardData> ccdList = new List<CreditCardData>();

            foreach (PaymentProfiles p in cp.PaymentProfiles) {
                ccdList.Add(new CreditCardData()
                {
                    PaymentProfileID = p.PaymentProfileID.ToString(),
                    CardNumber = p.CardLastFour,
                    CardType = p.CardType,
                    CardTypeCode = ADCPHelper.MapCardType[p.CardType.Value[0].ToString()],
                    CardExpirationDate = p.CardExpirationDate ?? DateTime.Now.AddMonths(20)
                });
            }

            //    //below is the way when Hosted Form does return token
            //    string ppid, cpid, trantype;
            //    List<CreditCardData> ccdList = new List<CreditCardData>();
            //    string CLResponseToken = PXContext.Session["CLResponseToken"] as string;
            //    if (string.IsNullOrEmpty(CLResponseToken)) return null;                                         //will throw an exception (nothing retrieved in Hosted Form)

            //    var responseDetails = JObject.Parse(CLResponseToken);
            //    ppid    = responseDetails["PPID"].ToString();
            //  //  trantype = responseDetails["Type"].ToString();                                                //This parameter passed in HF previously to verify action called (here CreateOnly)
            //    cpid    = responseDetails["CPID"].ToString();                                                 //This just another peice of data, may be for verification to compare with customerProfileId


            //    Dictionary<string, string> responseFromProcessingCenter = ProcessingCenterGateway.GetSomeCardDetailsByToken(ppid);
            //    if (responseFromProcessingCenter == null) return null;                                          //will throw an exception (no such card found in the Processing Center)

            //    CreditCardData ccd = new CreditCardData()
            //    {
            //        PaymentProfileID = ppid,
            //      //  CardExpirationDate = new DateTime(1970, 1, 1).AddMilliseconds(double.Parse(responseFromProcessingCenter["ExpDate"])),
            //        CardExpirationDate = DateTime.Now.AddMonths(20),
            //       // CardNumber = responseFromProcessingCenter["LastFour"],
            //        CardNumber = "1111",
            //      //  CardType = responseFromProcessingCenter["CardType"],                                                        //As Card Type comes from the Processing Center
            //    //    CardTypeCode = ADCPHelper.MapCardType[responseFromProcessingCenter["CardType"]]                             //As Acumatica Internal enum         
            //};
            //    ccdList.Add(ccd);
                return ccdList;
        }

        public CustomerData GetCustomerProfile(string customerProfileId)
        {
            CustomerProfile cp = Requests.GetCustomerProfileByCPID(ADCPHelper.GetPCGredentials(settingValues), customerProfileId);

            CustomerData cd = new CustomerData();
            cd.CustomerName = cp.CustomerDescription;
            cd.CustomerCD = cp.CustomerName;
            cd.CustomerProfileID = cp.CustomerProfileID;
            cd.Email = cp.Email;
            

            return cd;

        }

        public CreditCardData GetPaymentProfile(string customerProfileId, string paymentProfileId)
        {

            CustomerProfile cp = Requests.GetCustomerProfileByCPID(ADCPHelper.GetPCGredentials(settingValues), customerProfileId, true);

            PaymentProfiles pp = cp.PaymentProfiles.Where(p => p.PaymentProfileID.ToString() == paymentProfileId).FirstOrDefault();

             return new CreditCardData()
                {
                    PaymentProfileID = pp.PaymentProfileID.ToString(),
                    CardNumber = pp.CardLastFour,
                    CardType = pp.CardType,
                    CardTypeCode = ADCPHelper.MapCardType[pp.CardType.Value[0].ToString()],
                    CardExpirationDate = pp.CardExpirationDate ?? DateTime.Now.AddMonths(20)
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