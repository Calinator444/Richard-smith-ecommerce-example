<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="Assgt1.Cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">

    <asp:Panel runat="server" ID="emptyError">
        <h1>:( Your shopping cart is empty</h1>
        <p>Hm, it looks like you haven't added anything to your Shopping cart yet. Why don't you head back to our store and take a look around!</p>
    </asp:Panel>
    <asp:Panel runat="server" ID="CartMenu">
    <table class="table table-bordered">

        <thead>
        <tr>
                <th>Model</th>
                <th>Preview</th>
                <th>Description</th>
                <th>QTY</th>
                <th>Price</th>
                
            </tr>
        </thead>
        <tbody>

                <asp:Repeater runat="server" ID="cartItems" OnDataBinding="cartItems_DataBinding">
                    <ItemTemplate>
                    <tr>
                        <td><%#Eval("Model")%></td>
                        <td><img src='<%#Eval("Image")%>' style="width:3em;" /></td>
                        <td><%#Eval("Description") %></td>
                        <td><asp:TextBox runat="server" type="number" AutoPostBack="true" ID="txtQty" CommandArgument='<%#Eval("ID") %>' OnTextChanged="txtQty_TextChanged" Text='<%#Eval("Quantity")%>'></asp:TextBox><asp:Button runat="server" ID="btnRemove" Text="Remove" OnClick="Unnamed_Click" CommandArgument='<%#Eval("ID") %>'/></td>
                        <td>$<%#Eval("Price") %></td>
           
                    </tr>
                    </ItemTemplate>
                </asp:Repeater>
                    <!--uses the databound description from earlier-->

                <tr>
                    <td colspan="4"></td>
                    
                    <td>
                        Total: $<asp:Label runat="server" ID="lblTotal">

                        </asp:Label>


                    </td>
                </tr>
                    <!--the item templates below can be bound to an ItemTemplate to show each item in the user's cart-->
        </tbody>


    </table>

    <!--<asp:Label runat="server" Text="You must have an account to make a purchase" ID="LoggedErr"></asp:Label>-->
    
    <asp:Button runat="server" CssClass="btn btn-primary" Text="Checkout Items" ID="purchase" OnClick="purchase_Click" ></asp:Button>
        


    </asp:Panel>
    </form>
</asp:Content>
