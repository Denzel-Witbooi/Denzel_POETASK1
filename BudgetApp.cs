using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
                                                                                   
namespace PoeDesign
{
    public partial class BudgetApp : Form
    {
        #region main vars
        private static int numExpenses = 5; //Array size for Expenses
        private static double income;    //Var to store gross monthly income
        private static double tax;       // Var to store monthly tax
        private static double monthlyLoanRepay;  //Var to store returned value of monthly loan repay method
        private static double rentAmt;           //Var to store rent amount
        #endregion

        #region initialising objects
        Budget b = new Budget();
        HomeLoan hl = new HomeLoan();
        #endregion

        public BudgetApp()
        {
            btnNAbout.Focus();  //Place focus on Next button on program start
            InitializeComponent();           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            #region convert text box vars and store in new vars
            income = Convert.ToDouble(txtIncome.Text);
            tax = Convert.ToDouble(txtTax.Text);
            double groceries = Convert.ToDouble(txtGroceries.Text);
            double waterALights = Convert.ToDouble(txtWT.Text);
            double travel = Convert.ToDouble(txtTravel.Text);
            double cell = Convert.ToDouble(txtCell.Text);
            double other = Convert.ToDouble(txtOther.Text);
            #endregion

            #region pass vars to set methods from Budget and HomeLoan class
            b.MonthlyIncome = income;
            b.MonthlyIncome = tax;
            hl.setArray(numExpenses);
            hl.addToArray(groceries, waterALights, travel, cell, other);
            #endregion

            tpHome.SelectTab(tpAccom); //redirects to Accommodation page

            cbPropType.Focus(); //Place focus on combox box
        }

        #region Buttons that redirect to next page
        private void btnTAbout_Click(object sender, EventArgs e)
        {
            tpHome.SelectTab(tpAbout);
        }

        private void btnBHome_Click(object sender, EventArgs e)
        {
            tpHome.SelectTab(tbHome);
        }

        private void btnTFinances_Click(object sender, EventArgs e)
        {
            tpHome.SelectTab(tpFinances);
        }
        #endregion

        private void tpHome_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        #region method for check box to with condition to redirect to a selected page
        private void cbPropType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPropType.SelectedIndex.Equals(0))
            {
                tpHome.SelectTab(tpRent);//redirects to Rent page
                txtRentAmt.Focus();
            }
            else if (cbPropType.SelectedIndex.Equals(1))
            {
                tpHome.SelectTab(tpBuy);//redirects to Buy page
                txtPurchPrice.Focus();
            }
        }
        #endregion

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        #region calcBuy button method to calculate monthly loan repayment
        private void btnCalcBuy_Click(object sender, EventArgs e)
        {
            double Price = Convert.ToDouble(txtPurchPrice.Text);
            double deposit = Convert.ToDouble(txtDeposit.Text);
            double interest = Convert.ToDouble(txtInterest.Text);
            double period = Convert.ToDouble(txtNumMonths.Text);

            monthlyLoanRepay = hl.CalcRepayment(Price, period, interest, deposit);

            LoanApproval(monthlyLoanRepay);
        }
        #endregion

        #region Method to make the necessary deductions when property is bought
        private void PropBuyDeduction()
        {
            decimal MoneyLeft;
            double expTot;//total expenses 
            expTot = hl.getTotal();
            MoneyLeft = (decimal)((income * tax / 100) - (expTot + monthlyLoanRepay));

            MessageBox.Show("Money left: R" + MoneyLeft);
        }
        #endregion

        #region Method to check if Loan is Approved
        private static void LoanApproval(double monthlyPay)
        {

            double thirdIncome = 0.3 * income;
            if (monthlyPay >= thirdIncome)
            {
                MessageBox.Show("Monthly loan payment is : R" + monthlyPay +
                    "\nA Third of users income: R" + thirdIncome + "\nHome Loan not approved");
            }
            else
            {
                MessageBox.Show("Home Loan approved");
            }
            BudgetApp ba = new BudgetApp();
            ba.PropBuyDeduction();

            Application.Exit();  //Ends the program
        }
        #endregion

        private void exitToolStripMenuItem_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {
            Application.Exit();
        }

        #region button to calculate rent amount and deductions
        private void btnCalcRent_Click(object sender, EventArgs e)
        {
            rentAmt = Convert.ToDouble(txtRentAmt.Text);
            decimal MoneyLeft;
            double expTot;//total expenses 
            expTot = hl.getTotal();
            MoneyLeft = (decimal)(income - (rentAmt + expTot));

            MessageBox.Show("Money left: R" + MoneyLeft);
            Application.Exit(); //Ends the program
        }
        #endregion

        private void tpAbout_Click(object sender, EventArgs e)
        {

        }

        private void mnuMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Application.Exit(); //Ends the program
        }
    }
}
