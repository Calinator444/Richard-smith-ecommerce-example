<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ProductView.aspx.cs" Inherits="Assgt1.ProductView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">




        
        <form runat="server">
        <asp:FormView ID="form" runat="server"  OnDataBinding="stuffstuff_DataBinding">

        <ItemTemplate>
        <h2 style="padding:1em;font-size:30px;"><%# Eval("manufacturer")%> <%# Eval("modelName")%></h2>
        <div class="row">
        <div class="col">
        <img src='<%#Eval("previewImage")%>' style="width:20em;">
        </div>
        <div class="col">

        <p style="width:30em;"><b>Description:</b> <br /><%#Eval("description")%></p>
        <p><b style="font-size:30px;">Price <%#Eval("price")%></b></p>
        </div>
        </div>

        <asp:Button runat="server" ID="btnAddToCart" OnClick="cartview_Click" CssClass="btn btn-primary" Text="Add to cart" /> 
        </ItemTemplate>
        </asp:FormView>
        </form>
</asp:Content>
