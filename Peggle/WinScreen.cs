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
using System.IO;

namespace Ball_Physics_Test
{
    public partial class WinScreen : UserControl
    {
        static public List<string> winners = new List<string>();
        bool savedOnce = false;

        public WinScreen()
        {
            InitializeComponent();
        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            Form1.ChangeScreen(this, new TitleScreen());
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void nameInputButton_Click(object sender, EventArgs e)
        {
            if (savedOnce)
            {
                victoryLabel.Text = "Win Again to Add Another Name!";
            }
            else if (nameInput.Text == "")
            {
                victoryLabel.Text = "Please Input a Name!";
            }
            else
            {
                winners.Clear();
                winners = File.ReadAllLines("Winners.txt").ToList();
                winners.Add(nameInput.Text);

                File.WriteAllLines("Winners.txt", winners);

                victoryLabel.Text = "Saved!";
                savedOnce = true;
            }
        }

        private void WinScreen_Load(object sender, EventArgs e)
        {
            savedOnce = false;
        }
    }
}
