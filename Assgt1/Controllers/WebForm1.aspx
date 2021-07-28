<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPage1.master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Assgt1.Controllers.WebForm1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="sidebar" runat="server">


    
    <p>Product tier</p>
    

    <!-- ###SIDEBAR###-->

    <asp:CheckBoxList runat="server" ID="Manufacturer" AutoPostBack="true">
        <asp:ListItem>Intel</asp:ListItem>
        <asp:ListItem>AMD</asp:ListItem>
    </asp:CheckBoxList>
    <asp:CheckBox autopostback="true" runat="server" Text="blablabla"/>
        
    <p>Generation</p>
    <asp:CheckBoxList ID="IntelGeneration" Visible="false"
    runat="server">
    <asp:ListItem>8th Gen</asp:ListItem>
    <asp:ListItem>9th Gen</asp:ListItem>
    <asp:ListItem>10th Gen</asp:ListItem>
    </asp:CheckBoxList>
    <asp:CheckBoxList ID="AMDGeneration" Visible="false"
    runat="server">
    <asp:ListItem>2nd Gen</asp:ListItem>
    <asp:ListItem>3rd Gen</asp:ListItem>
    <asp:ListItem>5th Gen</asp:ListItem>
    </asp:CheckBoxList>
    

    <!--END OF SIDEBAR-->
</asp:Content>




<asp:Content ID="Content2" ContentPlaceholderID="mainbody" runat="server">
    <!--this data binding was inspired by

    Author: Microsoft corp
    Date accessed: 5/03/2021

    -->

<asp:Repeater ID="ProcessorsPage" runat='server'>
    <ItemTemplate >
    <div class="ContentHead"><%# Eval("Manufacturer") %></div><br />
      <table  border="0">
        <tr>
          <td>
            <asp:Image runat="server" ImageUrl='<%#Eval("PreviewImg") %>'  border="0" width="200"/>
          </td>
        </tr>
      </table>

      <b>Price</b><%# Eval("Price") %><br /> 
      <span class="ModelNumber">
        <b>Model Number:</b> <%# Eval("Model") %>
      </span><br />
      <a href='#'>
        
        <img id="Img2" src="~/Styles/Images/add_to_cart.gif" runat="server" 
             alt="lol there's no cart link" />
      </a>
      <br /><br />
        <!--next item-->
      </ItemTemplate>
      </asp:Repeater>
    <asp:EntityDataSource runat="server">
        <WhereParameters>
        </WhereParameters>
    </asp:EntityDataSource>
</asp:Content>
