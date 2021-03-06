//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RetireHappy.Models
{
    using DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class ExpenditureList
    {
        public int mId { get; set; }
        public string item1 { get; set; }
        public string item2 { get; set; }
        public string item3 { get; set; }
        public string item4 { get; set; }
        public string item5 { get; set; }
        public string item6 { get; set; }
        public string item7 { get; set; }
        public string item8 { get; set; }
        public string item9 { get; set; }
        public string item10 { get; set; }
    
        public virtual Member Member { get; set; }

        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public List<string> itemList { get; set; }

        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public double totalAmount { set; get; }


        //function to populate list of items for easy amt tabulation
        public void populateList()
        {
            itemList.Add(item1);
            itemList.Add(item2);
            itemList.Add(item3);
            itemList.Add(item4);
            itemList.Add(item5);
            itemList.Add(item6);
            itemList.Add(item7);
            itemList.Add(item8);
            itemList.Add(item9);
            itemList.Add(item10);
            itemList.Add(item1);
        }

        public void popItemsFromList()
        {
            item1 = itemList.ElementAt(0);
            item2 = itemList.ElementAt(1);
            item3 = itemList.ElementAt(2);
            item4 = itemList.ElementAt(3);
            item5 = itemList.ElementAt(4);
            item6 = itemList.ElementAt(5);
            item7 = itemList.ElementAt(6);
            item8 = itemList.ElementAt(7);
            item9 = itemList.ElementAt(8);
            item10 = itemList.ElementAt(9);
        }

        public void updateList(String expStr)
        {
            List<string> avgExpIDs = expStr.Split(',').ToList<string>();
            itemList = new List<string>();
            int arrLen = avgExpIDs.Count();
            //populate itemList with items passed in
            if (arrLen > 0)
            {
                for (int i = 0; i < arrLen; i++)
                {
                    itemList.Add(avgExpIDs.ElementAt(i));
                }
            }

            for (int j = arrLen + 1; j <= 10; j++)
            {
                itemList.Add("");
            }
            popItemsFromList();
        }
        public double calcTotalExpenditure()
        {
            double totalAmt = 0.0;
            // go to the table to find the id
            AvgExpenditureGateway avgExpGW = new AvgExpenditureGateway();
            AvgExpenditure tempAvgExpObj = new AvgExpenditure();
            //loop through each item in itemlist in avgExpenditure DB then increment total amount
            foreach (string item in itemList)
            {

                if (item.Equals("")) return totalAmt;
                tempAvgExpObj = avgExpGW.SelectById(Int32.Parse(item));
                totalAmt += (double)tempAvgExpObj.avgAmount;

            }



            return totalAmt;

        }
    }
}
