--CREATE DATABASE RichardSmithElectronics
/*
SELECT * FROM  Products

-- use this to drop the database

SELECT * FROM chipManufacturers

	DROP TABLE UserAddress;
	DROP TABLE Addresses;
	DROP TABLE UserPhone
	DROP TABLE Phones
	DROP TABLE Accounts;
	DROP TABLE Products;
	DROP TABLE OrderItems;
	DROP TABLE Orders;
	DROP TABLE Mice;
	DROP TABLE Keyboards;
	DROP TABLE Peripherals;
	DROP TABLE PowerSupplies;
	DROP TABLE SolidState;
	DROP TABLE Storage;
	DROP TABLE Processors;
	DROP TABLE GraphicsCards;
	DROP TABLE Monitors;
	DROP TABLE Motherboards;
	DROP TABLE Coolers
	DROP TABLE Manufacturers;
	DROP TABLE ChipManufacturers
	DROP TABLE Computers
	DROP TABLE ShippingOptions;
*/

--EXECUTE HERE ONWARD
/*
valid insert statement for orders
INSERT INTO Orders(shippingID,accountID) OUTPUT Inserted.orderID
VALUES (2, 10000)
*/

SELECT * FROM ShippingOptions

Create TABLE Manufacturers(
	manufacturerID int IDENTITY(1,1),
	name varchar(100),
	CONSTRAINT PK_ManufacturerID PRIMARY KEY(ManufacturerID)
	);
INSERT INTO Manufacturers VALUES ('Asus')
INSERT INTO Manufacturers VALUES ('MSI')
INSERT INTO Manufacturers VALUES ('AMD')
INSERT INTO Manufacturers VALUES ('Intel')
INSERT INTO Manufacturers VALUES ('Corsair')
INSERT INTO Manufacturers VALUES ('Logitech')
INSERT INTO Manufacturers VALUES ('AOC')
INSERT INTO Manufacturers VALUES ('Western Digital')
INSERT INTO Manufacturers VALUES ('Microsoft')

CREATE TABLE Products(
	productID int IDENTITY(1,1),
	CONSTRAINT PK_Products PRIMARY KEY(productID),
	listed bit,
	modelName varchar(50),
	description varchar(max),
	price DECIMAL(6,2),
	previewImage varchar(max),
	manufacturerID int FOREIGN KEY REFERENCES Manufacturers(manufacturerID));


INSERT INTO Products VALUES(1, 'ML 120','a high quality fan with a magnetic levitation bearing',50,'https://assets.umart.com.au/dbs/images/products/35823.jpg', 5)

INSERT INTO Products VALUES(1,'GAMING X RTX 3070','A triple fanned rtx 3070 for overclocking',1200,'https://ccimg.canadacomputers.com/Products/1000x1000/230/522/183210/80413.jpg',2)
--LAPTOP GPUS
INSERT INTO Products VALUES(0,'GTX 1660ti MAX-Q', null,null,null,null);


INSERT INTO Products VALUES(1,'Zephyrus g14', 'A hella fast gaming laptop by Asus', 1900.00, 'https://cdn.shopify.com/s/files/1/0024/9803/5810/products/440347-Product-0-I-637300010469346457_ecdb12b2-3058-4676-ad62-c0a6ef7647af_800x800.jpg',1);

--LAPTOP PROCESSORS
INSERT INTO Products VALUES(0,'4800HS','a laptop processor by amd, not sold on the store front (unless in laptops)', null, null,3)

--COOLERS
INSERT INTO Products VALUES(1, 'h100i', 'a 240mm aio by corsair', 200, 'https://www.corsair.com/medias/sys_master/images/images/h4a/hd2/8839007174686/-CW-9060025-WW-Gallery-H100i-v2-01.png',5)

--MOTHERBOARDS
INSERT INTO Products VALUES(1,'Z490-p','A budget friendly motherboard with overclocking potential', 200, 'https://www.asus.com/media/global/products/jncxksvmspeeihnb/P_setting_xxx_0_90_end_500.png',1)

