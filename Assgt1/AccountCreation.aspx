<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountCreation.aspx.cs" MasterPageFile="~/Site1.master" Inherits="Assgt1.AccountCreation" %>
    


<asp:Content ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!--
        Author: W3Schools
        Date accessed: 16/03/2021
        Purpose: a W3 schools login page template was incorporated for this page
        -->
    <div class="formholder">
    <div class="form" runat="server">
    <h2>Create account</h2>

      <!--should probably add a character limit to each of these fields to accompany my data types (e.g. char limit of 150 for usernames)-->
    <form runat="server" id="accountForm" >
    <asp:CustomValidator runat="server" ID="checkExists" Text="An account going by that email address already exists" CssClass="alert alert-danger" OnServerValidate="checkExists_ServerValidate" ValidationGroup="a"></asp:CustomValidator>
    <div class="form-group">
    
    <asp:Label runat="server" AssosciatedControlID="txtEmail" Text="Email Address"></asp:Label>
        <asp:CustomValidator ID="emailValidator" CssClass="alert alert-danger" OnServerValidate="emailValidator_ServerValidate" runat="server" ValidationGroup="a"></asp:CustomValidator>
        <asp:Textbox ID="txtEmail" CssClass="form-control" runat="server"></asp:Textbox>

    
    </div>

    <div class="form-group">
    
    <asp:Label Text="Password" AssosciatedControlID="txtPassword" runat="server"></asp:Label>
    <asp:CustomValidator runat="server" ID="passValidate" CssClass="alert alert-danger" ControlToValidate="txtPassword" OnServerValidate="passValidate_ServerValidate" ValidationGroup="a"/>
    <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="form-control"></asp:TextBox>
    </div>
    </div>


      
    

    <div class="form-group">

    <asp:Label Text="Confirm Password" CssClass="asplabel" AssociatedControlID="txtPasswordConfirm" runat="server"></asp:Label>
            <asp:CustomValidator ID="passConfirmValid" OnServerValidate="passConfirmValid_ServerValidate" Text="Passwords must match" CssClass="alert alert-danger" runat="server" ValidationGroup="a"></asp:CustomValidator>
    <asp:TextBox runat="server" CssClass="form-control" TextMode="Password" ID="txtPasswordConfirm"></asp:TextBox>

    
    </div>
    <asp:Button runat="server" OnClick="submitBtn_Click" CssClass="btn btn-primary" Text="Submit" ID="submitBtn" CausesValidation="true" ValidationGroup="a"></asp:Button>
        



    </form>
    </div>
        </div>
</asp:Content>

