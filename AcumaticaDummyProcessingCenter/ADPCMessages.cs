using System;
using PX.Common;

namespace AcumaticaDummyProcessingCenter
{
  [PXLocalizable()]
  public static class ADPCMessages
  {
    public const string Visa= "Visa";
    public const string MasterCard = "MasterCard";
    public const string AmericanExpress = "American Express";
    public const string UnionPay = "UnionPay";
 
    public const string Approved = "Approved";
    public const string Declined = "Declined";
    public const string Error = "Error"; 
    public const string HeldForReview  = "Held For Review";
    public const string Expired = "Expired";
    public const string Unknown = "Unknown";
    public const string SettledSuccessfully = "Settled Successfully";
    public const string Voided = "Voided";
    public const string RefundSettledSuccessfully ="Refund Settled Successfully ";
  
    public const string Authorization = "Authorization";
    public const string Capture= "Capture";      
    public const string Void= "Void";
    public const string Refund= "Refund";      
    public const string Credit= "Credit";    
  
  }
}