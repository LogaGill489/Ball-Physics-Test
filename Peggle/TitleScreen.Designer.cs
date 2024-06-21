namespace Ball_Physics_Test
{
    partial class TitleScreen
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sideBar = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.hatImage = new System.Windows.Forms.PictureBox();
            this.notQuiteLabel = new System.Windows.Forms.Label();
            this.copyrightLabel = new System.Windows.Forms.Label();
            this.rocks1 = new System.Windows.Forms.PictureBox();
            this.rocks2 = new System.Windows.Forms.PictureBox();
            this.flyingBallImage = new System.Windows.Forms.PictureBox();
            this.mastersButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.hatImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rocks1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rocks2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flyingBallImage)).BeginInit();
            this.SuspendLayout();
            // 
            // sideBar
            // 
            this.sideBar.BackColor = System.Drawing.Color.DimGray;
            this.sideBar.Location = new System.Drawing.Point(1319, -23);
            this.sideBar.Name = "sideBar";
            this.sideBar.Size = new System.Drawing.Size(481, 1250);
            this.sideBar.TabIndex = 0;
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.BurlyWood;
            this.startButton.Font = new System.Drawing.Font("Elephant", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.ForeColor = System.Drawing.Color.Black;
            this.startButton.Location = new System.Drawing.Point(1346, 181);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(426, 159);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.BurlyWood;
            this.exitButton.Font = new System.Drawing.Font("Elephant", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitButton.ForeColor = System.Drawing.Color.Black;
            this.exitButton.Location = new System.Drawing.Point(1346, 693);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(426, 159);
            this.exitButton.TabIndex = 2;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // titleLabel
            // 
            this.titleLabel.BackColor = System.Drawing.Color.Transparent;
            this.titleLabel.Font = new System.Drawing.Font("Tempus Sans ITC", 199.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.Black;
            this.titleLabel.Location = new System.Drawing.Point(3, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(1310, 628);
            this.titleLabel.TabIndex = 3;
            this.titleLabel.Text = "Peggle";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // hatImage
            // 
            this.hatImage.BackColor = System.Drawing.Color.Transparent;
            this.hatImage.BackgroundImage = global::Ball_Physics_Test.Properties.Resources.magicHat;
            this.hatImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.hatImage.Location = new System.Drawing.Point(822, 21);
            this.hatImage.Name = "hatImage";
            this.hatImage.Size = new System.Drawing.Size(233, 256);
            this.hatImage.TabIndex = 4;
            this.hatImage.TabStop = false;
            // 
            // notQuiteLabel
            // 
            this.notQuiteLabel.BackColor = System.Drawing.Color.Transparent;
            this.notQuiteLabel.Font = new System.Drawing.Font("Palatino Linotype", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notQuiteLabel.ForeColor = System.Drawing.Color.Black;
            this.notQuiteLabel.Location = new System.Drawing.Point(69, 139);
            this.notQuiteLabel.Name = "notQuiteLabel";
            this.notQuiteLabel.Size = new System.Drawing.Size(432, 37);
            this.notQuiteLabel.TabIndex = 5;
            this.notQuiteLabel.Text = "\"Not Quite\"";
            this.notQuiteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // copyrightLabel
            // 
            this.copyrightLabel.BackColor = System.Drawing.Color.DimGray;
            this.copyrightLabel.Font = new System.Drawing.Font("Vladimir Script", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copyrightLabel.ForeColor = System.Drawing.Color.Black;
            this.copyrightLabel.Location = new System.Drawing.Point(1596, 973);
            this.copyrightLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.copyrightLabel.Name = "copyrightLabel";
            this.copyrightLabel.Size = new System.Drawing.Size(204, 27);
            this.copyrightLabel.TabIndex = 14;
            this.copyrightLabel.Text = "LGPeggleSoftware Ltd.";
            // 
            // rocks1
            // 
            this.rocks1.BackColor = System.Drawing.Color.Transparent;
            this.rocks1.Image = global::Ball_Physics_Test.Properties.Resources.rocks;
            this.rocks1.Location = new System.Drawing.Point(-246, 693);
            this.rocks1.Name = "rocks1";
            this.rocks1.Size = new System.Drawing.Size(747, 434);
            this.rocks1.TabIndex = 15;
            this.rocks1.TabStop = false;
            // 
            // rocks2
            // 
            this.rocks2.BackColor = System.Drawing.Color.Transparent;
            this.rocks2.Image = global::Ball_Physics_Test.Properties.Resources.rocks;
            this.rocks2.Location = new System.Drawing.Point(985, 597);
            this.rocks2.Name = "rocks2";
            this.rocks2.Size = new System.Drawing.Size(747, 434);
            this.rocks2.TabIndex = 16;
            this.rocks2.TabStop = false;
            // 
            // flyingBallImage
            // 
            this.flyingBallImage.BackColor = System.Drawing.Color.DimGray;
            this.flyingBallImage.BackgroundImage = global::Ball_Physics_Test.Properties.Resources.ballArt;
            this.flyingBallImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.flyingBallImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flyingBallImage.Location = new System.Drawing.Point(1394, 346);
            this.flyingBallImage.Name = "flyingBallImage";
            this.flyingBallImage.Size = new System.Drawing.Size(286, 325);
            this.flyingBallImage.TabIndex = 17;
            this.flyingBallImage.TabStop = false;
            // 
            // mastersButton
            // 
            this.mastersButton.BackColor = System.Drawing.Color.Gray;
            this.mastersButton.Font = new System.Drawing.Font("Elephant", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mastersButton.ForeColor = System.Drawing.Color.Black;
            this.mastersButton.Location = new System.Drawing.Point(507, 861);
            this.mastersButton.Name = "mastersButton";
            this.mastersButton.Size = new System.Drawing.Size(377, 72);
            this.mastersButton.TabIndex = 18;
            this.mastersButton.Text = "Peggle Masters";
            this.mastersButton.UseVisualStyleBackColor = false;
            this.mastersButton.Click += new System.EventHandler(this.mastersButton_Click);
            // 
            // TitleScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Ball_Physics_Test.Properties.Resources.worldBackground1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.mastersButton);
            this.Controls.Add(this.flyingBallImage);
            this.Controls.Add(this.rocks1);
            this.Controls.Add(this.copyrightLabel);
            this.Controls.Add(this.hatImage);
            this.Controls.Add(this.notQuiteLabel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.sideBar);
            this.Controls.Add(this.rocks2);
            this.DoubleBuffered = true;
            this.Name = "TitleScreen";
            this.Size = new System.Drawing.Size(1800, 1000);
            this.Load += new System.EventHandler(this.TitleScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.hatImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rocks1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rocks2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flyingBallImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label sideBar;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.PictureBox hatImage;
        private System.Windows.Forms.Label notQuiteLabel;
        private System.Windows.Forms.Label copyrightLabel;
        private System.Windows.Forms.PictureBox rocks1;
        private System.Windows.Forms.PictureBox rocks2;
        private System.Windows.Forms.PictureBox flyingBallImage;
        private System.Windows.Forms.Button mastersButton;
    }
}
