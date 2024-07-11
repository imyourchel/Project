using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

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
        Customers customer2;

        int tempIncome=0;
        List<int> listTempIncome = new List<int>();

        int custEasy = 8;
        int custMedium = 15;
        int custHard = 23;
        int custImpossible = 32;
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

        bool barMusic1 = true;
        bool barMusic2 = true;
        bool barMusic3 = true;
        bool barMusic4 = true;
        bool barMusic5 = true;
        bool barSFX1 = true;
        bool barSFX2 = true;
        bool barSFX3 = true;
        bool barSFX4 = true;
        bool barSFX5 = true;

        int selectedIngCount;
        int incTimerCust;
        int incTimerEmotion1;
        int incTimerEmotion2;
        int tempEmotion;
        bool timeFirst = true;

        List<bool> stockBear;
        List<bool> stockTumblr;
        List<bool> stockRobot;

        Customers tempCust;
        Customers tempOrder;

        bool first = true;

        WindowsMediaPlayer sound1 = new WindowsMediaPlayer();
        WindowsMediaPlayer sound2 = new WindowsMediaPlayer();

        public FormGame()
        {
            InitializeComponent();
        }
        #region setting panel game
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
            PlaySound("game");
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
            timerEmotion1.Enabled = false;
            timerEmotion1.Interval= 1000;
            timerEmotion2.Enabled = false;
            timerEmotion2.Interval = 1000;
        }
        private void buttonPlay_Click(object sender, EventArgs e)
        {
            PlaySound("button");
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
            PlaySound("button");
            if (MessageBox.Show("Are you sure want to exit the game?", "Exit Message", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void pictureBoxBackHome_Click(object sender, EventArgs e)
        {
            PlaySound("button");
            BackgroundVisible();
            panelCreateLoadPlayer.Visible = false;
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
        private void buttonNextDifficult_Click(object sender, EventArgs e)
        {
            try
            {
                PlaySound("button");
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
                    player.HighScore = new List<int>{0,0,0,0};
                    time = new Time(0,0,0);
                    stockBear = new List<bool> { true, true, true, true, true };
                    stockRobot = new List<bool> { true, true, true, true, true };
                    stockTumblr = new List<bool> { true, true, true, true, true };

                    player.BestTime = new List<Time> {time,time,time,time};
                    player.PrevTime = new List<Time> {time,time,time,time};                                                            
                }
                //Load Player
                else
                {
                    player = (Players)comboBoxNameLoad.SelectedItem;
                    foreach(Items i in player.StockMerchandise)
                    {
                        if(i is Merchandise)
                        {
                            Merchandise merch = (Merchandise)i;
                            if (merch.Name == "bear")
                            {
                                stockBear = merch.ListStock;
                            }
                            else if (merch.Name == "tumblr")
                            {
                                stockTumblr = merch.ListStock;
                            }
                            else if (merch.Name == "robot")
                            {
                                stockRobot = merch.ListStock;
                            }
                        }
                    }
                    //stock = player.StockMerchandise;
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

        private void buttonNextTutorial_Click(object sender, EventArgs e)
        {
            try
            {
                PlaySound("button");
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
        private void pictureBoxBackCreateLoadPlayer_Click(object sender, EventArgs e)
        {
            PlaySound("button");
            panelCreateLoadPlayer.Visible = true;
            panelDifficulty.Visible = false;
        }

        private void pictureBoxButtonReceipe_Click(object sender, EventArgs e)
        {
            PlaySound("button");
            timerCust.Stop();
            timerGame.Stop();
            timerEmotion1.Stop();
            timerEmotion2.Stop();
            panelGame.Visible = false;
            panelTutorial.Visible=true;
            buttonStartGame.Visible = false;
            buttonBackReceipe.Visible = true;
        }
        private void buttonBackReceipe_Click(object sender, EventArgs e)
        {
            PlaySound("button");
            timerCust.Start();
            timerGame.Start();
            timerEmotion1.Start();
            timerEmotion2.Start();
            panelTutorial.Visible = false;
            panelGame.Visible=true;
        }       
        private void pictureBoxButtonSetting_Click(object sender, EventArgs e)
        {
            PlaySound("button");
            timerCust.Stop();
            timerGame.Stop();
            timerEmotion1.Stop();
            timerEmotion2.Stop();
            panelSetting.Visible = true;
            panelGame.Visible=false;
        }
        private void pictureBoxResume_Click(object sender, EventArgs e)
        {
            PlaySound("button");
            timerCust.Start();
            timerGame.Start();
            timerEmotion1.Start();
            timerEmotion2.Start();
            panelSetting.Visible = false;
            panelGame.Visible = true;
        }
        #endregion setting panel game

        #region InGame
        private void pictureBoxHome_Click(object sender, EventArgs e)
        {
            PlaySound("button");
            panelWin.Visible = false;
            FormGame_Load(pictureBoxWinToHome, e);
        }
        private void pictureBoxRestart_Click(object sender, EventArgs e)
        {
            PlaySound("button");
            foreach(int i in listTempIncome)
            {
                tempIncome += i;
            }
            player.Income = (player.Income) - tempIncome;
            tempIncome = 0;
            listTempIncome.Clear();
            panelSetting.Visible = false;
            if (labelEasy.Font.Name == "Franklin Gothic Demi")
            {
                remainingCusts = custEasy;
            }
            else if (labelMedium.Font.Name == "Franklin Gothic Demi")
            {
                remainingCusts = custMedium;
            }
            else if (labelHard.Font.Name == "Franklin Gothic Demi")
            {
                remainingCusts = custHard;
            }
            else if (labelImpossible.Font.Name == "Franklin Gothic Demi")
            {
                remainingCusts = custImpossible;
            }
            buttonStartGame_Click(pictureBoxRestart, e);
        }
        private void StallDisplay()
        {
            #region Foods
            item = new Foods("burger", Properties.Resources.burger, 50);
            listOfItems.Add(item);
            ((Foods)item).AddingIngredients("plate", Properties.Resources.plates, Properties.Resources.burger1);
            ((Foods)item).AddingIngredients("bottombun", Properties.Resources.bottomBun, Properties.Resources.burger2);
            ((Foods)item).AddingIngredients("patty", Properties.Resources.patty, Properties.Resources.burger3);
            ((Foods)item).AddingIngredients("cheese", Properties.Resources.cheese, Properties.Resources.burger4);
            ((Foods)item).AddingIngredients("tomato", Properties.Resources.tomato, Properties.Resources.burger5);
            ((Foods)item).AddingIngredients("lettuce", Properties.Resources.lettuce, Properties.Resources.burger6);
            ((Foods)item).AddingIngredients("topbun", Properties.Resources.topBun, Properties.Resources.burger);

            pictureBoxPlate.Tag = ((Foods)item).ListOfIngredients[0].Name;
            pictureBoxBottomBun.Tag = ((Foods)item).ListOfIngredients[1].Name;
            pictureBoxPatty.Tag = ((Foods)item).ListOfIngredients[2].Name;
            pictureBoxCheese.Tag = ((Foods)item).ListOfIngredients[3].Name;
            pictureBoxTomato.Tag = ((Foods)item).ListOfIngredients[4].Name;
            pictureBoxLettuce.Tag = ((Foods)item).ListOfIngredients[5].Name;
            pictureBoxTopBun.Tag = ((Foods)item).ListOfIngredients[6].Name;

            pictureBoxPlate.Image = ((Foods)item).ListOfIngredients[0].Picture;
            pictureBoxBottomBun.Image = ((Foods)item).ListOfIngredients[1].Picture;
            pictureBoxPatty.Image = ((Foods)item).ListOfIngredients[2].Picture;
            pictureBoxCheese.Image = ((Foods)item).ListOfIngredients[3].Picture;
            pictureBoxTomato.Image = ((Foods)item).ListOfIngredients[4].Picture;
            pictureBoxLettuce.Image = ((Foods)item).ListOfIngredients[5].Picture;
            pictureBoxTopBun.Image = ((Foods)item).ListOfIngredients[6].Picture;

            pictureBoxPlate.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxBottomBun.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxPatty.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxCheese.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxTomato.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxLettuce.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxTopBun.SizeMode = PictureBoxSizeMode.StretchImage;


            item = new Foods("salad", Properties.Resources.salad, 25);
            listOfItems.Add(item);
            ((Foods)item).AddingIngredients("plate", Properties.Resources.plates, Properties.Resources.salad1);
            ((Foods)item).AddingIngredients("lettuce", Properties.Resources.lettuce, Properties.Resources.salad2);
            ((Foods)item).AddingIngredients("mayo", Properties.Resources.mayo, Properties.Resources.salad);

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
            ((Foods)item).AddingIngredients("cone", Properties.Resources.cone, Properties.Resources.icesream1);
            ((Foods)item).AddingIngredients("ice", Properties.Resources.icMachine, Properties.Resources.iceCream);

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
            ((Beverages)item).AddingIngredients("cup", Properties.Resources.beverages, Properties.Resources.cup);            
            ((Beverages)item).AddingIngredients("coffee", Properties.Resources.textL, Properties.Resources.hotL);

            pictureBoxBeverage.Tag = ((Beverages)item).ListOfIngredients[0].Name;
            pictureBoxBevL.Tag = ((Beverages)item).ListOfIngredients[1].Name;

            pictureBoxBeverage.Image = ((Beverages)item).ListOfIngredients[0].Picture;
            pictureBoxBevL.Image = ((Beverages)item).ListOfIngredients[1].Picture;

            pictureBoxBeverage.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxBevL.SizeMode = PictureBoxSizeMode.StretchImage;

            item = new Beverages(false, "M", "coffeeMHot", Properties.Resources.hotM, 20);
            listOfItems.Add(item);
            ((Beverages)item).AddingIngredients("cup", Properties.Resources.beverages, Properties.Resources.cup);
            ((Beverages)item).AddingIngredients("coffee", Properties.Resources.textM, Properties.Resources.hotM);

            pictureBoxBeverage.Tag = ((Beverages)item).ListOfIngredients[0].Name;
            pictureBoxBevM.Tag = ((Beverages)item).ListOfIngredients[1].Name;

            pictureBoxBeverage.Image = ((Beverages)item).ListOfIngredients[0].Picture;
            pictureBoxBevM.Image = ((Beverages)item).ListOfIngredients[1].Picture;

            pictureBoxBeverage.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxBevM.SizeMode = PictureBoxSizeMode.StretchImage;

            item = new Beverages(false, "S", "coffeeSHot", Properties.Resources.hotS, 15);
            listOfItems.Add(item);
            ((Beverages)item).AddingIngredients("cup", Properties.Resources.beverages, Properties.Resources.cup);
            ((Beverages)item).AddingIngredients("coffee", Properties.Resources.textS, Properties.Resources.hotS);

            pictureBoxBeverage.Tag = ((Beverages)item).ListOfIngredients[0].Name;
            pictureBoxBevS.Tag = ((Beverages)item).ListOfIngredients[1].Name;

            pictureBoxBeverage.Image = ((Beverages)item).ListOfIngredients[0].Picture;
            pictureBoxBevS.Image = ((Beverages)item).ListOfIngredients[1].Picture;

            pictureBoxBeverage.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxBevS.SizeMode = PictureBoxSizeMode.StretchImage;

            item = new Beverages(true, "L", "coffeeLCold", Properties.Resources.coldL, 25);
            listOfItems.Add(item);
            ((Beverages)item).AddingIngredients("cup", Properties.Resources.beverages, Properties.Resources.cup);
            ((Beverages)item).AddingIngredients("coffee", Properties.Resources.textL, Properties.Resources.coldL);
            ((Beverages)item).AddingIngredients("ice", Properties.Resources.iceBucket, Properties.Resources.coldL);

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
            ((Beverages)item).AddingIngredients("cup", Properties.Resources.beverages, Properties.Resources.cup);
            ((Beverages)item).AddingIngredients("coffee", Properties.Resources.textM, Properties.Resources.coldM);
            ((Beverages)item).AddingIngredients("ice", Properties.Resources.iceBucket, Properties.Resources.coldM);

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
            ((Beverages)item).AddingIngredients("cup", Properties.Resources.beverages, Properties.Resources.cup);
            ((Beverages)item).AddingIngredients("coffee", Properties.Resources.textS, Properties.Resources.coldS);
            ((Beverages)item).AddingIngredients("ice", Properties.Resources.iceBucket, Properties.Resources.coldS);

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
            
            item = new Merchandise(stockBear, "bear", Properties.Resources.bear, 100);
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

            item = new Merchandise(stockTumblr, "tumblr", Properties.Resources.tumblr, 100);
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

            item = new Merchandise(stockRobot, "robot", Properties.Resources.robot, 100);
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

        private void timerGame_Tick(object sender, EventArgs e)
        {
            time.Add(-1);
            labelRemainingTime.Text = time.Display();
            if (time.Hour == 0 && time.Minute == 0 && time.Second == 0)
            {
                if (remainingCusts != 0)
                {
                    timerGame.Stop();
                    panelGame.Visible = false;
                    panelLose.Visible = true;
                    PlaySound("lose");
                }
            }
        }
        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            PlaySound("button");
            PlaySound("play");
            panelTutorial.Visible = false;
            panelGame.Visible = true;
            panelGame.BackgroundImageLayout = ImageLayout.Stretch;
            time = new Time(0, 0, timeChoose);
            labelRemainingTime.Text = time.Display();
            timerGame.Start();
            labelRemainingCustomers.Text = "Remaining Customers : " + remainingCusts.ToString();
            labelNamePlayer.Text = player.Name;
            StallDisplay();
            timerEmotion1.Start();
            timerEmotion2.Start();
            incTimerEmotion1 = 0;
            incTimerEmotion2 = 0;

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

            CreateCustomers();
            CreateCustomers2();
        }
        private void CreateCustomerOrder()
        {
            if (first == true)
            {
                Random numRandomItemType = new Random();
                int randomItemType = numRandomItemType.Next(0, 3);
                if (randomItemType == 0) //Foods
                {
                    Random numRandomFood = new Random();
                    int randomFood = numRandomFood.Next(0, 3);
                    customers.OrderItem = listOfItems[randomFood];
                }
                else if (randomItemType == 1)
                {
                    Random numRandomBev = new Random();
                    int randomBev = numRandomBev.Next(3, 9);
                    customers.OrderItem = listOfItems[randomBev];
                }
                else if (randomItemType == 2)
                {
                    Random numRandomMerch = new Random();
                    int randomMerch = numRandomMerch.Next(9, 12);
                    customers.OrderItem = listOfItems[randomMerch];
                } 
            }
            else
            {
                customers = tempOrder;
            }
            pictureBoxOrder1.Image = customers.OrderItem.Picture;
            pictureBoxOrder1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private void CreateCustomerOrder2()
        {
            if (first == false)
            {
                CreateCustomerOrder();
            }
            Random numRandomItemType = new Random();
            int randomItemType = numRandomItemType.Next(0, 3);
            if (randomItemType == 0) //Foods
            {
                Random numRandomFood = new Random();
                int randomFood = numRandomFood.Next(0, 3);
                customer2.OrderItem = listOfItems[randomFood];
            }
            else if (randomItemType == 1)
            {
                Random numRandomBev = new Random();
                int randomBev = numRandomBev.Next(3, 9);
                customer2.OrderItem = listOfItems[randomBev];
            }
            else if (randomItemType == 2)
            {
                Random numRandomMerch = new Random();
                int randomMerch = numRandomMerch.Next(9, 12);
                customer2.OrderItem = listOfItems[randomMerch];
            }            
            tempOrder = customer2;
            pictureBoxOrder2.Image = tempOrder.OrderItem.Picture;
            pictureBoxOrder2.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private void CorrectOrder(Items order)
        {
            pictureBoxServe.Image = order.Picture;
            pictureBoxServe.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxServe.Tag = "done";
            pictureBoxOrder1.Image = Properties.Resources.coin;
            listTempIncome.Add(order.Price);
            player.Income += order.Price;
            selectedIngCount = 0;
            remainingCusts--;
            labelRemainingCustomers.Text = "Remaining Customers: " + remainingCusts.ToString();
            labelIncomeNow.Text=player.Income.ToString();
            incTimerCust = 0;
            timerCust.Start();
            PlaySound("correct");
        }
        private void WrongOrder()
        {
            pictureBoxServe.Image = Properties.Resources.wrong;
            selectedIngCount = 0;
            PlaySound("fail");
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
                        pictureBoxServe.Image = foodOrder.ListOfIngredients[selectedIngCount-1].ServePicture;
                        pictureBoxServe.SizeMode = PictureBoxSizeMode.StretchImage;
                        if (selectedIngCount == foodOrder.ListOfIngredients.Count)
                        {
                            CorrectOrder(foodOrder);
                        }
                    }
                    else
                    {
                        WrongOrder();
                    }
                }
                else
                {
                    selectedIngCount = 0;
                    WrongOrder();
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
                        pictureBoxServe.Image = bevOrder.ListOfIngredients[selectedIngCount-1].ServePicture ;
                        pictureBoxServe.SizeMode = PictureBoxSizeMode.StretchImage;

                        if (selectedIngCount == bevOrder.ListOfIngredients.Count)
                        {
                            CorrectOrder(bevOrder);
                        }
                    }
                    else
                    {
                        WrongOrder();
                    }
                }
                else
                {
                    selectedIngCount = 0;
                    WrongOrder();
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
                        merchOrder.Sell(1);                        
                        CorrectOrder(merchOrder);
                    }
                    else
                    {
                        WrongOrder();
                    }
                }
                else
                {
                    WrongOrder();
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
            PlaySound("click");
            this.labelEasy.Font = new System.Drawing.Font("Franklin Gothic Demi", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMedium.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHard.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelImpossible.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void pictureBoxEasy_Click(object sender, EventArgs e)
        {
            PlaySound("click");
            this.labelEasy.Font = new System.Drawing.Font("Franklin Gothic Demi", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMedium.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHard.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelImpossible.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void labelMedium_Click(object sender, EventArgs e)
        {
            PlaySound("click");
            this.labelMedium.Font = new System.Drawing.Font("Franklin Gothic Demi", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEasy.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHard.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelImpossible.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void pictureBoxMedium_Click(object sender, EventArgs e)
        {
            PlaySound("click");
            this.labelMedium.Font = new System.Drawing.Font("Franklin Gothic Demi", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEasy.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHard.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelImpossible.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void labelHard_Click(object sender, EventArgs e)
        {
            PlaySound("click");
            this.labelHard.Font = new System.Drawing.Font("Franklin Gothic Demi", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEasy.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMedium.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelImpossible.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void pictureBoxHard_Click(object sender, EventArgs e)
        {
            PlaySound("click");
            this.labelHard.Font = new System.Drawing.Font("Franklin Gothic Demi", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEasy.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMedium.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelImpossible.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void labelImpossible_Click(object sender, EventArgs e)
        {
            PlaySound("click");
            this.labelImpossible.Font = new System.Drawing.Font("Franklin Gothic Demi", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEasy.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMedium.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHard.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void pictureBoxImpossible_Click(object sender, EventArgs e)
        {
            PlaySound("click");
            this.labelImpossible.Font = new System.Drawing.Font("Franklin Gothic Demi", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEasy.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMedium.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHard.Font = new System.Drawing.Font("Franklin Gothic Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }
        #endregion Panel Difficulty Click
        #region picbox click
        private void pictureBoxTopBun_Click(object sender, EventArgs e)
        {
            PlaySound("click");
            ServeOrder(pictureBoxTopBun, "foods");
        }

        private void pictureBoxBottomBun_Click(object sender, EventArgs e)
        {
            PlaySound("click"); 
            ServeOrder(pictureBoxBottomBun, "foods");
        }

        private void pictureBoxPatty_Click(object sender, EventArgs e)
        {
            PlaySound("click"); 
            ServeOrder(pictureBoxPatty, "foods");
        }

        private void pictureBoxCheese_Click(object sender, EventArgs e)
        {
            PlaySound("click"); 
            ServeOrder(pictureBoxCheese, "foods");
        }
        private void pictureBoxTomato_Click(object sender, EventArgs e)
        {
            PlaySound("click"); 
            ServeOrder(pictureBoxTomato, "foods");
        }

        private void pictureBoxLettuce_Click(object sender, EventArgs e)
        {
            PlaySound("click"); 
            ServeOrder(pictureBoxLettuce, "foods");
        }

        private void pictureBoxPlate_Click(object sender, EventArgs e)
        {
            PlaySound("click"); 
            ServeOrder(pictureBoxPlate, "foods");
        }

        private void pictureBoxMayo_Click(object sender, EventArgs e)
        {
            PlaySound("click"); 
            ServeOrder(pictureBoxMayo, "foods");
        }
        private void pictureBoxCone_Click(object sender, EventArgs e)
        {
            PlaySound("click"); 
            ServeOrder(pictureBoxCone, "foods");
        }

        private void pictureBoxICMachine_Click(object sender, EventArgs e)
        {
            PlaySound("click"); 
            ServeOrder(pictureBoxICMachine, "foods");
        }
        private void pictureBoxIceBucket_Click(object sender, EventArgs e)
        {
            PlaySound("click"); 
            ServeOrder(pictureBoxIceBucket, "beverages");
        }

        private void pictureBoxBeverage_Click(object sender, EventArgs e)
        {
            PlaySound("click"); 
            ServeOrder(pictureBoxBeverage, "beverages");
        }

        private void pictureBoxBevL_Click(object sender, EventArgs e)
        {
            PlaySound("click"); 
            ServeOrder(pictureBoxBevL, "beverages");
        }

        private void pictureBoxBevM_Click(object sender, EventArgs e)
        {
            PlaySound("click"); 
            ServeOrder(pictureBoxBevM, "beverages");
        }

        private void pictureBoxBevS_Click(object sender, EventArgs e)
        {
            PlaySound("click"); 
            ServeOrder(pictureBoxBevS, "beverages");
        }

        private void pictureBoxBear5_Click(object sender, EventArgs e)
        {
            PlaySound("click"); 
            ServeOrder(pictureBoxBear5, "merchandise");
        }

        private void pictureBoxBear4_Click(object sender, EventArgs e)
        {
            PlaySound("click"); 
            ServeOrder(pictureBoxBear4, "merchandise");
        }

        private void pictureBoxBear3_Click(object sender, EventArgs e)
        {
            PlaySound("click"); 
            ServeOrder(pictureBoxBear3, "merchandise");
        }

        private void pictureBoxBear2_Click(object sender, EventArgs e)
        {
            PlaySound("click"); 
            ServeOrder(pictureBoxBear2, "merchandise");
        }

        private void pictureBoxBear1_Click(object sender, EventArgs e)
        {
            PlaySound("click"); 
            ServeOrder(pictureBoxBear1, "merchandise");
        }

        private void pictureBoxTumblr5_Click(object sender, EventArgs e)
        {
            PlaySound("click"); 
            ServeOrder(pictureBoxTumblr5, "merchandise");
        }

        private void pictureBoxTumblr4_Click(object sender, EventArgs e)
        {
            PlaySound("click"); 
            ServeOrder(pictureBoxTumblr4, "merchandise");
        }

        private void pictureBoxTumblr3_Click(object sender, EventArgs e)
        {
            PlaySound("click"); 
            ServeOrder(pictureBoxTumblr3, "merchandise");
        }

        private void pictureBoxTumblr2_Click(object sender, EventArgs e)
        {
            PlaySound("click"); 
            ServeOrder(pictureBoxTumblr2, "merchandise");
        }

        private void pictureBoxTumblr1_Click(object sender, EventArgs e)
        {
            PlaySound("click"); 
            ServeOrder(pictureBoxTumblr1, "merchandise");
        }

        private void pictureBoxRobot5_Click(object sender, EventArgs e)
        {
            PlaySound("click"); 
            ServeOrder(pictureBoxRobot5, "merchandise");
        }

        private void pictureBoxRobot4_Click(object sender, EventArgs e)
        {
            PlaySound("click"); 
            ServeOrder(pictureBoxRobot4, "merchandise");
        }

        private void pictureBoxRobot3_Click(object sender, EventArgs e)
        {
            PlaySound("click"); 
            ServeOrder(pictureBoxRobot3, "merchandise");
        }

        private void pictureBoxRobot2_Click(object sender, EventArgs e)
        {
            PlaySound("click"); 
            ServeOrder(pictureBoxRobot2, "merchandise");
        }

        private void pictureBoxRobot1_Click(object sender, EventArgs e)
        {
            PlaySound("click"); 
            ServeOrder(pictureBoxRobot1, "merchandise");
        }
        #endregion picbox click
        #endregion InGame

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

        private void pictureBoxButtonPlayAgain_Click(object sender, EventArgs e)
        {
            PlaySound("button");
            foreach(int i in listTempIncome)
            {
                tempIncome += i;
            }
            player.Income = (player.Income) - tempIncome;
            panelWin.Visible = false;
            if (labelEasy.Font.Name == "Franklin Gothic Demi")
            {
                remainingCusts = custEasy;
            }
            else if (labelMedium.Font.Name == "Franklin Gothic Demi")
            {
                remainingCusts = custMedium;
            }
            else if (labelHard.Font.Name == "Franklin Gothic Demi")
            {
                remainingCusts = custHard;
            }
            else if (labelImpossible.Font.Name == "Franklin Gothic Demi")
            {
                remainingCusts = custImpossible;
            }
            buttonStartGame_Click(pictureBoxButtonPlayAgain, e);
        }

        private void pictureBoxQuit_Click(object sender, EventArgs e)
        {
            PlaySound("button");
            if (MessageBox.Show("Are you sure want to exit the game?", "Exit Message", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void pictureBoxWinToHome_Click(object sender, EventArgs e)
        {
            PlaySound("button");
            panelWin.Visible = false;
            FormGame_Load(pictureBoxWinToHome, e);
        }

        private void pictureBoxPlayAgain_Click(object sender, EventArgs e)
        {
            PlaySound("button");
            panelLose.Visible = false;
            if (labelEasy.Font.Name == "Franklin Gothic Demi")
            {
                remainingCusts = custEasy;
            }
            else if (labelMedium.Font.Name == "Franklin Gothic Demi")
            {
                remainingCusts = custMedium;
            }
            else if (labelHard.Font.Name == "Franklin Gothic Demi")
            {
                remainingCusts = custHard;
            }
            else if (labelImpossible.Font.Name == "Franklin Gothic Demi")
            {
                remainingCusts = custImpossible;
            }
            buttonStartGame_Click(pictureBoxPlayAgain, e);
        }

        private void pictureBoxLoseToHome_Click(object sender, EventArgs e)
        {
            PlaySound("button");
            panelLose.Visible = false;
            FormGame_Load(pictureBoxLoseToHome, e);
        }

        private void pictureBoxExit_Click(object sender, EventArgs e)
        {
            PlaySound("button");
            if (MessageBox.Show("Are you sure want to exit the game?", "Exit Message", MessageBoxButtons.YesNo,
              MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void CreateCustomers()
        {
            if (first == true)
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
                else if (randomCust == 2)
                {
                    customers = new Customers("Bumi", Properties.Resources.bumi, "kid", null);
                }
            }
            else
            {
                customers = tempCust;
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
        private void CreateCustomers2()
        {
            if (first == false)
            {
                CreateCustomers();
            }
            Random numRandomCust = new Random();
            int randomCust = numRandomCust.Next(0, 3);
            if (randomCust == 0)
            {
                customer2 = new Customers("Dustin", Properties.Resources.dustin, "male", null);
            }
            else if (randomCust == 1)
            {
                customer2 = new Customers("Jeni", Properties.Resources.jeni, "female", null);
            }
            else if (randomCust == 2)
            {
                customer2 = new Customers("Bumi", Properties.Resources.bumi, "kid", null);
            }           
            tempCust = customer2;
            pictureBoxCustomer2.Image = tempCust.Picture;
            pictureBoxCustomer2.BackColor = Color.Transparent;
            pictureBoxCustomer2.SizeMode = PictureBoxSizeMode.StretchImage;

            panelDialog2.BackgroundImage = Properties.Resources.dialog;
            panelDialog2.BackgroundImageLayout = ImageLayout.Stretch;
        }
        private void timerCust_Tick(object sender, EventArgs e)
        {
            incTimerCust++;            
            if (incTimerCust == 1 && pictureBoxServe.Tag.ToString() == "none")
            {
                if (first == true){ CreateCustomerOrder();CreateCustomerOrder2(); }
                else { CreateCustomerOrder2(); }
                timerCust.Stop();
                first = false;
            }
            else if (incTimerCust == 1 && pictureBoxServe.Tag.ToString() == "done")
            {
                panelDialog1.Visible= true;
                pictureBoxServe.Image = null;
                pictureBoxCustomer1.Image = null;
                incTimerCust = 0;
                timerCust.Stop();
                if (remainingCusts > 1)
                {
                    CreateCustomers2();
                }
                else if (remainingCusts == 1)
                {
                    pictureBoxCustomer2.Visible = false;
                    panelDialog2.Visible = false;
                }
                else
                {
                    timerGame.Stop();
                    panelGame.Visible = false;
                    panelWin.Visible = true;
                    PlaySound("win");
                }
            }
        }
        private void timerEmotion2_Tick(object sender, EventArgs e)
        {
            incTimerEmotion2++;
            Image image = null;
            if (incTimerEmotion1 <= 7)
            {
                image = Properties.Resources.happy;
            }
            else if (incTimerEmotion1 > 7 && incTimerEmotion1 <= 14)
            {
                image = Properties.Resources.flat;
            }
            else if (incTimerEmotion1 > 7)
            {
                image = Properties.Resources.angry;
            }
            pictureBoxEmotion2.Image = image;
            pictureBoxEmotion2.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private void timerEmotion1_Tick(object sender, EventArgs e)
        {
            Image image = null;
            incTimerEmotion1++;
            if (incTimerEmotion1 <= 4)
            {
                image = Properties.Resources.happy;
            }
            else if (incTimerEmotion1 > 4 && incTimerEmotion1 <= 7)
            {
                image = Properties.Resources.flat;
            }
            else if (incTimerEmotion1 > 7)
            {
                image = Properties.Resources.angry;
            }
            pictureBoxEmotion1.Image = image;
            pictureBoxEmotion1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private void PlaySound(string type)
        {
            if (type == "fail")
            {
                sound1.URL = Application.StartupPath + "\\sound\\fail.mp3";
                sound1.controls.play();
            }
            else if (type == "correct")
            {
                sound1.URL = Application.StartupPath + "\\sound\\correct.mp3";
                sound1.controls.play();
            }
            else if (type == "button")
            {
                sound1.URL = Application.StartupPath + "\\sound\\button.mp3";
                sound1.controls.play();
            }
            else if (type == "click")
            {
                sound1.URL = Application.StartupPath + "\\sound\\click.mp3";
                sound1.controls.play();
            }
            else if (type == "lose")
            {
                sound2.URL = Application.StartupPath + "\\sound\\lose.mp3";
                sound2.controls.play();
            }
            else if (type == "win")
            {
                sound2.URL = Application.StartupPath + "\\sound\\win.mp3";
                sound2.controls.play();
            }
            else if (type == "play")
            {
                sound2.URL = Application.StartupPath + "\\sound\\play.mp3";
                sound2.controls.play();
            }
            else if (type == "game")
            {
                sound2.URL = Application.StartupPath + "\\sound\\game.mp3";
                sound2.controls.play();
            }
            else if (type == "stop")
            {
                sound2.controls.stop();
            }
        }

        private void buttonBuyMerchandiseBear_Click(object sender, EventArgs e)
        {
            PlaySound("click");
        }

        private void buttonBuyMerchandiseTumblr_Click(object sender, EventArgs e)
        {
            PlaySound("click");
        }

        private void buttonBuyMerchandiseRobot_Click(object sender, EventArgs e)
        {
            PlaySound("click");
        }

        private void pictureBoxVolumeDownMusic_Click(object sender, EventArgs e)
        {
            PlaySound("button");
            if (barMusic1 == true && barMusic2 == true && barMusic3 == true && barMusic4 == true & barMusic5 == true)
            {
                pictureBoxBarMusic5.BackgroundImage = Properties.Resources.volumeBar;
                sound2.settings.volume = 80;
                barMusic5 = false;
            }
            else if (barMusic1 == true && barMusic2 == true && barMusic3 == true && barMusic4 == true)
            {
                pictureBoxBarMusic5.BackgroundImage = Properties.Resources.volumeBar;
                pictureBoxBarMusic4.BackgroundImage = Properties.Resources.volumeBar;
                sound2.settings.volume = 60;
                barMusic5 = false;
                barMusic4 = false;
            }
            else if (barMusic1 == true && barMusic2 == true && barMusic3 == true)
            {
                pictureBoxBarMusic5.BackgroundImage = Properties.Resources.volumeBar;
                pictureBoxBarMusic4.BackgroundImage = Properties.Resources.volumeBar;
                pictureBoxBarMusic3.BackgroundImage = Properties.Resources.volumeBar;
                sound2.settings.volume = 40;
                barMusic5 = false;
                barMusic4 = false;
                barMusic3 = false;
            }
            else if (barMusic1 == true && barMusic2 == true)
            {
                pictureBoxBarMusic5.BackgroundImage = Properties.Resources.volumeBar;
                pictureBoxBarMusic4.BackgroundImage = Properties.Resources.volumeBar;
                pictureBoxBarMusic3.BackgroundImage = Properties.Resources.volumeBar;
                pictureBoxBarMusic2.BackgroundImage = Properties.Resources.volumeBar;
                sound2.settings.volume = 20;
                barMusic5 = false;
                barMusic4 = false;
                barMusic3 = false;
                barMusic2 = false;
            }
            else if (barMusic1 == true)
            {
                pictureBoxBarMusic5.BackgroundImage = Properties.Resources.volumeBar;
                pictureBoxBarMusic4.BackgroundImage = Properties.Resources.volumeBar;
                pictureBoxBarMusic3.BackgroundImage = Properties.Resources.volumeBar;
                pictureBoxBarMusic2.BackgroundImage = Properties.Resources.volumeBar;
                pictureBoxBarMusic1.BackgroundImage = Properties.Resources.volumeBar;
                sound2.settings.volume = 0;
                barMusic5 = false;
                barMusic4 = false;
                barMusic3 = false;
                barMusic2 = false;
                barMusic1 = false;
            }
        }

        private void pictureBoxVolumeUpMusic_Click(object sender, EventArgs e)
        {
            PlaySound("button");
            if (barMusic1 == false && barMusic2 == false && barMusic3 == false && barMusic4 == false & barMusic5 == false)
            {
                pictureBoxBarMusic1.BackgroundImage = Properties.Resources.volumeBarFull;
                sound2.settings.volume = 20;
                barMusic1 = true;
            }
            else if (barMusic2 == false && barMusic3 == false && barMusic4 == false && barMusic5 == false)
            {
                pictureBoxBarMusic1.BackgroundImage = Properties.Resources.volumeBarFull;
                pictureBoxBarMusic2.BackgroundImage = Properties.Resources.volumeBarFull;
                sound2.settings.volume = 40;
                barMusic1 = true;
                barMusic2 = true;
            }
            else if (barMusic3 == false && barMusic4 == false && barMusic5 == false)
            {
                pictureBoxBarMusic1.BackgroundImage = Properties.Resources.volumeBarFull;
                pictureBoxBarMusic2.BackgroundImage = Properties.Resources.volumeBarFull;
                pictureBoxBarMusic3.BackgroundImage = Properties.Resources.volumeBarFull;
                sound2.settings.volume = 60;
                barMusic1 = true;
                barMusic2 = true;
                barMusic3 = true;
            }
            else if (barMusic4 == false && barMusic5 == false)
            {
                pictureBoxBarMusic1.BackgroundImage = Properties.Resources.volumeBarFull;
                pictureBoxBarMusic2.BackgroundImage = Properties.Resources.volumeBarFull;
                pictureBoxBarMusic3.BackgroundImage = Properties.Resources.volumeBarFull;
                pictureBoxBarMusic4.BackgroundImage = Properties.Resources.volumeBarFull;
                sound2.settings.volume = 80;
                barMusic1 = true;
                barMusic2 = true;
                barMusic3 = true;
                barMusic4 = true;
            }
            else if (barMusic5 == false)
            {
                pictureBoxBarMusic1.BackgroundImage = Properties.Resources.volumeBarFull;
                pictureBoxBarMusic2.BackgroundImage = Properties.Resources.volumeBarFull;
                pictureBoxBarMusic3.BackgroundImage = Properties.Resources.volumeBarFull;
                pictureBoxBarMusic4.BackgroundImage = Properties.Resources.volumeBarFull;
                pictureBoxBarMusic5.BackgroundImage = Properties.Resources.volumeBarFull;
                sound2.settings.volume = 100;
                barMusic1 = true;
                barMusic2 = true;
                barMusic3 = true;
                barMusic4 = true;
                barMusic5 = true;
            }
        }

        private void pictureBoxVolumeDownMusic_MouseEnter(object sender, EventArgs e)
        {
            pictureBoxVolumeDownMusic.BackgroundImage = Properties.Resources.buttonVolDownFull;
        }

        private void pictureBoxVolumeDownMusic_MouseLeave(object sender, EventArgs e)
        {
            pictureBoxVolumeDownMusic.BackgroundImage = Properties.Resources.buttonVolDown;
        }

        private void pictureBoxVolumeUpMusic_MouseEnter(object sender, EventArgs e)
        {
            pictureBoxVolumeUpMusic.BackgroundImage = Properties.Resources.buttonVolUpFull;
        }

        private void pictureBoxVolumeUpMusic_MouseLeave(object sender, EventArgs e)
        {
            pictureBoxVolumeUpMusic.BackgroundImage = Properties.Resources.buttonVolUp;
        }

        private void pictureBoxVolumeDownSFX_MouseEnter(object sender, EventArgs e)
        {
            pictureBoxVolumeDownSFX.BackgroundImage = Properties.Resources.buttonVolDownFull;
        }

        private void pictureBoxVolumeDownSFX_MouseLeave(object sender, EventArgs e)
        {
            pictureBoxVolumeDownSFX.BackgroundImage = Properties.Resources.buttonVolDown;
        }

        private void pictureBoxVolmeUpSFX_MouseLeave(object sender, EventArgs e)
        {
            pictureBoxVolmeUpSFX.BackgroundImage = Properties.Resources.buttonVolUp;
        }

        private void pictureBoxVolmeUpSFX_MouseEnter(object sender, EventArgs e)
        {
            pictureBoxVolmeUpSFX.BackgroundImage = Properties.Resources.buttonVolUpFull;
        }

        private void pictureBoxVolumeDownSFX_Click(object sender, EventArgs e)
        {
            PlaySound("button");
            if (barSFX1 == true && barSFX2 == true && barSFX3 == true && barSFX4 == true & barSFX5 == true)
            {
                pictureBoxVolumeBarSFX5.BackgroundImage = Properties.Resources.volumeBar;
                sound1.settings.volume = 80;
                barSFX5 = false;
            }
            else if (barSFX1 == true && barSFX2 == true && barSFX3 == true && barSFX4 == true)
            {
                pictureBoxVolumeBarSFX5.BackgroundImage = Properties.Resources.volumeBar;
                pictureBoxVolumeBarSFX4.BackgroundImage = Properties.Resources.volumeBar;
                sound1.settings.volume = 60;
                barSFX5 = false;
                barSFX4 = false;
            }
            else if (barSFX1 == true && barSFX2 == true && barSFX3 == true)
            {
                pictureBoxVolumeBarSFX5.BackgroundImage = Properties.Resources.volumeBar;
                pictureBoxVolumeBarSFX4.BackgroundImage = Properties.Resources.volumeBar;
                pictureBoxVolumeBarSFX3.BackgroundImage = Properties.Resources.volumeBar;
                sound1.settings.volume = 40;
                barSFX5 = false;
                barSFX4 = false;
                barSFX3 = false;
            }
            else if (barSFX1 == true && barSFX2 == true)
            {
                pictureBoxVolumeBarSFX5.BackgroundImage = Properties.Resources.volumeBar;
                pictureBoxVolumeBarSFX4.BackgroundImage = Properties.Resources.volumeBar;
                pictureBoxVolumeBarSFX3.BackgroundImage = Properties.Resources.volumeBar;
                pictureBoxVolumeBarSFX2.BackgroundImage = Properties.Resources.volumeBar;
                sound1.settings.volume = 20;
                barSFX5 = false;
                barSFX4 = false;
                barSFX3 = false;
                barSFX2 = false;
            }
            else if (barSFX1 == true)
            {
                pictureBoxVolumeBarSFX5.BackgroundImage = Properties.Resources.volumeBar;
                pictureBoxVolumeBarSFX4.BackgroundImage = Properties.Resources.volumeBar;
                pictureBoxVolumeBarSFX3.BackgroundImage = Properties.Resources.volumeBar;
                pictureBoxVolumeBarSFX2.BackgroundImage = Properties.Resources.volumeBar;
                pictureBoxVolumeBarSFX1.BackgroundImage = Properties.Resources.volumeBar;
                sound1.settings.volume = 0;
                barSFX5 = false;
                barSFX4 = false;
                barSFX3 = false;
                barSFX2 = false;
                barSFX1 = false;
            }
        }

        private void pictureBoxVolmeUpSFX_Click(object sender, EventArgs e)
        {
            PlaySound("button");
            if (barSFX1 == false && barSFX2 == false && barSFX3 == false && barSFX4 == false & barSFX5 == false)
            {
                pictureBoxVolumeBarSFX1.BackgroundImage = Properties.Resources.volumeBarFull;
                sound1.settings.volume = 20;
                barSFX1 = true;
            }
            else if (barSFX2 == false && barSFX3 == false && barSFX4 == false & barSFX5 == false)
            {
                pictureBoxVolumeBarSFX1.BackgroundImage = Properties.Resources.volumeBarFull;
                pictureBoxVolumeBarSFX2.BackgroundImage = Properties.Resources.volumeBarFull;
                sound1.settings.volume = 40;
                barSFX1 = true;
                barSFX2 = true;
            }
            else if (barSFX3 == false && barSFX4 == false & barSFX5 == false)
            {
                pictureBoxVolumeBarSFX1.BackgroundImage = Properties.Resources.volumeBarFull;
                pictureBoxVolumeBarSFX2.BackgroundImage = Properties.Resources.volumeBarFull;
                pictureBoxVolumeBarSFX3.BackgroundImage = Properties.Resources.volumeBarFull;
                sound1.settings.volume = 60;
                barSFX1 = true;
                barSFX2 = true;
                barSFX3 = true;
            }
            else if (barSFX4 == false & barSFX5 == false)
            {
                pictureBoxVolumeBarSFX1.BackgroundImage = Properties.Resources.volumeBarFull;
                pictureBoxVolumeBarSFX2.BackgroundImage = Properties.Resources.volumeBarFull;
                pictureBoxVolumeBarSFX3.BackgroundImage = Properties.Resources.volumeBarFull;
                pictureBoxVolumeBarSFX4.BackgroundImage = Properties.Resources.volumeBarFull;
                sound1.settings.volume = 80;
                barSFX1 = true;
                barSFX2 = true;
                barSFX3 = true;
                barSFX4 = true;
            }
            else if (barSFX5 == false)
            {
                pictureBoxVolumeBarSFX1.BackgroundImage = Properties.Resources.volumeBarFull;
                pictureBoxVolumeBarSFX2.BackgroundImage = Properties.Resources.volumeBarFull;
                pictureBoxVolumeBarSFX3.BackgroundImage = Properties.Resources.volumeBarFull;
                pictureBoxVolumeBarSFX4.BackgroundImage = Properties.Resources.volumeBarFull;
                pictureBoxVolumeBarSFX5.BackgroundImage = Properties.Resources.volumeBarFull;
                sound1.settings.volume = 100;
                barSFX1 = true;
                barSFX2 = true;
                barSFX3 = true;
                barSFX4 = true;
                barSFX5 = true;
            }
        }
    }
}
