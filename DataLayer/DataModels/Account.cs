using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace DataLayer.DataModels
{
    public class Account
    {

        //default constructor
        public Account(int accountID, string password, string emailAddress)
        {
            this.password = password;
            this.emailAddress = emailAddress;
            //accounts are active by default
            active = true;
            adminPriveleges = false;


        }
        public Account()
        {

        }


        //overload with all attributes
     
        //constructor with priviliges and active status & admin status specified
        public Account(int accountID, string password, string emailAddress, bool active, bool adminPriveleges)
        {
            this.password = password;
            this.emailAddress = emailAddress;
            //accounts are active by default but don't have admin priveleges
            this.active = active;
            this.adminPriveleges = adminPriveleges;
        }
        public Account(IDataRecord record)
        {
            //Didn't really wanna do things this way but I'm assigning attributes by memorizing the indexes of each attribute
            accountID = (int) record[0];
            password = (string) record[1];
            emailAddress = (string)record[2];
            active = (bool)  record[3];
            adminPriveleges = (bool) record[4];

            //empty constructor
        }
        public string password { get; set; }
        public string emailAddress { get; set; }
        public int accountID { get; set; }
        public bool active { get; set; }
        public bool adminPriveleges { get; set; }


    }
}