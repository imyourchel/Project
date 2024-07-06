using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Project
{
    public class Players
    {
        #region Fields        
        private string name;
        private int income;
        private Image picture;
        private Time time;
        private List<int> highScore;
        private List<Merchandise> stockMerchandise;
        private List<Time> bestTime;
        #endregion Fields       

        #region Constructors
        public Players(string name, int income, Image picture, Time time)
        {
            this.Name = name;
            this.Income = income;
            this.Picture = picture;
            this.Time = time;
            this.HighScore = new List<int>();
            this.StockMerchandise = new List<Merchandise>();
            this.BestTime = new List<Time>();
        }
        #endregion Constructors

        #region Properties
        public string Name 
        {
            get => name;
            set
            {
                if (value != "")
                {
                    name = value;
                }
                else
                {
                    throw (new ArgumentException("Please fill in the name textbox"));
                }
            }
        }
        public int Income { get => income; set => income = value; }
        public Image Picture
        {
            get => picture;
            set
            {
                if (value != null)
                {
                    picture = value;
                }
                else
                {
                    throw new Exception("Please choose the gender");
                }
            }
        }
        public Time Time { get => time; set => time = value; }
        public List<int> HighScore { get => highScore; set => highScore = value; }
        public List<Merchandise> StockMerchandise { get => stockMerchandise; set => stockMerchandise = value; }
        public List<Time> BestTime { get => bestTime; set => bestTime = value; }
        #endregion Properties

        #region Methods
        public string Display()
        {
            string data = "Player : " + this.Name + "\n" +
                          "Prev Income : " + this.Income + "\n" +
                          "Prev Time : " + this.Time.Display();
            return data;
        }
        #endregion Methods

    }
}