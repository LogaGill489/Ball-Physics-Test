namespace Ball_Physics_Test
{
    partial class WinScreen
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
            this.returnButton = new System.Windows.Forms.Button();
            this.winnerLabel = new System.Windows.Forms.Label();
            this.exitButton = new System.Windows.Forms.Button();
            this.victoryLabel = new System.Windows.Forms.Label();
            this.nameInput = new System.Windows.Forms.TextBox();
            this.nameInputButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // returnButton
            // 
            this.returnButton.BackColor = System.Drawing.Color.BurlyWood;
            this.returnButton.Font = new System.Drawing.Font("Elephant", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.returnButton.ForeColor = System.Drawing.Color.Black;
            this.returnButton.Location = new System.Drawing.Point(272, 628);
            this.returnButton.Name = "returnButton";
            this.returnButton.Size = new System.Drawing.Size(426, 159);
            this.returnButton.TabIndex = 3;
            this.returnButton.Text = "Return";
            this.returnButton.UseVisualStyleBackColor = false;
            this.returnButton.Click += new System.EventHandler(this.returnButton_Click);
            // 
            // winnerLabel
            // 
            this.winnerLabel.BackColor = System.Drawing.Color.Transparent;
            this.winnerLabel.Font = new System.Drawing.Font("Tempus Sans ITC", 199.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.winnerLabel.ForeColor = System.Drawing.Color.Black;
            this.winnerLabel.Location = new System.Drawing.Point(73, 126);
            this.winnerLabel.Name = "winnerLabel";
            this.winnerLabel.Size = new System.Drawing.Size(1482, 432);
            this.winnerLabel.TabIndex = 4;
            this.winnerLabel.Text = "Winner!";
            this.winnerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.BurlyWood;
            this.exitButton.Font = new System.Drawing.Font("Elephant", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitButton.ForeColor = System.Drawing.Color.Black;
            this.exitButton.Location = new System.Drawing.Point(874, 628);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(426, 159);
            this.exitButton.TabIndex = 5;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // victoryLabel
            // 
            this.victoryLabel.BackColor = System.Drawing.Color.Transparent;
            this.victoryLabel.Font = new System.Drawing.Font("Palatino Linotype", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.victoryLabel.ForeColor = System.Drawing.Color.Black;
            this.victoryLabel.Location = new System.Drawing.Point(478, 480);
            this.victoryLabel.Name = "victoryLabel";
            this.victoryLabel.Size = new System.Drawing.Size(656, 37);
            this.victoryLabel.TabIndex = 6;
            this.victoryLabel.Text = "Forever Imortalize Your Victory:";
            this.victoryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nameInput
            // 
            this.nameInput.Font = new System.Drawing.Font("Tempus Sans ITC", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameInput.Location = new System.Drawing.Point(612, 532);
            this.nameInput.Name = "nameInput";
            this.nameInput.Size = new System.Drawing.Size(397, 38);
            this.nameInput.TabIndex = 7;
            // 
            // nameInputButton
            // 
            this.nameInputButton.BackColor = System.Drawing.Color.BurlyWood;
            this.nameInputButton.Font = new System.Drawing.Font("Elephant", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameInputButton.ForeColor = System.Drawing.Color.Black;
            this.nameInputButton.Location = new System.Drawing.Point(704, 576);
            this.nameInputButton.Name = "nameInputButton";
            this.nameInputButton.Size = new System.Drawing.Size(178, 58);
            this.nameInputButton.TabIndex = 8;
            this.nameInputButton.Text = "Input";
            this.nameInputButton.UseVisualStyleBackColor = false;
            this.nameInputButton.Click += new System.EventHandler(this.nameInputButton_Click);
            // 
            // WinScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.nameInputButton);
            this.Controls.Add(this.nameInput);
            this.Controls.Add(this.victoryLabel);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.winnerLabel);
            this.Controls.Add(this.returnButton);
            this.Name = "WinScreen";
            this.Size = new System.Drawing.Size(1800, 1000);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button returnButton;
        private System.Windows.Forms.Label winnerLabel;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label victoryLabel;
        private System.Windows.Forms.TextBox nameInput;
        private System.Windows.Forms.Button nameInputButton;
    }
}
