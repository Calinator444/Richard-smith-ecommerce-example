<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="Assgt1.UserLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    
    <form runat="server" id="Form1">
    <div class="form-row">
    <h2>Login</h2>

    
    </div>
    <asp:Label runat="server" ID="accountActivateSuccess" Visible="false" CssClass="alert alert-success" Text="Your account was successfully activated"></asp:Label>
    <div class="form-group">
    <asp:CustomValidator ID="LoginValidate" OnServerValidate="LoginValidate_ServerValidate" runat="server" ValidationGroup="a"></asp:CustomValidator>
    <asp:Label ID="accountSuccess" runat="server" Visible="false" Text="Your account was successfully created! Check your email for instructions on how to activate it" CssClass="alert alert-success"></asp:Label>
    
    <asp:Label ID="usrEnterErr" runat="server" role="alert" CssClass="alert alert-danger" Visible="false"></asp:Label>
    </div>
    <div class="form-group">
    <asp:Label runat="server" AssociatedControlID="txtUsername" Text="Email Address"></asp:Label>
    <asp:Textbox ID="txtUsername" CssClass="form-control" runat="server"></asp:Textbox>
    </div>

    <div class="form-group">
    <asp:Label Text="Password" AssociatedControlID="txtPassword" runat="server"></asp:Label>
    <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="form-control" runat="server"/>
    </div>


    <asp:Button runat="server" type="submit" CssClass="btn btn-primary" Text="Login" AutoPostBack="true" OnClick="submitBtn_Click" ID="submitBtn" ValidationGroup="a" CausesValidation="true"></asp:Button>

    <asp:Button runat="server" ID="accountCreate" CssClass="btn btn-secondary" Text="Create Account" OnClick="accountCreate_Click"  CausesValidation="false"></asp:Button>
    <asp:Button runat="server" ID="resetPassword" Text="Reset Password" OnClick="resetPassword_Click" CssClass="btn btn-secondary" />
    </form>

    

</asp:Content>
