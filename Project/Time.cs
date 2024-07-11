using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Project
{
    [Serializable]
    public class Time
    {
        #region Fields
        private int hour;
        private int minute;
        private int second;
        #endregion Fields

        #region Constructors
        public Time(int hour, int minute, int second)
        {
            this.Hour = hour;
            this.Minute = minute;
            this.Second = second;
        }
        #endregion Constructors

        #region Properties
        public int Hour { get => hour; set => hour = value; }
        public int Minute { get => minute; set => minute = value; }
        public int Second { get => second; set => second = value; }
        #endregion Properties

        #region Methods
        public int Convert()
        {
            int totalSec = (Hour * 3600) + (Minute * 60) + (Second);
            return totalSec;
        }

        public void Add(int time)
        {
            int totalSec = Convert();
            int newTime = totalSec + time;

            Hour = newTime / 3600;
            Minute = (newTime % 3600) / 60;
            Second = (newTime % 3600) % 60;
        }
        public string Display()
        {
            string time = $"{Hour:D2} : {Minute:D2} : {Second:D2}";
            return time;
        }
        #endregion Methods
    }
}