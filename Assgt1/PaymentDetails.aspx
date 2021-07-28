<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PaymentDetails.aspx.cs" Inherits="Assgt1.PaymentDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script src="Scripts/Addressfiller.js"></script>
    <link href="css/Addressfiller.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customnav" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form runat="server">

    <asp:Panel runat="server" ID="errorpreamble" Visible="false">
        <p style="color:red;">Hmm... you don't seem to be logged in :(<br /></p>
        <p>If you'd like to save your payment details and for later use feel free to <a href="AccountCreation.aspx">create an account</a> </p>
    </asp:Panel>
    <asp:CheckBox runat="server" ID="saveShipping" Visible="false" Text="Save Shipping Information" />
    <h3>Shipping Address</h3>

    <div class="form-row">
        <asp:CustomValidator runat="server" ID="shipAddressValid" CssClass ="alert alert-danger" ValidationGroup="a" OnServerValidate="shipAddressValid_ServerValidate" ></asp:CustomValidator>
    </div>
    <asp:CheckBox runat="server" Text="Use as Billing Address" OnCheckedChanged="useAsBilling_CheckedChanged" ID="useAsBilling" AutoPostBack="true"/>
    <div class="form-row">
        <div class="col">
            <asp:Label runat="server" Text="Post Code" AssociatedControlID="shipCode"></asp:Label>
            <asp:TextBox runat="server" ID="shipCode" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col">
            <asp:Label runat="server" Text="Street" AssociatedControlID="shipStreet"></asp:Label>
            <asp:TextBox runat="server" ID="shipStreet" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col">
            <asp:Label runat="server" AssosciatedControlID="shipStreetno" Text="Street number"></asp:Label>
            <asp:TextBox runat="server" CssClass="form-control" ID="shipStreetno" TextMode="Number"></asp:TextBox>
        </div>
    </div>
   

        <div class="form-row">
        <div class="col">
            <asp:Label runat="server" Text="Country" AssociatedControlID="shipCountry"></asp:Label>
            <span class="autocomplete">
                <asp:TextBox runat="server" ID="shipCountry" CssClass="form-control"></asp:TextBox>
            </span>
        </div>
        <div class="col">
                <asp:Label runat="server" Text="Suburb" AssociatedControlID="shipSuburb">
                </asp:Label>
                <asp:TextBox runat="server" ID="shipSuburb" CssClass="form-control"></asp:TextBox>
        </div>
    </div>

    <h3>Billing Address</h3>
        <asp:CustomValidator runat="server" CssClass="alert alert-danger" ValidationGroup="a" OnServerValidate="billAddressValid_ServerValidate" ID="billAddressValid"></asp:CustomValidator>
    <div class="form-row">
        <div class="col">
            <asp:Label runat="server" Text="Post Code" AssociatedControlID="billCode" ></asp:Label>
            <asp:TextBox runat="server" ID="billCode" CssClass="form-control"></asp:TextBox>
            
        </div>
        <div class="col">
            <asp:Label runat="server" Text="Street"  AssociatedControlID="billStreet">           </asp:Label>
            <asp:TextBox runat="server" CssClass="form-control" ID="billStreet"></asp:TextBox>
        </div>
        <div class="col">
            
                <asp:Label runat="server" Text="Street Number" AssociatedControlID="billStreetNo"></asp:Label>
                <asp:TextBox runat="server" CssClass="form-control" ID="billStreetNo" TextMode="Number"></asp:TextBox>
        </div>
    </div>

        <div class="form-row">
            <div class="col">
                <asp:Label runat="server" Text="Country" AssociatedControlID="billCountry"></asp:Label>
                    <span class="autocomplete">
                <asp:TextBox runat="server" ID="billCountry" CssClass="form-control"></asp:TextBox>
                    </span>
            </div>
            <div class="col">
                <asp:Label runat="server" Text="Suburb" AssociatedControlID="billSuburb">
                </asp:Label>
                <asp:TextBox runat="server" ID="billSuburb" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

        <h3>Credit cart information</h3>
        <asp:CustomValidator runat="server" CssClass="alert alert-danger" ID="CreditCardValidator" Text="please review your credit card information" ValidationGroup="a" OnServerValidate="CreditCardValidator_ServerValidate"></asp:CustomValidator>
        <div class="form=row">
            <div class="col">
                <asp:Label runat="server" AssociatedControlID="CreditCardNo" Text="Credit Card information"></asp:Label>
                <asp:Textbox runat="server" ID="CreditCardNo" CssClass="form-control" TextMode="Number" />
            </div>
        </div>

        <div class="form-row">
            <div class="col">
                <asp:Label runat="server" AssosciatedControlID="txtCCV" Text="CCV">
                </asp:Label>
                <asp:Textbox runat="server" CssClass="form-control" ID="txtCCV"></asp:Textbox>
            </div>
            <div class="col">>
                <asp:Label runat="server" AssosciatedControlID="txtExpiry" Text="Expiry (MMYY)">
                </asp:Label>
                <asp:Textbox runat="server" CssClass="form-control" ID="txtExpiry" TextMode="Number"></asp:Textbox>
            </div>
        </div>

        <div class="form-row">
            <div class="col">
                <asp:dropDownList CssClass="form-control" runat="server" ID="shipOptions" AutoPostBack="true" OnSelectedIndexChanged="shipOptions_SelectedIndexChanged" DataTextField="name" DataValueField="shippingID">

                </asp:dropDownList>

            </div>
            <div class="col"><asp:Label runat="server" ID="lblShip"></asp:Label></div>
        </div>

            <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="a" ID="confirmOrder" OnClick="confirmOrder_Click" CausesValidation="true" AutoPostBack="true" Text="Confirm Order" />

    </form>
</asp:Content>
