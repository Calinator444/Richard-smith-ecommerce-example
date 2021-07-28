<%@ Page Title="" Language="C#" MasterPageFile="~/AdminSite/AdminMaster.Master" AutoEventWireup="true" CodeBehind="StockLevels.aspx.cs" Inherits="Assgt1.StockLevels" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
        <form runat="server">

            <asp:Panel runat="server" ID="insertHeader" Visible="false">
                <h3><asp:Label runat="server" ID="insertText"></asp:Label></h3>
            </asp:Panel>

            <asp:Label runat="server" ID="insertSuccess" Text="Item successfully added to database" CssClass="alert alert-success" Visible="false"></asp:Label>
            <asp:Label runat="server" ID="insertError" Text="Something went wrong when trying to add the item to our database" CssClass="alert alert-success" Visible="false"></asp:Label>

            <asp:Panel runat="server" Visible="false" ID="addPanel">
                <div class="row">
                <asp:Label runat="server" ID="InsertCustom">
                </asp:Label>
                </div>
                <div class="row">
                    <div class="col">
                        <asp:Label runat="server" AssociatedControlID="txtAddItem" Text="Dropdown label"></asp:Label>
                        <asp:TextBox runat="server" CssClass="form-control" ToolTip="The label which appears on th dropdown menu"  ID="txtAddItem"></asp:TextBox>
                    </div>
                    <div class="col">
                        <asp:Label runat="server" AssociatedControlID="txtAddValue" Text="Dropdown value"></asp:Label>
                        <asp:TextBox runat="server" CssClass="form-control"  ToolTip="The value which will be added to the database" ID="txtAddValue"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col"><asp:Button runat="server" Text="add item" ID="btnAddItem" OnClick="btnAddItem_Click" /></div>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="MainItems">

            <h3>Filters</h3>
            <div class="row">
            <div class="col">
            <asp:Label runat="server" for="drpCategory" Text="Product Category" AssociatedControlID="drpCategory"></asp:Label>
            <asp:DropDownList runat="server" CssClass="form-control" ID="drpCategory" OnSelectedIndexChanged="drpCategory_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Value="Storage">Storage Devices</asp:ListItem>
                <asp:ListItem Value="Processors">Processors</asp:ListItem>
                <asp:ListItem Value="PowerSupplies">Power Supplies</asp:ListItem>
                <asp:ListItem Value="Monitors">Monitors</asp:ListItem>
                <asp:ListItem Value="Mice">Mice</asp:ListItem>
                <asp:ListItem Value="Keyboards">Keyboards</asp:ListItem>
                <asp:ListItem Value="GraphicsCards">Graphics Cards</asp:ListItem>
                <asp:ListItem Value="Laptops">Laptops</asp:ListItem>
                <asp:ListItem Value="Fans">Case fans</asp:ListItem>
                <asp:ListItem Value="Coolers">CPU Coolers</asp:ListItem>
            </asp:DropDownList>
            </div>
            <div class="col">
            <!--set of values common to every device-->
            <asp:Label runat="server" Text="Device" AssociatedControlID="drpDevice">
            </asp:Label>
            <asp:DropDownList runat="server" ID="drpDevice" DataTextField="modelName" DataValueField="productID" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="drpDevice_SelectedIndexChanged">
            </asp:DropDownList>
            </div>
            </div>
                </asp:Panel>
            
        
            
            <h3>Customize attributes</h3>

            <asp:CustomValidator runat="server" ID="addItemaddItemValidation" OnServerValidate="addItemValidation_ServerValidate" ValidationGroup="b"></asp:CustomValidator>
            <!--Panels translate to div elements when the web app compiles-->
            <asp:Panel runat="server" CssClass="alert alert-danger" role="alert" Visible="false" ID="errPanel">
                <asp:Label runat="server" ID="Validatoroutput"></asp:Label>
            </asp:Panel>
            <div class="form-row">
            
            <!--none of the columns have a listed property yet-->
            <!--so right now the page just says that any item yoy select will be listed on the storepage-->

            <div class="col">
            <asp:Checkbox runat="server" Text="Listed" ID="chckListed" Checked="true"/>
            </div>

            <div class="col">
            <asp:Label runat="server" Text="Model Name" AssociatedControlID="alterModelNo"></asp:Label>
            <asp:Textbox runat="server" ID="alterModelNo" CssClass="form-control"></asp:Textbox>

            </div>

            <div class="col">
            <asp:Label runat="server" AssociatedControlID="txtPrice" Text="Price ($)"></asp:Label>
            <asp:TextBox runat="server" CssClass="form-control" OnTextChanged="txtPrice_TextChanged" TextMode="Number" ID="txtPrice" DataTextField="Price" AutoPostBack="true"></asp:TextBox>

            </div>

            </div>

            <asp:Panel runat="server">
            <div class="form-row">
            <div class="col">
            
            <asp:Label runat="server" Text="Manufacturer" AssociatedControlID="drpManufacturer"></asp:Label>
            <asp:DropDownList runat="server" CssClass="form-control" ID="drpManufacturer" DataValueField="manufacturerID" DataTextField="manufacturer"></asp:DropDownList>
            </div>
                <div class="col">
                    <asp:Label runat="server" Text="Description" for="txtDescription"></asp:Label>
                    <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <asp:Label runat="server" Text="Preview Image URL"></asp:Label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtImage"></asp:TextBox>
                </div>

            </div>
            
                    
                
            </asp:Panel>


            
            <!--specific values for a given product category-->
            <!--these will be made visible when the user selects a product category-->

            <!--PROCESSOR ATTRIBUTES-->
            <asp:Panel runat="server" Visible="false" ID="processorAttributes">

            <div class ="form-row">
                <div class="col">
            <asp:Label runat="server" Text="Processor Sku" for="drpcpuSku"></asp:Label>
                <asp:DropDownList runat="server" ID="drpcpuSku" CssClass="form-control" OnSelectedIndexChanged="generic_SelectIndexChanged" DataTextField="sku" DataValueField="sku" AutoPostBack="true">
                </asp:DropDownList>
                <asp:CheckBox runat="server" ID="chckIGPU" Text="Has Ingegrated graphics" />
                    </div>
                <div class="col">


                <asp:Label runat="server" Text="CPU Generation" AssociatedControlID="drpcpuGen"></asp:Label>
                <asp:DropDownList runat="server" ID="drpcpuGen" OnSelectedIndexChanged="generic_SelectIndexChanged" Cssclass="form-control" DataTextField="Generation" DataValueField="Generation" AutoPostBack="true"></asp:DropDownList>
                </div>
                <!--a textbpx which lets you manually change the model number of the product-->
                </div>
            </asp:Panel>

            <!--STORAGE ATTRIBUTES-->
            <asp:Panel runat="server" Visible="false" ID="StorageAttributes">
                
            <div class="form-row">
            <div class="col">
            <asp:Label runat="server" Text="Capacity (in GB)" AssociatedControlID="drpCapacity">
            </asp:Label>
                <asp:DropDownList runat="server" ID="drpCapacity" OnSelectedIndexChanged="generic_SelectIndexChanged" DataValueField="Capacity" DataTextField="Capacity" CssClass="form-control" AutoPostBack="true">
                </asp:DropDownList>
            </div>
            <div class="col">
            <asp:Label runat="server" Text="Form Factor">
            </asp:Label>
            <asp:DropDownList runat="server" ID="drpFormFactor" OnSelectedIndexChanged="generic_SelectIndexChanged" CssClass="form-control" DataValueField="FormFactor" DataTextField="FormFactor" AutoPostBack="true"></asp:DropDownList>
            </div>
            <div class="col">
                <asp:Label runat="server" Text="storage technology" AssosciatedControlID=""></asp:Label>
                <asp:DropDownList runat="server" ToolTip="e.g. Hard Drives, Solid State Drives, NVME SSDS" OnSelectedIndexChanged="generic_SelectIndexChanged" ID="drpTechnology" DataTextField="technology" DataValueField="technology" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
            </div>

            </div>

            </asp:Panel>
            
            <!--POWER SUPPLY ATTRIBUTES-->
            <asp:Panel runat="server" Visible="false" ID="powerAttributes">
             <div class="form-row">
            <div class="col">

                <asp:Label runat="server" Text="Rating" AssociatedControlID="drpRating">
                </asp:Label>
            <asp:DropDownList runat="server" ID="drpRating" OnSelectedIndexChanged="generic_SelectIndexChanged" DataValueField="Rating" DataTextField="Rating" CssClass="form-control" AutoPostBack="true">
                <asp:ListItem Text="bronze" Value="1"></asp:ListItem>
                <asp:ListItem Text="silver" Value="2"></asp:ListItem>
                <asp:ListItem Text="gold" Value="3"></asp:ListItem>
                <asp:ListItem Text="platinum" Value="4"></asp:ListItem>
            </asp:DropDownList>
            </div>
            <div class="col">
            <asp:Label runat="server" Text="Wattage" AssociatedControlID="drpWattage">
            </asp:Label>
            <asp:DropDownList runat="server" CssClass="form-control" ID="drpWattage" OnSelectedIndexChanged="generic_SelectIndexChanged" DataValueField="Wattage" AutoPostBack="true" DataTextField="Wattage"></asp:DropDownList>
            </div>
            </div>
            </asp:Panel>

            <!-- MONITOR ATTRIBUTES-->

            
            <asp:Panel runat="server" Visible="false" ID="monitorAttributes">
            <div class="form-row">
            <div class="col">
            <asp:Label runat="server" Text="Refresh Rate" AssociatedControlID="drpRefresh">
            </asp:Label>
            <asp:DropDownList runat="server" OnSelectedIndexChanged="generic_SelectIndexChanged" AutoPostBack="true" CssClass="form-control" ID="drpRefresh" DataValueField="refreshRate" DataTextField="refreshRate">
            </asp:DropDownList>
            </div>
            <div class="col">
            <asp:Label runat="server" Text="Monitor Size" AssociatedControlID="drpSize">
            </asp:Label>
            <asp:DropDownList runat="server" ID="drpSize" OnSelectIndexChanged="generic_SelectIndexChanged" CssClass="form-control" AutoPostBack="true" DataValueField="size" DataTextField="Size"></asp:DropDownList>
            </div>
            </div>
            <div class="form-row">
            <div class="col">
            <asp:Label runat="server" Text="Resoluton" AssociatedControlID="drpRes">
            </asp:Label>
            <asp:DropDownList runat="server" ID="drpRes" OnSelectedIndexChanged="generic_SelectIndexChanged" CssClass="form-control" DataValueField="resolution" AutoPostBack="true" DataTextField="Resolution"></asp:DropDownList>
            </div></div>
            </asp:Panel>

          
            <!--Connection attribute is shared by both Mouse and Keyboard objects --> 
            <asp:Panel runat="server" ID="connectionControls" Visible="false">
                <asp:Label runat="server" Text="Connection" AssociatedControlID="drpConnection">
                </asp:Label>
                <asp:DropDownList runat="server" ID="drpConnection" ToolTip="Whether or not the device has a wireless/wired connection" OnSelectedIndexChanged="generic_SelectIndexChanged" CssClass="form-control" DataValueField="wireless" DataTextField="wireless" AutoPostBack="true">
                    <asp:ListItem Value="False" Text="wired">

                    </asp:ListItem>
                    <asp:ListItem Value="True" Text="wireless">
                    </asp:ListItem>
                </asp:DropDownList>
            </asp:Panel>

            <!--MOUSE CONTROLS-->
            <asp:Panel runat="server" Visible="false">
                <div class="form-row">
                    <div class="col">
                        <asp:Label runat="server" Text="Optical Sensor">

                        </asp:Label>
                        <asp:DropDownList runat="server" ID="drpOptical" DataTextField="optical" DataValueField="optical" CssClass="form-control">
                            <asp:ListItem Text="True" Value="True">
                            </asp:ListItem>
                            <asp:ListItem Text="False" Value="False">
                            </asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </asp:Panel>

            <!--KEYBOARD CONTROLS-->
            <asp:Panel runat="server" ID="KeyboardsAttributes" Visible="false">

                <div class="form-row">
                <div class="col">
                <asp:Label runat="server" Text="KeyCaps" AssociatedControlID="drpKeyCaps">
                </asp:Label>
                <asp:DropDownList runat="server" ID="drpKeyCaps" CssClass="form-control" DataValueField="keyCaps" DataTextField="keyCaps" OnSelectedIndexChanged="generic_SelectIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                </div>
                </div>
            </asp:Panel>

            <!--GRAPHICS CARD CONTROLS-->
            <asp:Panel runat="server" ID="graphicsCardAttributes" Visible="false">

                <div class="form-row">
                <div class="col">
                <asp:Label runat="server" Text="Chip Manufacturer" AssociatedControlID="drpChipManufacturer">
                </asp:Label>
                <asp:DropDownList runat="server" OnSelectedIndexChanged="generic_SelectIndexChanged" ID="drpChipManufacturer" CssClass="form-control" DataValueField="chipManufacturerID" DataTextField="chipManufacturer" AutoPostBack="true">
                </asp:DropDownList>
                </div>
                <div class="col">
                <asp:Label runat="server" Text="VRAM" AssociatedControlID="drpVram">
                </asp:Label>
                <asp:DropDownList runat="server" ID="drpVram" OnSelectedIndexChanged="generic_SelectIndexChanged" CssClass="form-control" DataValueField="vram" DataTextField="VRAM" AutoPostBack="true">
                </asp:DropDownList>
                </div>
                </div>
            </asp:Panel>
            
            <!--Dimensions for fans/cpu coolers-->
            <asp:Panel runat="server" ID="dimensionControls" Visible="false">
                <div class="form-row">

                <div class="col">
                <asp:Label runat="server" Text="Dimensions" AssociatedControlID="drpDimensions">
                </asp:Label>
                <asp:DropDownList runat="server" ID="drpDimensions" OnSelectedIndexChanged="generic_SelectIndexChanged" DataTextField="Dimensions" DataValueField="dimensions" CssClass="form-control" AutoPostBack="true">
                </asp:DropDownList>
                    </div>
                </div>
            </asp:Panel>

            <!--cooler attributes-->

            <asp:Panel runat="server" ID="CoolersAttributes" Visible="false">

                <div class="form-row">
                    <div class="col">
                        <asp:Label runat="server" Text="Cooler type" AssociatedControlID="drpDimensions">
                        </asp:Label>
                        <asp:DropDownList runat="server" OnSelectIndexChanged="generic_SelectIndexChanged" ID="drpCoolType" CssClass="form-control" DataValueField="type" DataTextField="type" AutoPostBack="true">
                            <asp:ListItem Value="0" Text="Air"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Water"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </asp:Panel>


            <!--PC SPECS-->
            <!--These controls are shared by both laptops and desktops-->
            <asp:Panel runat="server" ID="specAttributes" Visible="false">
                <div class="form-row">
                    <div class="col">
                        <asp:Label runat="server" Text="RAM" AssociatedControlID="drpRam">
                        </asp:Label>
                        <asp:DropDownList runat="server" ID="drpRam" OnSelectedIndexChanged="generic_SelectIndexChanged" CssClass="form-control" DataValueField="ram" DataTextField="Ram" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col">
                        <asp:Label runat="server" Text="Graphics Card" AssociatedControlID="drpGraphics">
                        </asp:Label>
                        <asp:DropDownList runat="server" CssClass="form-control" ToolTip="To add a Graphics card to the dropdown menu add a graphics card and set it to unlisted" ID="drpGraphics"  OnSelectedIndexChanged="generic_SelectIndexChanged" DataValueField="graphicsCardID" DataTextField="GraphicsCard" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                <div class="col">
                <asp:Label runat="server" Text="Processor" AssociatedControlID="drpProcessor">
                </asp:Label>
                <asp:DropDownList runat="server" ID="drpProcessor" OnSelectedIndexChanged="generic_SelectIndexChanged" ToolTip="To add a Processor to the dropdown menu add a graphics card and set it to unlisted" CssClass="form-control" DataValueField="processorID" DataTextField="Processor" AutoPostBack="true">
                </asp:DropDownList>

                </div>
                </div>
            </asp:Panel>

            <asp:Button runat="server" Text="Confirm Changes" OnClick="changeConfirm_Click" ID="changeConfirm" CssClass="btn btn-primary" />
            <asp:Button runat="server" ID="revertChange" OnClick="drpDevice_SelectedIndexChanged" Text="Revert changes" CssClass="btn btn-secondary" />
            <asp:Button runat="server" ID="addItem" Text="Add new item" OnClick="addItem_Click" CausesValidation="false" CssClass="btn btn-secondary" />
            <asp:Button runat="server" ID="submitProduct" Visible="false" Text="Submit" OnClick="submitProduct_Click" CausesValidation="true" ValidationGroup="b" CssClass="btn btn-primary" />
            <asp:Button runat="server" ID="cancelProduct" Visible="false" Text="Cancel" OnClick="cancelProduct_Click" CssClass="btn btn-secondary" />
            

        
        </form>
  
    


</asp:Content>
