namespace Peggle
{
    partial class TestScreen
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
            this.components = new System.ComponentModel.Container();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.xLabel = new System.Windows.Forms.Label();
            this.yLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // gameTimer
            // 
            this.gameTimer.Enabled = true;
            this.gameTimer.Interval = 16;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // xLabel
            // 
            this.xLabel.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xLabel.Location = new System.Drawing.Point(30, 28);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(180, 31);
            this.xLabel.TabIndex = 0;
            this.xLabel.Text = "X-Position: 404";
            // 
            // yLabel
            // 
            this.yLabel.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yLabel.Location = new System.Drawing.Point(30, 71);
            this.yLabel.Name = "yLabel";
            this.yLabel.Size = new System.Drawing.Size(180, 31);
            this.yLabel.TabIndex = 1;
            this.yLabel.Text = "Y-Position: 404";
            // 
            // TestScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.Controls.Add(this.yLabel);
            this.Controls.Add(this.xLabel);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Transparent;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TestScreen";
            this.Size = new System.Drawing.Size(1125, 650);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TestScreen_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TestScreen_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TestScreen_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Label xLabel;
        private System.Windows.Forms.Label yLabel;
    }
}
