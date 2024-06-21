using Peggle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ball_Physics_Test
{
    public partial class Masters : UserControl
    {
        public Masters()
        {
            InitializeComponent();
        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            Form1.ChangeScreen(this, new TitleScreen());
        }

        private void Masters_Load(object sender, EventArgs e)
        {
            winnerText1.BackColor = Color.FromArgb(160, 0, 0, 0);
            winnerText1.Text = "";

            winnerText2.BackColor = Color.FromArgb(160, 0, 0, 0);
            winnerText2.Text = "";

            winnerText3.BackColor = Color.FromArgb(160, 0, 0, 0);
            winnerText3.Text = "";

            for (int i = 0; i < WinScreen.winners.Count; i++)
            {
                switch (i % 3)
                {
                    case 0:
                        winnerText1.Text += WinScreen.winners[i] + "\n";
                        break;
                    case 1:
                        winnerText2.Text += WinScreen.winners[i] + "\n";
                        break;
                    case 2:
                        winnerText3.Text += WinScreen.winners[i] + "\n";
                        break;
                }
            }
        }
    }
}
