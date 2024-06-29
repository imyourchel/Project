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
            this.BackgroundImage = Properties.Resources.Background;
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
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            BackgroundInvisible();
            //display panel create load player
            panelCreateLoadPlayer.Visible = true;
            panelCreateLoadPlayer.BackgroundImage = Properties.Resources.bg_CreateLoadPlayer;
            panelCreateLoadPlayer.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {

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
            panelDifficulty.Visible = false;
            panelTutorial.Visible = true;
            panelTutorial.BackgroundImage = Properties.Resources.bg_Tutorial;
            panelTutorial.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            panelTutorial.Visible = false;
            panelGame.Visible = true;
            panelGame.BackgroundImage = Properties.Resources.bg_Ingame;
            panelGame.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void panelGame_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }
    }
}
