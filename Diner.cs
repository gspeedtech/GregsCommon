using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GregsCommon
{
    class Diner
    {
        public Diner()
        {
            dinerTaxItems = new List<TaxItem>();
            dinerTipItems = new List<TipItem>();
        }
        public List<TaxItem> AddDinerTaxItems(Check Check)
        {
            foreach (var Id in Check.items.Select(x => x.DinerId).Distinct())
            {
                double itemTotal = 0;
                TaxItem taxItem = new TaxItem();                
                foreach (var row in Check.items.Where(y => y.DinerId == Id))
                {
                    itemTotal += row.Price;
                }
                taxItem.PartyId = 0;
                taxItem.DinerId = Id;
                taxItem.ItemTotal = itemTotal;
                taxItem.TaxAmount = Math.Round((itemTotal * Check.CheckTaxPercentage), 2);
                taxItem.TaxPercent = Check.CheckTaxPercentage;
                taxItem.Diviser = 1;
                dinerTaxItems.Add(taxItem);
            }
            return dinerTaxItems;
        }

        public List<TipItem> AddDinerTipItems(Check Check)
        {
            foreach (var Id in Check.items.Select(x => x.DinerId).Distinct())
            {
                double itemTotal = 0;
                TipItem tipItem = new TipItem();
                foreach (var row in Check.items.Where(y => y.DinerId == Id))
                {
                    itemTotal += row.Price;
                }
                tipItem.PartyId = 0;
                tipItem.DinerId = Id;
                tipItem.ItemTotal = itemTotal;
                tipItem.TipAmount = Math.Round((itemTotal * Check.CheckTipPercentage), 2);
                tipItem.TipPercent = Check.CheckTipPercentage;
                tipItem.Diviser = 1;
                dinerTipItems.Add(tipItem);
            }

            return dinerTipItems;
        }
        public List<TaxItem> dinerTaxItems;
        public List<TipItem> dinerTipItems;  
    }
}
