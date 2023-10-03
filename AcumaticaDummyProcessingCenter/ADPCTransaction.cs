using System;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;

namespace AcumaticaDummyProcessingCenter
{
  [Serializable]
  [PXCacheName("ADPCTransaction")]
  public class ADPCTransaction : IBqlTable
  {
    
    
    #region PaymentProfileID
    [PXDBGuid(IsKey = true)]
    [PXUIField(DisplayName = "Payment Profile ID")]
    [PXSelector(typeof(Search<ADPCPaymentProfile.paymentProfileID>))]
    [PXDBDefault(typeof(ADPCPaymentProfile.paymentProfileID))]
    [PXParent(typeof(SelectFrom<ADPCPaymentProfile>.
       Where<ADPCPaymentProfile.paymentProfileID.
       IsEqual<ADPCTransaction.paymentProfileID.FromCurrent>>))]
    public virtual Guid? PaymentProfileID { get; set; }
    public abstract class paymentProfileID : PX.Data.BQL.BqlGuid.Field<paymentProfileID> { }
    #endregion
    
   
    #region CustomerProfileID

    [PXDBString(10, IsUnicode = true, InputMask = "")]
    [PXDBDefault(typeof(Search<ADPCPaymentProfile.customerProfileID, Where<ADPCPaymentProfile.paymentProfileID.IsEqual<ADPCTransaction.paymentProfileID.FromCurrent>>>), PersistingCheck = PXPersistingCheck.Nothing)]
    //[PXSelector(typeof(Search<ADPCPaymentProfile.customerProfileID, Where<ADPCPaymentProfile.paymentProfileID.IsEqual<ADPCTransaction.paymentProfileID.FromCurrent>>>))]
    [PXUIField(DisplayName = "Customer Profile ID", Enabled = false, IsReadOnly = true)]
    public virtual string CustomerProfileID { get; set; }
    public abstract class customerProfileID : PX.Data.BQL.BqlString.Field<customerProfileID> { }
      
