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
        #region var for text file path
        private string path = System.IO.Path.GetFullPath(@"..\..\..\") + "Expenses.txt";
        #endregion
        #region vars for expense name and cost
        private static string[] ExpenseName;
        private static double[] ExpenseCost;
        #endregion

        #region vars for total, array amount and counter
        private static int stackPointer = 0;
        private static int expSize;
        private static double Total = 0;
        #endregion

        #region method to set array size
        public void setExpList(int size)
        {
            ExpenseName = new string[size];
            ExpenseCost = new double[size];
            expSize = size;
        }
        #endregion

        #region method to add to the array while values not null/array full
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
                SortExpenses();
                stackPointer++; //incrementing stackpointer
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region method save the values in each array into a text file
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
        #endregion

        #region getters for both arrays
        public string getExpName(int x)
        {
            return ExpenseName[x];
        }
        public double getExpCost(int x)
        {
            return ExpenseCost[x];
        }
        #endregion

        #region methods for the counter and array size and total cost expenses
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
        #endregion

        #region method to sort expenses in descending according to cost
        /*
        * Author: deathismyfriend
        * URL: https://stackoverflow.com/users/2860193/deathismyfriend
        * 
        * code version: 
        *       public void Print()
                {
                    Array.Sort(nScore, nPlayer);
                    Array.Reverse(nScore);
                    Array.Reverse(nPlayer);
                    PrintKeysAndValues(nPlayer, nScore);
                }
        Type: c# (console)
        * URL for Question: 
        * https://stackoverflow.com/questions/30967867/sort-2-arrays-in-reverse-in-c-sharp
        */
        public void SortExpenses()
        {
            //Sorts array based on expense cost. 
            //Then reverses both arrays to keep data in line
            Array.Sort(ExpenseCost, ExpenseName); 
            Array.Reverse(ExpenseCost);
            Array.Reverse(ExpenseName);
        }

        public void PrintSort()
        {
            string strDisplay = "";
            strDisplay += "Expense List \n";
            strDisplay += "*****************************\n";
            strDisplay += "Expense Name \t Cost\n";

            for (int i = 0; i < ExpenseCost.Length; i++)
            {
                strDisplay += ExpenseName[i] + "\t \tR" + ExpenseCost[i] + "\n";
            }
            MessageBox.Show(strDisplay);
        }

        #endregion

    }
}
