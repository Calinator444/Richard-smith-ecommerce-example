<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="OrderConfirmation.aspx.cs" Inherits="Assgt1.OrderConfirmation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">


    <asp:Panel runat="server" ID="errorMessage" Visible="false">
        <p>hm... still not logged in?
            <br />Ok, but you'll need to add an email address so we can ship you the invoice:
        </p>
        <asp:CustomValidator ValidationGroup="a" runat="server" OnServerValidate="Unnamed_ServerValidate" CssClass="alert-danger" Text="invalid email address"></asp:CustomValidator>
        <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control"></asp:TextBox>
    </asp:Panel>
    <asp:Panel runat="server" ID="CartMenu">
    <h1>Order Confirmation</h1>
    
    <table class="table table-bordered">

        <thead>
        <tr>
                <th>Item</th>
                <th>Quantity</th>
                <th>Price</th>
                
            </tr>
        </thead>
        <tbody>
                <asp:Repeater runat="server" ID="orderItems">
                    <ItemTemplate>
                    <tr>
                        <td><%#Eval("Model")%></td>
                        <td><asp:TextBox runat="server" type="number" OnTextChanged="txtQty_TextChanged" AutoPostBack="true" ID="txtQty" CommandArgument='<%#Eval("ID") %>' Text='<%#Eval("Quantity")%>'></asp:TextBox><asp:Button runat="server" ID="btnRemove" AutoPostBack="true" OnClick="btnRemove_Click" Text="Remove" CommandArgument='<%#Eval("ID") %>'/></td>
                        <td>$<%#Eval("Price") %></td>
                    </tr>
                    </ItemTemplate>
                </asp:Repeater>
                    <!--uses the databound description from earlier-->
                <tr>
                    <td colspan="2"></td>
                    <td><asp:Label runat="server" ID="shipCost"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td><asp:Label runat="server" ID="totalCost"></asp:Label></td>
                
                </tr>
                    <!--the item templates below can be bound to an ItemTemplate to show each item in the user's cart-->
        </tbody>


    </table>

    <!--<asp:Label runat="server" Text="You must have an account to make a purchase" ID="LoggedErr"></asp:Label>-->
    
    <asp:Button runat="server" CssClass="btn btn-primary" CausesValidation="true" ValidationGroup="a" Text="Place Order" OnClick="btnPlace_Click" ID="btnPlace"></asp:Button>
        


    </asp:Panel>
    </form>
</asp:Content>
