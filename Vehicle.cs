﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PoeDesign
{
    class Vehicle
    {
        #region vars for vechicle for task 3
        public string strModel { get; set; }
        public string strMake { get; set; }
        public double decPurchPrice { get; set; }
        public double decTotDep { get; set; }
        public double decIntRate { get; set; }
        public double decInsurancePrem { get; set; }
        #endregion

        /*Formula: A = P(1 + in)
         * URL: https://intl.siyavula.com/read/maths/grade-10/finance-and-growth/09-finance-and-growth-03
         * In-text: (Siyavula,2021)
         */
        private static double monthlyCarPayment;// monthly payment
        public double dblTotalMonthlyCost(double purchasePrice, double period, double interest, double deposit, double insurance)
        {
            double depAmt = purchasePrice * (deposit / 100); //deposit in cash
            double principalAmt; // principal amount
            double amtPaid; //amount paid

            principalAmt = purchasePrice - depAmt;//New opening balance
            interest = interest / 100;                                                             
            period = (period / 12);

            //Simple interest formula
            amtPaid = (principalAmt * (1 + (interest * period)));

            monthlyCarPayment = amtPaid / (period * 12);
            monthlyCarPayment = Math.Round(monthlyCarPayment + insurance,2);
            Print(purchasePrice, interest ,deposit,insurance, monthlyCarPayment);
            return monthlyCarPayment;
        }

        public double GetMonthlyCost()
        {
            return monthlyCarPayment;
        }


        public void Print(double purchasePrice,double interest, double deposit, double insurance, double cost)
        {
            MessageBox.Show("VEHICLE REPORT \n\n" +
                "Purchase Price: R" + purchasePrice + 
                "\nTotal deposit: " + deposit + "%" +
                " \n Interest Rate: "+ interest +"% " +
                " \n Estimated Premium: R" + insurance + 
                "\n Monthly cost: R" + cost);
        }

       
    }
}
