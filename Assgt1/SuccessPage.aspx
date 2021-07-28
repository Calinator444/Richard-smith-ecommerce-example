<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="SuccessPage.aspx.cs" Inherits="Assgt1.SuccessPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customnav" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Success!</h1>
    <p>Thank you for your purchase! We'll begin shipping your order shortly</p>
    
    <p><asp:Label runat="server" ID="lblInvoice"></asp:Label></p>
</asp:Content>
