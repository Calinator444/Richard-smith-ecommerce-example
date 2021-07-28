using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer.DataModels
{
    public class Processor : Product
    {
        public Processor(int processorSku)
        {
            this.processorSku = processorSku;
            //int manufacturerID;

        }
        //e.g. 3,5, etc;
        public int processorSku;

    }
}