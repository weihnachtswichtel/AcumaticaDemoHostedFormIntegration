using System;
using PX.Data;
using PX.Data.BQL.Fluent;

namespace AcumaticaDummyProcessingCenter
{
  [Serializable]
  [PXCacheName("ADPCPaymentProfile")]
  public class ADPCPaymentProfile : IBqlTable
  {
    #region CustomerProfileID
    [PXDBString(10, IsKey = true, IsUnicode = true, InputMask = "")]
    [PXDBDefault(typeof(ADPCCustomerProfile.customerProfileID))]
    [PXParent(typeof(SelectFrom<ADPCCustomerProfile>.
       Where<ADPCCustomerProfile.customerProfileID.
       IsEqual<ADPCPaymentProfile.customerProfileID.FromCurrent>>))]
    [PXUIField(DisplayName = "Customer Profile ID")]
    public virtual string CustomerProfileID { get; set; }
    public abstract class customerProfileID : PX.Data.BQL.BqlString.Field<customerProfileID> { }
    #endregion

    #region PaymentProfileID
    [PXDBGuid(IsKey = true)]
    [PXUIField(DisplayName = "Payment Profile ID", Enabled = false, IsReadOnly = true)]
    public virtual Guid? PaymentProfileID { get; set; }
    public abstract class paymentProfileID : PX.Data.BQL.BqlGuid.Field<paymentProfileID> { }
    #endregion

    #region CardType
    [PXDBString(1, IsFixed = true, InputMask = "")]
    [PXUIField(DisplayName = "Card Type")]
    [PXStringList(
      new [] {"V", "M", "A", "U"}, 
      new [] { ADPCMessages.Visa, ADPCMessages.MasterCard, ADPCMessages.AmericanExpress, ADPCMessages.UnionPay})]  
    public virtual string CardType { get; set; }
    public abstract class cardType : PX.Data.BQL.BqlString.Field<cardType> { }
    #endregion

    #region CardExpirationDate
    [PXDBDate()]
    [PXUIField(DisplayName = "Card Expiration Date")]
    public virtual DateTime? CardExpirationDate { get; set; }
    public abstract class cardExpirationDate : PX.Data.BQL.BqlDateTime.Field<cardExpirationDate> { }
    #endregion

    #region Cardbin
    [PXDBString(6, IsFixed = true, InputMask = "")]
    [PXUIField(DisplayName = "Cardbin")]
    public virtual string Cardbin { get; set; }
    public abstract class cardbin : PX.Data.BQL.BqlString.Field<cardbin> { }
    #endregion

    #region CardLastFour
    [PXDBString(4, IsFixed = true, InputMask = "")]
    [PXUIField(DisplayName = "Card Last Four")]
    public virtual string CardLastFour { get; set; }
    public abstract class cardLastFour : PX.Data.BQL.BqlString.Field<cardLastFour> { }
    #endregion

    #region Phone
    [PXDBString(50, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Phone")]
    public virtual string Phone { get; set; }
    public abstract class phone : PX.Data.BQL.BqlString.Field<phone> { }
    #endregion

    #region Name
    [PXDBString(50, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Name")]
    public virtual string Name { get; set; }
    public abstract class name : PX.Data.BQL.BqlString.Field<name> { }
    #endregion

    #region Address
    [PXDBString(50, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Address")]
    public virtual string Address { get; set; }
    public abstract class address : PX.Data.BQL.BqlString.Field<address> { }
    #endregion

    #region City
    [PXDBString(50, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "City")]
    public virtual string City { get; set; }
    public abstract class city : PX.Data.BQL.BqlString.Field<city> { }
    #endregion

    #region State
    [PXDBString(50, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "State")]
    public virtual string State { get; set; }
    public abstract class state : PX.Data.BQL.BqlString.Field<state> { }
    #endregion

    #region PostalCode
    [PXDBString(50, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Postal Code")]
    public virtual string PostalCode { get; set; }
    public abstract class postalCode : PX.Data.BQL.BqlString.Field<postalCode> { }
    #endregion

    #region CardDescription
    [PXDBString(255, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Card Description")]
    public virtual string CardDescription { get; set; }
    public abstract class cardDescription : PX.Data.BQL.BqlString.Field<cardDescription> { }
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