--Mice
INSERT INTO Products VALUES(1,'G502','A high end gaming mouse by logitech',100, 'https://plecom.imgix.net/iil-234700-633789.jpg?fit=fillmax&fill=solid&fill-color=ffffff&auto=format&w=1000&h=1000',6)

INSERT INTO Products VALUES(1,'Intellimouse','An office mouse by microsoft',80, 'https://s3-ap-southeast-2.amazonaws.com/wc-prod-pim/JPEG_1000x1000/MSCLINMSE_microsoft_classic_intellimouse.jpg',9)

--Keyboards
INSERT INTO Products VALUES (1, 'K95 Platinum','A dope a$$ RGB gaming keyboard by corsair',200,'https://plecom.imgix.net/iil-212392-638859.jpg?fit=fillmax&fill=solid&fill-color=ffffff&auto=format&w=1000&h=1000',5)
INSERT INTO Products VALUES(1,'22B2H','A monitor made by Alexandria Ocasio-Cortez apparently',400, 'https://cdn11.bigcommerce.com/s-8e755/images/stencil/1280x1280/products/164225/136595/aoc_22b2h_215_75hz_fhd_flickerfree_frameless_va_monitor_ac33067__46854.1583817282.jpg?c=2&imbypass=on',7)
CREATE TABLE Coolers(
	productID int FOREIGN KEY REFERENCES Products(productID),
	--CoolingID int FOREIGN KEY REFERENCES Devices(DeviceID),
	Type int, -- air - 0, water - 1, fan - 2
	dimensions int--e.g. 240 fopr 240mm
);


--processors
INSERT INTO Products VALUES (1, '9700k','a very fast 9th gen processor',450,'https://www.netnest.com.au/Content/Images/gen9-i7.jpg',4)

--power supplies
INSERT INTO Products VALUES (1, 'RM850x', 'A fairly reliable unit from corsair', 200, 'https://cdn.mwave.com.au/images/400/AC14757.jpg',5)
--Storage
INSERT INTO Products VALUES(1, 'WD40EFRX', 'A high quality NAS drive from Western Digital', 300, 'https://www.radioparts.com.au/Images/ProductImages/69000062.jpg',8)
	
INSERT INTO Coolers VALUES(6, 1, 240)
INSERT INTO Coolers VALUES(1, 2, 120)

--INSERT INTO Cooling dimensions
	
CREATE TABLE Motherboards(
	productID int FOREIGN KEY REFERENCES Products(productID),
	chipset Char(4),
	wifi Bit,
	formFactor varchar(10),
	CONSTRAINT PK_Motherboards PRIMARY KEY(productID)
);




INSERT INTO Motherboards VALUES (7, 'Z490', 0, 'ATX')

CREATE TABLE Monitors(
	productID int FOREIGN KEY REFERENCES Products(productID),
	refreshRate int, --in hz
	size DECIMAL(3,1), --in inches
	resolution varchar(9)
	CONSTRAINT PK_Monitors PRIMARY KEY(productID)
);
INSERT INTO Monitors VALUES(11,75,21.5, '1920x1080')

/*
SELECT p.productID, modelName, description, price, previewImage, resolution, refreshRate, size, ma.name AS manufacturer FROM Products as p
INNER JOIN Monitors AS m
ON (m.productID = p.productID)
INNER JOIN Manufacturers AS ma
ON (ma.manufacturerID = p.manufacturerID)*/

CREATE TABLE Processors(
	productID int FOREIGN KEY REFERENCES Products(productID),
	generation int,
	iGPU bit,
	sku int,
	CONSTRAINT PK_Processors PRIMARY KEY(productID)
	);
	--LAPTOP PROCESSORS
	INSERT INTO Processors VALUES(5,4,0,9)
	INSERT INTO Processors VALUES(12,9,1,7)

