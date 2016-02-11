using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GregsCommon
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".

    public class TaxItem
    {
        public int PartyId;
        public int DinerId;
        public double ItemTotal;
        public double TaxPercent;
        public double TaxAmount;
        public int Diviser;
    }
}
