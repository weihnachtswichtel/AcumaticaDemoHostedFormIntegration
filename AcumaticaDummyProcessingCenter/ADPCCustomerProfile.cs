using System;
using PX.Data;
using PX.Objects.CS;

namespace AcumaticaDummyProcessingCenter
{
  [Serializable]
  [PXCacheName("ADPCCustomerProfile")]
  [PXPrimaryGraph(typeof(ADPCCustomerProfileEntry))] 
  public class ADPCCustomerProfile : IBqlTable
  {
    #region CustomerProfileID
    [PXDBString(15,  IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
    [PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
    [AutoNumber(typeof(ADPCSetup.cPIDNumberingID), typeof (AccessInfo.businessDate))]
    [PXSelector(typeof(Search<ADPCCustomerProfile.customerProfileID >))]
    [PXUIField(DisplayName = "Customer Profile ID",
            Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string CustomerProfileID { get; set; }
    public abstract class customerProfileID : PX.Data.BQL.BqlString.Field<customerProfileID> { }
    #endregion

    #region CustomerName
    [PXDBString(30, IsUnicode = true)]
    [PXUIField(DisplayName = "Customer Name")]
    public virtual string CustomerName { get; set; }
    public abstract class customerName : PX.Data.BQL.BqlString.Field<customerName> { }
    #endregion

    #region Email
    [PXDBString(255, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Email")]
    public virtual string Email { get; set; }
    public abstract class email : PX.Data.BQL.BqlString.Field<email> { }
    #endregion

    #region CustomerDescription
    [PXDBString(255, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Customer Description")]
    public virtual string CustomerDescription { get; set; }
    public abstract class customerDescription : PX.Data.BQL.BqlString.Field<customerDescription> { }
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