using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;


namespace GregsCommon
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

        public static Check NewCheck()
        {
            Check NewCheck = new Check();
            NewCheck.CheckID = 1;
            NewCheck.CheckName = "First Check";
            NewCheck.CheckTaxPercentage = .0725;
            NewCheck.CheckTipPercentage = .18;
            NewCheck.AddCheckItem(0, 1, "Entree", 19.96, 3);
            NewCheck.AddCheckItem(0, 2, "Drink", 9.90, 2);
            NewCheck.AddCheckItem(0, 3, "Dessert", 5.95);
            NewCheck.AddCheckItem(1, 3, "Entree", 19.96, 3);
            NewCheck.AddCheckItem(1, 2, "Drink", 9.90, 2);
            NewCheck.AddCheckItem(1, 1, "Dessert", 5.95);
            NewCheck.AddPartyTaxItems(NewCheck);
            NewCheck.AddPartyTipItems(NewCheck);
            NewCheck.AddDinerTaxItems(NewCheck);
            NewCheck.AddDinerTipItems(NewCheck);
            NewCheck.GetCheckTotals(NewCheck);

            return NewCheck;
        }

        public static DataTable GetCheck(Check NewCheck)
        {            

            DataTable Check = new DataTable();
            Check = CreateDataTable(NewCheck.items);

            return Check;
        }

        public static DataTable CreateDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetFields();

            DataTable dataTable = new DataTable();
            foreach (FieldInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.FieldType) ?? info.FieldType));
            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        public List<CheckItem> GetCheckItems(Check Check)
        {
            foreach (var Id in Check.items)
            {
                CheckItem checkItem = new CheckItem();
                items.Add(checkItem);
            }
            return items;
        }

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
