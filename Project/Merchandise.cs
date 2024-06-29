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
        private int stock;
        #endregion Fields

        #region Constructors
        public Merchandise(int stock, string name, Image picture, int price) : base(name, picture, price)
        {
            this.Stock = stock;
        }
        #endregion Constructors

        #region Properties
        public int Stock { get => stock; set => stock = value; }
        #endregion Properties

        #region Methods
        public override string Display()
        {
            throw new System.NotImplementedException();
        }

        public void Sell()
        {
            this.Stock = this.Stock - 1;
        }
        #endregion Methods
    }
}