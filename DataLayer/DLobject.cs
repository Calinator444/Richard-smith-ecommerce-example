using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
//using Assgt1.
using DataLayer.DataModels;

namespace DataLayer
{
    public class DLobject
    {

        SqlConnection con = new SqlConnection();
        public DLobject()
        {
            //con.Open();
        }


        private string connectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
        }


        public int addAccount(string userName, string password)
        {
            SqlConnection con = new SqlConnection(connectionString);
            

            //by default account is inactive and has no admin priveleges
            //The user can activate their account by accessing the link emailed to them
            SqlCommand cmd = new SqlCommand("INSERT INTO Accounts " +
                "OUTPUT Inserted.accountID " +
                "VALUES(@password, @emailAddress,0,0)", con);
            con.Open();
            SqlParameter paramUser = new SqlParameter();
            paramUser.Value = userName;
            paramUser.ParameterName = "@emailAddress";

            cmd.Parameters.Add(paramUser);

            SqlParameter paramPass = new SqlParameter();
            paramPass.Value = password;
            paramPass.ParameterName = "@password";

            cmd.Parameters.Add(paramPass);
            try
            {
                return (int) cmd.ExecuteScalar();
            }
            catch
            {
                Debug.WriteLine("Failed to add account to database");
            }
            finally
            {
                con.Close();
            }
            //indicates an error
            return -1;


        }