CREATE TABLE PowerSupplies(
	productID int FOREIGN KEY REFERENCES Products(productID),
	rating varchar(8),
	wattage int
);


INSERt INTO PowerSupplies VALUES(13, 3, 850)

CREATE TABLE Storage(
	Capacity int,--capacity (in GB)
	FormFactor Decimal(3,1), --represented in inches (e.g. 3.5")
	productID int FOREIGN KEY REFERENCES Products(productID),
	CONSTRAINT PK_Storage PRIMARY KEY(productID),
	technology varchar(15)
);

INSERT INTO Storage VALUES(4000, 3.5,14, 'Hard Drive')
CREATE TABLE Peripherals(
	productID int FOREIGN KEY REFERENCES Products(productID),
	wireless bit,
	CONSTRAINT PK_Peripherals PRIMARY KEY(productID)
)
INSERT INTO Peripherals VALUES(8,0)
INSERT INTO Peripherals VALUES(9,0)
INSERT INTO Peripherals VALUES(10,0)

CREATE TABLE Mice(
	productID int FOREIGN KEY REFERENCES Peripherals(productID),
	optical bit,
	CONSTRAINT PK_Mice PRIMARY KEY(productID)
);
/*
SELECT p.productID, modelName, description, price, previewImage, m.name AS manufacturer, wireless, optical FROM Products AS p
INNER JOIN Manufacturers AS m 
	ON (m.manufacturerID = p.manufacturerID)
INNER JOIN Peripherals AS pe
	ON (pe.productID = p.productID)
INNER JOIN Mice AS mi
	ON(mi.productID = p.productID)
*/


INSERT INTO Mice VALUES (8,1)
INSERT INTO Mice VALUES (9,0)

CREATE TABLE Keyboards(
	productID int FOREIGN KEY REFERENCES Products(productID), 
	keyCaps varchar(10)
);


INSERT INTO Keyboards VALUES(10,'Blue')
--select graphics cards
/*
SELECT p.ProductID, p.listed, modelName, previewImage
FROM Products as p
INNER JOIN GraphicsCards as gc
	ON(gc.ProductID = p.ProductId)
INNER JOIN Manufacturers as m
	ON(gc.manufacturerID = m.ManufacturerID)--CHIP MANUFACTURERS?! */

	--the chip maker for a given GPU


CREATE TABLE ChipManufacturers(
	chipManufacturerID int IDENTITY(1,1),
	name varchar(50)
	CONSTRAINT PK_ChipMaker PRIMARY KEY(chipManufacturerID)
);

--SELECT productID AS pp, price FROM Products
INSERT INTO ChipManufacturers VALUES('Nvidia');
INSERT INTO ChipManufacturers VALUES('AMD');

CREATE TABLE GraphicsCards(
	productID int FOREIGN KEY REFERENCES Products(productID),
	VRAM int,
	--manufacturerID int FOREIGN KEY REFERENCES Manufacturers(ManufacturerID),
	chipManufacturer int FOREIGN KEY REFERENCES ChipManufacturers(chipManufacturerID),
	CONSTRAINT PK_GraphicsCards PRIMARY KEY(productID)
);


INSERT INTO GraphicsCards VALUES (2, 8, 1)

--1660ti max q
INSERT INTO GraphicsCards VALUES (3,6, 1)



CREATE TABLE Accounts(
	accountID int IDENTITY(10000,1),
	CONSTRAINT PK_Account PRIMARY KEY (AccountID),
	password varchar(100),
	emailAddress varchar(max), --you can blame this asshole for the high varchar https://laughingsquid.com/the-worlds-longest-active-email-address/
	active bit,
	adminPriveleges bit
);


INSERT INTO Accounts VALUES('hardPass1', 'admin@uon.edu.au',1,1)
INSERT INTO Accounts VALUES('dummyPass', 'dummy@dummydomain.com',1,1) 
INSERT INTO Accounts VALUES ('hardPass1', 'jonnySmith@dummydomain.com',1,0)

CREATE TABLE Phones( --dont you guys have phones?
	PhoneID int IDENTITY(1,1),
	Number varchar(15),
	Mobile bit
	CONSTRAINT PK_Phones PRIMARY KEY(PhoneID)
);

CREATE TABLE UserPhone(
	AccountID int FOREIGN KEY REFERENCES Accounts(AccountID),
	PhoneID int FOREIGN KEY REFERENCES Phones(PhoneID),
	CONSTRAINT PK_UserPhone PRIMARY KEY(AccountID,PhoneID)
);


CREATE TABLE Computers(
	productID int FOREIGN KEY REFERENCES Products(productID),
	ram int,
	laptop bit,
	--the PK for the Graphics card and processor
	graphicsCard int FOREIGN KEY REFERENCES GraphicsCards(productID),
	processor int FOREIGN KEY REFERENCES Processors(productID),
	CONSTRAINT PK_Computer PRIMARY KEY(productID)
);

INSERT INTO Computers VALUES(4, 16,1, 3, 5)




CREATE TABLE Addresses(
	addressID int IDENTITY(1,1),
	postCode int,
	country varchar(14),
	street varchar(100),
	suburb varchar(200),
	unit int,
	CONSTRAINT PK_AddressID Primary Key(addressID),
	billingAddress bit
);



--was testing some stuff here
/*
INSERT INTO Addresses
OUTPUT Inserted.addressID
VALUES('2296', 'Austria','Fakestreet', 'Wallsend',23,0);
*/




INSERT INTO Addresses VALUES(2287,'Australia','Fakestreet','Wallsend',22,0)
INSERT INTO Addresses VALUES(2287,'Australia','Fakestreet','Wallsend',22,1)

CREATE TABLE UserAddress(
	addressID int FOREIGN KEY REFERENCES Addresses(addressID),
	accountID int FOREIGN KEY REFERENCES Accounts(accountID),
	CONSTRAINT PK_UsrAddress PRIMARY KEY(addressID,accountID)
);

INSERT INTO UserAddress VALUES(1,10002);
INSERT INTO UserAddress VALUES(2,10002);
/*
CREATE TABLE SolidState(
	DeviceID int FOREIGN KEY REFERENCES Storage(productID),
	Technology varchar(15),
	CONSTRAINT PK_SSD PRIMARY KEY(DeviceID)
);*/



CREATE TABLE ShippingOptions(
	shippingID int IDENTITY(1,1),
	days int,
	cost Decimal(6,2),
	name varchar(50),
	description varchar(max),
	CONSTRAINT PK_ShippingID PRIMARY KEY(shippingID)
);

INSERT INTO ShippingOptions VALUES(365,0,'Australia Post','Completely and utterly incompetent')
INSERT INTO ShippingOptions VALUES(7,50,'Courier','Completely and utterly incompetent')
INSERT INTO ShippingOptions VALUES(1,500,'Fighter Jet','A military grade fighter jet swoops over your house and drops off the package')


CREATE TABLE Orders(
	--add a seed for orders so a customer doesn't say to themselves "wow, I just placed the first ever order for this site"
	orderID int IDENTITY(10000,1),
	datePlaced DATETIME DEFAULT CURRENT_TIMESTAMP,
	--DateReceived Date,
	total DECIMAL(6,2),
	shippingID int FOREIGN KEY REFERENCES ShippingOptions(shippingID), 
	accountID int FOREIGN KEY REFERENCES Accounts(accountID),
	
	CONSTRAINT PK_Orders PRIMARY KEY(OrderID)
);
INSERT INTO Orders(total,shippingID,accountID ) 
VALUES(450, 2,10002 )

CREATE TABLE OrderItems(
	orderID int FOREIGN KEY REFERENCES Orders(OrderID),
	itemID int FOREIGN KEY REFERENCES Products(productID),
	quantity int
);

INSERT INTO OrderItems VALUES(10000,6,2)