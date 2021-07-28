using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace Assgt1
{
    public static class EmailValidator
    {
        public static bool CheckValid(string emailAddress)
        {
            if (emailAddress == "")
                return false;
            //Author a.garg
            //Date accessed: 26/02/2021
            //Purpose: Email validator
            //source: https://www.netdeft.com/2015/09/custom-validator-server-side-validation.html

            //txtEmail.Text;
            string strRegex = "^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}" + "\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\" + ".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$";
            Regex re = new Regex(strRegex);
            return re.IsMatch(emailAddress);
        }
    }
}