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
    public partial class TestScreen : UserControl
    {
        Ball ball = new Ball(100, 100, 20, 0, 0);
        List<Peg> testPeg = new List<Peg>();
        RectangleF mouseRect;
        public TestScreen()
        {
            InitializeComponent();
            testPeg.Add(new Peg(400, 400, 25, 25, true));
            testPeg.Add(new Peg(600, 400, 25, 25, true));
            testPeg.Add(new Peg(800, 400, 25, 25, true));
            testPeg.Add(new Peg(200, 400, 25, 25, true));
            testPeg.Add(new Peg(400, 200, 25, 25, true));
            testPeg.Add(new Peg(600, 200, 25, 25, true));
            testPeg.Add(new Peg(800, 200, 25, 25, true));
            testPeg.Add(new Peg(200, 200, 25, 25, true));
            testPeg.Add(new Peg(300, 300, 30, 50, false));
            testPeg.Add(new Peg(500, 300, 30, 50, false));
            testPeg.Add(new Peg(700, 300, 30, 50, false));
            ball.addPegs(testPeg);
        }

        private void TestScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRegion(Brushes.White, ball.ballRegion);
            foreach (Peg peg in testPeg)
            {
                if (peg.circle)
                {
                    e.Graphics.FillRegion(Brushes.Black, peg.circRegion);
                }
                else
                {
                    e.Graphics.FillRectangle(Brushes.Black, peg.rectangle);
                }

            }
        }

        bool hasSpeed;
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            ball.doGravity(this.Height, this.Width, testPeg);
            using (Graphics g = this.CreateGraphics())
            {
                xLabel.Text = $"X-Position: {ball.x}";
                yLabel.Text = $"Y-Speed: {ball.ySpeed}";
                for (int i = 0; i < testPeg.Count; i++)
                {
                    if (testPeg[i].circle) 
                    {
                        bool removePeg = ball.circleCollision(new RectangleF(testPeg[i].x, testPeg[i].y, testPeg[i].width, testPeg[i].height), g, i);
                    }
                    else
                    {
                        bool removePeg = ball.BlockCollision(testPeg[i]);
                    }

                }

                //if (removePeg) testPeg = new Peg (0, 0, 0, 0, true);
            }

                if (active)
                {
                    ball.ySpeed = 0;
                    ball.xSpeed = 0;
                    ball.x = mouseRect.X - (ball.size / 2);
                    ball.y = mouseRect.Y - (ball.size / 2);
                }
                else if (!hasSpeed)
                {
                    hasSpeed = true;
                    ball.xSpeed = xSp;
                    ball.ySpeed = ySp;
                }
            Refresh();
        }

        float xSp, ySp;
        PointF prevPosition;

        private void TestScreen_MouseMove(object sender, MouseEventArgs e)
        {
            PointF cursorPoint = this.PointToClient(Cursor.Position);
            mouseRect = new RectangleF(cursorPoint.X, cursorPoint.Y, 1, 1);
            xSp = cursorPoint.X - prevPosition.X;
            ySp = cursorPoint.Y - prevPosition.Y;
            prevPosition = cursorPoint;
            if (active)
                hasSpeed = false;
        }

        bool active = false;

        private void TestScreen_MouseClick(object sender, MouseEventArgs e)
        {
            if (active)
            {
                active = false;
            }
            else
            {
                active = true;
            }
        }
    }
}
