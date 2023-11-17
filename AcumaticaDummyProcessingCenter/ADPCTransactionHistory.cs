using System;
using PX.Data;
using PX.Data.BQL.Fluent;

namespace AcumaticaDummyProcessingCenter
{
  [Serializable]
  [PXCacheName("ADPCTransactionHistory")]
  public class ADPCTransactionHistory : IBqlTable
  {
    #region ID
    [PXDBIdentity(IsKey = true)]
    [PXUIField(DisplayName = "ID", Visible = false)]
    public virtual int? ID { get; set; }
    public abstract class iD : PX.Data.BQL.BqlInt.Field<iD> { }
    #endregion

    #region TransactionID
    [PXDBString(15, IsUnicode = true, InputMask = "")]
    [PXDBDefault(typeof(ADPCTransaction.transactionID))]
    [PXParent(typeof(SelectFrom<ADPCTransaction>.
       Where<ADPCTransaction.transactionID.
       IsEqual<ADPCTransactionHistory.transactionID.FromCurrent>>))]
    [PXUIField(DisplayName = "Transaction ID")]
    public virtual string TransactionID { get; set; }
    public abstract class transactionID : PX.Data.BQL.BqlString.Field<transactionID> { }
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
    [PXUIField(DisplayName = "Transaction Status")]
    public virtual string TransactionStatus { get; set; }
    public abstract class transactionStatus : PX.Data.BQL.BqlString.Field<transactionStatus> { }
    #endregion

    #region TransactionType
    [PXDBString(1, IsFixed = true, InputMask = "")]
    [PXStringList(
    new [] {"A", "C", "V", "R", "T"},
    new [] { ADPCMessages.Authorization ,
             ADPCMessages.Capture,
             ADPCMessages.Void,
             ADPCMessages.Refund,
             ADPCMessages.Credit})]  
    [PXUIField(DisplayName = "Transaction Type", Required=true)]
    public virtual string TransactionType{ get; set; }
    public abstract class transactionType: PX.Data.BQL.BqlString.Field<transactionType> { }
    #endregion     
      
    #region ChangeDate
    [PXDBDate()]
    [PXUIField(DisplayName = "Status change DateTime")]
    public virtual DateTime? ChangeDate { get; set; }
    public abstract class changeDate : PX.Data.BQL.BqlDateTime.Field<changeDate> { }
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