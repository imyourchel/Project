using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Project
{
    public class Beverages : Items
    {
        #region Fields
        private bool isCold;
        private string size;
        #endregion Fields

        #region Constructors
        public Beverages(bool isCold, string size, string name, Image picture, int price) : base(name, picture, price)
        {
            this.IsCold = isCold;
            this.Size = size;
        }
        #endregion Constructors

        #region Properties
        public bool IsCold { get => isCold; set => isCold = value; }
        public string Size { get => size; set => size = value; }
        #endregion Properties

        #region Methods
        public override string Display()
        {
            string temp = "";
            if (isCold == true)
            {
                temp = "Cold";
            }
            else
            {
                temp = "Hot";
            }
            string data = base.DisplayItems() + "\n" + temp + "\n" +
                "Size : " + this.Size;
            return data;
        }
        public void AddingIngredients(string name, Image picture)
        {
            Ingredients ingredient = new Ingredients(name, picture);
            //listOfIngredients.Add(ingredient);
        }
        #endregion Methods
    }
}