        //returns all rows of a given table as a binable object
        public DataTable getAllBindable(string tableName)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM " + tableName, con);
            return convertToDataTable(cmd);
        }

        //a reusable function that returns a bindable object given the tablename, parameter, and parameter value
        public DataTable getBindableGeneric(string tableName, string paramName, string param)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM " + tableName + " WHERE " + paramName + " = '" + param + "'", con);
            return convertToDataTable(cmd);
        }


        //this one throws an error


        public DataTable getManufacturers()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT name AS manufacturer, manufacturerID FROM Manufacturers", con);
            return convertToDataTable(cmd);
        }



        //special commands for populating the admin form
        public DataTable getLaptopGraphics()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT p.productID AS graphicsCardID, modelName AS GraphicsCard FROM Products AS p " +
                                            "INNER JOIN GraphicsCards AS g ON p.productID = g.productID " +
                                            "WHERE listed = 0", con);
            Debug.WriteLine("laptopGraphics called");
            return convertToDataTable(cmd);
        }

        public DataTable getLaptopProcessor()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT p.modelName AS processor, p.productID AS processorID FROM Products AS p " +
                                            "INNER JOIN Processors AS pc ON p.productID = pc.productID " +
                                            "WHERE Listed = 0", con);

            return convertToDataTable(cmd);
        }
        //returns a product class
        public Product getProduct(int ID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * FROM Products WHERE productID = @productID", con);

            SqlParameter param = new SqlParameter();
            param.Value = ID;
            param.ParameterName = "@productID";

            cmd.Parameters.Add(param);
            SqlDataReader sd = cmd.ExecuteReader();
            if (sd.Read())
            {
                Product p = new Product();
                p.description = (string)sd["description"];
                p.productId = (int)sd["productID"];
                p.previewImage = (string)sd["previewImage"];
                p.modelName = (string)sd["modelName"];

                //Y DO U BE LIEK DIS

                decimal geddit = (decimal)sd["price"];
                p.price = Convert.ToDouble(geddit);
                p.productId = (int)sd["productID"];
                return p;
            }
            return null;




        }




        public DataTable getProductsBindable(string productCategory)
        {

            //string andParams = "";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string sqlString = "";
            switch (productCategory)
            {
                //y do I gotta do this microsoft :'(
                case "Laptops":
                    sqlString = "SELECT p.modelName, pg.productID AS graphicsCardID, p.listed, p.productID, p.manufacturerID, ram, p.previewImage, m.name AS manufacturer, pp.modelName AS processor, pp.productID AS processorID, p.description, p.price, pg.modelName AS graphicsCard " +
                                "FROM Computers AS c " +
                                "INNER JOIN Products AS p " +
                                "ON(p.productID = c.productID) " +
                                "INNER JOIN Processors AS pr " +
                                "ON(pr.productID = processor) " +
                                "INNER JOIN GraphicsCards AS g " +
                                "ON(g.productID = graphicsCard) " +
                                "INNER JOIN Products as pg " +
                                "ON pg.productID = g.productID " +
                                "INNER JOIN Manufacturers as m " +
                                "ON m.manufacturerID = p.manufacturerID " +
                                "INNER JOIN Products as pp " +
                                "ON pp.productID = pr.productID";
                    break;
                case "Branded":

                    break;
                case "Mice":
                    sqlString = "SELECT p.productID, modelName, p.manufacturerID ,description, listed, price, previewImage, m.name AS manufacturer, wireless, optical FROM Products AS p " +
                            "INNER JOIN Manufacturers AS m " +
                            "ON(m.manufacturerID = p.manufacturerID) " +
                            "INNER JOIN Peripherals AS pe " +
                            "ON(pe.productID = p.productID) " +
                            "INNER JOIN Mice AS mi " +
                            "ON(mi.productID = p.productID) ";
                    break;
                case "Keyboards":
                    sqlString = "SELECT p.productID, modelName, description, p.manufacturerID , listed, price, previewImage, wireless, keyCaps, m.name AS manufacturer FROM Products As p " +
                                    "INNER JOIN Manufacturers AS m " +
                                    "ON(m.manufacturerID = p.manufacturerID) " +
                                    "INNER JOIN Peripherals AS pe " +
                                    "ON(pe.productID = p.productID) " +
                                    "INNER JOIN Keyboards AS k " +
                                    "ON(k.productID = pe.productID)";
                    break;
                case "Monitors":
                    sqlString = "SELECT p.productID, modelName, description, p.manufacturerID , listed,  price, previewImage, resolution, refreshRate, size, ma.name AS manufacturer FROM Products as p " +
                                "INNER JOIN Monitors AS m " +
                                "ON(m.productID = p.productID) " +
                                "INNER JOIN Manufacturers AS ma " +
                                "ON(ma.manufacturerID = p.manufacturerID)";
                    break;
                case "GraphicsCards":
                    sqlString = "SELECT p.productID, listed, modelName, description, p.manufacturerID, price, previewImage, vram, c.name AS chipManufacturer,c.chipManufacturerID,  m.name AS manufacturer FROM Products AS p " +
                                "INNER JOIN Manufacturers AS m " +
                                "ON m.manufacturerId = p.manufacturerID " +
                                "INNER JOIN GraphicsCards AS g " +
                                "ON g.productID = p.productID " +
                                "INNER JOIN ChipManufacturers AS c " +
                                "ON chipManufacturer = c.chipManufacturerID ";

                    break;
                case "Processors":
                    sqlString = "SELECT p.productID, modelName, description, price, p.manufacturerID, listed, previewImage, sku, iGPU, generation, m.name AS manufacturer FROM Products AS p " +
                                    "INNER JOIN Manufacturers AS m " +
                                    "ON m.manufacturerID = p.manufacturerID " +
                                    "INNER JOIN Processors as pr " +
                                    "ON pr.productID = p.productID ";
                    break;
                case "PowerSupplies":
                    sqlString = "SELECT p.productID, modelName, p.manufacturerID, listed, description, rating, wattage,price, previewImage, m.name AS manufacturer FROM Products AS p " +
                                    "INNER JOIN Manufacturers AS m " +
                                    "ON m.manufacturerID = p.manufacturerID " +
                                    "INNER JOIN PowerSupplies AS pp " +
                                    "ON pp.productId = p.productID";
                    break;
                case "Storage":
                    sqlString = "SELECT p.productID, modelName, listed, description, p.manufacturerID, capacity, FormFactor, technology, price, previewImage, m.name AS manufacturer FROM Products AS p " +
                                "INNER JOIN Manufacturers AS m " +
                                "ON m.manufacturerID = p.manufacturerID " +
                                "INNER JOIN Storage AS s " +
                                "ON s.productId = p.productID";
                    break;
                case "Fans":
                    sqlString = "SELECT p.productID, modelName, description, listed, p.manufacturerID,  price, previewImage, m.name AS manufacturer, type, dimensions FROM Products AS p " +
                                "INNER JOIN Manufacturers AS m " +
                                "ON m.manufacturerID = p.manufacturerID " +
                                "INNER JOIN Coolers AS c " +
                                "ON c.productID = p.productID " +
                                "WHERE type = 2";
                    break;
                case "Coolers":
                    sqlString = "SELECT modelName, p.productID ,dimensions, p.manufacturerID , listed, description, dimensions price, previewImage, Type, m.name AS manufacturer FROM Products as p " +
                        "INNER JOIN Coolers as c " +
                        "ON(c.productID = p.productID) " +
                        "INNER JOIN Manufacturers AS m " +
                        "ON(m.manufacturerID = p.manufacturerID)";
                    break;
                case "Motherboards":
                    sqlString = "SELECT p.productID, modelName, description,p.manufacturerID , listed, m.name AS manufacturer, wifi, formFactor, chipset, price, previewImage " +
                        "FROM Products AS p " +
                        "INNER JOIN Manufacturers AS m " +
                        "ON(M.manufacturerID = p.manufacturerID) " +
                        "INNER JOIN Motherboards AS mo " +
                        "ON(mo.productID = p.productID)";
                    break;
            }
            SqlCommand cmd = new SqlCommand(sqlString, con);

            return convertToDataTable(cmd);
        }


        public Account getAccountObject(int ID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Accounts WHERE accountID = @accountID", con);
            SqlParameter param = new SqlParameter();
            param.Value = ID;
            param.ParameterName = "@accountID";
            cmd.Parameters.Add(param);
            SqlDataReader sd = cmd.ExecuteReader();
            if (sd.Read())
            {
                Account acc = new Account((int)sd["accountID"], (string)sd["password"], (string)sd["emailAddress"], (bool)sd["active"], (bool)sd["adminPriveleges"]);
                return acc;
            }
            else
                return null;
        }


        //takes an sql command (parameters need to be set up first) and converts the output of the query to a datatable
        //this is useful because Form controls can be databound to datatables
        public DataTable convertToDataTable(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            Debug.WriteLine(cmd.CommandText);
            //SqlDataReader sd = cmd.ExecuteReader();
            int passes = 0;
            string outString = "";
            SqlDataReader sd = cmd.ExecuteReader();
            while (sd.Read())
            {

                if (passes == 0)
                {
                    //creates the columns
                    for (int i = 0; i < sd.FieldCount; i++)
                    {
                        outString += sd.GetName(i) + ", ";
                        dt.Columns.Add(sd.GetName(i));
                    }
                    passes++;
                    outString += "\r\n";
                }
                DataRow currentRow = dt.NewRow();

                for (int i = 0; i < sd.FieldCount; i++)
                {
                    outString += Convert.ToString(sd[i]) + ", ";
                    currentRow[i] = sd[i];
                }
                dt.Rows.Add(currentRow);
                outString += "\r\n";
            }
            Debug.WriteLine(outString);
            return dt;
        }
        
        //returns a bindable data object
        public DataTable getSingleProduct(string productID)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT description, price, previewImage, m.name AS manufacturer, modelName FROM Products AS p " +
                "INNER JOIN Manufacturers AS m ON m.manufacturerID = p.manufacturerID " +
                "WHERE productID = @productID ", con);
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@productID";
            param.Value = productID;
            cmd.Parameters.Add(param);
            //SqlDataReader sd = cmd.ExecuteReader();
            return convertToDataTable(cmd);

        }

        //it's necessary to call this function whenever the page loads (rather than just storing it as a session variable) because a user's privileges may be revoked while their logged in
        public bool isAdmin(int accountID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * from Accounts WHERE accountID = @accountID", con);
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@accountID";
            param.Value = accountID;
            cmd.Parameters.Add(param);
            try
            {
                SqlDataReader sd = cmd.ExecuteReader();
                if (sd.Read())
                    return (bool)sd["adminPriveleges"];
                //all paths must return a value
                //the if statement should always be true since this function isn't called unless the page validates but the compiler doesn't know that
                else
                    return false;

            }
            catch
            {
                Debug.WriteLine("The database threw a hissie");
                return false;
            }
            finally
            {
                con.Close();
            }

        }


        public List<Address> getAddressByUser(int usrID)
        {

            List<Address> addresses = new List<Address>();
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT a.addressID, a.postCode, a.country, a.street, a.suburb, a.unit, a.billingAddress"
                + " FROM UserAddress AS ua"
                + " INNER JOIN Addresses as a"
                + " ON(ua.addressID = a.addressID)"
                + " WHERE accountID = @accountID", con);
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@accountID";
            param.Value = usrID;
            cmd.Parameters.Add(param);
            SqlDataReader sd = cmd.ExecuteReader();
            while (sd.Read())
            {
                Address tempAddress = new Address();
                IDataRecord record = sd;
                tempAddress.addressID = (int)record["addressID"];
                tempAddress.postCode = (int)record["postCode"];
                tempAddress.country = (string)record["country"];
                tempAddress.street = (string)record["street"];
                tempAddress.suburb = (string)record["suburb"];
                tempAddress.billingAddress = (bool)record["billingAddress"];
                tempAddress.unit = (int)record["unit"];
                addresses.Add(tempAddress);
            }
            //if there are no results the count of addresses will be zero
            return addresses;


        }



        //if the billing address is the same as the shipping address just pass the 
        public void addAddresses(Address billingAddress, Address shippingAddress, int userID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string commandString = "INSERT INTO Addresses (postCode,country,street,suburb,unit,billingAddress) " +
                                    "OUTPUT Inserted.addressID " +
                                    "VALUES (@postCode, @country, @street, @suburb, @unit, @billingAddress)";
            SqlCommand cmd = new SqlCommand(commandString, con);

            //since a user can have 1 to many addresses we need a third table which links each address to a user

            SqlCommand cmd2 = new SqlCommand("INSERT INTO UserAddress VALUES (@addressID, @accountID)", con);

            SqlParameter paramAccountID = new SqlParameter();
            paramAccountID.Value = userID;
            paramAccountID.ParameterName = "@accountID";
            cmd2.Parameters.Add(paramAccountID);

            SqlParameter paramAddressId = new SqlParameter();
            paramAddressId.ParameterName = "@addressID";
            cmd2.Parameters.Add(paramAddressId);



            //setting up the parameters for the billing address;
            SqlParameter paramPost = new SqlParameter();
            paramPost.ParameterName = "@postCode";
            paramPost.Value = billingAddress.postCode;
            cmd.Parameters.Add(paramPost);

            SqlParameter paramCountry = new SqlParameter();
            paramCountry.ParameterName = "@country";
            paramCountry.Value = billingAddress.country;
            cmd.Parameters.Add(paramCountry);

            SqlParameter paramStreet = new SqlParameter();
            paramStreet.ParameterName = "@street";
            paramStreet.Value = billingAddress.street;
            cmd.Parameters.Add(paramStreet);

            SqlParameter paramSuburb = new SqlParameter();
            paramSuburb.ParameterName = "@suburb";
            paramSuburb.Value = billingAddress.suburb;
            cmd.Parameters.Add(paramSuburb);

            SqlParameter paramUnit = new SqlParameter();
            paramUnit.ParameterName = "@unit";
            paramUnit.Value = billingAddress.unit;
            cmd.Parameters.Add(paramUnit);

            SqlParameter paramBillingAddress = new SqlParameter();
            paramBillingAddress.ParameterName = "@billingAddress";
            paramBillingAddress.Value = Convert.ToString(billingAddress.billingAddress);
            cmd.Parameters.Add(paramBillingAddress);

            //           try
            //      {
            int AddressId;

            //gets the ID of the new row we inserted (as per the output we specified)
            AddressId = (int)cmd.ExecuteScalar();
            //int success = cmd.ExecuteNonQuery();

            paramAddressId.Value = AddressId;
            cmd2.ExecuteNonQuery();
            //now for the shipping address

            //were reusing the same parameters here with different values
            paramPost.Value = shippingAddress.postCode;
            paramCountry.Value = shippingAddress.country;
            paramStreet.Value = shippingAddress.street;
            paramSuburb.Value = shippingAddress.suburb;
            paramUnit.Value = shippingAddress.unit;
            paramBillingAddress.Value = Convert.ToString(shippingAddress.billingAddress);



            AddressId = (int)cmd.ExecuteScalar();
            paramAddressId.Value = AddressId;
            cmd2.ExecuteNonQuery();

            //      }
            //      catch
            //      {
            //          Debug.WriteLine("Something went wrong when we tried to add the user's account");
            //      }
            //      finally
            //      {
            //          con.Close();
            //       }
        }

        //an overload to be used when the billing and shipping address are the same
        public void addAddresses(Address shippingAddress, int userID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Addresses " +
                                            "OUTPUT Inserted.addressID " +
                                            "VALUES (@postCode, @country, @street, @suburb, @unit, @billingAddress)", con);

            SqlCommand cmd2 = new SqlCommand("INSERT INTO UserAddress VALUES (@addressId, @accountID)", con);
            SqlParameter paramAddress = new SqlParameter();

            //we can't assign address ID yet becausee we get that when we execute the first query
            paramAddress.ParameterName = "@addressId";
            cmd2.Parameters.Add(paramAddress);

            SqlParameter paramAccount = new SqlParameter();
            paramAccount.Value = userID;
            paramAccount.ParameterName = "@accountID";
            cmd2.Parameters.Add(paramAccount);


            SqlParameter paramCode = new SqlParameter();
            paramCode.ParameterName = "@postCode";
            paramCode.Value = shippingAddress.postCode;
            cmd.Parameters.Add(paramCode);

            SqlParameter paramCountry = new SqlParameter();
            paramCountry.ParameterName = "@country";
            paramCountry.Value = shippingAddress.country;
            cmd.Parameters.Add(paramCountry);

            SqlParameter paramStreet = new SqlParameter();
            paramStreet.ParameterName = "@street";
            paramStreet.Value = shippingAddress.street;
            cmd.Parameters.Add(paramStreet);

            SqlParameter paramSuburb = new SqlParameter();
            paramSuburb.ParameterName = "@suburb";
            paramSuburb.Value = shippingAddress.suburb;
            cmd.Parameters.Add(paramSuburb);

            SqlParameter paramUnit = new SqlParameter();
            paramUnit.ParameterName = "@unit";
            paramUnit.Value = shippingAddress.unit;
            cmd.Parameters.Add(paramUnit);

            SqlParameter paramBillingAddress = new SqlParameter();
            paramBillingAddress.ParameterName = "@billingAddress";
            paramBillingAddress.Value = 0;
            cmd.Parameters.Add(paramBillingAddress);

            int AddressID = (int)cmd.ExecuteScalar();
            paramAddress.Value = AddressID;
            cmd2.ExecuteNonQuery();

            //now we insert the same set of values but this time as a billing address

            paramBillingAddress.Value = 1;
            AddressID = (int)cmd.ExecuteScalar();

            //and update the multiplicity table between account and address
            paramAddress.Value = AddressID;
            cmd2.ExecuteNonQuery();

        }

        public int getOrderShipping(int orderID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Orders WHERE orderID = @orderID", con);
            cmd.Parameters.AddWithValue("@orderID", orderID);
            SqlDataReader sd = cmd.ExecuteReader();
            if (sd.Read())
                return (int) sd["shippingID"];
            return -1;

        }
        public DataTable getOrderItemsBindable(int orderID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT modelName, price, quantity FROM orderItems AS o " +
                                            "INNER JOIN Products AS p ON p.productID = itemID " +
                                            "WHERE orderID = @orderID", con);

            cmd.Parameters.AddWithValue("@orderID", orderID);

            return convertToDataTable(cmd);
            
                
        }

        
        public List<OrderItem> getOrderItems(int orderID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT price, quantity, modelName FROM OrderItems AS o " +
                                            "INNER JOIN products AS p ON p.productID = o.itemID " +
                                            "WHERE orderID= @orderID", con);
            cmd.Parameters.AddWithValue("@orderID", orderID);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            List<OrderItem> orderitems = new List<OrderItem>();
            while(dr.Read())
            {
                OrderItem oi = new OrderItem();
                //oi.productId = dr["itemID"];
                decimal dprice = (decimal) dr["price"];
                oi.price = Convert.ToDouble(dprice);
                oi.quantity = (int)dr["quantity"];
                oi.modelName = (string)dr["modelName"];
                orderitems.Add(oi);
            }
            return orderitems;
        }

        public DataTable getUserOrders(int accountID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT datePlaced, o.orderID, s.name FROM Orders AS o "+
                                            "INNER JOIN ShippingOptions AS s ON s.shippingID = o.shippingID "+
                                            "WHERE o.accountID = @accountID", con);
            cmd.Parameters.AddWithValue("@accountID", accountID);
            return convertToDataTable(cmd);
        }

        public void addOrderItems(List<Item> items, int orderId)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO OrderItems VALUES (@orderID, @itemID, @quantity)", con);
            cmd.Parameters.AddWithValue("@orderID", orderId);

            SqlParameter paramQuantity = new SqlParameter();
            paramQuantity.ParameterName = "@quantity";

            SqlParameter paramID = new SqlParameter();
            paramID.ParameterName = "@itemID";

            cmd.Parameters.Add(paramQuantity);
            cmd.Parameters.Add(paramID);
            con.Open();

            foreach (Item item in items)
            {
                paramQuantity.Value = item.getQuantity();
                paramID.Value = item.getID();
                cmd.ExecuteNonQuery();
            }

        }
        

        public int placeOrder(int shippingID, int accountID, double subtotal)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO Orders(shippingID, accountID, total) OUTPUT Inserted.orderID "+
                                            "VALUES(@shippingId, @accountID, @total)", con);
            cmd.Parameters.AddWithValue("@shippingID", shippingID);
            cmd.Parameters.AddWithValue("@accountID", accountID);
            cmd.Parameters.AddWithValue("@total", subtotal);
            con.Open();

            return (int) cmd.ExecuteScalar();

        }
        public int placeOrder(int shippingID, double subtotal)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO Orders(shippingID, accountID, total) OUTPUT Inserted.orderID " +
                                            "VALUES(@shippingId, null, @total)", con);
            cmd.Parameters.AddWithValue("@shippingID", shippingID);
            cmd.Parameters.AddWithValue("@total", subtotal);
            //cmd.Parameters.AddWithValue("@accountID", null);
            con.Open();
            return (int)cmd.ExecuteScalar();
        }


        //returns rows affected, if zero the password does not exist
        public int tempPassword(string emailAddress, string passWord)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Accounts SET password = @password WHERE emailAddress = @emailAddress", con);


            cmd.Parameters.AddWithValue("@emailAddress", emailAddress);
            cmd.Parameters.AddWithValue("@password", passWord);
            try
            {
                return cmd.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                con.Close();
            }
            return 0;


        }
        

        //checks whether an email has been registered, regardless of whether the account is active or not
        public bool checkAnyUserExists(string emailAddress)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Accounts WHERE emailAddress = @emailAddress", con);
            cmd.Parameters.AddWithValue("@emailAddress", emailAddress);
            con.Open();
            
            SqlDataReader sd = cmd.ExecuteReader();
            if (sd.Read())
                return true;
            return false;
            

        }

        
        //checks for an existing user, only valid if the user is active
        public int UserExists(string userName, string passWord)
        {
            Debug.WriteLine(userName + passWord);
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            //if the account is deactivated the program will treat the account like it doesn't exist
            SqlCommand cmd = new SqlCommand("SELECT * FROM Accounts WHERE emailAddress = @emailAddress AND active = 1", con);
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@emailAddress";
            param.Value = userName;
            cmd.Parameters.Add(param);

            try
            {
                SqlDataReader sd = cmd.ExecuteReader();
                //sd.read is only true if a record is found
                while (sd.Read())
                {
                    //IDataRecord getRecord = sd;
                    //metadata can only be accessed while the reader is active, so we can't read the email address after executing the datareader
                    if ((string)sd["emailAddress"] == userName && (string) sd["password"] == passWord)
                    {
                        //con.Close();
                        return (int)sd["accountID"];
                        //con.Close();
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            finally
            {
                con.Close();
            }
            return 0;

        }

        public int addShippingOption(int days, double cost, string name, string description)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO ShippingOptions VALUES (@days, @cost, @name, @description)", con);

            cmd.Parameters.AddWithValue("@days", days);
            cmd.Parameters.AddWithValue("@cost", cost);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@description", description);
            //try
            //{
                return cmd.ExecuteNonQuery();
            //}
            //catch
            //{
            //    Debug.WriteLine("Couldn't add shipping option " + name);
            //    return -1;
            //}
            //finally
            //{
            //    con.Close();

            //}


        }


        public DataTable getShippingOptions()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * FROM shippingOptions",con);
            return convertToDataTable(cmd);
        }
        public void updateAddresses(int shippingID, Address shippingAddress, int billingID, Address billingAddress)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Addresses " +
                                            "SET postCode = @postCode, country = @country, street = @street, suburb = @suburb, unit = @unit " +
                                            "WHERE addressID = @addressID AND billingAddress = @billingAddress", con);

            //set up the details for the shipping address
            //cmd.Parameters.AddWithValue("@postCode", shippingAddress.postCode);
            SqlParameter paramPost = new SqlParameter();
            paramPost.ParameterName = "@postCode";
            paramPost.Value = shippingAddress.postCode;

            cmd.Parameters.Add(paramPost);

            SqlParameter paramCountry = new SqlParameter();
            paramCountry.ParameterName = "@country";
            paramCountry.Value = shippingAddress.country;

            cmd.Parameters.Add(paramCountry);

            SqlParameter paramStreet = new SqlParameter();
            paramStreet.ParameterName = "@street";
            paramStreet.Value = shippingAddress.street;

            cmd.Parameters.Add(paramStreet);

            SqlParameter paramSuburb = new SqlParameter();
            paramSuburb.ParameterName = "@suburb";
            paramSuburb.Value = shippingAddress.suburb;

            cmd.Parameters.Add(paramSuburb);

            SqlParameter paramUnit = new SqlParameter();
            paramUnit.ParameterName = "@unit";
            paramUnit.Value = shippingAddress.unit;

            cmd.Parameters.Add(paramUnit);

            SqlParameter addressID = new SqlParameter();
            addressID.ParameterName = "@addressID";
            addressID.Value = shippingID;

            cmd.Parameters.Add(addressID);

            SqlParameter paramIsBilling = new SqlParameter();
            paramIsBilling.ParameterName = "@billingAddress";
            paramIsBilling.Value = 0;

            cmd.Parameters.Add(paramIsBilling);

            cmd.ExecuteNonQuery();

            //reusing the same sql parameters to make a second addition to the database

            paramPost.Value = billingAddress.postCode;
            paramCountry.Value = billingAddress.country;
            paramStreet.Value = billingAddress.street;
            paramSuburb.Value = billingAddress.suburb;
            paramUnit.Value = billingAddress.unit;
            addressID.Value = billingID;
            paramIsBilling.Value = 1;

            cmd.ExecuteNonQuery();
        }


        public ShippingOptions getShippingOption(int ID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM ShippingOptions WHERE shippingID = @shippingID",con);
            con.Open();
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@shippingID";
            param.Value = ID;

            cmd.Parameters.Add(param);
            try
            {
                SqlDataReader DR = cmd.ExecuteReader();
                if (DR.Read())
                {
                    ShippingOptions s = new ShippingOptions();

                    decimal dcost = (decimal)DR["cost"];
                    s.cost = Convert.ToDouble(dcost);
                    s.days = (int)DR["days"];
                    s.description = (string)DR["description"];
                    s.name = (string)DR["name"];
                    return s;

                }
            }
            catch
            {

            }
            finally
            {
                con.Close();
            }
            return null;

        }


        //Adding products to database
        private int addProduct(int listed, string modelName, string description, double price, string previewImage, int manufacturerID/*can be null*/)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Products " +
                                            "OUTPUT Inserted.productID " +
                                            "VALUES (@listed, @modelName , @description, @price, @previewImage, @manufacturerID)", con);

            cmd.Parameters.AddWithValue("@listed", listed);

            cmd.Parameters.AddWithValue("@modelName", modelName);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@previewImage", previewImage);
            cmd.Parameters.AddWithValue("@manufacturerID", manufacturerID);

            try
            {
                return (int)cmd.ExecuteScalar();
            }
            catch
            {
                Debug.WriteLine("Failed to add item to the database\r\nperhaps one or more variables was not declared");
            }
            finally
            {
                con.Close();
            }
            return -1;

        }


        public void addGraphicsCard(int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int VRAM, int chipManufacturerID)
        {
            int productID = addProduct(listed, modelName, description, price, previewImage, manufacturerID);
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO GraphicsCards VALUES(@productID,@VRAM, chipManufacturerID)",con);

            cmd.Parameters.AddWithValue("@productID", productID);
            cmd.Parameters.AddWithValue("@VRAM",VRAM);
            cmd.Parameters.AddWithValue("@chipManufacturerID",chipManufacturerID);
            try
            {

                cmd.ExecuteScalar();
            }
            catch
            {

            }
            finally
            {
                con.Close();
            }
        }
        public void addProcessor(int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int generation, int iGPU, int sku)
        {
            int productID = addProduct(listed, modelName, description, price, previewImage, manufacturerID);
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Processors VALUES(@generation,@iGPU, @sku)", con);

            cmd.Parameters.AddWithValue("@generation", generation);
            cmd.Parameters.AddWithValue("@iGPU", iGPU);
            cmd.Parameters.AddWithValue("@sku", sku);
            try
            {

                cmd.ExecuteScalar();
            }
            catch
            {

            }
            finally
            {
                con.Close();
            }
        }

        public void addKeyboard(int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int wireless, string keyCaps)
        {
            int productID = addProduct(listed, modelName, description, price, previewImage, manufacturerID);
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO Peripherals VALUES(@productID,@wireless) " +
                                            "INSERT INTO Keyboards VALUES(@productID, @keyCaps)", con);
            cmd.Parameters.AddWithValue("@wireless", wireless);
            cmd.Parameters.AddWithValue("@keyCaps", keyCaps);
            try
            {
                cmd.ExecuteScalar();
            }
            catch
            {

            }
            finally
            {
                con.Close();
            }
        }

    public void addMouse(int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int wireless, int optical)
    {
        int productID = addProduct(listed, modelName, description, price, previewImage, manufacturerID);
        SqlConnection con = new SqlConnection(connectionString);
        con.Open();

        SqlCommand cmd = new SqlCommand("INSERT INTO Peripherals VALUES(@productID,@wireless) " +
                                        "INSERT INTO Mice VALUES(@productID, @optical)", con);
        cmd.Parameters.AddWithValue("@wireless", wireless);
        cmd.Parameters.AddWithValue("@optical", optical);
        cmd.Parameters.AddWithValue("@productID", productID);

        //    try
        //{
            cmd.ExecuteScalar();
            //    }
            //    catch
            //    {

            //    }
            //    finally
            //    {
            //        con.Close();
            //    }
        }
        public void addStorage(int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int capacity, double formFactor, string technology)
        {
            int productID = addProduct(listed, modelName, description, price, previewImage, manufacturerID);
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Storage VALUES(@capacity,@formFactor,@productID,@formFactor, @technology", con);

            cmd.Parameters.AddWithValue("@capacity", capacity);
            cmd.Parameters.AddWithValue("@formFactor", formFactor);
            cmd.Parameters.AddWithValue("@productID", productID);
            cmd.Parameters.AddWithValue("@formFactor", productID);
            cmd.Parameters.AddWithValue("@technology", productID);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                con.Close();
            }
        }
        public void addPowerSupply(int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int rating, int wattage )
        {
            int productID = addProduct(listed, modelName, description, price, previewImage, manufacturerID);
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO PowerSupplies VALUES(@productID,@rating,@wattage)", con);
            cmd.Parameters.AddWithValue("@productID", productID);
            cmd.Parameters.AddWithValue("@rating", rating);
            cmd.Parameters.AddWithValue("@wattage", wattage);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Database insert broke");
            }
            finally
            {
                con.Close();
            }
        }
        public void addCooler(int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int type, int dimensions)
        {
            int productID = addProduct(listed, modelName, description, price, previewImage, manufacturerID);
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Coolers VALUES(@productID,@type,@dimensions)", con);
            cmd.Parameters.AddWithValue("@productID", productID);
            cmd.Parameters.AddWithValue("@rating", type);
            cmd.Parameters.AddWithValue("@wattage", dimensions);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Database insert broke");
            }
            finally
            {
                con.Close();
            }
        }

        public void addMonitor(int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int refreshRate, double size, string resolution)
        {
            int productID = addProduct(listed, modelName, description, price, previewImage, manufacturerID);
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Monitors VALUES(@productID,@refreshRate,@size,@resolution)", con);
            cmd.Parameters.AddWithValue("@productID", productID);
            cmd.Parameters.AddWithValue("@refreshRate", refreshRate);
            cmd.Parameters.AddWithValue("@size", size);
            cmd.Parameters.AddWithValue("@resolution", resolution);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Database insert broke");
            }
            finally
            {
                con.Close();
            }
        }


        public void addMotherboard(int listed, string modelName, string description, double price, string previewImage, int manufacturerID, string chipset, int wifi, string formFactor)
        {
            int productID = addProduct(listed, modelName, description, price, previewImage, manufacturerID);
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Monitors VALUES(@chipset,@wifi,@formFactor)", con);
            cmd.Parameters.AddWithValue("@productID", productID);
            cmd.Parameters.AddWithValue("@chipset", chipset);
            cmd.Parameters.AddWithValue("@wifi", wifi);
            cmd.Parameters.AddWithValue("@formFactor", formFactor);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Database insert broke");
            }
            finally
            {
                con.Close();
            }
        }
        public void addComputer(int listed, string modelName, string description, double price, string previewImage, int manufacturerID,
            int ram, int laptop, int graphicsCard, int processor)
        {
            int productID = addProduct(listed, modelName, description, price, previewImage, manufacturerID);
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Computers VALUES(@productID,@ram,@laptop,@graphicsCard, @processor)", con);
            cmd.Parameters.AddWithValue("@ram", ram);
            cmd.Parameters.AddWithValue("@laptop", laptop);
            cmd.Parameters.AddWithValue("@processor", processor);
            cmd.Parameters.AddWithValue("@graphicsCard", graphicsCard);
            cmd.Parameters.AddWithValue("@productID", productID);

            //try
            //{
            cmd.ExecuteNonQuery();
            //}
            //catch
            //{

            //}
            //finally
            //{
            //    con.Close();
            //}


        }


        //all store prducts are part of the product table so I didn't want to have the same insert statement repeated for every product
        //the product method is private because the item will always appear in at least one other table, and a method will need to insert the values into that table
        private void alterProduct(int productID, int listed, string modelName, string description, double price, string previewImage, int manufacturerID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Products SET listed = @listed, modelName = @modelName, description = @description, price = @price, previewImage = @previewImage, manufacturerID = @manufacturerID " +
                                            "WHERE productID = @productID", con);
            cmd.Parameters.AddWithValue("@listed", listed);
            cmd.Parameters.AddWithValue("@modelName", modelName);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@previewImage", previewImage);
            cmd.Parameters.AddWithValue("@manufacturerID", manufacturerID);
            cmd.Parameters.AddWithValue("@productID", productID);
            //con.Open();
            try 
            {
                cmd.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                Debug.WriteLine("DATABASE ERROR"+e.ToString());
            }
            finally
            {
                con.Close();
            }


        }


        public void alterGraphicsCard(int productID, int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int VRAM, int chipManufacturerID)
        {
            //int productID = addProduct(listed, modelName, description, price, previewImage, manufacturerID);
            alterProduct(productID, listed, modelName, description, price, previewImage, manufacturerID);
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE GraphicsCards SET listed VRAM = @VRAM, chipManufacturer = chipManufacturer)", con);

            cmd.Parameters.AddWithValue("@VRAM", VRAM);
            cmd.Parameters.AddWithValue("@chipManufacturer", chipManufacturerID);
            try
            {

                cmd.ExecuteScalar();
            }
            catch
            {

            }
            finally
            {
                con.Close();
            }
        }
        public int alterProcessor(int productID, int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int generation, int iGPU, int sku)
        {

            alterProduct(productID, listed, modelName, description, price, previewImage, manufacturerID);
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Processors SET generation = @generation, iGPU = @iGPU, sku = @sku " +
                                            "WHERE productID = @productID", con);

            cmd.Parameters.AddWithValue("@generation", generation);
            cmd.Parameters.AddWithValue("@iGPU", iGPU);
            cmd.Parameters.AddWithValue("@sku", sku);
            cmd.Parameters.AddWithValue("@productID", productID);
            //try
            //{

                return cmd.ExecuteNonQuery();
            //}
            //catch
            //{

            //}
            //finally
            //{
            //    con.Close();
            //}
        }

        public void alterKeyboard(int productID, int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int wireless, string keyCaps)
        {
            alterProduct(productID, listed, modelName, description, price, previewImage, manufacturerID);
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Peripherals SET wireless = @wireless " +
                                            "WHERE productID = @productID " +
                                            "UPDATE Keyboards SET keyCaps = @keyCaps " +
                                            "WHERE productID = @productID", con);

            cmd.Parameters.AddWithValue("@wireless", wireless);
            cmd.Parameters.AddWithValue("@keyCaps", keyCaps);
            cmd.Parameters.AddWithValue("@productID", productID);
            try
            {

                cmd.ExecuteScalar();
            }
            catch
            {

            }
            finally
            {
                con.Close();
            }
        }

        public void alterMouse(int productID, int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int wireless, int optical)
        {
            alterProduct(productID, listed, modelName, description, price, previewImage, manufacturerID);
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Peripherals SET wireless = @wireless " +
                                            "WHERE productID = @productID " +
                                            "UPDATE Mice SET optical = @optical " +
                                            "WHERE productID = @productID", con);

            cmd.Parameters.AddWithValue("@wireless", wireless);
            cmd.Parameters.AddWithValue("@productID", productID);
            cmd.Parameters.AddWithValue("@optical", optical);
            //try
            //{

                cmd.ExecuteScalar();
            //}
            //catch
            //{

            //}
            //finally
            //{
            //    con.Close();
            //}
        }
        public void alterStorage(int productID, int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int capacity, double formFactor, string technology)
        {
            alterProduct(productID, listed, modelName, description, price, previewImage, manufacturerID);
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Storage SET capacity = @capacity, formFactor = @formFactor, technology = @technology " +
                                            "WHERE productID = @productID", con);
            cmd.Parameters.AddWithValue("@capacity", capacity);
            cmd.Parameters.AddWithValue("@formFactor", formFactor);
            cmd.Parameters.AddWithValue("@technology", technology);
            cmd.Parameters.AddWithValue("@productID",productID);
            //try
            //{
                cmd.ExecuteNonQuery();
            //}
            //catch
            //{

            //}
            //finally
            //{
            //    con.Close();
            //}
        }

        //TFW I realized I've gotta add all of these functions to the Buisiness Layer ;-;

        public void alterPowerSupply(int productID, int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int rating, int wattage)
        {
            alterProduct(productID, listed, modelName, description, price, previewImage, manufacturerID);
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE PowerSupplies SET rating = @rating, wattage = @wattage, " +
                                            "WHERE productID = @productID", con);
            cmd.Parameters.AddWithValue("@rating", rating);
            cmd.Parameters.AddWithValue("@wattage", wattage);
            try
            {
                cmd.ExecuteScalar();
            }
            catch
            {

            }
            finally
            {
                con.Close();
            }
        }



        public void alterCooler(int productID, int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int type, int dimensions)
        {
            alterProduct(productID, listed, modelName, description, price, previewImage, manufacturerID);
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Coolers SET type = @type, dimensions = @dimensions " +
                                            "WHERE productID = @productID", con);
            cmd.Parameters.AddWithValue("@type", type);
            cmd.Parameters.AddWithValue("@dimensions", dimensions);
            cmd.Parameters.AddWithValue("@productID", productID);
            try
            {
                cmd.ExecuteScalar();
            }
            catch
            {

            }
            finally
            {
                con.Close();
            }
}

        public void alterMonitor(int productID, int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int refreshRate, double size, string resolution)
        {
            alterProduct(productID, listed, modelName, description, price, previewImage, manufacturerID);
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Monitors SET refreshRate = @refreshRate, size = @size, resolution = @resolution " +
                                            "WHERE productID = @productID", con);
            cmd.Parameters.AddWithValue("@refreshRate", refreshRate);
            cmd.Parameters.AddWithValue("@size", size);
            cmd.Parameters.AddWithValue("@resolution", resolution);
            cmd.Parameters.AddWithValue("@productID", productID);
            try
            {
                cmd.ExecuteScalar();
            }
            catch
            {

            }
            finally
            {
                con.Close();
            }
        }
        public void alterMotherboard(int productID, int listed, string modelName, string description, double price, string previewImage, int manufacturerID, string chipset, int wifi, string formFactor)
        {
            alterProduct(productID, listed, modelName, description, price, previewImage, manufacturerID);
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Motherboards SET chipset = @chipset, wifi = @wifi, formFactor = @formFactor " +
                                            "WHERE productID = @productID", con);
            cmd.Parameters.AddWithValue("@productID", productID);
            cmd.Parameters.AddWithValue("@chipset", chipset);
            cmd.Parameters.AddWithValue("@wifi", wifi);
            cmd.Parameters.AddWithValue("@formFactor", formFactor);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Database insert broke");
            }
            finally
            {
                con.Close();
            }
        }
        public void alterComputer(int productID, int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int ram, int laptop, int graphicsCard, int processor)
        {
            alterProduct(productID, listed, modelName, description, price, previewImage, manufacturerID);
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Computers SET ram = @ram, laptop = @laptop, graphicsCard = @graphicsCard, processor = @processor" +
                                            "WHERE productID = @productID", con);
            cmd.Parameters.AddWithValue("@productID", productID);
            cmd.Parameters.AddWithValue("@ram", ram);
            cmd.Parameters.AddWithValue("@laptop", laptop);
            cmd.Parameters.AddWithValue("@graphicsCard", graphicsCard);
            cmd.Parameters.AddWithValue("@processor", processor);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Database insert broke");
            }
            finally
            {
                con.Close();
            }
        }


 
        //changes the temporary password
        public int updatePassword(string currentPassword, string newPassword)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("UPDATE Accounts SET password = @newPassword WHERE password = @currentPassword", con);
            cmd.Parameters.AddWithValue("@currentPassword", currentPassword);
            cmd.Parameters.AddWithValue("@newPassword", newPassword);

            try
            {
                return cmd.ExecuteNonQuery();
            }
            catch
            {
                Debug.WriteLine("Failed to update password: " + currentPassword);
            }
            finally
            {
                con.Close();
            }
            return 0;//if the query fails the program will act as if no rows were altered


        }


        
        public void activateAccount(int accountID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Accounts SET active = 1 WHERE accountID = @accountID", con);
            cmd.Parameters.AddWithValue("@accountID", accountID);
            //try
            //{
                cmd.ExecuteNonQuery();
            //}
            //catch
            //{
            //    Debug.WriteLine("Failed to activate account " + Convert.ToString(accountID));
            //}
            //finally
            //{
            //    con.Close();
            //}
        }

        public int alterAccount(int accountID, string password, string emailAddress, int active, int adminPriveleges)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Accounts SET password = @password, emailAddress = @emailAddress, active = @active, adminPriveleges = @adminPriveleges " +
                                            "WHERE accountID = @accountID", con);
            cmd.Parameters.AddWithValue("@accountID", accountID);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@emailAddress", emailAddress);
            cmd.Parameters.AddWithValue("@active", active);
            cmd.Parameters.AddWithValue("@adminPriveleges", adminPriveleges);


            //try
            //{
                return cmd.ExecuteNonQuery();
            //}
            //catch
            //{
            //    return -1;
            //    //Console.WriteLine")
            //}
            //finally
            //{
            //    con.Close();
            //}


        }

    }


    
}
