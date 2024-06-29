namespace Project
{
    partial class FormGame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonPlay = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.radioButtonLoadPlayer = new System.Windows.Forms.RadioButton();
            this.pictureBoxBack = new System.Windows.Forms.PictureBox();
            this.pictureBoxLoadPlayer = new System.Windows.Forms.PictureBox();
            this.buttonNext = new System.Windows.Forms.Button();
            this.panelCreatePlayer = new System.Windows.Forms.Panel();
            this.pictureBoxFemale = new System.Windows.Forms.PictureBox();
            this.pictureBoxMale = new System.Windows.Forms.PictureBox();
            this.radioButtonCreatePlayer = new System.Windows.Forms.RadioButton();
            this.panelLoadPlayer = new System.Windows.Forms.Panel();
            this.panelCreateLoadPlayer = new System.Windows.Forms.Panel();
            this.labelNameCreate = new System.Windows.Forms.Label();
            this.radioButtonMale = new System.Windows.Forms.RadioButton();
            this.radioButtonFemale = new System.Windows.Forms.RadioButton();
            this.labelNameLoad = new System.Windows.Forms.Label();
            this.labelIncomeLoad = new System.Windows.Forms.Label();
            this.comboBoxNameLoad = new System.Windows.Forms.ComboBox();
            this.labelHSLoad = new System.Windows.Forms.Label();
            this.textBoxNameCreate = new System.Windows.Forms.TextBox();
            this.labelIncome = new System.Windows.Forms.Label();
            this.labelHS = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoadPlayer)).BeginInit();
            this.panelCreatePlayer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFemale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMale)).BeginInit();
            this.panelLoadPlayer.SuspendLayout();
            this.panelCreateLoadPlayer.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonPlay
            // 
            this.buttonPlay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(217)))), ((int)(((byte)(87)))));
            this.buttonPlay.Font = new System.Drawing.Font("Franklin Gothic Demi", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPlay.ForeColor = System.Drawing.Color.White;
            this.buttonPlay.Location = new System.Drawing.Point(493, 440);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(180, 60);
            this.buttonPlay.TabIndex = 0;
            this.buttonPlay.Text = "PLAY";
            this.buttonPlay.UseVisualStyleBackColor = false;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.Red;
            this.buttonExit.Font = new System.Drawing.Font("Franklin Gothic Demi", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.ForeColor = System.Drawing.Color.White;
            this.buttonExit.Location = new System.Drawing.Point(493, 525);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(180, 60);
            this.buttonExit.TabIndex = 1;
            this.buttonExit.Text = "EXIT";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // radioButtonLoadPlayer
            // 
            this.radioButtonLoadPlayer.AutoSize = true;
            this.radioButtonLoadPlayer.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonLoadPlayer.Location = new System.Drawing.Point(677, 94);
            this.radioButtonLoadPlayer.Name = "radioButtonLoadPlayer";
            this.radioButtonLoadPlayer.Size = new System.Drawing.Size(147, 33);
            this.radioButtonLoadPlayer.TabIndex = 1;
            this.radioButtonLoadPlayer.TabStop = true;
            this.radioButtonLoadPlayer.Text = "LOAD PLAYER";
            this.radioButtonLoadPlayer.UseVisualStyleBackColor = true;
            // 
            // pictureBoxBack
            // 
            this.pictureBoxBack.BackgroundImage = global::Project.Properties.Resources.back;
            this.pictureBoxBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxBack.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxBack.Name = "pictureBoxBack";
            this.pictureBoxBack.Size = new System.Drawing.Size(75, 74);
            this.pictureBoxBack.TabIndex = 2;
            this.pictureBoxBack.TabStop = false;
            this.pictureBoxBack.Click += new System.EventHandler(this.pictureBoxBack_Click);
            // 
            // pictureBoxLoadPlayer
            // 
            this.pictureBoxLoadPlayer.BackgroundImage = global::Project.Properties.Resources.female;
            this.pictureBoxLoadPlayer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxLoadPlayer.Location = new System.Drawing.Point(136, 36);
            this.pictureBoxLoadPlayer.Name = "pictureBoxLoadPlayer";
            this.pictureBoxLoadPlayer.Size = new System.Drawing.Size(161, 187);
            this.pictureBoxLoadPlayer.TabIndex = 5;
            this.pictureBoxLoadPlayer.TabStop = false;
            // 
            // buttonNext
            // 
            this.buttonNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(217)))), ((int)(((byte)(87)))));
            this.buttonNext.Font = new System.Drawing.Font("Franklin Gothic Demi", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNext.ForeColor = System.Drawing.Color.White;
            this.buttonNext.Location = new System.Drawing.Point(485, 546);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(180, 60);
            this.buttonNext.TabIndex = 6;
            this.buttonNext.Text = "NEXT";
            this.buttonNext.UseVisualStyleBackColor = false;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // panelCreatePlayer
            // 
            this.panelCreatePlayer.Controls.Add(this.textBoxNameCreate);
            this.panelCreatePlayer.Controls.Add(this.radioButtonFemale);
            this.panelCreatePlayer.Controls.Add(this.radioButtonMale);
            this.panelCreatePlayer.Controls.Add(this.labelNameCreate);
            this.panelCreatePlayer.Controls.Add(this.pictureBoxMale);
            this.panelCreatePlayer.Controls.Add(this.pictureBoxFemale);
            this.panelCreatePlayer.Location = new System.Drawing.Point(93, 133);
            this.panelCreatePlayer.Name = "panelCreatePlayer";
            this.panelCreatePlayer.Size = new System.Drawing.Size(393, 367);
            this.panelCreatePlayer.TabIndex = 7;
            // 
            // pictureBoxFemale
            // 
            this.pictureBoxFemale.BackgroundImage = global::Project.Properties.Resources.female;
            this.pictureBoxFemale.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxFemale.Location = new System.Drawing.Point(188, 36);
            this.pictureBoxFemale.Name = "pictureBoxFemale";
            this.pictureBoxFemale.Size = new System.Drawing.Size(178, 187);
            this.pictureBoxFemale.TabIndex = 4;
            this.pictureBoxFemale.TabStop = false;
            // 
            // pictureBoxMale
            // 
            this.pictureBoxMale.BackgroundImage = global::Project.Properties.Resources.male;
            this.pictureBoxMale.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxMale.Location = new System.Drawing.Point(26, 36);
            this.pictureBoxMale.Name = "pictureBoxMale";
            this.pictureBoxMale.Size = new System.Drawing.Size(156, 187);
            this.pictureBoxMale.TabIndex = 3;
            this.pictureBoxMale.TabStop = false;
            this.pictureBoxMale.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // radioButtonCreatePlayer
            // 
            this.radioButtonCreatePlayer.AutoSize = true;
            this.radioButtonCreatePlayer.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonCreatePlayer.Location = new System.Drawing.Point(108, 94);
            this.radioButtonCreatePlayer.Name = "radioButtonCreatePlayer";
            this.radioButtonCreatePlayer.Size = new System.Drawing.Size(167, 33);
            this.radioButtonCreatePlayer.TabIndex = 0;
            this.radioButtonCreatePlayer.TabStop = true;
            this.radioButtonCreatePlayer.Text = "CREATE PLAYER";
            this.radioButtonCreatePlayer.UseVisualStyleBackColor = true;
            // 
            // panelLoadPlayer
            // 
            this.panelLoadPlayer.Controls.Add(this.labelHS);
            this.panelLoadPlayer.Controls.Add(this.labelIncome);
            this.panelLoadPlayer.Controls.Add(this.labelHSLoad);
            this.panelLoadPlayer.Controls.Add(this.comboBoxNameLoad);
            this.panelLoadPlayer.Controls.Add(this.labelIncomeLoad);
            this.panelLoadPlayer.Controls.Add(this.labelNameLoad);
            this.panelLoadPlayer.Controls.Add(this.pictureBoxLoadPlayer);
            this.panelLoadPlayer.Location = new System.Drawing.Point(649, 133);
            this.panelLoadPlayer.Name = "panelLoadPlayer";
            this.panelLoadPlayer.Size = new System.Drawing.Size(406, 367);
            this.panelLoadPlayer.TabIndex = 8;
            // 
            // panelCreateLoadPlayer
            // 
            this.panelCreateLoadPlayer.BackColor = System.Drawing.Color.Transparent;
            this.panelCreateLoadPlayer.BackgroundImage = global::Project.Properties.Resources.bg_CreateLoadPlayer;
            this.panelCreateLoadPlayer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelCreateLoadPlayer.Controls.Add(this.radioButtonLoadPlayer);
            this.panelCreateLoadPlayer.Controls.Add(this.panelLoadPlayer);
            this.panelCreateLoadPlayer.Controls.Add(this.panelCreatePlayer);
            this.panelCreateLoadPlayer.Controls.Add(this.buttonNext);
            this.panelCreateLoadPlayer.Controls.Add(this.radioButtonCreatePlayer);
            this.panelCreateLoadPlayer.Controls.Add(this.pictureBoxBack);
            this.panelCreateLoadPlayer.Location = new System.Drawing.Point(0, 0);
            this.panelCreateLoadPlayer.Name = "panelCreateLoadPlayer";
            this.panelCreateLoadPlayer.Size = new System.Drawing.Size(1150, 650);
            this.panelCreateLoadPlayer.TabIndex = 2;
            // 
            // labelNameCreate
            // 
            this.labelNameCreate.AutoSize = true;
            this.labelNameCreate.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNameCreate.Location = new System.Drawing.Point(76, 286);
            this.labelNameCreate.Name = "labelNameCreate";
            this.labelNameCreate.Size = new System.Drawing.Size(73, 25);
            this.labelNameCreate.TabIndex = 9;
            this.labelNameCreate.Text = "Name :";
            // 
            // radioButtonMale
            // 
            this.radioButtonMale.AutoSize = true;
            this.radioButtonMale.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonMale.Location = new System.Drawing.Point(61, 229);
            this.radioButtonMale.Name = "radioButtonMale";
            this.radioButtonMale.Size = new System.Drawing.Size(76, 29);
            this.radioButtonMale.TabIndex = 10;
            this.radioButtonMale.TabStop = true;
            this.radioButtonMale.Text = "Male";
            this.radioButtonMale.UseVisualStyleBackColor = true;
            // 
            // radioButtonFemale
            // 
            this.radioButtonFemale.AutoSize = true;
            this.radioButtonFemale.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonFemale.Location = new System.Drawing.Point(224, 229);
            this.radioButtonFemale.Name = "radioButtonFemale";
            this.radioButtonFemale.Size = new System.Drawing.Size(98, 29);
            this.radioButtonFemale.TabIndex = 11;
            this.radioButtonFemale.TabStop = true;
            this.radioButtonFemale.Text = "Female";
            this.radioButtonFemale.UseVisualStyleBackColor = true;
            // 
            // labelNameLoad
            // 
            this.labelNameLoad.AutoSize = true;
            this.labelNameLoad.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNameLoad.Location = new System.Drawing.Point(88, 238);
            this.labelNameLoad.Name = "labelNameLoad";
            this.labelNameLoad.Size = new System.Drawing.Size(73, 25);
            this.labelNameLoad.TabIndex = 13;
            this.labelNameLoad.Text = "Name :";
            // 
            // labelIncomeLoad
            // 
            this.labelIncomeLoad.AutoSize = true;
            this.labelIncomeLoad.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelIncomeLoad.Location = new System.Drawing.Point(75, 277);
            this.labelIncomeLoad.Name = "labelIncomeLoad";
            this.labelIncomeLoad.Size = new System.Drawing.Size(85, 25);
            this.labelIncomeLoad.TabIndex = 15;
            this.labelIncomeLoad.Text = "Income :";
            // 
            // comboBoxNameLoad
            // 
            this.comboBoxNameLoad.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxNameLoad.FormattingEnabled = true;
            this.comboBoxNameLoad.Location = new System.Drawing.Point(163, 235);
            this.comboBoxNameLoad.Name = "comboBoxNameLoad";
            this.comboBoxNameLoad.Size = new System.Drawing.Size(176, 33);
            this.comboBoxNameLoad.TabIndex = 19;
            // 
            // labelHSLoad
            // 
            this.labelHSLoad.AutoSize = true;
            this.labelHSLoad.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHSLoad.Location = new System.Drawing.Point(45, 318);
            this.labelHSLoad.Name = "labelHSLoad";
            this.labelHSLoad.Size = new System.Drawing.Size(114, 25);
            this.labelHSLoad.TabIndex = 20;
            this.labelHSLoad.Text = "High Score :";
            // 
            // textBoxNameCreate
            // 
            this.textBoxNameCreate.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNameCreate.Location = new System.Drawing.Point(151, 285);
            this.textBoxNameCreate.Name = "textBoxNameCreate";
            this.textBoxNameCreate.Size = new System.Drawing.Size(150, 30);
            this.textBoxNameCreate.TabIndex = 12;
            // 
            // labelIncome
            // 
            this.labelIncome.AutoSize = true;
            this.labelIncome.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelIncome.Location = new System.Drawing.Point(158, 277);
            this.labelIncome.Name = "labelIncome";
            this.labelIncome.Size = new System.Drawing.Size(48, 25);
            this.labelIncome.TabIndex = 21;
            this.labelIncome.Text = "200";
            // 
            // labelHS
            // 
            this.labelHS.AutoSize = true;
            this.labelHS.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHS.Location = new System.Drawing.Point(158, 318);
            this.labelHS.Name = "labelHS";
            this.labelHS.Size = new System.Drawing.Size(48, 25);
            this.labelHS.TabIndex = 22;
            this.labelHS.Text = "200";
            // 
            // FormGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Project.Properties.Resources.Background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1149, 650);
            this.Controls.Add(this.panelCreateLoadPlayer);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonPlay);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormGame";
            this.Text = "Food Wars";
            this.Load += new System.EventHandler(this.FormGame_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoadPlayer)).EndInit();
            this.panelCreatePlayer.ResumeLayout(false);
            this.panelCreatePlayer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFemale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMale)).EndInit();
            this.panelLoadPlayer.ResumeLayout(false);
            this.panelLoadPlayer.PerformLayout();
            this.panelCreateLoadPlayer.ResumeLayout(false);
            this.panelCreateLoadPlayer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.RadioButton radioButtonLoadPlayer;
        private System.Windows.Forms.PictureBox pictureBoxBack;
        private System.Windows.Forms.PictureBox pictureBoxLoadPlayer;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Panel panelCreatePlayer;
        private System.Windows.Forms.Label labelNameCreate;
        private System.Windows.Forms.RadioButton radioButtonCreatePlayer;
        private System.Windows.Forms.PictureBox pictureBoxMale;
        private System.Windows.Forms.PictureBox pictureBoxFemale;
        private System.Windows.Forms.Panel panelLoadPlayer;
        private System.Windows.Forms.Panel panelCreateLoadPlayer;
        private System.Windows.Forms.RadioButton radioButtonFemale;
        private System.Windows.Forms.RadioButton radioButtonMale;
        private System.Windows.Forms.Label labelHSLoad;
        private System.Windows.Forms.ComboBox comboBoxNameLoad;
        private System.Windows.Forms.Label labelIncomeLoad;
        private System.Windows.Forms.Label labelNameLoad;
        private System.Windows.Forms.TextBox textBoxNameCreate;
        protected internal System.Windows.Forms.Label labelHS;
        protected internal System.Windows.Forms.Label labelIncome;
    }
}