using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Project
{
    public class Merchandise : Items
    {
        #region Fields
        private List<bool> listStock;
        #endregion Fields

        #region Constructors
        public Merchandise(List<bool> stock, string name, Image picture, int price) : base(name, picture, price)
        {
            this.ListStock = stock;
        }
        #endregion Constructors

        #region Properties
        public List<bool> ListStock { get => listStock; set => listStock = value; }
        #endregion Properties

        #region Methods
        public override string Display()
        {
            throw new System.NotImplementedException();
        }

        public void Sell(int i)
        {
            this.ListStock[i] = false;
        }
        #endregion Methods
    }
}