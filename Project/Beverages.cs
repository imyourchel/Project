using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace Project
{
    public class Beverages : Items
    {
        #region Fields
        private bool isCold;
        private string size;
        private List<IngredientsBeverages> listOfIngredients;
        #endregion Fields

        #region Constructors
        public Beverages(bool isCold, string size, string name, Image picture, int price) : base(name, picture, price)
        {
            this.IsCold = isCold;
            this.Size = size;
            this.ListOfIngredients = new List<IngredientsBeverages>();
        }
        #endregion Constructors

        #region Properties
        public bool IsCold { get => isCold; set => isCold = value; }
        public string Size { get => size; set => size = value; }
        public List<IngredientsBeverages> ListOfIngredients { get => listOfIngredients; set => listOfIngredients = value; }
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
            foreach (IngredientsBeverages i in this.ListOfIngredients)
            {
                data = data + i.Display() + "\n";
            }
            return data;
        }
        public void AddingIngredients(string name, Image picture)
        {
            IngredientsBeverages ingredient = new IngredientsBeverages(name, picture);
            listOfIngredients.Add(ingredient);
        }
        #endregion Methods
    }
}