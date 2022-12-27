<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="ADPC3020.aspx.cs" Inherits="Page_ADPC3020" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="AcumaticaDummyProcessingCenter.ADPCTransactionEntry"
        PrimaryView="Transaction"
        >
		<CallbackCommands>

		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
	<px:PXFormView MinHeight="385" ID="form" runat="server" DataSourceID="ds" DataMember="Transaction" Width="100%" Height="" AllowAutoHide="false">
		<Template>
			<px:PXLayoutRule runat="server" ID="CstPXLayoutRule12" StartColumn="True" LabelsWidth="M" ControlSize="M"></px:PXLayoutRule>
			<px:PXSelector CommitChanges="True" runat="server" ID="ADPCPaymentProfileID" DataField="PaymentProfileID" ></px:PXSelector>
			<px:PXSelector CommitChanges="True" runat="server" ID="ADPCTransactionID" DataField="TransactionID" ></px:PXSelector>
			<px:PXDropDown runat="server" ID="CstPXDropDown18" DataField="TransactionType" />
			<px:PXTextEdit runat="server" ID="CstPXTextEdit1" DataField="AuthorizationNbr" ></px:PXTextEdit>
			<px:PXDateTimeEdit runat="server" ID="CstPXDateTimeEdit5" DataField="TransactionDate" ></px:PXDateTimeEdit>
			<px:PXLayoutRule runat="server" ID="CstPXLayoutRule14" StartColumn="True" LabelsWidth="M" ControlSize="M"></px:PXLayoutRule>
			<px:PXTextEdit runat="server" ID="CstPXTextEdit6" DataField="TransactionDescription" ></px:PXTextEdit>
			<px:PXTextEdit runat="server" ID="CstPXTextEdit7" DataField="TransactionDocument" ></px:PXTextEdit>
			<px:PXDateTimeEdit runat="server" ID="CstPXDateTimeEdit8" DataField="TransactionExpirationDate" ></px:PXDateTimeEdit>
			<px:PXDropDown runat="server" ID="CstPXDropDown17" DataField="TransactionStatus" ></px:PXDropDown>
			<px:PXLayoutRule runat="server" ID="CstPXLayoutRule13" StartColumn="True" LabelsWidth="M" ControlSize="M"></px:PXLayoutRule>
			<px:PXNumberEdit runat="server" ID="CstPXNumberEdit3" DataField="TransactionAmount" ></px:PXNumberEdit>
			<px:PXTextEdit runat="server" ID="CstPXTextEdit4" DataField="TransactionCurrency" ></px:PXTextEdit>
			<px:PXTextEdit runat="server" ID="CstPXTextEdit11" DataField="Tranuid" ></px:PXTextEdit>
			<px:PXTextEdit runat="server" ID="CstPXTextEdit16" DataField="CustomerProfileID" CommitChanges="True" ></px:PXTextEdit></Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" Runat="Server">
	<px:PXGrid SyncPosition="True" ID="grid" runat="server" DataSourceID="ds" Width="100%" Height="385px" SkinID="Details" AllowAutoHide="false">
		<Levels>
			<px:PXGridLevel DataMember="TransactionHistory">
			    <Columns>
				<px:PXGridColumn DataField="TransactionType" Width="70" />
				<px:PXGridColumn DataField="TransactionStatus" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn TimeMode="True" DataField="ChangeDate" Width="90" ></px:PXGridColumn></Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" ></AutoSize>
		<ActionBar >
		</ActionBar>
	
		<Mode AllowRowSelect="False" />
		<Mode AllowDelete="False" />
		<Mode AllowDragRows="False" />
		<Mode AllowAddNew="False" />
		<Mode AllowSort="False" />
		<Mode AllowUpdate="False" />
		<Mode AutoInsert="False" /></px:PXGrid>
</asp:Content>