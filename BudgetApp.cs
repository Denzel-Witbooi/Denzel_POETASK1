using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
                                                                                   
namespace PoeDesign
{
   
    public partial class BudgetApp : Form

    {
        public delegate void MentionLimit(double total); // delegate to mention limit
        private string path = System.IO.Path.GetFullPath(@"..\..\..\") + "Expenses.txt"; //File path
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

        private void btnAddFinances_Click(object sender, EventArgs e)
        {
            AddFinances();
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
            txtIncome.Focus();
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
                if (txtPurchPrice.Text != null && txtDeposit.Text != null  
                    && txtInterest.Text != null && txtNumMonths.Text != null)
                {
                    #region convert Prop buy text boxes to double
                    double Price = Convert.ToDouble(txtPurchPrice.Text);
                    double deposit = Convert.ToDouble(txtDeposit.Text);
                    double interest = Convert.ToDouble(txtInterest.Text);
                    double period = Convert.ToDouble(txtNumMonths.Text);
                    #endregion
                    monthlyLoanRepay = hl.CalcRepayment(Price, period, interest, deposit);

                    LoanApproval(monthlyLoanRepay);
                }
                
          
            }
            catch (Exception ex)                                    
            {
                MessageBox.Show(ex.ToString());
            }
            #endregion
            tpHome.SelectTab(tpVehicle); //redirects to Vehicle Option page

            cbVehicleOption.Focus();
        }
        #endregion

        #region Method to make the necessary deductions when property is bought
        private void PropBuyDeduction()
        {
            double MoneyLeft;
            double expTot;//total expenses 
            expTot = el.getTotal();
            MoneyLeft = ((b.MonthlyIncome * tax / 100) - (expTot + monthlyLoanRepay));

            el.PrintSort();

            MessageBox.Show("Money left: R" + MoneyLeft,"Buy Deduction Amount");

        }
        #endregion

        #region Method to check if Loan is Approved
        private void LoanApproval(double monthlyPay)
        {
            MessageBox.Show("Checking Loan status....");
            double thirdIncome = (3/100) * b.MonthlyIncome;
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
        }
        #endregion

        private void exitToolStripMenuItem_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {
            Application.Exit();
        }

        #region button to calculate rent amount and deductions
        private void btnCalcRent_Click(object sender, EventArgs e)
        {
            double MoneyLeft;
            double expTot;//total expenses 
            expTot = el.getTotal();
            MoneyLeft = b.MonthlyIncome - (rentAmt + expTot);

            el.PrintSort();

            MessageBox.Show("Money left: R" + MoneyLeft,"Rent Deduction Amount");

            tpHome.SelectTab(tpVehicleBuy);
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
            if (el.Push(txtExpName.Text, Convert.ToDouble(txtExpCost.Text)) == false)
            {
                MessageBox.Show("Your Expenses list is full");
            }
            ItemsList();

            #region tab position expense page/tab
            txtExpName.Focus();
            txtExpName.TabIndex = 0;
            txtExpCost.TabIndex = 1;
            btnAdd.TabIndex = 2;
            btnClear.TabIndex = 3;
            #endregion
        }

        #region Method To Add expenses to ListBox
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
        #endregion
       
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtExpName.Clear();
            txtExpCost.Clear();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            read();
        }

        #region method to read from textfile to list box
        /*
* Author: Reece Wanvig
* Date: 5 May 2021
* Title of Source code: Read values to text file
* Code version:
*   public string read()
        {
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("18017262");//adding values 
                    sw.WriteLine(99);
                    sw.WriteLine(70);
                    sw.WriteLine(98);
                    sw.WriteLine(100);

                    sw.WriteLine("18000612");//adding values 
                    sw.WriteLine(99);
                    sw.WriteLine(70);
                    sw.WriteLine(98);
                    sw.WriteLine(100);
                    sw.Close();//closeing the text file
                }
            }
            Display = "";

            try
            {
                StreamReader sr = new StreamReader(path, true);
                for (int x =0;x!= File.ReadLines(path).Count()/5;x++)
                {
                    StudentID[x] = sr.ReadLine();//reading from the text file 
                    Test[x] = sr.ReadLine();
                    Assignment[x] = sr.ReadLine();
                    ICE[x] = sr.ReadLine();
                    Exam[x] = sr.ReadLine();

                    Display += "ID - " + StudentID[x] + "\n";
                    //printing with the generic class 
                    Display += "Test - " + Test[x] + "\n";
                    Display += "Assignment - " + Assignment[x] + "\n";
                    Display += "ICE - " + ICE[x] + "\n";
                    Display += "Exam - " + Exam[x] + "\n";
                    Display += "Final - " + ((Convert.ToDouble(Test[x]) * 0.25) +
                        (Convert.ToDouble(Assignment[x]) * 0.35) +
                        (Convert.ToDouble(ICE[x]) * 0.10) +
                        (Convert.ToDouble(Exam[x]) * 0.30)) + "\n\n";
                    //work out the final mark
                }
                sr.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("An error ocured " + ex.ToString());
            }
            return Display;
        }
    } 
*  Type: WindowsForms (.NetCore)
*  URL: https://github.com/VCNMB-2021C/TextFilesGenericClasses.git
*/

