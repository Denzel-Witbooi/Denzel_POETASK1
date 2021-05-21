using System;
using System.Collections.Generic;
using System.Text;

namespace PoeDesign
{
    abstract class Expenses
    {

        #region Display Expenses
        public abstract string Display();
        #endregion

        #region pushes to the List
        public abstract bool Push(object Names, object Costs);
        #endregion


    }
}
