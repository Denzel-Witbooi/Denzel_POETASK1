using System;
using System.Collections.Generic;
using System.Text;

namespace PoeDesign
{
    class ExpenseList<T>: Expenses
    {
        private static object[] ExpenseName;
        private static object[] ExpenseCost;
        private static int stackPointer = 0;
        private string strDisplay;
        private static int expSize;
        private static double Total = 0;

        public void setExpList(int size)
        {
            ExpenseName = new object[size];
            ExpenseCost = new object[size];
            expSize = size;
        }

        /*
         * Author: Reece Wanvig
         * Date: 23 March 2021
         * Title of Source code: Add to Array method as bool type
         * Code version:
         *  #region add to arrays
         *   public bool addToArray(string strProduct, double dblPrice)
         *   {
         *      if(counter < iSizeArray)
         *      {
         *          productName[counter] = strProduct;
         *          price[counter] = dblPrice;
         *          dTotal += dblPrice;
         *          counter++;
         *          return true;
         *      }
         *        else
         *        {
         *          return false;
         *        }
         *   }
         *  #endregion
         *  Type: WindowsForms (.NetCore)
         *  URL: https://github.com/VCNMB-2021C/CovidForms.git
        */
        public override string Display()
        {
            for (int x = 0; x < stackPointer; x++)
            {
                strDisplay += "Expense Name: " + ExpenseName[x] +
                              "\nCost: R" + ExpenseCost[x] + "\n";
            }
            return strDisplay;
        }

        public override bool Push(object Names, object Costs)
        {
            if (stackPointer < expSize)
            {
                ExpenseName[stackPointer] = Names;
                ExpenseCost[stackPointer] = Costs;
                Total += Convert.ToDouble(Costs);
                stackPointer++;
                return true;
            }
            else
            {
                return false;
            }
        }

        public object getExpName(int x)
        {
            return ExpenseName[x];
        }
        public object getExpCost(int x)
        {
            return ExpenseCost[x];
        }

        public int getCounter()
        {
            return stackPointer;
        }
        public int size()
        {
            return expSize;
        }
        public double getTotal()
        {
            return Total;
        }
    }
}
