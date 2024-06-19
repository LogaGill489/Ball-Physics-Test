using Ball_Physics_Test;
using Ball_Physics_Test.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Peggle
{
    public partial class GameScreen : UserControl
    {
        bool gameTestState = true;

        Ball ball = new Ball(100, 100, 20, 0, 0);
        List<Peg> pegs = new List<Peg>();
        RectangleF mouseRect;

        public int lives = 10;
        public int score = 0;
        bool ballIsShot = false;
        PointF shotPosition = new PointF();

        Region lPadRegion = new Region();
        Region rPadRegion = new Region();
        GraphicsPath gPath = new GraphicsPath();
        Random randGen = new Random();

        Paddle paddle = new Paddle(0, 0, 0, 0, 0);
        float paddleWidth = 80;

        bool hasSpeed = true;

        float xSp, ySp;
        PointF prevPosition;
        PointF cursorPoint;

        Stopwatch recentlyScored = new Stopwatch();

        List<Stopwatch> pegGlow = new List<Stopwatch>();
        List<int> hitArrayLocations = new List<int>();

        RectangleF lPadHitBox = new Rectangle();
        RectangleF rPadHitBox = new RectangleF();

        public GameScreen()
        {
            InitializeComponent();

            pegs.Add(new Peg(400, 400, 25, 25, true, false));
            pegs.Add(new Peg(600, 400, 25, 25, true, false));
            pegs.Add(new Peg(800, 400, 25, 25, true, false));
            pegs.Add(new Peg(200, 400, 25, 25, true, false));
            pegs.Add(new Peg(400, 200, 25, 25, true, false));
            pegs.Add(new Peg(600, 200, 25, 25, true, false));
            pegs.Add(new Peg(800, 200, 25, 25, true, false));
            pegs.Add(new Peg(200, 200, 25, 25, true, false));
            pegs.Add(new Peg(300, 300, 30, 50, false, false));
            pegs.Add(new Peg(500, 300, 30, 50, false, false));
            pegs.Add(new Peg(700, 300, 30, 50, false, false));

            pegs.Add(new Peg(700, 500, 50, 30, false, true));
            pegs.Add(new Peg(645, 500, 50, 30, false, true));
            pegs.Add(new Peg(590, 500, 50, 30, false, true));
            pegs.Add(new Peg(535, 500, 50, 30, false, true));
            pegs.Add(new Peg(480, 500, 50, 30, false, true));
            pegs.Add(new Peg(425, 500, 50, 30, false, true));
            pegs.Add(new Peg(370, 500, 50, 30, false, true));
            ball.addPegs(pegs);

            onStart();
        }

        void onStart()
        {
            paddle = new Paddle((this.Width / 2) - 100, this.Height - 35, 200, 35, 9);
            updateCurve();

            int storage = 0;
            for (int i = 0; i < 3; i++)
            {
                int otherPegs = randGen.Next(0, pegs.Count);
                if (otherPegs == storage)
                {
                    while (otherPegs == storage)
                    {
                        otherPegs = randGen.Next(0, pegs.Count);
                    }
                }
                if (i == 2)
                {
                    pegs[otherPegs].colour = new SolidBrush(Color.FromArgb(255, 214, 37, 220));
                    pegs[otherPegs].colState = "purple";
                }
                else
                {
                    pegs[otherPegs].colour = new SolidBrush(Color.FromArgb(255, 17, 156, 0));
                    pegs[otherPegs].colState = "green";

                }
                storage = otherPegs;
            }
            int range = (pegs.Count - 3) / 3;
            for (int i = 0; i < range; i++)
            {
                int orangePeg = randGen.Next(0, pegs.Count);
                if (pegs[orangePeg].colour != null)
                {
                    while (pegs[orangePeg].colour != null)
                    {
                        orangePeg = randGen.Next(0, pegs.Count);
                    }
                }
                pegs[orangePeg].colour = new SolidBrush(Color.FromArgb(255, 210, 103, 50));
                pegs[orangePeg].colState = "orange";
            }

            foreach (Peg p in pegs)
            {
                if (p.colour == null)
                {
                    p.colour = new SolidBrush(Color.FromArgb(255, 7, 41, 127));
                    p.colState = "blue";
                }
            }
        }

        private void TestScreen_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < hitArrayLocations.Count; i++)
            {
                float speedInc = 20;
                int outwardsAdjustment = 5;
                RectangleF glowEffect = new RectangleF(pegs[hitArrayLocations[i]].x - outwardsAdjustment - (int)(pegGlow[i].ElapsedMilliseconds / speedInc), pegs[hitArrayLocations[i]].y - outwardsAdjustment - (int)(pegGlow[i].ElapsedMilliseconds / speedInc),
                pegs[hitArrayLocations[i]].width + (2 * outwardsAdjustment) + (2 * (int)(pegGlow[i].ElapsedMilliseconds / speedInc)), pegs[hitArrayLocations[i]].height + (2 * outwardsAdjustment) + (2 * (int)(pegGlow[i].ElapsedMilliseconds / speedInc)));
                if (pegs[hitArrayLocations[i]].circle)
                    e.Graphics.DrawEllipse(Pens.Orange, glowEffect);
                else
                    e.Graphics.DrawRectangle(Pens.Orange, new Rectangle((int)glowEffect.X, (int)glowEffect.Y, (int)glowEffect.Width, (int)glowEffect.Height));
            }

            foreach (Peg peg in pegs)
            {
                Pen trim = new Pen(Color.FromArgb(255, 193, 98));
                if (peg.circle)
                {
                    e.Graphics.FillRegion(peg.colour, peg.circRegion);
                    e.Graphics.DrawEllipse(trim, peg.rectangle);
                }
                else
                {
                    e.Graphics.FillRectangle(peg.colour, peg.rectangle);
                    e.Graphics.DrawRectangle(trim, peg.x, peg.y, peg.width, peg.height);
                }
            }

            // Draws paddle
            RectangleF paddleImg = new RectangleF(paddle.x - 57, paddle.y - 29, 314, 65);
            e.Graphics.DrawImage(Resources.paddle, paddleImg);

            //e.Graphics.FillRectangle(Brushes.White, paddle.x, paddle.y, paddle.width, paddle.height);
            //e.Graphics.FillRegion(Brushes.White, lPadRegion);
            //e.Graphics.FillRegion(Brushes.White, rPadRegion);

            //cursorPoint is a PointF tracking your position
            Pen pen = new Pen(Color.Black, 20);
            Pen goldPen = new Pen(Color.FromArgb(255, 193, 98), 22);
            if (cursorPoint.Y >= 5)
            {
                float theta = (float)Math.Atan((cursorPoint.X - (this.Width / 2)) / (cursorPoint.Y - 5));
                float x = 81 * (float)Math.Sin(theta);
                float y = 81 * (float)Math.Cos(theta);

                e.Graphics.DrawLine(goldPen, this.Width / 2, 5, (this.Width / 2) + x, 5 + y);

                x = 80 * (float)Math.Sin(theta);
                y = 80 * (float)Math.Cos(theta);

                shotPosition = new PointF((this.Width / 2) + x, 5 + y);

                e.Graphics.DrawLine(pen, this.Width / 2, 5, (this.Width / 2) + x, 5 + y);
            }
            else
            {
                if (cursorPoint.X <= this.Width / 2) //left
                {
                    e.Graphics.DrawLine(goldPen, this.Width / 2, 5, (this.Width / 2) - 81, 5);
                    e.Graphics.DrawLine(pen, this.Width / 2, 5, (this.Width / 2) - 80, 5);
                    shotPosition = new PointF((this.Width / 2) - 80, 5);
                }
                else //right
                {
                    e.Graphics.DrawLine(goldPen, this.Width / 2, 5, (this.Width / 2) + 81, 5);
                    e.Graphics.DrawLine(pen, this.Width / 2, 5, (this.Width / 2) + 80, 5);
                    shotPosition = new PointF((this.Width / 2) + 80, 5);
                }
            }

            Rectangle circ = new Rectangle((this.Width / 2) - 60, -60, 120, 120);
            e.Graphics.DrawImage(Resources.goldCircle, circ);

            e.Graphics.DrawImage(Resources.peggleBall, ball.x, ball.y, ball.size, ball.size);
            //e.Graphics.FillRegion(Brushes.White, ball.ballRegion);

            //left and right border items and bg
            Brush borderBrush = new SolidBrush(Color.FromArgb(50, 50, 50));
            e.Graphics.FillRectangle(borderBrush, 0, 0, 200, this.Height);
            e.Graphics.FillRectangle(borderBrush, this.Width - 200, 0, 200, this.Height);

            //RectangleF ballCount = new RectangleF(0, 25, 200, 200);
            //RectangleF resized = new RectangleF(ballCount.X + 40, ballCount.Y + 40, 120, 120);
            //Brush innerFill = new SolidBrush(Color.FromArgb(100, 255, 255, 255));

            //e.Graphics.FillEllipse(innerFill, resized);
            //e.Graphics.DrawImage(Resources.peggleBall, resized);
            //e.Graphics.DrawImage(Resources.goldSideCircle, ballCount);
            //PaintText(e.Graphics, Convert.ToString(lives), 30, new PointF(ballCount.X + (ballCount.Width / 2) - 40, ballCount.Y + (ballCount.Height / 2) - 31), Color.FromArgb(255, 193, 98));

            //RectangleF character = new RectangleF(this.Width - 200, 25, 200, 200);
            //resized = new RectangleF(character.X + 40, character.Y + 40, 120, 120);
            //e.Graphics.FillEllipse(innerFill, resized);
            //e.Graphics.DrawImage(Resources.goldSideCircle, character);


            //free ball / no ball
            if (recentlyScored.IsRunning)
            {
                float speedInc = 10;
                Rectangle freeBallRect = new Rectangle((this.Width / 2) - 150, (this.Height / 2) - 150, 300, 300);
                Rectangle glowEffect = new Rectangle(freeBallRect.X - 100 - (int)(recentlyScored.ElapsedMilliseconds / speedInc), freeBallRect.Y - 100 - (int)(recentlyScored.ElapsedMilliseconds / speedInc),
                    500 + (2 * (int)(recentlyScored.ElapsedMilliseconds / speedInc)), 500 + (2 * (int)(recentlyScored.ElapsedMilliseconds / speedInc)));

                e.Graphics.DrawImage(Resources.freeBallGlow, glowEffect);
                e.Graphics.DrawImage(Resources.freeBall, freeBallRect);
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            xLabel.Text = $"X-Speed: {cursorPoint.X}";
            yLabel.Text = $"Y-Speed: {cursorPoint.Y}";
            if (!ballIsShot)
            {
                if (recentlyScored.ElapsedMilliseconds > 2000)
                {
                    recentlyScored.Reset();
                }
            }
            else
            {
                ball.doGravity(this.Height, this.Width - 200, 200, 0, pegs);
                int hitPeg = -1;
                using (Graphics g = this.CreateGraphics())
                {
                    xLabel.Text = $"X-Position: {ball.x}";
                    yLabel.Text = $"Y-Speed: {ball.ySpeed}";
                    for (int i = 0; i < pegs.Count; i++)
                    {
                        if (pegs[i].circle)
                        {
                            hitPeg = ball.circleCollision(new RectangleF(pegs[i].x, pegs[i].y, pegs[i].width, pegs[i].height), g, i, false, 0);
                            if (hitPeg != -1 && !pegs[i].hit) { adjustColorState(i); break; }
                            else { hitPeg = -1; }
                        }
                        else
                        {
                            bool state = false;
                            state = ball.BlockCollision(pegs[i], g);
                            if (state && !pegs[i].hit) { adjustColorState(i); hitPeg = i; break; }
                            else { hitPeg = -1; }
                        }

                    }
                    ball.circleCollision(lPadHitBox, g, 1, true, paddle.speed);
                    ball.circleCollision(rPadHitBox, g, 2, true, paddle.speed);
                    //if (removePeg) pegs = new Peg (0, 0, 0, 0, true);
                }

                if (hitPeg != -1)
                {
                    hitArrayLocations.Add(hitPeg);
                    pegGlow.Add(new Stopwatch());
                    pegGlow[pegGlow.Count - 1].Start();
                }
                for (int i = 0; i < pegGlow.Count; i++)
                {
                    if (pegGlow[i].IsRunning && pegGlow[i].ElapsedMilliseconds > 500)
                    {
                        pegGlow.RemoveAt(i);
                        hitArrayLocations.RemoveAt(i);
                    }
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

                if (new RectangleF(ball.x, ball.y, ball.size, ball.size).IntersectsWith(new RectangleF(paddle.x, paddle.y, paddle.width, paddle.height)))
                {
                    newShot(true);
                }
            }
            paddle.Move(this.Width);
            updateCurve();

            Refresh();
        }

        private void adjustColorState(int i)
        {
            pegs[i].hit = true;
            int upAdjust = 30;
            switch (pegs[i].colState)
            {
                case "orange":
                    pegs[i].colour = new SolidBrush(Color.FromArgb(210, 103 + (upAdjust / 2), 50 + (upAdjust / 2)));
                    break;
                case "blue":
                    pegs[i].colour = new SolidBrush(Color.FromArgb(7, 41 + upAdjust, 127 + upAdjust));
                    break;
                case "purple":
                    pegs[i].colour = new SolidBrush(Color.FromArgb(214, 37 + upAdjust, 220 + upAdjust));
                    break;
                case "green":
                    pegs[i].colour = new SolidBrush(Color.FromArgb(17, 156 + upAdjust, 0 + upAdjust));
                    break;
            }
        }

        private void updateCurve()
        {
            gPath.Reset();
            lPadRegion.Dispose();
            lPadHitBox = new RectangleF(paddle.x - 25, paddle.y - 15, 50, 50);
            gPath.AddEllipse(lPadHitBox);
            lPadRegion = new Region(gPath);
            lPadRegion.Exclude(new RectangleF(paddle.x - 25, paddle.y + 10, 50, 25));
            lPadRegion.Exclude(new RectangleF(paddle.x, paddle.y - 15, 25, 25));

            gPath.Reset();
            rPadRegion.Dispose();
            rPadHitBox = new RectangleF(paddle.x + paddle.width - 25, paddle.y - 15, 50, 50);
            gPath.AddEllipse(rPadHitBox);
            rPadRegion = new Region(gPath);
            rPadRegion.Exclude(new RectangleF(paddle.x + (int)paddle.width - 25, paddle.y + 10, 50, 25));
            rPadRegion.Exclude(new RectangleF(paddle.x + (int)paddle.width - 25, paddle.y - 15, 25, 25));
        }

        public void newShot(bool scored)
        {
            lives--;
            ballIsShot = false;
            ball.x = 2000;
            ball.y = 2000;

            ball.ballPath.Reset();
            ball.ballPath.AddEllipse(new RectangleF(ball.x, ball.y, ball.size, ball.size));
            ball.ballRegion = new Region(ball.ballPath);
            ball.xSpeed = 0;
            ball.ySpeed = 0;

            if (scored)
            {
                lives++;
                recentlyScored.Start();
            }
        }

        static public void PaintText(Graphics e, string displayText, int size, PointF position, Color color)
        {
            Font myFont = new Font("Elephant", size, FontStyle.Bold);
            SolidBrush brush = new SolidBrush(color);
            e.DrawString(displayText, myFont, brush, position.X, position.Y);
            brush.Dispose();
            myFont.Dispose();
        }

        private void TestScreen_MouseMove(object sender, MouseEventArgs e)
        {
            cursorPoint = this.PointToClient(Cursor.Position);
            mouseRect = new RectangleF(cursorPoint.X, cursorPoint.Y, 1, 1);
            xSp = cursorPoint.X - prevPosition.X;
            ySp = cursorPoint.Y - prevPosition.Y;
            prevPosition = cursorPoint;
            if (active)
                hasSpeed = false;
        }

        bool active = false;

        private void GameScreen_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameTestState)
            { //hotkeys to help test additions / changes, disabled in final product
                switch (e.KeyCode)
                {
                    case Keys.Escape: //quick close
                        Application.Exit();
                        break;
                    case Keys.R: //quick restart
                        Application.Restart();
                        break;
                    case Keys.B: //break point on variable, making the code breakable at any point
                        var breakGame = 0;
                        break;
                }
            }
        }

        private void TestScreen_MouseClick(object sender, MouseEventArgs e)
        {
            if (!ballIsShot)
            {
                ball.x = shotPosition.X - (ball.size / 2);
                ball.y = shotPosition.Y - (ball.size / 2);
                ball.xSpeed = (float)0.23 * (ball.x - (this.Width / 2));
                ball.ySpeed = (float)0.23 * (ball.y - 5);
                ballIsShot = true;
            }

            else if (gameTestState) //allows for picking up and throwing ball with mouse if in testing state
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
}
