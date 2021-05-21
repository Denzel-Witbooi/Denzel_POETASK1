using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PoeDesign
{
    class HomeLoan
    {
        private delegate void MentionIncomeLimit(double income);
        ExpenseList<object> expenses = new ExpenseList<object>();
        

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
            expenses.getTotal();
            principalAmt = purchasePrice - depAmt;//New opening balance
            interest = interest / 100;
            period = (period / 12);

            //Simple interest formula
            amtPaid = (principalAmt * (1 + (interest * period)));
            monthlyPayment = Math.Round(amtPaid / (period * 12),2);
            PrintReport(principalAmt, amtPaid, monthlyPayment);
            return monthlyPayment;
        }
        #endregion


        public void PrintReport(double loanAmt, double accLoanAmt, double monthlyRepay)
        {

            String strDisplay = "";
            strDisplay = "Principal loan amount: R" + loanAmt +
                "\nAccumulated loan amount: R" + accLoanAmt +
                "\nMonthly Repayments: R" + monthlyRepay;
            MessageBox.Show(strDisplay, "Report");
        }
    }
}