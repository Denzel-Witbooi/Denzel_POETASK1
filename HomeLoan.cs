using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PoeDesign
{
    class HomeLoan : Expenses
    {
        #region vars expenses
        private static double[] Groceries;
        private static double[] WaterAndLights;
        private static double[] Travel;
        private static double[] Phone;
        private static double[] OtherExp;
        private static double dTotal = 0;
        #endregion

        #region vars for a array counter and array size
        private static int counter = 0;
        private static int iSizeArray = 0;
        #endregion

        /*Formula: A = P(1 + in)
         * URL: https://intl.siyavula.com/read/maths/grade-10/finance-and-growth/09-finance-and-growth-03
         * In-text: (Siyavula,2021)
         */
        #region method to calculate monthly loan repayment
        public double CalcRepayment(double purchasePrice, double period, double interest, double deposit)
        {
            double depAmt = purchasePrice * (deposit / 100); //deposit in cash
            double monthlyPayment;// monthly payment
            double principalAmt; // principal amount
            double amtPaid; //amount paid

            principalAmt = (double)(decimal)(purchasePrice - depAmt);//New opening balance
            interest = interest / 100;
            period = (period / 12);

            //Simple interest formula
            amtPaid = (double)(decimal)(principalAmt * (1 + (interest * period)));

            monthlyPayment = (double)(decimal)(amtPaid / (period * 12));
            PrintReport(principalAmt, amtPaid, monthlyPayment);
            return monthlyPayment;
        }
        #endregion
        public void PrintReport(double loanAmt, double accLoanAmt, double monthlyRepay )
        {
            String strDisplay = "";
            strDisplay = "Principal loan amount: R" + loanAmt + 
                "\nAccumulated loan amount: R" + accLoanAmt + 
                "\nMonthly Repayments: R" + monthlyRepay;
            MessageBox.Show(strDisplay,"Report"); 
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
        #region add to arrays method
        public override bool addToArray(double dblGroceries, double dblWaterAndLights, double dblTravel, double dblPhone, double dblOtherExp)
        {
            if (counter < iSizeArray)
            {
                Groceries[counter] = dblGroceries;
                WaterAndLights[counter] = dblWaterAndLights;
                Travel[counter] = dblTravel;
                Phone[counter] = dblPhone;
                OtherExp[counter] = dblOtherExp;
                dTotal += dblGroceries + dblWaterAndLights + dblTravel + dblPhone + dblOtherExp;
                counter++;
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region method to get total of array expenses
        public double getTotal()
        {
            return dTotal;
        }
        #endregion

        #region getters for expenses vars
        public override double getGroceries(int x)
        {
            return Groceries[x];
        }

        public override double getOtherExp(int x)
        {
            return OtherExp[x];
        }

        public override double getPhone(int x)
        {
            return Phone[x];
        }

        public override double getTravel(int x)
        {
            return Travel[x];
        }

        public override double getWateAndLights(int x)
        {
            return WaterAndLights[x];
        }
        #endregion

        #region set array method
        public override void setArray(int num)
        {
            Groceries = new double[num];
            WaterAndLights = new double[num];
            Travel = new double[num];
            Phone = new double[num];
            OtherExp = new double[num];

            iSizeArray = num;
        }
        #endregion



        #region method for array size and counter
        public override int Size()
        {
            return iSizeArray;
        }

        public int getcounter()
        {
            return counter;
        }
        #endregion
    }
}
