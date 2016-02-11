using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GregsCommon
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".

    public class Check
    {
        public Check()
        {
            items = new List<CheckItem>();
            partyTaxItems = new List<TaxItem>();
            partyTipItems = new List<TipItem>();
        }

        //public Check(Check firstCheck)
        //{
        //    this.firstCheck = firstCheck;
        //}

        public void AddCheckItem(int PartyID, int DinerId, string Type, double Price)
        {
            CheckItem item = new CheckItem();
            item.PartyId = PartyID;
            item.DinerId = DinerId;
            item.Type = Type;
            item.Price = Price;
            item.Diviser = 1;
            items.Add(item);
        }

        public void AddCheckItem(int PartyId, int DinerId, string Type, double Price, int Diviser)
        {
            double subtotal = 0;
            for (int x = 0; x <= Diviser - 1; ++x)
            {
                CheckItem item = new CheckItem();
                item.PartyId = PartyId;
                item.DinerId = DinerId;
                item.Type = Type;
                item.Diviser = Diviser;
                if ((x + 1) == Diviser)
                {
                    //account for rounding
                    item.Price = Math.Round((Price - subtotal), 2);
                }
                else
                {
                    item.Price = Math.Round((Price / Diviser), 2);
                    subtotal += item.Price;
                }

                items.Add(item);
            }
        }

        public void AddPartyTaxItems(Check Check)
        {                       
            foreach(var Id in Check.items.Select(x=> x.PartyId).Distinct())
            {
                double itemTotal = 0;
                TaxItem taxItem = new TaxItem();
                foreach (var row in Check.items.Where(y => y.PartyId == Id))
                {                    
                    itemTotal += row.Price;
                }
                taxItem.PartyId = Id;
                taxItem.DinerId = 0;
                taxItem.ItemTotal = itemTotal;
                taxItem.TaxAmount = Math.Round((itemTotal * Check.CheckTaxPercentage), 2);
                taxItem.TaxPercent = Check.CheckTaxPercentage;
                taxItem.Diviser = 1;
                partyTaxItems.Add(taxItem);
            }
        }

        public void AddPartyTipItems(Check Check)
        {                    
            foreach (var Id in Check.items.Select(x => x.PartyId).Distinct())
            {
                double itemTotal = 0;
                TipItem tipItem = new TipItem();
                foreach (var row in Check.items.Where(y => y.PartyId == Id))
                {
                    itemTotal += row.Price;
                }
                tipItem.PartyId = Id;
                tipItem.DinerId = 0;
                tipItem.ItemTotal = itemTotal;
                tipItem.TipAmount = Math.Round((itemTotal * Check.CheckTipPercentage), 2);
                tipItem.TipPercent = Check.CheckTipPercentage;
                tipItem.Diviser = 1;
                partyTipItems.Add(tipItem);
            }
        }
        public void GetCheckTotals(Check Check)
        {
            double ItemTotal = 0;
            foreach (var row in Check.items)
            {
                ItemTotal += row.Price;
            }
            Check.CheckSubtotal = ItemTotal;
            Check.CheckTaxTotal = Math.Round((ItemTotal * Check.CheckTaxPercentage),2);
            Check.CheckTipTotal = Math.Round((ItemTotal * Check.CheckTipPercentage),2);
            Check.CheckGrandTotal = (Check.CheckSubtotal + Check.CheckTaxTotal + Check.CheckTipTotal);           
        }
        public int CheckID;
        public string CheckName;
        public double CheckGrandTotal;
        public double CheckSubtotal;
        public double CheckTaxTotal;
        public double CheckTipTotal;
        public double CheckTaxPercentage;
        public double CheckTipPercentage;
        public List<CheckItem> items;
        public List<TaxItem> partyTaxItems;
        public List<TipItem> partyTipItems;
    }
}
