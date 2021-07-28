using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assgt1
{
    public class AddressRequest
    {
        //a very simple class for that we deserialize the result of the api request to
        public AddressRequest()
        {
        }

        public string status
        {
            get;
            set;
        }

    }
}