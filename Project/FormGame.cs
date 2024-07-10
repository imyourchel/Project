using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class FormGame : Form
    {
        Players player;
        Time time;
        List<Players> listOfPlayer = new List<Players>();
        List<Items> listOfItems = new List<Items>();
        Items item;
        Customers customers;

        int custEasy = 8;
        int custMedium = 12;
        int custHard = 18;
        int custImpossible = 25;
        int remainingCusts;

        int timeChoose;
        int timeEasy = 30;
        int timeMedium = 40;
        int timeHard = 50;
        int timeImpossible = 60;

        bool easy;
        bool medium;
        bool hard;
        bool impossible;

        int selectedIngCount;
        int incTimerCust;
        public FormGame()
        {
            InitializeComponent();
        }
        private void BackgroundVisible()
        {
            this.BackgroundImage = Properties.Resources.background;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            buttonExit.Visible = true;
            buttonPlay.Visible = true;            
        }
        private void BackgroundInvisible()
        {
            this.BackgroundImage = null;
            buttonExit.Visible = false;
            buttonPlay.Visible = false;             

        }
        private void FormGame_Load(object sender, EventArgs e)
        {
            BackgroundVisible();
            panelCreateLoadPlayer.Visible = false;
            panelDifficulty.Visible = false;
            panelTutorial.Visible = false;
            panelGame.Visible = false;
            panelSetting.Visible = false;
            panelWin.Visible= false;
            panelLose.Visible = false;
            timerCust.Enabled = false;
            timerCust.Interval= 1000;
            timerGame.Enabled = false;
            timerGame.Interval= 1000;
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            radioButtonLoadPlayer.Checked = false;
            radioButtonMale.Checked = false;
            radioButtonFemale.Checked = false;
            textBoxNameCreate.Clear();
            BackgroundInvisible();
            //display panel create load player
            panelCreateLoadPlayer.Visible = true;
            panelCreateLoadPlayer.BackgroundImage = Properties.Resources.bg_CreateLoadPlayer;
            panelCreateLoadPlayer.BackgroundImageLayout = ImageLayout.Stretch;
            //create player
            radioButtonCreatePlayer.Checked = true;
            panelLoadPlayer.Enabled = false;
            radioButtonMale.Enabled = true;
            //combobox
            if (listOfPlayer.Count == 0)
            {
                radioButtonLoadPlayer.Enabled = false;
            }
            comboBoxNameLoad.DataSource = listOfPlayer;
            comboBoxNameLoad.DisplayMember = "Name";
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit the game?", "Exit Message", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void pictureBoxBackHome_Click(object sender, EventArgs e)
        {
            BackgroundVisible();
            panelCreateLoadPlayer.Visible = false;
        }
        private void buttonNextDifficult_Click(object sender, EventArgs e)
        {
            try
            {                
                //create Player
                if (radioButtonCreatePlayer.Checked)
                {
                    Image pic = null;
                    if(radioButtonMale.Checked)
                    {
                        pic = Properties.Resources.male;
                    }
                    else if (radioButtonFemale.Checked)
                    {
                        pic = Properties.Resources.female;
                    }
                    //default value
                    player = new Players(textBoxNameCreate.Text, 0, pic);
                    player.HighScore = new List<int>{ 0,0,0,0};
                    time = new Time(0,0,0);                    
                    player.BestTime = new List<Time> { time,time,time,time};
                    player.PrevTime = new List<Time> { time,time,time,time};                                                            
                }
                //Load Player
                else
                {
                    player = (Players)comboBoxNameLoad.SelectedItem;
                }
                //display Panel Difficulty
                panelCreateLoadPlayer.Visible = false;
                panelDifficulty.Visible = true;
                panelDifficulty.BackgroundImage = Properties.Resources.bg_Difficulty;
                panelDifficulty.BackgroundImageLayout = ImageLayout.Stretch;

                this.labelEasy.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.labelMedium.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.labelHard.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.labelImpossible.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                
                this.labelHighScoreEasy.Text = player.HighScore[0].ToString();
                this.labelHighScoreMedium.Text = player.HighScore[1].ToString();
                this.labelHighScoreHard.Text = player.HighScore[2].ToString();
                this.labelHighScoreImpossible.Text = player.HighScore[3].ToString();

                this.labelBestTimeEasy.Text = player.BestTime[0].Display();
                this.labelBestTimeMedium.Text = player.BestTime[1].Display();
                this.labelBestTimeHard.Text = player.BestTime[2].Display();
                this.labelBestTimeImpossible.Text = player.BestTime[3].Display();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"ERROR");
            }
        }

        private void pictureBoxBackCreateLoadPlayer_Click(object sender, EventArgs e)
        {
            panelCreateLoadPlayer.Visible = true;
            panelDifficulty.Visible = false;
        }

        private void buttonNextTutorial_Click(object sender, EventArgs e)
        {
            try
            {
                if (labelEasy.Font.Name == "Franklin Gothic Demi")
                {
                    easy = true;
                    medium = false;
                    hard = false;
                    impossible = false;
                    timeChoose = timeEasy;
                    remainingCusts = custEasy;
                }
                else if (labelMedium.Font.Name == "Franklin Gothic Demi")
                {
                    easy = false;
                    medium = true;
                    hard = false;
                    impossible = false;
                    timeChoose = timeMedium;
                    remainingCusts = custMedium;
                }
                else if (labelHard.Font.Name == "Franklin Gothic Demi")
                {
                    easy = false;
                    medium = false;
                    hard = true;
                    impossible = false;
                    timeChoose = timeHard;
                    remainingCusts = custHard;
                }
                else if (labelImpossible.Font.Name == "Franklin Gothic Demi")
                {
                    easy = false;
                    medium = false;
                    hard = false;
                    impossible = true;
                    timeChoose = timeImpossible;
                    remainingCusts = custImpossible;
                }
                else
                {
                    throw (new ArgumentException("Select Difficulty Level"));
                }
                buttonBackReceipe.Visible = false;
                panelDifficulty.Visible = false;
                panelTutorial.Visible = true;
                panelTutorial.BackgroundImage = Properties.Resources.bg_Tutorial;
                panelTutorial.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"ERROR");
            }
        }

        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            panelTutorial.Visible = false;
            panelGame.Visible = true;
            panelGame.BackgroundImageLayout = ImageLayout.Stretch;
            time = new Time(0, 0, timeChoose);
            labelRemainingTime.Text = time.Display();
            timerGame.Start();
            labelRemainingCustomers.Text = "Remaining Customers : " + remainingCusts.ToString();
            labelNamePlayer.Text = player.Name;            
            StallDisplay();            

            //Selected level
            int selected;
            if (easy)
            {
                selected = 0;
            }
            else if (medium)
            {
                selected = 1;
            }
            else if (hard)
            {
                selected = 2;
            }
            else 
            { 
                selected = 3; 
            }
            labelDisplayDataPlayer.Text = player.Display(selected);
            labelIncomeNow.Text = player.Income.ToString();

            //tes
            CreateCustomers();
            //Items item = (Items)customers.OrderItem;
            //Merchandise merch = (Merchandise)item;
            //player.StockMerchandise = new List<Merchandise> { merch, merch, merch };
            //label1.Text = player.StockMerchandise[0].Stock.ToString();
        }        
        private void pictureBoxButtonReceipe_Click(object sender, EventArgs e)
        {
            panelGame.Visible = false;
            panelTutorial.Visible=true;
            buttonStartGame.Visible = false;
            buttonBackReceipe.Visible = true;
        }

        private void buttonBackReceipe_Click(object sender, EventArgs e)
        {
            panelTutorial.Visible = false;
            panelGame.Visible=true;
        }       

        private void pictureBoxButtonSetting_Click(object sender, EventArgs e)
        {
            timerGame.Stop();
            panelSetting.Visible = true;
            panelGame.Visible=false;
        }

        private void pictureBoxResume_Click(object sender, EventArgs e)
        {
            timerGame.Start();
            panelSetting.Visible = false;
            panelGame.Visible = true;
        }

        private void radioButtonCreatePlayer_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCreatePlayer.Checked == true)
            {
                panelLoadPlayer.Enabled = false;
                panelCreatePlayer.Enabled = true;
            }
            else if (radioButtonLoadPlayer.Checked == true)
            {
                panelLoadPlayer.Enabled = true;
                panelCreatePlayer.Enabled = false;
            }
        }

        private void radioButtonLoadPlayer_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCreatePlayer.Checked == true)
            {
                panelLoadPlayer.Enabled = false;
                panelCreatePlayer.Enabled = true;
            }
            else if (radioButtonLoadPlayer.Checked == true)
            {
                panelLoadPlayer.Enabled = true;
                panelCreatePlayer.Enabled = false;
            }
        }
        public void SaveToFile()
        {
            FileStream myFile = new FileStream("PlayerData", FileMode.Create, FileAccess.Write);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(myFile, listOfPlayer);
            myFile.Close();
        }
        public void ReadFromFile()
        {
            if (File.Exists("PlayerData"))
            {
                FileStream myFile = new FileStream("PlayerData", FileMode.Open, FileAccess.Read);
                BinaryFormatter formatter = new BinaryFormatter();
                listOfPlayer = (List<Players>)formatter.Deserialize(myFile);
                myFile.Close();
            }
        }


        private void StallDisplay()
        {
            #region Foods
            item = new Foods("burger", Properties.Resources.burger, 50);
            listOfItems.Add(item);
            ((Foods)item).AddingIngredients("plate", Properties.Resources.plates);
            ((Foods)item).AddingIngredients("bottombun", Properties.Resources.bottomBun);
            ((Foods)item).AddingIngredients("patty", Properties.Resources.patty);
            ((Foods)item).AddingIngredients("cheese", Properties.Resources.cheese);
            ((Foods)item).AddingIngredients("tomato", Properties.Resources.tomato);
            ((Foods)item).AddingIngredients("lettuce", Properties.Resources.lettuce);
            ((Foods)item).AddingIngredients("topbun", Properties.Resources.topBun);

            pictureBoxPlate.Tag = ((Foods)item).ListOfIngredients[0].Name;
            pictureBoxBottomBun.Tag = ((Foods)item).ListOfIngredients[1].Name;
            pictureBoxPatty.Tag = ((Foods)item).ListOfIngredients[2].Name;
            pictureBoxCheese.Tag = ((Foods)item).ListOfIngredients[3].Name;
            pictureBoxTomato.Tag = ((Foods)item).ListOfIngredients[4].Name;
            pictureBoxLettuce.Tag = ((Foods)item).ListOfIngredients[5].Name;
            pictureBoxTopBun.Tag = ((Foods)item).ListOfIngredients[6].Name;

            pictureBoxPlate.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxBottomBun.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxPatty.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxCheese.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxTomato.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxLettuce.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxTopBun.SizeMode = PictureBoxSizeMode.StretchImage;


            item = new Foods("salad", Properties.Resources.salad, 25);
            listOfItems.Add(item);
            ((Foods)item).AddingIngredients("plate", Properties.Resources.plates);
            ((Foods)item).AddingIngredients("lettuce", Properties.Resources.lettuce);
            ((Foods)item).AddingIngredients("mayo", Properties.Resources.mayo);

            pictureBoxPlate.Tag = ((Foods)item).ListOfIngredients[0].Name;
            pictureBoxLettuce.Tag = ((Foods)item).ListOfIngredients[1].Name;
            pictureBoxMayo.Tag = ((Foods)item).ListOfIngredients[2].Name;

            pictureBoxPlate.Image = ((Foods)item).ListOfIngredients[0].Picture;
            pictureBoxLettuce.Image = ((Foods)item).ListOfIngredients[1].Picture;
            pictureBoxMayo.Image = ((Foods)item).ListOfIngredients[2].Picture;

            pictureBoxPlate.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxLettuce.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxMayo.SizeMode = PictureBoxSizeMode.StretchImage;


            item = new Foods("icecream", Properties.Resources.iceCream, 10);
            listOfItems.Add(item);
            ((Foods)item).AddingIngredients("cone", Properties.Resources.cone);
            ((Foods)item).AddingIngredients("ice", Properties.Resources.icMachine);

            pictureBoxCone.Tag = ((Foods)item).ListOfIngredients[0].Name;
            pictureBoxICMachine.Tag = ((Foods)item).ListOfIngredients[1].Name;

            pictureBoxCone.Image = ((Foods)item).ListOfIngredients[0].Picture;
            pictureBoxICMachine.Image = ((Foods)item).ListOfIngredients[1].Picture;

            pictureBoxCone.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxICMachine.SizeMode = PictureBoxSizeMode.StretchImage;
            #endregion Foods

            #region Beverages
            item = new Beverages(false, "L", "coffeeLHot", Properties.Resources.hotL, 25);
            listOfItems.Add(item);
            ((Beverages)item).AddingIngredients("cup", Properties.Resources.beverages);            
            ((Beverages)item).AddingIngredients("coffee", Properties.Resources.textL);

            pictureBoxBeverage.Tag = ((Beverages)item).ListOfIngredients[0].Name;
            pictureBoxBevL.Tag = ((Beverages)item).ListOfIngredients[1].Name;

            pictureBoxBeverage.Image = ((Beverages)item).ListOfIngredients[0].Picture;
            pictureBoxBevL.Image = ((Beverages)item).ListOfIngredients[1].Picture;

            pictureBoxBeverage.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxBevL.SizeMode = PictureBoxSizeMode.StretchImage;

            item = new Beverages(false, "M", "coffeeMHot", Properties.Resources.hotM, 20);
            listOfItems.Add(item);
            ((Beverages)item).AddingIngredients("cup", Properties.Resources.beverages);
            ((Beverages)item).AddingIngredients("coffee", Properties.Resources.textM);

            pictureBoxBeverage.Tag = ((Beverages)item).ListOfIngredients[0].Name;
            pictureBoxBevM.Tag = ((Beverages)item).ListOfIngredients[1].Name;

            pictureBoxBeverage.Image = ((Beverages)item).ListOfIngredients[0].Picture;
            pictureBoxBevM.Image = ((Beverages)item).ListOfIngredients[1].Picture;

            pictureBoxBeverage.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxBevM.SizeMode = PictureBoxSizeMode.StretchImage;

            item = new Beverages(false, "S", "coffeeSHot", Properties.Resources.hotS, 15);
            listOfItems.Add(item);
            ((Beverages)item).AddingIngredients("cup", Properties.Resources.beverages);
            ((Beverages)item).AddingIngredients("coffee", Properties.Resources.textS);

            pictureBoxBeverage.Tag = ((Beverages)item).ListOfIngredients[0].Name;
            pictureBoxBevS.Tag = ((Beverages)item).ListOfIngredients[1].Name;

            pictureBoxBeverage.Image = ((Beverages)item).ListOfIngredients[0].Picture;
            pictureBoxBevS.Image = ((Beverages)item).ListOfIngredients[1].Picture;

            pictureBoxBeverage.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxBevS.SizeMode = PictureBoxSizeMode.StretchImage;

            item = new Beverages(true, "L", "coffeeLCold", Properties.Resources.coldL, 25);
            listOfItems.Add(item);
            ((Beverages)item).AddingIngredients("cup", Properties.Resources.beverages);
            ((Beverages)item).AddingIngredients("coffee", Properties.Resources.textL);
            ((Beverages)item).AddingIngredients("ice", Properties.Resources.iceBucket);

            pictureBoxBeverage.Tag = ((Beverages)item).ListOfIngredients[0].Name;
            pictureBoxBevL.Tag = ((Beverages)item).ListOfIngredients[1].Name;
            pictureBoxIceBucket.Tag = ((Beverages)item).ListOfIngredients[2].Name;

            pictureBoxBeverage.Image = ((Beverages)item).ListOfIngredients[0].Picture;
            pictureBoxBevL.Image = ((Beverages)item).ListOfIngredients[1].Picture;
            pictureBoxIceBucket.Image = ((Beverages)item).ListOfIngredients[2].Picture;

            pictureBoxBeverage.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxBevL.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxIceBucket.SizeMode = PictureBoxSizeMode.StretchImage;

            item = new Beverages(true, "M", "coffeeMCold", Properties.Resources.coldM, 20);
            listOfItems.Add(item);
            ((Beverages)item).AddingIngredients("cup", Properties.Resources.beverages);
            ((Beverages)item).AddingIngredients("coffee", Properties.Resources.textM);
            ((Beverages)item).AddingIngredients("ice", Properties.Resources.iceBucket);

            pictureBoxBeverage.Tag = ((Beverages)item).ListOfIngredients[0].Name;
            pictureBoxBevM.Tag = ((Beverages)item).ListOfIngredients[1].Name;
            pictureBoxIceBucket.Tag = ((Beverages)item).ListOfIngredients[2].Name;

            pictureBoxBeverage.Image = ((Beverages)item).ListOfIngredients[0].Picture;
            pictureBoxBevM.Image = ((Beverages)item).ListOfIngredients[1].Picture;
            pictureBoxIceBucket.Image = ((Beverages)item).ListOfIngredients[2].Picture;

            pictureBoxBeverage.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxBevM.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxIceBucket.SizeMode = PictureBoxSizeMode.StretchImage;

            item = new Beverages(true, "S", "coffeeSCold", Properties.Resources.coldS, 15);
            listOfItems.Add(item);
            ((Beverages)item).AddingIngredients("cup", Properties.Resources.beverages);
            ((Beverages)item).AddingIngredients("coffee", Properties.Resources.textS);
            ((Beverages)item).AddingIngredients("ice", Properties.Resources.iceBucket);

            pictureBoxBeverage.Tag = ((Beverages)item).ListOfIngredients[0].Name;
            pictureBoxBevS.Tag = ((Beverages)item).ListOfIngredients[1].Name;
            pictureBoxIceBucket.Tag = ((Beverages)item).ListOfIngredients[2].Name;

            pictureBoxBeverage.Image = ((Beverages)item).ListOfIngredients[0].Picture;
            pictureBoxBevS.Image = ((Beverages)item).ListOfIngredients[1].Picture;
            pictureBoxIceBucket.Image = ((Beverages)item).ListOfIngredients[2].Picture;

            pictureBoxBeverage.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxBevS.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxIceBucket.SizeMode = PictureBoxSizeMode.StretchImage;
            #endregion Beverages

            #region Merchandise            
            item = new Merchandise(5, "bear", Properties.Resources.bear, 100);
            listOfItems.Add(item);
            pictureBoxBear1.Tag = ((Merchandise)item).Name;
            pictureBoxBear2.Tag = ((Merchandise)item).Name;
            pictureBoxBear3.Tag = ((Merchandise)item).Name;
            pictureBoxBear4.Tag = ((Merchandise)item).Name;
            pictureBoxBear5.Tag = ((Merchandise)item).Name;

            pictureBoxBear1.Image = ((Merchandise)item).Picture;
            pictureBoxBear2.Image = ((Merchandise)item).Picture;
            pictureBoxBear3.Image = ((Merchandise)item).Picture;
            pictureBoxBear4.Image = ((Merchandise)item).Picture;
            pictureBoxBear5.Image = ((Merchandise)item).Picture;

            pictureBoxBear1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxBear2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxBear3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxBear4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxBear5.SizeMode = PictureBoxSizeMode.StretchImage;

            item = new Merchandise(5, "tumblr", Properties.Resources.tumblr, 100);
            listOfItems.Add(item);
            pictureBoxTumblr1.Tag = ((Merchandise)item).Name;
            pictureBoxTumblr2.Tag = ((Merchandise)item).Name;
            pictureBoxTumblr3.Tag = ((Merchandise)item).Name;
            pictureBoxTumblr4.Tag = ((Merchandise)item).Name;
            pictureBoxTumblr5.Tag = ((Merchandise)item).Name;

            pictureBoxTumblr1.Image = ((Merchandise)item).Picture;
            pictureBoxTumblr2.Image = ((Merchandise)item).Picture;
            pictureBoxTumblr3.Image = ((Merchandise)item).Picture;
            pictureBoxTumblr4.Image = ((Merchandise)item).Picture;
            pictureBoxTumblr5.Image = ((Merchandise)item).Picture;

            pictureBoxTumblr1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxTumblr2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxTumblr3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxTumblr4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxTumblr5.SizeMode = PictureBoxSizeMode.StretchImage;

            item = new Merchandise(5, "robot", Properties.Resources.robot, 100);
            listOfItems.Add(item);
            pictureBoxRobot1.Tag = ((Merchandise)item).Name;
            pictureBoxRobot2.Tag = ((Merchandise)item).Name;
            pictureBoxRobot3.Tag = ((Merchandise)item).Name;
            pictureBoxRobot4.Tag = ((Merchandise)item).Name;
            pictureBoxRobot5.Tag = ((Merchandise)item).Name;

            pictureBoxRobot1.Image = ((Merchandise)item).Picture;
            pictureBoxRobot2.Image = ((Merchandise)item).Picture;
            pictureBoxRobot3.Image = ((Merchandise)item).Picture;
            pictureBoxRobot4.Image = ((Merchandise)item).Picture;
            pictureBoxRobot5.Image = ((Merchandise)item).Picture;

            pictureBoxRobot1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxRobot2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxRobot3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxRobot4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxRobot5.SizeMode = PictureBoxSizeMode.StretchImage;

            #endregion Merchandise

        }   

        private void pictureBoxHome_Click(object sender, EventArgs e)
        {
            panelSetting.Visible = false;
            panelGame.Visible = false;
            panelCreateLoadPlayer.Visible = false;
            panelDifficulty.Visible = false;
            panelTutorial.Visible = false;
            BackgroundVisible();
        }

        private void pictureBoxRestart_Click(object sender, EventArgs e)
        {
            panelSetting.Visible = false;
            panelGame.Visible = true;
            timerGame.Stop();
            time = new Time(0, 0, timeChoose);
            timerGame.Start();
            labelRemainingTime.Text = time.Display();
        }


        private void timerGame_Tick(object sender, EventArgs e)
        {
            time.Add(-1);
            labelRemainingTime.Text = time.Display();
            if (time.Hour == 0 && time.Minute == 0 && time.Second == 0)
            {
                timerGame.Stop();
                MessageBox.Show("Gameover");
            }
        }

        private void CreateCustomers()
        {
            Random numRandomCust = new Random();
            int randomCust = numRandomCust.Next(0, 3);
            if (randomCust == 0)
            {
                customers = new Customers("Dustin", Properties.Resources.dustin, "male", null);
            }
            else if (randomCust == 1)
            {
                customers = new Customers("Jeni", Properties.Resources.jeni, "female", null);
            }
            else if(randomCust == 2)
            {
                customers = new Customers("Bumi", Properties.Resources.bumi, "kid", null);
            }
            pictureBoxCustomer1.Image = customers.Picture;
            pictureBoxCustomer1.BackColor = Color.Transparent;
            pictureBoxCustomer1.SizeMode = PictureBoxSizeMode.StretchImage;

            panelDialog1.BackgroundImage = Properties.Resources.dialog;
            incTimerCust = 0;

            pictureBoxServe.Image = null;
            pictureBoxServe.Tag = "none";

            timerCust.Start();
        }
        private void CreateCustomerOrder()
        {
            Random numRandomItemType = new Random();
            int randomItemType = numRandomItemType.Next(0, 3);
            if(randomItemType == 0) //Foods
            {
                if(customers.Type == "male")
                {
                    customers.OrderItem = listOfItems[0];
                }
                else if(customers.Type == "female")
                {
                    customers.OrderItem = listOfItems[1];
                }
                else if(customers.Type == "kid")
                {
                    customers.OrderItem = listOfItems[2];
                }
            }
            else if (randomItemType == 1)
            {
                Random numRandomBev = new Random();
                int randomBev = numRandomBev.Next(3, 9);
                customers.OrderItem = listOfItems[randomBev];
            }
            else if(randomItemType == 2)
            {
                Random numRandomMerch = new Random();
                int randomMerch = numRandomMerch.Next(9, 12);                
                customers.OrderItem = listOfItems[randomMerch];                
            }
            pictureBoxOrder1.Image = customers.OrderItem.Picture;
            pictureBoxOrder1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private void CorrectOrder(Items order)
        {
            pictureBoxServe.Image = order.Picture;
            pictureBoxServe.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxServe.Tag = "done";
            pictureBoxOrder1.Image = Properties.Resources.angry;
            player.Income += order.Price;
            selectedIngCount = 0;
            remainingCusts--;
            labelRemainingCustomers.Text = "Remaining \nCustomers: " + remainingCusts.ToString();
            incTimerCust = 0;
            timerCust.Start();
            //PlaySound("correct");
        }
        private void timerCust_Tick(object sender, EventArgs e)
        {
            incTimerCust++;
            if (incTimerCust == 1 && pictureBoxServe.Tag.ToString() == "none")
            {
                CreateCustomerOrder();
                timerCust.Stop();
            }
            else if (incTimerCust == 1 && pictureBoxServe.Tag.ToString() == "done")
            {
                panelDialog1.Visible= false;
                pictureBoxServe.Image = null;
                pictureBoxCustomer1.Image = null;
                incTimerCust = 0;
                timerCust.Stop();
                if (remainingCusts > 0)
                {
                    CreateCustomers();
                }
                else
                {
                    timerGame.Stop();
                    panelWin.Visible = true;
                }
            }
        }
        private void ServeOrder(PictureBox PictureBox, string type)
        {
            if (type == "foods")
            {
                if (customers.OrderItem is Foods)
                {
                    Foods foodOrder = (Foods)customers.OrderItem;
                    if (PictureBox.Tag.ToString() == foodOrder.ListOfIngredients[selectedIngCount].Name)
                    {
                        selectedIngCount++;
                        pictureBoxServe.Image = PictureBox.Image;
                        pictureBoxServe.SizeMode = PictureBoxSizeMode.StretchImage;
                        if (selectedIngCount == foodOrder.ListOfIngredients.Count)
                        {
                            CorrectOrder(foodOrder);
                        }
                    }
                    else
                    {
                        //WrongOrder();
                    }
                }
                else
                {
                    selectedIngCount = 0;
                    //WrongOrder();
                }
            }
            else if (type == "beverages")
            {
                if (customers.OrderItem is Beverages)
                {
                    Beverages bevOrder = (Beverages)customers.OrderItem;
                    if (PictureBox.Tag.ToString() == bevOrder.ListOfIngredients[selectedIngCount].Name)
                    {
                        selectedIngCount++;
                        pictureBoxServe.Image = PictureBox.Image;
                        pictureBoxServe.SizeMode = PictureBoxSizeMode.StretchImage;

                        if (selectedIngCount == bevOrder.ListOfIngredients.Count)
                        {
                            CorrectOrder(bevOrder);
                        }
                    }
                    else
                    {
                        //WrongOrder();
                    }
                }
                else
                {
                    selectedIngCount = 0;
                    //WrongOrder();
                }
            }
            else if (type == "merchandise")
            {
                if (customers.OrderItem is Merchandise)
                {
                    Merchandise merchOrder = (Merchandise)customers.OrderItem;
                    pictureBoxServe.Image = PictureBox.Image;
                    pictureBoxServe.SizeMode = PictureBoxSizeMode.StretchImage;
                    if (PictureBox.Tag.ToString() == merchOrder.Name)
                    {
                        merchOrder.Sell();
                        if (merchOrder.Name == "bear")
                        {
                            label1.Text = merchOrder.Stock.ToString() + "x";
                        }
                        else if (merchOrder.Name == "tumblr")
                        {
                            label2.Text = merchOrder.Stock.ToString() + "x";
                        }
                        else if (merchOrder.Name == "robot")
                        {
                            label2.Text = merchOrder.Stock.ToString() + "x";
                        }
                        CorrectOrder(merchOrder);
                    }
                    else
                    {
                        //WrongOrder();
                    }
                }
                else
                {
                    //WrongOrder();
                }

            }
        }
        #region PictureBox Mouse
        private void ChangePictureBoxColor(PictureBox pictureBox, string status)
        {
            if (status == "enter")
            {
                pictureBox.BackColor = Color.White;
            }
            else if (status == "leave")
            {
                pictureBox.BackColor = Color.Transparent;
            }
        }
        private void ChangePictureBoxColorBev(PictureBox pictureBox, string status, Color color)
        {
            if (status == "enter")
            {
                pictureBox.BackColor = Color.Silver;
            }
            else if (status == "leave")
            {
                pictureBox.BackColor = color;
            }
        }
        #region Level
        private void pictureBoxEasy_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxEasy, "enter");
        }

        private void pictureBoxEasy_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxEasy, "leave");
        }

        private void pictureBoxMedium_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxMedium, "enter");
        }

        private void pictureBoxMedium_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxMedium, "leave");
        }

        private void pictureBoxHard_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxHard, "enter");
        }

        private void pictureBoxHard_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxHard, "leave");
        }

        private void pictureBoxImpossible_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxImpossible, "enter");
        }

        private void pictureBoxImpossible_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxImpossible, "leave");
        }        
        #endregion Level

        #region Merchandise
        private void pictureBoxBear1_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxBear1, "enter");
        }

        private void pictureBoxBear1_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxBear1, "leave");

        }

        private void pictureBoxBear2_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxBear2, "enter");
        }

        private void pictureBoxBear2_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxBear2, "leave");
        }

        private void pictureBoxBear3_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxBear3, "enter");
        }

        private void pictureBoxBear3_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxBear3, "leave");
        }

        private void pictureBoxBear4_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxBear4, "enter");
        }

        private void pictureBoxBear4_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxBear4, "leave");
        }

        private void pictureBoxBear5_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxBear5, "enter");
        }

        private void pictureBoxBear5_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxBear5, "leave");
        }

        private void pictureBoxTumblr1_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxTumblr1, "enter");
        }

        private void pictureBoxTumblr1_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxTumblr1, "leave");
        }

        private void pictureBoxTumblr2_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxTumblr2, "enter");
        }

        private void pictureBoxTumblr2_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxTumblr2, "leave");
        }

        private void pictureBoxTumblr3_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxTumblr3, "enter");
        }

        private void pictureBoxTumblr3_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxTumblr3, "leave");
        }

        private void pictureBoxTumblr4_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxTumblr4, "enter");
        }

        private void pictureBoxTumblr4_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxTumblr4, "leave");
        }

        private void pictureBoxTumblr5_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxTumblr5, "enter");
        }

        private void pictureBoxTumblr5_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxTumblr5, "leave");
        }

        private void pictureBoxRobot1_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxRobot1, "enter");
        }
        private void pictureBoxRobot1_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxRobot1, "leave");
        }

        private void pictureBoxRobot2_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxRobot2, "enter");
        }

        private void pictureBoxRobot2_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxRobot2, "leave");
        }

        private void pictureBoxRobot3_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxRobot3, "enter");
        }

        private void pictureBoxRobot3_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxRobot3, "leave");
        }

        private void pictureBoxRobot4_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxRobot4, "enter");
        }

        private void pictureBoxRobot4_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxRobot4, "leave");
        }

        private void pictureBoxRobot5_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxRobot5, "enter");
        }

        private void pictureBoxRobot5_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxRobot5, "leave");
        }
        #endregion Merchandise

        #region Foods & Beverages
        private void pictureBoxTopBun_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxTopBun, "enter");
        }

        private void pictureBoxTopBun_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxTopBun, "leave");
        }

        private void pictureBoxBottomBun_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxBottomBun, "enter");
        }

        private void pictureBoxBottomBun_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxBottomBun, "leave");
        }

        private void pictureBoxTomato_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxTomato, "enter");
        }

        private void pictureBoxTomato_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxTomato, "leave");
        }

        private void pictureBoxLettuce_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxLettuce, "enter");
        }

        private void pictureBoxLettuce_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxLettuce, "leave");
        }

        private void pictureBoxPatty_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxPatty, "enter");
        }

        private void pictureBoxPatty_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxPatty, "leave");
        }

        private void pictureBoxCheese_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxCheese, "enter");
        }

        private void pictureBoxCheese_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxCheese, "leave");
        }

        private void pictureBoxIceBucket_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxIceBucket, "enter");
        }

        private void pictureBoxIceBucket_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxIceBucket, "leave");
        }        
        
        private void pictureBoxICMachine_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxICMachine, "enter");
        }

        private void pictureBoxICMachine_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxICMachine, "leave");
        }

        private void pictureBoxCone_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxCone, "enter");
        }

        private void pictureBoxCone_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxCone, "leave");
        }

        private void pictureBoxBeverage_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxBeverage, "enter");
        }

        private void pictureBoxBeverage_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxBeverage, "leave");
        }

        private void pictureBoxPlate_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxPlate, "enter");
        }

        private void pictureBoxPlate_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxPlate, "leave");
        }

        private void pictureBoxMayo_MouseEnter(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxMayo, "enter");
        }

        private void pictureBoxMayo_MouseLeave(object sender, EventArgs e)
        {
            ChangePictureBoxColor(pictureBoxMayo, "leave");
        }

        private void pictureBoxBevL_MouseEnter(object sender, EventArgs e)
        {
            Color color = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(177)))), ((int)(((byte)(177)))));
            ChangePictureBoxColorBev(pictureBoxBevL, "enter",color);
        }

        private void pictureBoxBevL_MouseLeave(object sender, EventArgs e)
        {
            Color color = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(177)))), ((int)(((byte)(177)))));
            ChangePictureBoxColorBev(pictureBoxBevL, "leave", color);
        }

        private void pictureBoxBevM_MouseEnter(object sender, EventArgs e)
        {
            Color color = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(58)))), ((int)(((byte)(60)))));
            ChangePictureBoxColorBev(pictureBoxBevM, "enter", color);
        }

        private void pictureBoxBevM_MouseLeave(object sender, EventArgs e)
        {
            Color color = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(58)))), ((int)(((byte)(60)))));
            ChangePictureBoxColorBev(pictureBoxBevM, "leave", color);
        }

        private void pictureBoxBevS_MouseEnter(object sender, EventArgs e)
        {
            Color color = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(167)))), ((int)(((byte)(78)))));
            ChangePictureBoxColorBev(pictureBoxBevS, "enter", color);
        }

        private void pictureBoxBevS_MouseLeave(object sender, EventArgs e)
        {
            Color color = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(167)))), ((int)(((byte)(78)))));
            ChangePictureBoxColorBev(pictureBoxBevS, "leave", color);
        }
        #endregion Foods & Beverages
        #endregion PictureBox Mouse
        #region Label Difficulty Click
        private void labelEasy_Click(object sender, EventArgs e)
        {
            this.labelEasy.Font = new System.Drawing.Font("Franklin Gothic Demi", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMedium.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHard.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelImpossible.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void pictureBoxEasy_Click(object sender, EventArgs e)
        {
            this.labelEasy.Font = new System.Drawing.Font("Franklin Gothic Demi", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMedium.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHard.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelImpossible.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void labelMedium_Click(object sender, EventArgs e)
        {
            this.labelMedium.Font = new System.Drawing.Font("Franklin Gothic Demi", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEasy.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHard.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelImpossible.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void pictureBoxMedium_Click(object sender, EventArgs e)
        {
            this.labelMedium.Font = new System.Drawing.Font("Franklin Gothic Demi", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEasy.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHard.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelImpossible.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void labelHard_Click(object sender, EventArgs e)
        {
            this.labelHard.Font = new System.Drawing.Font("Franklin Gothic Demi", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEasy.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMedium.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelImpossible.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void pictureBoxHard_Click(object sender, EventArgs e)
        {
            this.labelHard.Font = new System.Drawing.Font("Franklin Gothic Demi", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEasy.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMedium.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelImpossible.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void labelImpossible_Click(object sender, EventArgs e)
        {
            this.labelImpossible.Font = new System.Drawing.Font("Franklin Gothic Demi", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEasy.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMedium.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHard.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void pictureBoxImpossible_Click(object sender, EventArgs e)
        {
            this.labelImpossible.Font = new System.Drawing.Font("Franklin Gothic Demi", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEasy.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMedium.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHard.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }
        #endregion Panel Difficulty Click
        #region picbox click
        private void pictureBoxTopBun_Click(object sender, EventArgs e)
        {
            ServeOrder(pictureBoxTopBun, "foods");
        }

        private void pictureBoxBottomBun_Click(object sender, EventArgs e)
        {
            ServeOrder(pictureBoxBottomBun, "foods");
        }

        private void pictureBoxPatty_Click(object sender, EventArgs e)
        {
            ServeOrder(pictureBoxPatty, "foods");
        }

        private void pictureBoxCheese_Click(object sender, EventArgs e)
        {
            ServeOrder(pictureBoxCheese, "foods");
        }
        private void pictureBoxTomato_Click(object sender, EventArgs e)
        {
            ServeOrder(pictureBoxTomato, "foods");
        }

        private void pictureBoxLettuce_Click(object sender, EventArgs e)
        {
            ServeOrder(pictureBoxLettuce, "foods");
        }

        private void pictureBoxPlate_Click(object sender, EventArgs e)
        {
            ServeOrder(pictureBoxPlate, "foods");
        }

        private void pictureBoxMayo_Click(object sender, EventArgs e)
        {
            ServeOrder(pictureBoxMayo, "foods");
        }
        private void pictureBoxCone_Click(object sender, EventArgs e)
        {
            ServeOrder(pictureBoxCone, "foods");
        }

        private void pictureBoxICMachine_Click(object sender, EventArgs e)
        {
            ServeOrder(pictureBoxICMachine, "foods");
        }
        private void pictureBoxIceBucket_Click(object sender, EventArgs e)
        {
            ServeOrder(pictureBoxIceBucket, "beverages");
        }

        private void pictureBoxBeverage_Click(object sender, EventArgs e)
        {
            ServeOrder(pictureBoxBeverage, "beverages");
        }

        private void pictureBoxBevL_Click(object sender, EventArgs e)
        {
            ServeOrder(pictureBoxBevL, "beverages");
        }

        private void pictureBoxBevM_Click(object sender, EventArgs e)
        {
            ServeOrder(pictureBoxBevM, "beverages");
        }

        private void pictureBoxBevS_Click(object sender, EventArgs e)
        {
            ServeOrder(pictureBoxBevS, "beverages");
        }

        private void pictureBoxBear5_Click(object sender, EventArgs e)
        {
            ServeOrder(pictureBoxBear5, "merchandise");
        }

        private void pictureBoxBear4_Click(object sender, EventArgs e)
        {
            ServeOrder(pictureBoxBear4, "merchandise");
        }

        private void pictureBoxBear3_Click(object sender, EventArgs e)
        {
            ServeOrder(pictureBoxBear3, "merchandise");
        }

        private void pictureBoxBear2_Click(object sender, EventArgs e)
        {
            ServeOrder(pictureBoxBear2, "merchandise");
        }

        private void pictureBoxBear1_Click(object sender, EventArgs e)
        {
            ServeOrder(pictureBoxBear1, "merchandise");
        }

        private void pictureBoxTumblr5_Click(object sender, EventArgs e)
        {
            ServeOrder(pictureBoxTumblr5, "merchandise");
        }

        private void pictureBoxTumblr4_Click(object sender, EventArgs e)
        {
            ServeOrder(pictureBoxTumblr4, "merchandise");
        }

        private void pictureBoxTumblr3_Click(object sender, EventArgs e)
        {
            ServeOrder(pictureBoxTumblr3, "merchandise");
        }

        private void pictureBoxTumblr2_Click(object sender, EventArgs e)
        {
            ServeOrder(pictureBoxTumblr2, "merchandise");
        }

        private void pictureBoxTumblr1_Click(object sender, EventArgs e)
        {
            ServeOrder(pictureBoxTumblr1, "merchandise");
        }

        private void pictureBoxRobot5_Click(object sender, EventArgs e)
        {
            ServeOrder(pictureBoxRobot5, "merchandise");
        }

        private void pictureBoxRobot4_Click(object sender, EventArgs e)
        {
            ServeOrder(pictureBoxRobot4, "merchandise");
        }

        private void pictureBoxRobot3_Click(object sender, EventArgs e)
        {
            ServeOrder(pictureBoxRobot3, "merchandise");
        }

        private void pictureBoxRobot2_Click(object sender, EventArgs e)
        {
            ServeOrder(pictureBoxRobot2, "merchandise");
        }

        private void pictureBoxRobot1_Click(object sender, EventArgs e)
        {
            ServeOrder(pictureBoxRobot1, "merchandise");
        }
        #endregion picbox click
    }
}
