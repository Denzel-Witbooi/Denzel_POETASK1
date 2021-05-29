using System;
using System.Collections.Generic;
using System.Text;

namespace PoeDesign
{
    class Budget
    {
        //Youtube video: https://youtu.be/g6P9GECt11o
        #region properties for main vars
        private double monthlyIncome;
        private double monthlyTax;

        public double MonthlyIncome { get => monthlyIncome; set => monthlyIncome = value; }
        public double MonthlyTax { get => monthlyTax; set => monthlyTax = value; }
        #endregion
    }
}
