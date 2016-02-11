using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GregsConsoleSplitCheck
{
    public class Check
    {
        public Check()
        {
            items = new List<CheckItem>();
            partyTaxItems = new List<TaxItem>();
            partyTipItems = new List<TipItem>();
            dinerTaxItems = new List<TaxItem>();
            dinerTipItems = new List<TipItem>();
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
            Party party = new Party();
            partyTaxItems = party.AddPartyTaxItems(Check);
        }

        public void AddPartyTipItems(Check Check)
        {
            Party party = new Party();
            partyTipItems = party.AddPartyTipItems(Check);                 
        }

        public void AddDinerTaxItems(Check Check)
        {
            Diner diner = new Diner();
            dinerTaxItems = diner.AddDinerTaxItems(Check);
        }

        public void AddDinerTipItems(Check Check)
        {
            Diner diner = new Diner();
            dinerTipItems = diner.AddDinerTipItems(Check);
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
        public List<TaxItem> dinerTaxItems;
        public List<TipItem> dinerTipItems;
    }
}
