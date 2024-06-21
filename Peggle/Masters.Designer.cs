namespace Ball_Physics_Test
{
    partial class Masters
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
            this.winnerText2 = new System.Windows.Forms.Label();
            this.returnButton = new System.Windows.Forms.Button();
            this.winnerText3 = new System.Windows.Forms.Label();
            this.winnerText1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // winnerText2
            // 
            this.winnerText2.BackColor = System.Drawing.Color.Transparent;
            this.winnerText2.Font = new System.Drawing.Font("Gabriola", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.winnerText2.ForeColor = System.Drawing.Color.Gold;
            this.winnerText2.Location = new System.Drawing.Point(600, 25);
            this.winnerText2.Name = "winnerText2";
            this.winnerText2.Size = new System.Drawing.Size(600, 950);
            this.winnerText2.TabIndex = 0;
            this.winnerText2.Text = "TEMPNAMEHERE";
            this.winnerText2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // returnButton
            // 
            this.returnButton.BackColor = System.Drawing.Color.BurlyWood;
            this.returnButton.Font = new System.Drawing.Font("Elephant", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.returnButton.ForeColor = System.Drawing.Color.Black;
            this.returnButton.Location = new System.Drawing.Point(1635, 910);
            this.returnButton.Name = "returnButton";
            this.returnButton.Size = new System.Drawing.Size(150, 75);
            this.returnButton.TabIndex = 4;
            this.returnButton.Text = "Return";
            this.returnButton.UseVisualStyleBackColor = false;
            this.returnButton.Click += new System.EventHandler(this.returnButton_Click);
            // 
            // winnerText3
            // 
            this.winnerText3.BackColor = System.Drawing.Color.Transparent;
            this.winnerText3.Font = new System.Drawing.Font("Gabriola", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.winnerText3.ForeColor = System.Drawing.Color.Gold;
            this.winnerText3.Location = new System.Drawing.Point(1225, 25);
            this.winnerText3.Name = "winnerText3";
            this.winnerText3.Size = new System.Drawing.Size(550, 950);
            this.winnerText3.TabIndex = 5;
            this.winnerText3.Text = "TEMPNAMEHERE";
            this.winnerText3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // winnerText1
            // 
            this.winnerText1.BackColor = System.Drawing.Color.Transparent;
            this.winnerText1.Font = new System.Drawing.Font("Gabriola", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.winnerText1.ForeColor = System.Drawing.Color.Gold;
            this.winnerText1.Location = new System.Drawing.Point(25, 25);
            this.winnerText1.Name = "winnerText1";
            this.winnerText1.Size = new System.Drawing.Size(550, 950);
            this.winnerText1.TabIndex = 6;
            this.winnerText1.Text = "TEMPNAMEHERE";
            this.winnerText1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Masters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackgroundImage = global::Ball_Physics_Test.Properties.Resources.hallBG;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.winnerText1);
            this.Controls.Add(this.returnButton);
            this.Controls.Add(this.winnerText3);
            this.Controls.Add(this.winnerText2);
            this.DoubleBuffered = true;
            this.Name = "Masters";
            this.Size = new System.Drawing.Size(1800, 1000);
            this.Load += new System.EventHandler(this.Masters_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label winnerText2;
        private System.Windows.Forms.Button returnButton;
        private System.Windows.Forms.Label winnerText3;
        private System.Windows.Forms.Label winnerText1;
    }
}
