using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Project
{
    public class Foods : Items
    {
        #region Fields
        private List<IngredientsFoods> listOfIngredients;
        #endregion Fields

        #region Constructors        
        public Foods(string name, Image picture, int price) : base(name, picture, price)
        {
            this.ListOfIngredients = new List<IngredientsFoods>();
        }
        #endregion Constructors

        #region Properties
        public List<IngredientsFoods> ListOfIngredients { get => listOfIngredients; set => listOfIngredients = value; }
        #endregion Properties

        #region Methods
        public override string Display()
        {
            string data = base.DisplayItems() + "\n" +
                "Ingredients : " + "\n";
            foreach (IngredientsFoods i in this.ListOfIngredients)
            {
                data = data + i.Display() + "\n";
            }
            return data;
        }

        public void AddingIngredients(string name, Image picture, Image serve)
        {
            IngredientsFoods ingredient = new IngredientsFoods(name, picture,serve);
            listOfIngredients.Add(ingredient);
        }
        #endregion Methods
    }
}