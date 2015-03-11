using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestProject
{
    [Serializable]
    public class FundNotOwnedException:Exception
    {
        public FundNotOwnedException(Fund fund)
        {
            this.Fund = fund;
        }

        public Fund Fund { get; set; }        

    }
}
