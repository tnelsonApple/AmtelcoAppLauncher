<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserAdmin.aspx.cs" Inherits="AmtelcoAppLauncher.UserAdmin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>User Profiles</h2>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table>
        <tr>
            <td>
                <asp:ListBox ID="lbUsers" runat="server" DataSourceID="SqlDataSource1" AutoPostBack="true" 
                    DataTextField="adUsername" DataValueField="adUsername" Height="302px" 
                    Width="159px" onselectedindexchanged="ListBox1_SelectedIndexChanged"></asp:ListBox>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
                    SelectCommand="SELECT [adUsername] FROM [tblUsers] ORDER BY [adUsername]"></asp:SqlDataSource>
            </td>
            <td valign="top">
                <table>
                    <tr>
                        <td>Username:</td>
                        <td><asp:TextBox ID="txtDisplayUsername" runat="server" Enabled="false"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Telephone Agent:</td>
                        <td><asp:CheckBox ID="cbDisplayTelephoneAgent" runat="server" Enabled="false" /></td>
                    </tr>
                    <tr>
                        <td>Supervisor:</td>
                        <td><asp:CheckBox ID="cbDisplaySupervisor" runat="server" Enabled="false" /></td>
                    </tr>
                    <tr>
                        <td>On-Call Supervisor:</td>
                        <td><asp:CheckBox ID="cbDisplayOCSupervisor" runat="server" Enabled="false" /></td>
                    </tr>
                    <tr>
                        <td>IS Supervisor:</td>
                        <td><asp:CheckBox ID="cbDisplayISSupervisor" runat="server" Enabled="false" /></td>
                    </tr>
                    <tr>
                        <td>User Admin:</td>
                        <td><asp:CheckBox ID="cbDisplayUserAdmin" runat="server" Enabled="false" /></td>
                    </tr>
                    <tr>
                        <td>App Admin:</td>
                        <td><asp:CheckBox ID="cbDisplayAppAdmin" runat="server" Enabled="false" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnAdd" runat="server" Text="Add" onclick="btnAdd_Click" />&nbsp;<asp:Button runat="server" ID="btnPopupModalPlaceholder"  style="display:none" />
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" Enabled="false" 
                                onclick="btnEdit_Click" />&nbsp;
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" Enabled="false" 
                                onclick="btnDelete_Click" />
                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" 
                                TargetControlID="btnDelete" ConfirmText="Are you sure you want to delete this user?">
                            </asp:ConfirmButtonExtender>
                        </td>
                    </tr>
                </table>
                
            </td>
        </tr>
    </table>

    <asp:ModalPopupExtender ID="mpeAddEditUser" 
            runat="server"
            TargetControlID="btnPopupModalPlaceholder"
            PopupControlID="pnlModalPopUp"
            BackgroundCssClass="modalBackground"
            CancelControlID="btnAddEditCancel">
    </asp:ModalPopupExtender>
    <asp:Panel runat="Server" ID="pnlModalPopUp" BorderStyle="Solid" CssClass="modalPopup">
        <asp:HiddenField ID="hfAddOrEdit" runat="server" />
        <table>
            <tr>
                <td>Username:</td>
                <td><asp:TextBox ID="txtUsername" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Telephone Agent:</td>
                <td><asp:CheckBox ID="cbTelephoneAgent" runat="server" /></td>
            </tr>
            <tr>
                <td>Supervisor:</td>
                <td><asp:CheckBox ID="cbSupervisor" runat="server" /></td>
            </tr>
            <tr>
                <td>On-Call Supervisor:</td>
                <td><asp:CheckBox ID="cbOCSupervisor" runat="server" /></td>
            </tr>
            <tr>
                <td>IS Supervisor:</td>
                <td><asp:CheckBox ID="cbISSupervisor" runat="server" /></td>
            </tr>
            <tr>
                <td>User Admin:</td>
                <td><asp:CheckBox ID="cbUserAdmin" runat="server" /></td>
            </tr>
            <tr>
                <td>App Admin:</td>
                <td><asp:CheckBox ID="cbAppAdmin" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnAddEditSave" runat="server" Text="Save" 
                        onclick="btnAddEditSave_Click" />&nbsp;
                    <asp:Button ID="btnAddEditCancel" runat="server" Text="Cancel"/>
                </td>
            </tr>
        </table>
    
    </asp:Panel>

    </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
