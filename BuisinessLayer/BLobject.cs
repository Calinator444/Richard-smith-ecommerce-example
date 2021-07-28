using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
//using DataLayer.DataModels;
using DataLayer;
using DataLayer.DataModels;
using System.Diagnostics;

    //acesses the database through calls to the DataLayer
namespace BuisinessLayer
{

    // = new DLobject();
    public class BLobject
    {
        DLobject DLayer;
        public BLobject()
        {
            DLayer = new DLobject();
        }


        public Product getProduct(int ID)
        {
            return DLayer.getProduct(ID);
        }


        //insert username/password ass arguments
        //public DataTable returnBindable()
        //{
        //return DLayer.fetchBindable();
        //}

        //returns true of the update was successful

        public void activateAccount(int accountID)
        {
            DLayer.activateAccount(accountID);
        }
        public bool alterAccount(int accountID, string password, string emailAddress, int active, int adminPriveleges)
        {
            return DLayer.alterAccount(accountID, password, emailAddress, active, adminPriveleges) > 0;
        }
        public int addAccount(string userName, string password)
        {
            return DLayer.addAccount(userName, password);
        }


        public DataTable getLaptopGraphics()
        {
            return DLayer.getLaptopGraphics();
        }

        public DataTable getLaptopProcessor()
        {
            return DLayer.getLaptopProcessor();
        }

        public DataTable getManufacturers()
        {
            return DLayer.getManufacturers();
        }
        public Account getAccountObject(int ID)
        {
            return DLayer.getAccountObject(ID);
        }


        public bool updatePassword(string currentPassword, string newPassword)
        {
            return DLayer.updatePassword(currentPassword, newPassword) > 0;
        }

        public void updateAddresses(int shippingID, Address shippingAddress, int billingID, Address billingAddress)
        {
            DLayer.updateAddresses(shippingID, shippingAddress, billingID, billingAddress);
        }


        public DataTable getBindableGeneric(string tableName, string paramName, string param)
        {
            return DLayer.getBindableGeneric(tableName, paramName, param);
        }
        public DataTable getBindable(string tableName)
        {
            return DLayer.getAllBindable(tableName);
        }
        //This is 

        public DataTable getSingleProduct(string productID)
        {
            return DLayer.getSingleProduct(productID);
        }

        public DataTable getProductsBindable(string productCategory)
        {
            return DLayer.getProductsBindable(productCategory);
        }

        public bool checkAdmin(int accountID)
        {
            return DLayer.isAdmin(accountID);
        }

        public bool addShippingOption(int days, double cost, string name, string description)
        {
            return DLayer.addShippingOption(days, cost, name, description) > 0;
        }

        public void addAddresses(Address billingAddress, Address shippingAddress, int usrID)
        {
            DLayer.addAddresses(billingAddress, shippingAddress, usrID);
        }

        public void addAddresses(Address shippingAddress, int usrID)
        {
            DLayer.addAddresses(shippingAddress, usrID);
        }

        public List<Address> getAddressByUser(int ID)
        {
            return DLayer.getAddressByUser(ID);
        }
        public void addOrderItems(List<Item> items, int orderId)
        {
            DLayer.addOrderItems(items, orderId);
        }
        public int placeOrder(int shippingID, int accountID, double subtotal)
        {
            return DLayer.placeOrder(shippingID, accountID, subtotal);
        }
        public int getOrderShipping(int orderID)
        {
            return DLayer.getOrderShipping(orderID);
        }
        public int placeOrder(int shippingID, double subtotal)
        {
            return DLayer.placeOrder(shippingID, subtotal);
        }

        public bool checkAnyUserExists(string emailAddress)
        {
            return DLayer.checkAnyUserExists(emailAddress);
        }
        public int CheckUser(string userName, string passWord)
        {

            int userExists = DLayer.UserExists(userName, passWord); //will return "null" if no object exists

            return userExists;
            /*
            while(record.Read())
            bool detailsMatch = (string) record["emailAddress"] == userName && (string) record["password"] == passWord;
            if (!detailsMatch)
            {
                if (record["emailAddress"] == userName)
                    return 1;
                else
                    return 0;

            }
            else
                return 2;*/
            /*
             * 
             * Possible outcomes
             * 0: user does not exist
             * 1: user exists, password doesn't match
             * [AccoutID]: user exists, password matches
             * 
             * 
             * 
             */


            //tempobj.allRows();
            //Debug.WriteLine(tempobj.allRows().ToString());
        }

        public List<OrderItem> getOrderItems(int orderID)
        {
            return DLayer.getOrderItems(orderID);
        }

        //attempts to change the user's password, returns true if successful
        public bool tempPassword(string emailAddress, string password)
        {
            return DLayer.tempPassword(emailAddress, password) > 0;
        }
        public DataTable getUserOrders(int accountID)
        {
            return DLayer.getUserOrders(accountID);
        }

        public DataTable getOrderItemsBindable(int orderID)
        {
            return DLayer.getOrderItemsBindable(orderID);
        }


