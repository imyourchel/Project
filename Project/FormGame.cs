using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
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
            if (listOfPlayer == null)
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
                    else if (radioButtonMale.Checked)
                    {
                        pic = Properties.Resources.female;
                    }                    
                    time = new Time(0,0,0);
                    player = new Players(textBoxNameCreate.Text, 0, pic,time);
                }
                //Load Player
                else
                {
                    player = (Players)comboBoxNameLoad.SelectedItem;

                    labelIncome.Text = player.Income.ToString();

                }
                //display Panel Difficulty
                panelCreateLoadPlayer.Visible = false;
                panelDifficulty.Visible = true;
                panelDifficulty.BackgroundImage = Properties.Resources.bg_Difficulty;
                panelDifficulty.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBoxBackCreateLoadPlayer_Click(object sender, EventArgs e)
        {
            panelCreateLoadPlayer.Visible = true;
            panelDifficulty.Visible = false;
        }

        private void buttonNextTutorial_Click(object sender, EventArgs e)
        {
            buttonBackReceipe.Visible = false;
            panelDifficulty.Visible = false;
            panelTutorial.Visible = true;
            panelTutorial.BackgroundImage = Properties.Resources.bg_Tutorial;
            panelTutorial.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            panelTutorial.Visible = false;
            panelGame.Visible = true;
            panelGame.BackgroundImageLayout = ImageLayout.Stretch;
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
            panelSetting.Visible = true;
        }

        private void pictureBoxResume_Click(object sender, EventArgs e)
        {
            panelSetting.Visible = false;
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
        private void ChangeBackgroundLabel(Label label, string status, Color color)
        {
            if (status == "enter")
            {
                label.BackColor = Color.Silver;
            }
            else if (status == "leave")
            {
                label.BackColor = color;

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

        private void labelLarge_MouseEnter(object sender, EventArgs e)
        {            
            Color color = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(177)))), ((int)(((byte)(177)))));
            ChangeBackgroundLabel(labelLarge, "enter", color);
        }

        private void labelLarge_MouseLeave(object sender, EventArgs e)
        {
            Color color = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(177)))), ((int)(((byte)(177)))));
            ChangeBackgroundLabel(labelLarge, "leave", color);
        }

        private void labelMedium_MouseEnter(object sender, EventArgs e)
        {
            Color color = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(58)))), ((int)(((byte)(60)))));
            ChangeBackgroundLabel(labelMedium, "enter", color);
        }

        private void labelMedium_MouseLeave(object sender, EventArgs e)
        {
            Color color = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(58)))), ((int)(((byte)(60)))));
            ChangeBackgroundLabel(labelMedium, "leave", color);
        }

        private void labelSmall_MouseEnter(object sender, EventArgs e)
        {
            Color color = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(167)))), ((int)(((byte)(78)))));
            ChangeBackgroundLabel(labelSmall, "enter", color);
        }

        private void labelSmall_MouseLeave(object sender, EventArgs e)
        {
            Color color = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(167)))), ((int)(((byte)(78)))));
            ChangeBackgroundLabel(labelSmall, "leave", color);
        }
        #endregion Foods & Beverages
        #endregion PictureBox Mouse

        private void StallDisplay()
        {
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
            pictureBoxTopBun.Tag = ((Foods)item).ListOfIngredients[2].Name;

            pictureBoxPlate.Image = ((Foods)item).ListOfIngredients[0].Picture;
            pictureBoxLettuce.Image = ((Foods)item).ListOfIngredients[1].Picture;
            pictureBoxTopBun.Image = ((Foods)item).ListOfIngredients[2].Picture;

            pictureBoxPlate.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxLettuce.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxTopBun.SizeMode = PictureBoxSizeMode.StretchImage;


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

            item = new Beverages(false, "L", "coffeeLHot", Properties.Resources.hotL, 25);
            listOfItems.Add(item);
            
        }
    }
}