        public void read()
        {
            lbExpenses.Items.Clear();
            string strTemp = String.Format("{0} ,\t{1}", "Expense Name", "Costs");
            lbExpenses.Items.Add(strTemp);

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    strTemp = String.Format("{0} ,\t \t R{1}", "Exp", 2000);

                    lbExpenses.Items.Add(strTemp);
                    lblTotal.Text = "The total is: " + 2000;
                }
            }
            try
            {
                StreamReader sr = new StreamReader(path, true);
                for (int x = 0; x != File.ReadLines(path).Count() / 2; x++)
                {
                    string expn = el.getExpName(x);
                    double expc = el.getExpCost(x);
                    expn = sr.ReadLine(); // reading from text file
                    expc = Convert.ToDouble(sr.ReadLine());

                    strTemp = String.Format("{0} ,\t \t R{1}", expn, expc);
                    lbExpenses.Items.Add(strTemp);
                }
                lblTotal.Text = "The total is: " + el.getTotal();
                sr.Close();
            }
            catch (Exception err)
            {

                MessageBox.Show("An error occurred " + err.ToString());
            }
        }
        #endregion


        private void btnBFinances_Click(object sender, EventArgs e)
        {
            tpHome.SelectTab(tpFinances);
        }

        private void btnNAcc_Click(object sender, EventArgs e)
        {
            tpHome.SelectTab(tpAccom);
        }

        private void btnVehicleCalc_Click(object sender, EventArgs e)
        {
            #region try-catch for vehicle tab
            try
            {
                if (txtCarPurchPrice.Text != null && txtCarTotDep.Text != null &&
                    txtCarInterestRate.Text != null && txtEstInsurancePrem.Text != null 
                    && txtPeriod.Text != null)
                {
                    #region convert text boxes to double
                    double vehiclePurchase = Convert.ToDouble(txtCarPurchPrice.Text);
                    double vehicleDeposit = Convert.ToDouble(txtCarTotDep.Text);
                    double vehicleRate = Convert.ToDouble(txtCarInterestRate.Text);
                    double vehicleInsurance = Convert.ToDouble(txtEstInsurancePrem.Text);
                    double PERIOD = Convert.ToDouble(txtPeriod.Text);
                    #endregion


                    car.dblTotalMonthlyCost(vehiclePurchase, PERIOD, vehicleRate, vehicleDeposit, vehicleInsurance);
                    GenerateLimit(ExpLimitAlert);//Alert user when expenses < 
                    el.PrintSort();
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.ToString());
            }
            #endregion

        }
        #region method to alert user when expense > income*0.75 
        private static void ExpLimitAlert(double expTotal)
        {
            MessageBox.Show("Your expenses have exceeded your income by 75%");
        }
        #endregion

        #region Method takes in a delegate check if expense > income + propBuy + carPay* 0.75
        public double GenerateLimit(MentionLimit mentionLimit)
        {
            double totExp = el.getTotal();
            double propBuy = hl.GetMonthlyPay();
            double carPay = car.GetMonthlyCost();

            if (totExp > ((income + propBuy + carPay) * 0.75))
            {
                mentionLimit(totExp);
            }
            return totExp;
        }
        #endregion

        #region Add Items from Finances tab
        private void AddFinances()
        {
            
            #region convert text box vars and store in new vars
            //try-catch for when user enters text/symbols or
            //leave the text box empty
            try
            {
                #region pass vars to set methods from Budget and HomeLoan class

                if (txtIncome.Text != null && txtTax.Text != null)
                {
                    income = Convert.ToDouble(txtIncome.Text);
                    tax = Convert.ToDouble(txtTax.Text);
                    b.MonthlyIncome = income;
                    b.MonthlyTax = tax;
                }


                el.setExpList(Convert.ToInt32(nudExpCount.Value.ToString()));

                lblTotal.Text = "The total is: " + el.getTotal();
                #region tab position for finances tab
                txtExpName.Focus();
                txtExpName.TabIndex = 0;
                txtExpCost.TabIndex = 1;
                btnAdd.TabIndex = 2;
                btnClear.TabIndex = 3;
                #endregion

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please enter the correct value and no text/symbols " +
                    "\n or leaving the textbox empty" + "\n" + ex.ToString());
            }
            #endregion

            tpHome.SelectTab(tpExp); //redirects to Accommodation page

            cbPropType.Focus(); //Place focus on combox box
        }
        #endregion

        private void cbVehicleOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbVehicleOption.SelectedIndex.Equals(0))
            {
                el.PrintSort();
                Application.Exit();
            }
            else if(cbVehicleOption.SelectedIndex.Equals(1))
            {
                tpHome.SelectTab(tpVehicleBuy);
               
                #region tab position for vehicle buy tab
                txtModel.Focus();
                txtModel.TabIndex = 0;
                txtMake.TabIndex = 1;
                txtCarPurchPrice.TabIndex = 2;
                txtCarTotDep.TabIndex = 3;
                txtCarInterestRate.TabIndex = 4;
                txtPeriod.TabIndex = 5;
                txtEstInsurancePrem.TabIndex = 6;
                #endregion
            }
        }

        #region Displays expenses in message box
        private void ExpenseReport()
        {
            String strDisplay = "";
            strDisplay += "Expense List \n";
            strDisplay += "*****************************\n";
            strDisplay += "Expense Name \t Cost\n";

            for (int x = 0; x < el.size(); x++)
            {
                strDisplay += el.getExpName(x) + "\t\t R" + el.getExpCost(x) + "\n";
            }
            MessageBox.Show(strDisplay);
        }
        #endregion
    }
}
