using System;
using PX.Data;
using PX.Objects.AR;
using PX.TM;
using PX.Objects.CS;


namespace AcumaticaDummyProcessingCenter
{

  [PXCacheName("ADPC Preferences")]
  [PXPrimaryGraph(typeof(ADPCSetupMaint))]

  public class ADPCSetup : IBqlTable
  {
    #region CPIDNumberingID 
     [PXDBString(10, IsUnicode = true)]
      [PXUIField(DisplayName = "CPID Numbering Seq.")]
      [PXSelector(typeof(Numbering.numberingID),
          DescriptionField = typeof(Numbering.descr))]
      [PXDefault("ADPCCPID")]
    public virtual string CPIDNumberingID { get; set; }
    public abstract class cPIDNumberingID : PX.Data.BQL.BqlString.Field<cPIDNumberingID> { }
    #endregion

    #region TranNumberingID 
    [PXDBString(10, IsUnicode = true)]
    [PXUIField(DisplayName = "Tran. Numbering Seq.")]
    [PXSelector(typeof(Numbering.numberingID),
        DescriptionField = typeof(Numbering.descr))]
    [PXDefault("ADPCTRAN")]
    public virtual string TranNumberingID { get; set; }
    public abstract class tranNumberingID : PX.Data.BQL.BqlString.Field<tranNumberingID> { }
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