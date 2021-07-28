<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PasswordChange.aspx.cs" Inherits="Assgt1.PasswordChange" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customnav" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">


        <div id="form-group">
        <asp:customValidator runat="server" OnServerValidate="Unnamed_ServerValidate" ErrorMessage="passwords must match" ValidationGroup="a" CssClass="alert alert-danger">
        </asp:customValidator>
        <asp:customValidator runat="server" ID="passwordValidator" OnServerValidate="passwordValidator_ServerValidate" ErrorMessage="Password must contain at least one number" ValidationGroup="a" CssClass="alert alert-danger">
        </asp:customValidator>


        <asp:label runat="server" ID="successMessage" Text="Your password was successfully updated" CssClass="alert alert-success" Visible="false"></asp:label>
        <asp:Label runat="server" ID="errorMessage" Text="Something went wrong, we couldn't change your password" CssClass="alert alert-danger" Visible="false"></asp:Label>
        </div>

        <div class="form-group">
            <asp:Label runat="server" for="enterpassword" Text="Enter Password"></asp:Label>
            
            <asp:TextBox runat="server" TextMode="Password" ID="enterpassword" CssClass="form-control"></asp:TextBox>
            
        </div>
        <div class="form-group">
            <asp:Label runat="server" for="txtConfirm" Text="Confirm password" ID="Label1"></asp:Label>
            <asp:TextBox runat="server" ID="txtConfirm" TextMode="Password" CssClass="form-control"></asp:TextBox>
        </div>
        <asp:Button runat="server" OnClick="btnChangePassword_Click" CssClass="btn btn-primary" Text="Reset Password" CausesValidation="true" ValidationGroup="a" ID="btnChangePassword" AutoPostBack="true"/>


    </form>
</asp:Content>