    #endregion
      
      
    #region TransactionID
    [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
    [PXDefault(typeof(Search<ADPCTransaction.transactionID, Where<ADPCTransaction.paymentProfileID.IsEqual<ADPCPaymentProfile.paymentProfileID.FromCurrent>>>), PersistingCheck = PXPersistingCheck.NullOrBlank)]
    [PXUIField(DisplayName = "Transaction ID", Visibility = PXUIVisibility.SelectorVisible)]
    [AutoNumber(typeof(ADPCSetup.tranNumberingID),typeof(AccessInfo.businessDate))]
    [PXSelector(typeof(Search<ADPCTransaction.transactionID, Where<ADPCTransaction.paymentProfileID.IsEqual<ADPCPaymentProfile.paymentProfileID.FromCurrent>>>))]
    public virtual string TransactionID { get; set; }
    public abstract class transactionID : PX.Data.BQL.BqlString.Field<transactionID> { }
    #endregion
      

      
    #region TransactionDate
    [PXDBDate()]
    [PXDefault(typeof(AccessInfo.businessDate))]
    [PXUIField(DisplayName = "Transaction Date", Required = true)]
    public virtual DateTime? TransactionDate { get; set; }
    public abstract class transactionDate : PX.Data.BQL.BqlDateTime.Field<transactionDate> { }
    #endregion

    #region TransactionDocument
    [PXDBString(10, IsFixed = true, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Transaction Document", Required = true)]
    public virtual string TransactionDocument { get; set; }
    public abstract class transactionDocument : PX.Data.BQL.BqlString.Field<transactionDocument> { }
    #endregion

    #region TransactionAmount
    [PXDBDecimal()]
    [PXUIField(DisplayName = "Transaction Amount", Required = true)]
    public virtual Decimal? TransactionAmount { get; set; }
    public abstract class transactionAmount : PX.Data.BQL.BqlDecimal.Field<transactionAmount> { }
    #endregion

    #region TransactionExpirationDate
    [PXDBDate()]
    [PXUIField(DisplayName = "Transaction Expiration Date")]
    public virtual DateTime? TransactionExpirationDate { get; set; }
    public abstract class transactionExpirationDate : PX.Data.BQL.BqlDateTime.Field<transactionExpirationDate> { }
    #endregion

    #region TransactionCurrency
    [PXDBString(3, IsFixed = true, IsUnicode = true, InputMask = "")]
    [PXStringList("USD, CAD")]
    [PXDefault("USD")]  
    [PXUIField(DisplayName = "Transaction Currency", Required=true)]
    public virtual string TransactionCurrency { get; set; }
    public abstract class transactionCurrency : PX.Data.BQL.BqlString.Field<transactionCurrency> { }
    #endregion

    #region TransactionStatus
    [PXDBString(1, IsFixed = true, InputMask = "")]
    [PXStringList(
    new [] {"A", "D", "E", "H", "X", "U", "S", "V", "R"},
    new [] { ADPCMessages.Approved,
             ADPCMessages.Declined,
             ADPCMessages.Error,
             ADPCMessages.HeldForReview,
             ADPCMessages.Expired,
             ADPCMessages.Unknown,
             ADPCMessages.SettledSuccessfully,
             ADPCMessages.Voided,
             ADPCMessages.RefundSettledSuccessfully})]  
    [PXUIField(DisplayName = "Transaction Status", Required=true)]
    public virtual string TransactionStatus { get; set; }
    public abstract class transactionStatus : PX.Data.BQL.BqlString.Field<transactionStatus> { }
        #endregion

        #region TransactionType
        [PXDBString(1, IsFixed = true, InputMask = "")]
        [PXStringList(
        new[] { "A", "C", "V", "R", "T" },
        new[] { ADPCMessages.Authorization ,
             ADPCMessages.Capture,
             ADPCMessages.Void,
             ADPCMessages.Refund,
             ADPCMessages.Credit})]
        [PXUIField(DisplayName = "Transaction Type", Required = true)]
        public virtual string TransactionType { get; set; }
        public abstract class transactionType : PX.Data.BQL.BqlString.Field<transactionType> { }
        #endregion

        #region AuthorizationNbr
        [PXDBString(20, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Authorization Nbr")]
    public virtual string AuthorizationNbr { get; set; }
    public abstract class authorizationNbr : PX.Data.BQL.BqlString.Field<authorizationNbr> { }
    #endregion

    #region TransactionDescription
    [PXDBString(255, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Transaction Description")]
    public virtual string TransactionDescription { get; set; }
    public abstract class transactionDescription : PX.Data.BQL.BqlString.Field<transactionDescription> { }
    #endregion

    #region Tranuid
    [PXDBGuid()]
    [PXUIField(DisplayName = "Tranuid", Enabled = false, IsReadOnly = true)]
    public virtual Guid? Tranuid { get; set; }
    public abstract class tranuid : PX.Data.BQL.BqlGuid.Field<tranuid> { }
    #endregion

    #region CreatedByID
    [PXDBCreatedByID()]
    public virtual Guid? CreatedByID { get; set; }
    public abstract class createdByID : PX.Data.BQL.BqlGuid.Field<createdByID> { }
    #endregion

    #region CreatedByScreenID
    [PXDBCreatedByScreenID()]
    public virtual string CreatedByScreenID { get; set; }
    public abstract class createdByScreenID : PX.Data.BQL.BqlString.Field<createdByScreenID> { }
    #endregion

    #region CreatedDateTime
    [PXDBCreatedDateTime()]
    public virtual DateTime? CreatedDateTime { get; set; }
    public abstract class createdDateTime : PX.Data.BQL.BqlDateTime.Field<createdDateTime> { }
    #endregion

    #region LastModifiedByID
    [PXDBLastModifiedByID()]
    public virtual Guid? LastModifiedByID { get; set; }
    public abstract class lastModifiedByID : PX.Data.BQL.BqlGuid.Field<lastModifiedByID> { }
    #endregion

    #region LastModifiedByScreenID
    [PXDBLastModifiedByScreenID()]
    public virtual string LastModifiedByScreenID { get; set; }
    public abstract class lastModifiedByScreenID : PX.Data.BQL.BqlString.Field<lastModifiedByScreenID> { }
    #endregion

    #region LastModifiedDateTime
    [PXDBLastModifiedDateTime()]
    public virtual DateTime? LastModifiedDateTime { get; set; }
    public abstract class lastModifiedDateTime : PX.Data.BQL.BqlDateTime.Field<lastModifiedDateTime> { }
    #endregion

    #region Tstamp
    [PXDBTimestamp()]
    [PXUIField(DisplayName = "Tstamp")]
    public virtual byte[] Tstamp { get; set; }
    public abstract class tstamp : PX.Data.BQL.BqlByteArray.Field<tstamp> { }
    #endregion

    #region Noteid
    [PXNote()]
    public virtual Guid? Noteid { get; set; }
    public abstract class noteid : PX.Data.BQL.BqlGuid.Field<noteid> { }
    #endregion
  }
}