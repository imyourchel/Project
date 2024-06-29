using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace Project
{
    public class Customers
    {
        #region Fields
        private string name;
        private Image picture;
        private string type;
        private Items orderItem;
        #endregion Fields

        #region Constructors
        public Customers(string name, Image picture, string type, Items orderItem)
        {
            this.Name = name;
            this.Picture = picture;
            this.Type = type;
            this.OrderItem = orderItem;
        }
        #endregion Constructors

        #region Properties
        public string Name { get => name; set => name = value; }
        public Image Picture { get => picture; set => picture = value; }
        public string Type { get => type; set => type = value; }
        public Items OrderItem { get => orderItem; set => orderItem = value; }
        #endregion Properties

        #region Methods
        public string Display()
        {
            string data = "";
            if (type == "male")
            {
                data = "Haii! I'm " + this.Name;
            }
            else if (type == "female")
            {
                data = "Hai! I'm " + this.Name + "\nI'd like to order item please.. thank you";
            }
            else if (type == "kid")
            {
                data = "Morning.. I'm " + this.Name;
            }
            return data;
        }
        #endregion Methods
    }
}