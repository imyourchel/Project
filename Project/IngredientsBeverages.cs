using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace Project
{
    public class IngredientsBeverages
    {
        #region Fields
        private string name;
        private Image picture;
        private Image servePicture;
        #endregion Fields

        #region Constructors
        public IngredientsBeverages(string name, Image picture, Image servePicture)
        {
            this.Name = name;
            this.Picture = picture;
            this.ServePicture = servePicture;
        }
        #endregion Constructors

        #region Properties
        public string Name { get => name; set => name = value; }
        public Image Picture { get => picture; set => picture = value; }
        public Image ServePicture { get => servePicture; set => servePicture = value; }
        #endregion Properties

        #region Methods
        public string Display()
        {
            return this.Name;
        }
        #endregion Methods
    }
}