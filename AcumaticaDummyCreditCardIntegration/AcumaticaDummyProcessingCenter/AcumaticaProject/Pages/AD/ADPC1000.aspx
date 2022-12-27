<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="ADPC1000.aspx.cs" Inherits="Page_ADPC1000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="AcumaticaDummyProcessingCenter.ADPCSetupMaint"
        PrimaryView="Setup">
		<CallbackCommands>

		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" DataMember="Setup" Width="100%" AllowAutoHide="false">
		<Template>
			<px:PXLayoutRule ID="PXLayoutRule1" runat="server" StartRow="True"  LabelsWidth="SM" ControlSize="SM"></px:PXLayoutRule>
			<px:PXSelector runat="server"  AllowEdit="True"  ID="CstPXSelector13" DataField="CPIDNumberingID" />
			<px:PXSelector runat="server"  AllowEdit="True"  ID="CstPXSelector14" DataField="TranNumberingID" /></Template>
		<AutoSize Container="Window" Enabled="True" MinHeight="200" ></AutoSize>
	</px:PXFormView>
</asp:Content>