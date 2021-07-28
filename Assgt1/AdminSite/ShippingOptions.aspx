<%@ Page Title="" Language="C#" MasterPageFile="~/AdminSite/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ShippingOptions.aspx.cs" Inherits="Assgt1.ShippingOptions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <form runat="server">


        <asp:Panel runat="server" ID="testpanel"></asp:Panel>
        <asp:Panel runat="server" ID="viewShipOptions">



            <h3>Shipping options</h3>


            <asp:Label runat="server" ID="itemadded" Visible="false" Text="Item was successfully added to the database" CssClass="alert alert-success"></asp:Label>

            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th>name</th>
                        <th>Price</th>
                        <th>Description</th>
                        <th>Expected Arrival (in days)</th>
                    </tr>
                </thead>

                
                <tbody>
                    <asp:Repeater runat="server" ID="shippingTable">
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("name") %></td>
                            <td><%#Eval("cost") %></td>
                            <td><%#Eval("description") %></td>
                            <td><%#Eval("days") %></td>
                        </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                


            </table>
             
        <asp:Button runat="server" ID="addOption" Text="Add Shipping Option" CssClass="btn btn-primary" OnClick="addOption_Click" />
        </asp:Panel>

        <asp:Panel runat="server" ID="addItem" Visible="false">

            <h3>Create Shipping option</h3>


            <asp:CustomValidator runat="server" ID="formValidate" CssClass="alert-danger" OnServerValidate="formValidate_ServerValidate" ></asp:CustomValidator>
            <!--<asp:Label runat="server" CssClass="alert alert-danger" role="alert" ID="outMsg"></asp:Label>-->

            <div class="form-group">
                <asp:Label runat="server" Text="Item name" for="txtName">
                    <asp:TextBox runat="server" ID="txtName" CssClass="form-control">
                    </asp:TextBox>
                </asp:Label>
            </div>

            <div class="form-group row">
                

                <div class="col">

                <asp:Label runat="server" Text="Item Description" for="txtDescription"></asp:Label>
                <asp:TextBox runat="server" TextMode="MultiLine" CssClass="form-control" ID="txtDescription"></asp:TextBox>
                <asp:Label runat="server" Text="Shipping cost ($)" AssociatedControlID="cost"></asp:Label>
                <asp:TextBox ID="cost" runat="server" CssClass="form-control" TextMode="Number" AutoPostBack="true" OnTextChanged="cost_TextChanged" runat="server"></asp:TextBox>
                </div>
                <div class="col">
                <asp:Label runat="server" Text="Shipping Period (in days)" AssociatedControlID="period"></asp:Label>
                <asp:TextBox ID="period" CssClass="form-control" runat="server" OnTextChanged="period_TextChanged" AutoPostBack="true" TextMode="Number" ></asp:TextBox>
                </div>
            </div>
            
            


            <asp:Button runat="server" Text="Cancel" ID="cancelAdd" CssClass="btn btn-secondary" OnClick="cancelAdd_Click"/><asp:Button runat="server" CssClass="btn btn-primary" Visible="false" Text="submit item" OnClick="submitItem_Click" ID="submitItem" />
        </asp:Panel>
        
        
    </form>
</asp:Content>
