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

        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            BackgroundInvisible();
            panelCreateLoadPlayer.Visible = true;
            panelCreateLoadPlayer.BackgroundImage = Properties.Resources.bg_CreateLoadPlayer;
            panelCreateLoadPlayer.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxBack_Click(object sender, EventArgs e)
        {
            BackgroundVisible();
            panelCreateLoadPlayer.Visible=false;
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            panelCreateLoadPlayer.Visible = false;
        }
    }
}
