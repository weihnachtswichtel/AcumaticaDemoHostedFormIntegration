<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="ADPC3010.aspx.cs" Inherits="Page_ADPC3010" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="AcumaticaDummyProcessingCenter.ADPCCustomerProfileEntry"
        PrimaryView="CustomerProfile">
		<CallbackCommands>

		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" DataMember="CustomerProfile" Width="100%" Height="100px" AllowAutoHide="false">
		<Template>
			<px:PXLayoutRule ID="PXLayoutRule1" runat="server" StartRow="True"></px:PXLayoutRule>
			<px:PXSelector CommitChanges="True" runat="server" ID="ADPCCustomerProfileID" DataField="CustomerProfileID" ></px:PXSelector>
			<px:PXTextEdit runat="server" ID="ADPCCustomerName" DataField="CustomerName" ></px:PXTextEdit>
			<px:PXLayoutRule runat="server" ID="CstPXLayoutRule5" StartColumn="True" />
			<px:PXTextEdit runat="server" ID="ADPCCustomerEmail" DataField="Email" ></px:PXTextEdit>
			<px:PXTextEdit runat="server" ID="ADPCCustomerDescription" DataField="CustomerDescription" ></px:PXTextEdit></Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" Runat="Server">
	<px:PXGrid SyncPosition="True" ID="grid" runat="server" DataSourceID="ds" Width="100%" Height="150px" SkinID="Details" AllowAutoHide="false">
		<Levels>
			<px:PXGridLevel DataMember="PaymentProfiles">
			    <Columns>
				<px:PXGridColumn DataField="CustomerProfileID" Width="120" />
				<px:PXGridColumn DataField="PaymentProfileID" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="Name" Width="180" ></px:PXGridColumn>
				<px:PXGridColumn DataField="CardType" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="Cardbin" Width="72" ></px:PXGridColumn>
				<px:PXGridColumn DataField="CardLastFour" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="CardExpirationDate" Width="90" ></px:PXGridColumn>
				<px:PXGridColumn DataField="CardDescription" Width="280" ></px:PXGridColumn>
				<px:PXGridColumn DataField="Address" Width="180" ></px:PXGridColumn>
				<px:PXGridColumn DataField="Phone" Width="180" ></px:PXGridColumn>
				<px:PXGridColumn DataField="State" Width="180" ></px:PXGridColumn>
				<px:PXGridColumn DataField="City" Width="180" ></px:PXGridColumn>
				<px:PXGridColumn DataField="PostalCode" Width="180" ></px:PXGridColumn></Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" ></AutoSize>
		<ActionBar >
		</ActionBar>
	
		<Mode InitNewRow="True" ></Mode></px:PXGrid>
</asp:Content>