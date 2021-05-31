using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace PoeDesign
{
   
    class ExpenseList<T>: Expenses
    {
        private string path = System.IO.Path.GetFullPath(@"..\..\..\") + "Expenses.txt";
        private static string[] ExpenseName;
        private static double[] ExpenseCost;
        private static int stackPointer = 0;
        private string strDisplay;
        private static int expSize;
        private static double Total = 0;
        private string Display;
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
                save();
                stackPointer++; //incrementing stackpointer
                return true;
            }
            else
            {
                return false;
            }

        }

        /*
* Author: Reece Wanvig
* Date: 5 May 2021
* Title of Source code:Save values to text file
* Code version:
*   private void save()
        {
            try
            {
                if(! File.Exists(path))
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
                else
                {
                    using (StreamWriter sw = new StreamWriter(path, true))
                    {
                        sw.WriteLine(StudentID[stackPointer]);//writig to the file 
                        sw.WriteLine(Test[stackPointer]);
                        sw.WriteLine(Assignment[stackPointer]);
                        sw.WriteLine(ICE[stackPointer]);
                        sw.WriteLine(Exam[stackPointer]);
                        sw.Close();//closeing the text file
                    }
                }
            }
            catch ( Exception ex)
            {
                MessageBox.Show("An error ocured " + ex.ToString());
            }
        }
*  #endregion
*  Type: WindowsForms (.NetCore)
*  URL: https://github.com/VCNMB-2021C/TextFilesGenericClasses.git
*/
        private void save()
        {
            try
            {
                if (!File.Exists(path))
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine("Electricity"); //adding values
                        sw.WriteLine(1500);

                        sw.WriteLine("Petrol"); //adding values
                        sw.WriteLine(2500);
                        sw.Close(); //closing the text file
                    }
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(path,true))
                    {
                        sw.WriteLine(ExpenseName[stackPointer]);
                        sw.WriteLine(ExpenseCost[stackPointer]);
                        sw.Close(); //closing the text file
                    }
                }
            }
            catch(Exception err)
            {
                MessageBox.Show("An error occurred " + err.ToString());
            }
        }

        /*
* Author: Reece Wanvig
* Date: 5 May 2021
* Title of Source code:Save values to text file
* Code version:
*    public string read()
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
*  Type: WindowsForms (.NetCore)
*  URL: https://github.com/VCNMB-2021C/TextFilesGenericClasses.git
*/
        public string read()
        {
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("Electricity"); //adding values
                    sw.WriteLine(1500);

                    sw.WriteLine("Petrol"); //adding values
                    sw.WriteLine(2500);
                    sw.Close(); //closing the text file
                }
            }
            Display = "";

            try
            {
                StreamReader sr = new StreamReader(path, true);
                for (int x = 0; x != File.ReadLines(path).Count()/2; x++)
                {
                    ExpenseName[x] = sr.ReadLine(); // reading from text file
                    ExpenseCost[x] = Convert.ToDouble(sr.ReadLine());

                    Display += "Expense name - " + ExpenseName[x] + "\n";
                    //printing with the generic class
                    Display += "Amount - " + ExpenseCost[x] + "\n";
                }
                sr.Close();
            }
            catch (Exception err)
            {

                MessageBox.Show("An error occurred " + err.ToString());
            }
            return Display;
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
