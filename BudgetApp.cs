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
        private static double income;    //Var to store gross monthly income
        private static double tax;       // Var to store monthly tax
        private static double monthlyLoanRepay;  //Var to store returned value of monthly loan repay method
        private static double rentAmt;           //Var to store rent amount
        #endregion

        #region initialising objects
        Budget b = new Budget();
        HomeLoan hl = new HomeLoan();
        ExpenseList<object> el = new ExpenseList<object>();
        Vehicle car = new Vehicle();
        #endregion

        public BudgetApp()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
        }

        #region Buttons that redirect to next page
        private void btnTAbout_Click(object sender, EventArgs e)
        {
            tpHome.SelectTab(tpAbout);
        }

        private void btnBHome_Click(object sender, EventArgs e)
        {
            tpHome.SelectTab(tcHome);
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
            #region try-catch when user enters no values/incorrect format
            try
            {
                double Price = Convert.ToDouble(txtPurchPrice.Text);
                double deposit = Convert.ToDouble(txtDeposit.Text);
                double interest = Convert.ToDouble(txtInterest.Text);
                double period = Convert.ToDouble(txtNumMonths.Text);

                monthlyLoanRepay = hl.CalcRepayment(Price, period, interest, deposit);

                LoanApproval(monthlyLoanRepay);

                ExpenseReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please enter the correct value and no text/symbols " +
                     "\n or leaving the textbox empty");
                Application.Exit();
            }
            #endregion
        }
        #endregion

        #region Method to make the necessary deductions when property is bought
        private void PropBuyDeduction()
        {
            double MoneyLeft;
            double expTot;//total expenses 
            expTot = el.getTotal();
            MoneyLeft = ((income * tax / 100) - (expTot + monthlyLoanRepay));

            ExpenseReport();

            MessageBox.Show("Money left: R" + MoneyLeft,"Buy Deduction Amount");
        }
        #endregion

        #region Method to check if Loan is Approved
        private static void LoanApproval(double monthlyPay)
        {

            double thirdIncome = (3/100) * income;
            if (monthlyPay >= thirdIncome)
            {
                MessageBox.Show("Home Loan not approved", "Approval");
                
                MessageBox.Show("Monthly loan payment is : R" + monthlyPay +
                    "\nA Third of users income: R" + thirdIncome);
                
            }
            else
            {
                MessageBox.Show("Home Loan approved","Approval");
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
            double MoneyLeft;
            double expTot;//total expenses 
            expTot = el.getTotal();
            MoneyLeft = income - (rentAmt + expTot);

            ExpenseReport();

            MessageBox.Show("Money left: R" + MoneyLeft,"Rent Deduction Amount");
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            lbExpenses.Items.Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (el.Push(txtExpName.Text, Convert.ToDecimal(txtExpCost.Text)) == false)
            {
                MessageBox.Show("Your Expenses list is full");
            }
            ItemsList();


            txtExpName.Focus();
            txtExpName.TabIndex = 0;
            txtExpCost.TabIndex = 1;
            btnAdd.TabIndex = 2;
            btnClear.TabIndex = 3;

        }

        private void ItemsList()
        {
            lbExpenses.Items.Clear();
            string strTemp = String.Format("{0} ,\t{1}", "Expense Name", "Costs");
            lbExpenses.Items.Add(strTemp);
            for (int x = 0; x < el.getCounter(); x++)
            {
                strTemp = String.Format("{0} ,\t \t R{1}", el.getExpName(x), el.getExpCost(x));
                lbExpenses.Items.Add(strTemp);
            }
            lblTotal.Text = "The total is: " + el.getTotal();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtExpName.Clear();
            txtExpCost.Clear();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            ItemsList();
        }

        private void btnAddFinances_Click(object sender, EventArgs e)
        {
            #region convert text box vars and store in new vars
            //try-catch for when user enters text/symbols or
            //leave the text box empty
            try
            {
                #region pass vars to set methods from Budget and HomeLoan class
                b.MonthlyIncome = income;
                b.MonthlyIncome = tax;
                el.setExpList(Convert.ToInt32(nudExpCount.Value.ToString()));

                lblTotal.Text = "The total is: " + el.getTotal();
                txtExpName.Focus();
                txtExpName.TabIndex = 0;
                txtExpCost.TabIndex = 1;
                btnAdd.TabIndex = 2;
                btnClear.TabIndex = 3;
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please enter the correct value and no text/symbols " +
                    "\n or leaving the textbox empty");
                Application.Exit();
            }
            #endregion



            tpHome.SelectTab(tpExp); //redirects to Accommodation page

            cbPropType.Focus(); //Place focus on combox box
        }

        private void btnBFinances_Click(object sender, EventArgs e)
        {
            tpHome.SelectTab(tpFinances);
        }

        private void btnNAcc_Click(object sender, EventArgs e)
        {
            tpHome.SelectTab(tpAccom);
        }

        private void ExpenseReport()
        {
            String strDisplay = "";
            for (int x = 0; x < el.getCounter(); x++)
            {
                strDisplay = el.getExpName(x) + "\t\t" + el.getExpCost(x);  
            }
            MessageBox.Show(strDisplay);
        }

        private void btnVehicleCalc_Click(object sender, EventArgs e)
        {
            double vehiclePurchase = Convert.ToDouble(txtCarPurchPrice.Text);
            double vehicleDeposit = Convert.ToDouble(txtCarTotDep.Text);
            double vehicleRate = Convert.ToDouble(txtCarInterestRate.Text);
            double vehicleInsurance = Convert.ToDouble(txtEstInsurancePrem.Text);
            double PERIOD = 5;

            car.dblTotalMonthlyCost(vehiclePurchase, PERIOD, vehicleRate, vehicleDeposit, vehicleInsurance);
        }

        private void cbVehicleOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbVehicleOption.SelectedIndex.Equals(0))
            {

            }
            else if(cbVehicleOption.SelectedIndex.Equals(1))
            {
                tpHome.SelectTab(tpVehicleBuy);
            }
        }
    }
}
