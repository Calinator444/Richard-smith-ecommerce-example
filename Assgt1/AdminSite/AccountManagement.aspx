<%@ Page Title="" Language="C#" MasterPageFile="~/AdminSite/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AccountManagement.aspx.cs" Inherits="Assgt1.AccountManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    



    

        <form runat="server">
        <!--usernames-->
        <!--autopostback triggers refresh which resets some of the controls-->
            <div class="row">
                <div class="col">
                   


                            <asp:Label ID="txtSuccess" runat="server" CssClass="alert alert-success" Text="Record successfully updated" Visible="false">
                                </asp:Label>
                            <h3>User</h3>
                            
                            <!--the user needed a dropdown list so that they could tell what the names of the acocunts were-->
                            <asp:DropDownList OnSelectedIndexChanged="allUsers_SelectedIndexChanged" runat="server" CssClass="form-control" ID="drpUsers" AutoPostBack="true">
                            </asp:DropDownList>
                            
                        

                        <!--used as an output box-->
                        
                            

                        <!-- why does every control need CssClass="form-control"? Why doesn't bootstrap just apply formatting for every control automatically?-->    
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail"></asp:TextBox>

                        
                        <h3>Password</h3>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtPass"></asp:TextBox>
                        <asp:Button runat="server" ID="updateAccount" Onclick="updateAccount_Click" CssClass="btn btn-primary" Text="Update Account Details" />
                        <asp:Checkbox runat="server" Text="Account active" ID="chckActive"  />
                        <asp:Checkbox runat="server" Text="Admin Privieleges" ID="chkAdmin" />
                        

                        
                        <h3>Shipping Address</h3>

                                
                            <asp:Label runat="server" AssosciatedControlID="ShipPostCode" Text="Postal Code"></asp:Label>
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="a" ControlToValidate="ShipPostCode" Text="fill this out" CssClass="alert-danger"></asp:RequiredFieldValidator>
                            <asp:TextBox CssClass="form-control" runat="server" ID="ShipPostCode" ValidationGroup="a"></asp:TextBox>


                            <asp:Label AssociatedControlID="ShipCountry" runat="server" Text="Country"></asp:Label>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ShipPostCode" ValidationGroup="a" Text="fill this out" CssClass="alert-danger"></asp:RequiredFieldValidator>
                            <asp:TextBox runat="server" CssClass="form-control" ID="ShipCountry" ValidationGroup="a"></asp:TextBox>

                            <asp:Label AssociatedControlID="ShipStreet" runat="server" Text="Street"></asp:Label>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ShipStreet" ValidationGroup="a" Text="fill this out" CssClass="alert-danger"></asp:RequiredFieldValidator>
                            <asp:TextBox runat="server" CssClass="form-control" ID="ShipStreet" ValidationGroup="a"></asp:TextBox>

                            


                            <asp:Label runat="server" AssosciatedControlID="ShipSuburb" Text="Suburb"></asp:Label>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ShipSuburb" ValidationGroup="a" Text="fill this out" CssClass="alert-danger"></asp:RequiredFieldValidator>
                            <asp:TextBox runat="server" ValidationGroup="a" CssClass="form-control" ID="ShipSuburb"></asp:TextBox>

                            <asp:Label AssociatedControlID="ShipNo" runat="server" Text="Unit Number"></asp:Label>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ShipNo" ValidationGroup="a" Text="fill this out" CssClass="alert-danger"></asp:RequiredFieldValidator>
                            <asp:TextBox runat="server" ValidationGroup="a" CssClass="form-control" ID="ShipNo"></asp:TextBox>
                            
                            

                        <!--Autopostback must be enabled for server controls to update in real time-->
                        <asp:checkbox runat="server" ID="chkUseAsBilling" AutoPostBack="true" OnCheckedChanged="chkUseAsBilling_CheckedChanged" Text="use as billing address"/>

                        
                        <h3>Billing Address</h3>
                            <asp:Label runat="server" AssosciatedControlID="BillPostCode" Text="Postal Code"></asp:Label>
                            <asp:RequiredFieldValidator ID="BillPostCodeValid" runat="server" ValidationGroup="a" ControlToValidate="BillPostCode" Text="fill this out" CssClass="alert-danger"></asp:RequiredFieldValidator>
                            <asp:TextBox CssClass="form-control" runat="server" ValidationGroup="a" ID="BillPostCode"></asp:TextBox>


                            <asp:Label AssociatedControlID="BillCountry" runat="server" Text="Country"></asp:Label>
                            <asp:RequiredFieldValidator runat="server" Id="billCountryValid" ValidationGroup="a" ControlToValidate="BillCountry" Text="fill this out" CssClass="alert-danger"></asp:RequiredFieldValidator>
                            <asp:TextBox runat="server" ValidationGroup="a" CssClass="form-control" ID="BillCountry"></asp:TextBox>

                            <asp:Label AssociatedControlID="BillStreet" runat="server" Text="Street"></asp:Label>
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="a" ID="billStreetValid" ControlToValidate="BillStreet" Text="fill this out" CssClass="alert-danger"></asp:RequiredFieldValidator>
                            <asp:TextBox runat="server" ValidationGroup="a" CssClass="form-control" ID="BillStreet"></asp:TextBox>

                            


                            <asp:Label runat="server" AssosciatedControlID="BillSuburb" Text="Suburb"></asp:Label>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="BillSuburb" ID="billSuburbValid" ValidationGroup="a" Text="fill this out" CssClass="alert-danger"></asp:RequiredFieldValidator>
                            <asp:TextBox runat="server" ValidationGroup="a" CssClass="form-control" ID="BillSuburb"></asp:TextBox>

                            <asp:Label AssociatedControlID="BillNo" runat="server" Text="Unit Number"></asp:Label>
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="a" ControlToValidate="BillNo" ID="billNoValid" Text="fill this out" CssClass="alert-danger"></asp:RequiredFieldValidator>
                            <asp:TextBox runat="server" ValidationGroup="a" CssClass="form-control" ID="BillNo"></asp:TextBox>
                         
               


                            <!--

                                Author: Dipendu Paul (person who answered)
                                Purpose: couldn't figure out how to use eval statements as "Checked" parameters
                                Date accessed: 30/04/2021
                                source: https://stackoverflow.com/questions/19182859/setting-checked-value-for-evalbool/19183266
                            -->
                            
                            <!--#END REFERENCE-->
                            
                            <asp:Button runat="server" ID="updateAddresses" ValidationGroup="a" OnClick="updateAddresses_Click" CssClass="btn btn-primary" Text="Update Addresses" />
                <!--
                <asp:FormView runat="server" ID="addresses">
                    <ItemTemplate>

                    </ItemTemplate>

                </asp:FormView>-->
                </div>

                <div class="col">
                            


                            <!--This data's totally inconsistent with itself (the subtotals don't even match up)-->
                            <!--but this sis how the website will display the user's transaction history-->
                            <h3>Transaciton History</h3>

                            <table class="table table-hover">
                                <thead>
                                    <tr>
                                    <th>Date/Time</th>
                                    <!--date the order was placed-->
                                    <th colspan="2">shippingOption</th>
                                    
                                    </tr>
                                </thead>
                                <tbody>
                                    
                                <!-- well establish an itemsource later-->
                                <asp:Repeater runat="server" ID="OrderList">
                                        <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("datePlaced") %></td>
                                            <td><%#Eval("name") %></td>
                                            
                                            <td><asp:Button runat="server" Text="View" CommandArgument='<%#Eval("OrderID") %>' OnClick="viewObject_Click"/></td>
                                        </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                        
                                </tbody>
                            </table>


                            <asp:Panel runat="server" ID="orderDeets">
                            <h4>Order Details</h4>
                            <table class="table table-bordered">
                                <thead>
                                    <tr>
                                        <th>Item</th>
                                        <th>Quantity</th>
                                        <th>Price</th>
                                    </tr>
                                </thead>
                                
                                <asp:Repeater runat="server" ID="orderitems">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("modelName") %></td>
                                            <td><%#Eval("quantity") %></td>
                                            <td>$<%# Convert.ToDouble(Eval("price")) * Convert.ToDouble(Eval("quantity")) %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                
                            </table>
                            </asp:Panel>
                    </div>
                </div>
                
                
     
        </form>
    
       
    
    
</asp:Content>
