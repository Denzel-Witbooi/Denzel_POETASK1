using System;
using System.Collections.Generic;
using System.Text;

namespace PoeDesign
{
    abstract class Expenses
    {

        #region set arrays size
        public abstract void setArray(int num);
        public abstract int Size();
        #endregion

        #region add to arrays method
        public abstract bool addToArray(double dblGroceries,
             double dblWaterAndLights, double dblTravel,
             double dblPhone, double dblOtherExp);
        #endregion

        #region getters for expenses vars
        public abstract double getGroceries(int x);
        public abstract double getWateAndLights(int x);
        public abstract double getTravel(int x);
        public abstract double getPhone(int x);
        public abstract double getOtherExp(int x);
        #endregion
    }
}
