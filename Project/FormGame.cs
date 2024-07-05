using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class FormGame : Form
    {
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
            panelCreateLoadPlayer.Visible = false;
            panelDifficulty.Visible = true;
            panelDifficulty.BackgroundImage = Properties.Resources.bg_Difficulty;
            panelDifficulty.BackgroundImageLayout = ImageLayout.Stretch;
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
    }
}
