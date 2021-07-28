<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPage1.master" AutoEventWireup="true" CodeBehind="ProductListings.aspx.cs" Inherits="Assgt1.ProductListings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="sidebar" runat="server">
        <!--All items the site sells have a manufaturer so this attribute is always visible-->





    <asp:Label runat="server" Visible="false" ID="lblManufacturer" Text="Manufacturer"></asp:Label>
    
     <asp:CheckBoxList runat="server" OnSelectedIndexChanged="generic_SelectIndexChanged" ID="checkManufacturer" DataTextField="manufacturer" DataValueField="manufacturerID" AutoPostBack="true">

    </asp:CheckBoxList>



    <!--STORAGE DEVICES-->

    <asp:Label runat="server" Text="Capacity" Visible="false" ID="lblCapacity"></asp:Label>
     <asp:CheckBoxList runat="server" ID="checkCapacity" OnSelectedIndexChanged="generic_SelectIndexChanged" DataTextField="capacity" DataValueField="capacity" Visible="false" AutoPostBack="true">
    </asp:CheckBoxList>
    <asp:Label runat="server" Visible="false" Text="Form Factor" ID="lblFormFactor"></asp:Label>
    <asp:CheckBoxList runat="server" ID="checkFormFactor" OnSelectedIndexChanged="generic_SelectIndexChanged" DataTextField="FormFactor" DataValueField="FormFactor" Visible="false" AutoPostBack="true">
    </asp:CheckBoxList>


    <!--<asp:SqlDataSource runat="server">

    </asp:SqlDataSource>-->


    <!--Cooler attributes-->
    <asp:Label runat="server" Visible="false" Text="Dimensions" ID="lblDimensions"></asp:Label>
     <asp:CheckBoxList runat="server" OnSelectedIndexChanged="generic_SelectIndexChanged" DataTextField="dimensions" DataValueField="dimensions" ID="checkDimensions" Visible="false" AutoPostBack="true">
        
    </asp:CheckBoxList>

    <!--keycap types for keyboards-->

    <asp:Label runat="server" Visible="false" Text="Key Caps" ID="lblCherry"></asp:Label>
    <asp:CheckBoxList runat="server" OnSelectedIndexChanged="generic_SelectIndexChanged" DataTextField="keyCaps" DataValueField="keyCaps" ID="checkCherry" Visible="false" AutoPostBack="true">
    </asp:CheckBoxList>

    <asp:Label runat="server" Visible="false" Text="Cooler type" ID="lblCoolType"></asp:Label>
    <asp:CheckBoxList runat="server" ID="checkCoolType" OnSelectedIndexChanged="generic_SelectIndexChanged" DataTextField="Type" DataValueField="Type" Visible="false" AutoPostBack="true">
    </asp:CheckBoxList>

    <!--MONITOR ATTRIBUTES-->

    <asp:Label runat="server" Visible="false" Text="Refresh Rate" ID="lblRefresh"></asp:Label>
    <asp:CheckBoxList runat="server" ID="checkRefresh" DataTextField="refreshRate" DataValueField="refreshRate" OnSelectedIndexChanged="generic_SelectIndexChanged" Visible="false" AutoPostBack="true">
    </asp:CheckBoxList>

    <asp:Label runat="server" Visible="false" Text="Size" ID="lblInches"></asp:Label>
    <asp:CheckBoxList runat="server" OnSelectedIndexChanged="generic_SelectIndexChanged" DataTextField="size" DataValueField="size" ID="checkInches" Visible="false" AutoPostBack="true">
    </asp:CheckBoxList>

    <asp:Label runat="server" Visible="false" Text="Size" ID="lblResolution"></asp:Label>
    <asp:CheckBoxList runat="server" OnSelectedIndexChanged="generic_SelectIndexChanged" DataTextfield="resolution" DataValueField="resolution" ID="checkResolution" Visible="false" AutoPostBack="true">
    </asp:CheckBoxList>


    <!--PROCESSOR ATTRIBUTES-->

    <!--sku tends to be more of a marketing name, should change this to something else-->
    <asp:Label runat="server" Visible="false" ID="lblSku" Text="Procesor Sku"></asp:Label>
    <asp:CheckBoxList runat="server" ID="checkSku" OnSelectedIndexChanged="generic_SelectIndexChanged" DataTextField="sku" DataValueField="sku" Visible="false" AutoPostBack="true">
    </asp:CheckBoxList>

    <asp:Label runat="server" Visible="false" ID="lblGen" Text="Generation"></asp:Label>
    <asp:CheckBoxList runat="server" ID="checkGen" OnSelectedIndexChanged="generic_SelectIndexChanged" DataTextField="generation" DataValueField="generation" Visible="false" AutoPostBack="true">
    </asp:CheckBoxList>




    <!--computer specs-->
    <asp:Label runat="server" Visible="false" ID="lblGraphics" Text="Graphics Card"></asp:Label>
    <asp:CheckBoxList runat="server" DataTextField="graphicsCard" OnSelectedIndexChanged="generic_SelectIndexChanged" DataValueField="graphicsCardID" ID="checkGraphics" Visible="false" AutoPostBack="true">
    </asp:CheckBoxList>

    <asp:Label runat="server" Visible="false" ID="lblCpu" Text="Processor"></asp:Label>
    <asp:CheckBoxList runat="server" ID="checkCpu" OnSelectedIndexChanged="generic_SelectIndexChanged" DataTextField="processor" DataValueField="processorID" Visible="false" AutoPostBack="true">
    </asp:CheckBoxList>


    <!--
        ERAL ATTRIBUTE-->
    <asp:Label runat="server" Visible="false" ID="lblConn" Text="Wireless">
    </asp:Label>
    <asp:CheckBoxList OnSelectedIndexChanged="generic_SelectIndexChanged" DataValueField="wireless" DataTextField="wireless" runat="server" ID="checkConn" AutoPostBack="true"></asp:CheckBoxList>
    <!--POWER SUPPLY ATTRIBUTES-->

    <asp:Label runat="server" Visible="false" ID="lblRating" Text="Rating"></asp:Label>
    <asp:CheckBoxList OnSelectedIndexChanged="generic_SelectIndexChanged" runat="server" DataTextField="rating" DataValueField="rating" ID="checkRating" Visible="false" AutoPostBack="true">
    </asp:CheckBoxList>

    <asp:Label runat="server" Visible="false" ID="lblWattage" Text="Wattage"></asp:Label>
    <asp:CheckBoxList runat="server" ID="checkWattage" DataTextField="wattage" DataValueField="wattage" OnSelectedIndexChanged="generic_SelectIndexChanged" Visible="false" AutoPostBack="true">
    </asp:CheckBoxList>


    <!--MOTHERBOARD ATTRIUBUTES-->


    <asp:Label runat="server" ID="lblwifi" AssosciatedControlID="checkWifi" Visible="false" Text="Wifi"></asp:Label>
    <asp:CheckBoxList runat="server" DataTextField="wifi" DataValueField="wifi" ID="checkWifi" OnSelectedIndexChanged="generic_SelectIndexChanged" Visible="false" AutoPostBack="true">
    </asp:CheckBoxList>




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainbody" runat="server">
    
    <asp:Repeater ID="ProductList" runat='server' OnDataBinding="ProductList_DataBinding">
    <ItemTemplate>
    <div class="ContentHead"><b></b> <%# Eval("modelName") %></b></div><br />
      <table  border="0">
        <tr>
          <td>
            <asp:Image runat="server" ImageUrl='<%#Eval("previewImage") %>'  border="0" style="width:3em;"/>
          </td>
        </tr>
      </table>
      <b>Price</b> <%# Eval("price") %><br /> 
      <span class="ModelNumber">
        <b>Model Number:</b>
      </span><br />
        
        <!--Commnd argument is used here to pass the model to the codebehind-->

    
        <asp:Button runat="server" CommandArgument='<%#Eval("productID") %>'  CssClass="btn btn-secondary" OnClick="addCart_Click" Text="Add to cart"/>

        <asp:button runat="server" ID="btnItemView" CommandArgument='<%#Eval("productID") %>' Text="Preview Item" Onclick="btnItemView_Click" CssClass="btn btn-primary"/>
      <br /><br />
      </ItemTemplate>
      </asp:Repeater>
        
  
</asp:Content>
