<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="OrderHistory.aspx.cs" Inherits="Assgt1.OrderHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customnav" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Order History</h1>
    <form runat="server">

    <!--having an error message if the user hasn't made any purchases is a bit tidier than having an empty table-->

    <asp:Panel runat ="server" ID="standardControls">
    <table class="table table-hover">
                                <thead>
                                    <tr>
                                        <th>Date</th>
                                        
                                        <th colspan="2">Shipping Option</th>
                                    </tr>
                                </thead>
                                
                                <asp:Repeater runat="server" ID="orders">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("datePlaced") %></td>
                                            <td><%#Eval("name") %></td>
                                            <td><asp:Button runat="server" Text="view" ID="viewOrder" OnClick="viewOrder_Click" CommandArgument='<%#Eval("orderID") %>' /></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                
                            </table>

    <table class="table table-hover">
                                <thead>
                                    <tr>
                                        <th>Item</th>
                                        
                                        <th>Quantity</th>
                                        <th>price</th>
                                    </tr>
                                </thead>
                                
                                <asp:Repeater runat="server" ID="orderItems">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("modelName") %></td>
                                            <td><%#Eval("quantity") %></td>
                                            <td>$<%#Convert.ToDouble(Eval("price"))*Convert.ToDouble(Eval("quantity")) %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                
                            </table>
        </asp:Panel>
        </form>

    </asp:Content>
