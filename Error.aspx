<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="AmtelcoAppLauncher.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>An error has occurred.</h2>
    <asp:Label ID="lblErrorMessage" ForeColor="Red" runat="server" Text=""></asp:Label>
</asp:Content>