        public DataTable getShippingOptions()
        {
            return DLayer.getShippingOptions();
        }
        public ShippingOptions getShippingOption(int ID)
        {
            return DLayer.getShippingOption(ID);
        }



        //ADD STORE ITEMS
        public void addGraphicsCard(int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int VRAM, int chipManufacturerID)
        {
            DLayer.addGraphicsCard(listed, modelName, description, price, previewImage, manufacturerID, VRAM, chipManufacturerID);
        }
        public void addProcessor(int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int generation, int iGPU, int sku)
        {
            DLayer.addProcessor(listed, modelName, description, price, previewImage, manufacturerID, generation, iGPU, sku);
        }

        public void addKeyboard(int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int wireless, string keyCaps)
        {
            DLayer.addKeyboard(listed, modelName, description, price, previewImage, manufacturerID, wireless, keyCaps);
        }

        public void addMouse(int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int wireless, int optical)
        {
            DLayer.addMouse(listed, modelName, description, price, previewImage, manufacturerID, wireless, optical);
        }
        public void addStorage(int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int capacity, double formFactor, string technology)
        {
            DLayer.addStorage(listed, modelName, description, price, previewImage, manufacturerID, capacity, formFactor, technology);
        }

        //TFW I realized I've gotta add all of these functions to the Buisiness Layer ;-;

        public void addPowerSupply(int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int rating, int wattage)
        {
            DLayer.addPowerSupply(listed, modelName, description, price, previewImage, manufacturerID, rating, wattage);
        }



        public void addCooler(int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int type, int dimensions)
        {
            DLayer.addCooler(listed, modelName, description, price, previewImage, manufacturerID, type, dimensions);
        }

        public void addFan(int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int dimensions)
        {
            DLayer.addCooler(listed, modelName, description, price, previewImage, manufacturerID, 2, dimensions);
        }

        public void addMonitor(int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int refreshRate, double size, string resolution)
        {
            DLayer.addMonitor(listed, modelName, description, price, previewImage, manufacturerID, refreshRate, size, resolution);
        }


        public void addMotherboard(int listed, string modelName, string description, double price, string previewImage, int manufacturerID, string chipset, int wifi, string formFactor)
        {
            DLayer.addMotherboard(listed, modelName, description, price, previewImage, manufacturerID, chipset, wifi, formFactor);
        }

        public void addComputer(int listed, string modelName, string description, double price, string previewImage, int manufacturerID,
            int ram, int laptop, int graphicsCard, int processor)
        {
            DLayer.addComputer(listed, modelName, description, price, previewImage, manufacturerID,
            ram, laptop, graphicsCard, processor);
        }

        //alter statements for products

        public void alterGraphicsCard(int productID, int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int VRAM, int chipManufacturerID)
        {
            DLayer.alterGraphicsCard(productID, listed, modelName, description, price,previewImage, manufacturerID, VRAM, chipManufacturerID);
        }

        public void alterProcessor(int productID, int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int generation, int iGPU, int sku)
        {
            DLayer.alterProcessor(productID, listed, modelName, description, price, previewImage, manufacturerID, generation, iGPU, sku);
        }

        public void alterKeyboard(int productID, int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int wireless, string keyCaps)
        {
            DLayer.alterKeyboard(productID, listed, modelName, description, price, previewImage, manufacturerID, wireless, keyCaps);
        }

        public void alterMouse(int productID, int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int wireless, int optical)
        {
            DLayer.alterMouse(productID, listed, modelName, description, price, previewImage, manufacturerID, wireless, optical);
        }
        public void alterStorage(int productID, int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int capacity, double formFactor, string technology)
        {
            DLayer.alterStorage(productID, listed, modelName, description, price, previewImage, manufacturerID, capacity, formFactor, technology);
        }

        //TFW I realized I've gotta add all of these functions to the Buisiness Layer ;-;

        public void alterPowerSupply(int productID, int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int rating, int wattage)
        {
            DLayer.alterPowerSupply(productID, listed, modelName, description, price, previewImage, manufacturerID, rating, wattage);
        }

        public void alterCooler(int productID, int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int type, int dimensions)
        {
            DLayer.alterCooler(productID,listed,modelName,description,price,previewImage,manufacturerID,type,dimensions);
        }

        public void alterMonitor(int productID, int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int refreshRate, double size, string resolution)
        {
            DLayer.alterMonitor(productID, listed, modelName, description, price, previewImage, manufacturerID, refreshRate, size, resolution);
        }
        public void alterMotherboard(int productID, int listed, string modelName, string description, double price, string previewImage, int manufacturerID, string chipset, int wifi, string formFactor)
        {
            DLayer.alterMotherboard(productID, listed, modelName, description, price, previewImage, manufacturerID, chipset, wifi, formFactor);
        }


        public void alterComputer(int productID, int listed, string modelName, string description, double price, string previewImage, int manufacturerID, int ram, int laptop, int graphicsCard, int processor)
        {
            DLayer.alterComputer(productID, listed, modelName, description, price, previewImage, manufacturerID, ram, laptop, graphicsCard, processor);
        }
    }
}
