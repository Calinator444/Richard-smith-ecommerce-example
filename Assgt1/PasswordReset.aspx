<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PasswordReset.aspx.cs" Inherits="Assgt1.PasswordReset" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customnav" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Reset Password</h3>
    <form runat="server">

        <asp:Panel runat="server" ID="successMessage" Visible="false">
        <div class="form-group">
            <div class="alert alert-success">We've set you an email with password recovery instructions</div>
        </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="errorMessage" Visible="false">
        <div class="form-group" Visible="false">
            <div class="alert alert-danger">That email address doesn't match any of our records</div>
        </div>

        <div class="form-group">
        </asp:Panel>
        <asp:Label runat="server" for="txtEmail" Text="Email Address">
        </asp:Label>
        <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail">
        </asp:TextBox>
        <asp:Button runat="server" ID="btnReset" Text="Reset Password" CssClass="btn btn-primary" OnClick="btnReset_Click" />
        <asp:Button runat="server" ID="btnBack" Text="Back" CssClass="btn btn-secondary" OnClick="btnBack_Click" />
        </div>
    </form>
</asp:Content>
