using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace PoeDesign
{
   
    class ExpenseList<T>: Expenses
    {
        private static string[] ExpenseName;
        private static double[] ExpenseCost;
        private static int stackPointer = 0;
        private string strDisplay;
        private static int expSize;
        private static double Total = 0;
        public void setExpList(int size)
        {
            ExpenseName = new string[size];
            ExpenseCost = new double[size];
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
        public override bool Push(string Names, double Costs)
        {
            if (stackPointer < expSize)
            {
                ExpenseName[stackPointer] = Names;
                ExpenseCost[stackPointer] = Costs;
                Total += Costs;
                stackPointer++;
                return true;
            }

            else
            {
                return false;
            }
        }
        public string getExpName(int x)
        {
            return ExpenseName[x];
        }
        public double getExpCost(int x)
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
