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
using System.Timers;
using System.Windows.Forms;

namespace Peggle
{
    public partial class GameScreen : UserControl
    {
        //vars for testing the game, won't affect gameplay
        bool gameTestState = false;
        bool active = false;
        bool hasSpeed = true;

        //ball and mouse declaration
        Ball ball = new Ball(100, 100, 20, 0, 0);
        List<Peg> pegs = new List<Peg>();

        public int lives = 10;
        public int score = 0;
        bool ballIsShot = false;
        PointF shotPosition = new PointF();

        //paddle variables
        Region lPadRegion = new Region();
        Region rPadRegion = new Region();
        RectangleF lPadHitBox = new Rectangle();
        RectangleF rPadHitBox = new RectangleF();
        GraphicsPath gPath = new GraphicsPath();
        Paddle paddle = new Paddle(0, 0, 0, 0, 0);

        //random Gen
        Random randGen = new Random();

        //various tracking variables for a variety of tasks
        static public bool hitStartingPeg = false;
        bool returnBall = false;
        int spinner = 0;
        int tracker = 0;
        static public bool hitBottom = false;

        //mouse tracking
        float xSp, ySp;
        PointF prevPosition;
        PointF cursorPoint;
        RectangleF mouseRect;

        //Stopwatches for inbetween states during shots
        Stopwatch recentlyScored = new Stopwatch();
        Stopwatch fellOffBottom = new Stopwatch();
        Stopwatch hitNothing = new Stopwatch();
        Stopwatch clearer = new Stopwatch();

        //glow effect + list for tracking hit balls
        List<Stopwatch> pegGlow = new List<Stopwatch>();
        List<int> hitArrayLocations = new List<int>();
        static public List<int> hitPegStorage = new List<int>();
        bool animationState = false;

        public GameScreen()
        {
            InitializeComponent();

            //upper Section
            pegs.Add(new Peg(785, 400, 25, 25, true, false));
            pegs.Add(new Peg(985, 400, 25, 25, true, false));
            pegs.Add(new Peg(1185, 400, 25, 25, true, false));
            pegs.Add(new Peg(585, 400, 25, 25, true, false));
            pegs.Add(new Peg(785, 200, 25, 25, true, false));
            pegs.Add(new Peg(985, 200, 25, 25, true, false));
            pegs.Add(new Peg(1185, 200, 25, 25, true, false));
            pegs.Add(new Peg(585, 200, 25, 25, true, false));
            pegs.Add(new Peg(685, 300, 30, 50, false, false));
            pegs.Add(new Peg(885, 300, 30, 50, false, false));
            pegs.Add(new Peg(1085, 300, 30, 50, false, false));

            //left pegs
            pegs.Add(new Peg(390, 140, 25, 25, true, false));
            pegs.Add(new Peg(210, 200, 25, 25, true, false));
            pegs.Add(new Peg(510, 250, 25, 25, true, false));
            pegs.Add(new Peg(370, 300, 25, 25, true, false));
            pegs.Add(new Peg(300, 410, 25, 25, true, false));
            pegs.Add(new Peg(430, 450, 25, 25, true, false));
            pegs.Add(new Peg(470, 580, 25, 25, true, false));
            pegs.Add(new Peg(240, 600, 25, 25, true, false));
            pegs.Add(new Peg(260, 660, 25, 25, true, false));
            pegs.Add(new Peg(340, 680, 25, 25, true, false));
            pegs.Add(new Peg(580, 780, 25, 25, true, false));
            pegs.Add(new Peg(400, 850, 25, 25, true, false));

            //right pegs
            pegs.Add(new Peg(1310, 160, 25, 25, true, false));
            pegs.Add(new Peg(1440, 235, 25, 25, true, false));
            pegs.Add(new Peg(1550, 270, 25, 25, true, false));
            pegs.Add(new Peg(1270, 320, 25, 25, true, false));
            pegs.Add(new Peg(1400, 410, 25, 25, true, false));
            pegs.Add(new Peg(1530, 460, 25, 25, true, false));
            pegs.Add(new Peg(1330, 500, 25, 25, true, false));
            pegs.Add(new Peg(1450, 590, 25, 25, true, false));
            pegs.Add(new Peg(1270, 630, 25, 25, true, false));
            pegs.Add(new Peg(1530, 690, 25, 25, true, false));
            pegs.Add(new Peg(1290, 750, 25, 25, true, false));
            pegs.Add(new Peg(1410, 830, 25, 25, true, false));

            //upper line
            pegs.Add(new Peg(1150, 503, 25, 25, true, false));
            pegs.Add(new Peg(1095, 500, 50, 30, false, true));
            pegs.Add(new Peg(1040, 500, 50, 30, false, true));
            pegs.Add(new Peg(985, 500, 50, 30, false, true));
            pegs.Add(new Peg(930, 500, 50, 30, false, true));
            pegs.Add(new Peg(875, 500, 50, 30, false, true));
            pegs.Add(new Peg(820, 500, 50, 30, false, true));
            pegs.Add(new Peg(765, 500, 50, 30, false, true));
            pegs.Add(new Peg(710, 500, 50, 30, false, true));
            pegs.Add(new Peg(655, 500, 50, 30, false, true));
            pegs.Add(new Peg(625, 503, 25, 25, true, false));

            //half diamond
            pegs.Add(new Peg(887, 600, 25, 25, true, false));
            for (int i = 0; i < 7; i++)
            {
                pegs.Add(new Peg(887 - (30 * i), 600 + (30 * i), 25, 25, true, false));
                pegs.Add(new Peg(887 + (30 * i), 600 + (30 * i), 25, 25, true, false));
            }
            ball.addPegs(pegs);

            onStart();
        }

        void onStart()
        {
            //creates paddle properly and updates curves
            paddle = new Paddle((this.Width / 2) - 100, this.Height - 35, 200, 35, 9);
            updateCurve();

            //randomly decides which pegs are which colours, generating 2 green, 1 purple, and 33% orange
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
                if (i == 2) //makes 3rd random peg purple
                {
                    pegs[otherPegs].colour = new SolidBrush(Color.FromArgb(255, 214, 37, 220));
                    pegs[otherPegs].colState = "purple";
                }
                else //makes first 2 random pegs green
                {
                    pegs[otherPegs].colour = new SolidBrush(Color.FromArgb(255, 17, 156, 0));
                    pegs[otherPegs].colState = "green";

                }
                storage = otherPegs;
            }
            int range = (pegs.Count - 3) / 3;
            for (int i = 0; i < range; i++) //for loop, making 33% of remaining pegs orange
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

            foreach (Peg p in pegs) //makes any pegs left blue
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
            for (int i = 0; i < hitArrayLocations.Count; i++) //glowing effect for each of the pegs when hit
            {
                float speedInc = 20;
                int outwardsAdjustment = 5;
                RectangleF glowEffect = new RectangleF(0, 0, 0, 0);
                try //try catch is necessary because sometimes the index goes out of range, which couldn't be fixed.  Insteaad it just doesn't draw it for one or two frames, no big deal
                {
                    glowEffect = new RectangleF(pegs[hitArrayLocations[i]].x - outwardsAdjustment - (int)(pegGlow[i].ElapsedMilliseconds / speedInc), pegs[hitArrayLocations[i]].y - outwardsAdjustment - (int)(pegGlow[i].ElapsedMilliseconds / speedInc),
                    pegs[hitArrayLocations[i]].width + (2 * outwardsAdjustment) + (2 * (int)(pegGlow[i].ElapsedMilliseconds / speedInc)), pegs[hitArrayLocations[i]].height + (2 * outwardsAdjustment) + (2 * (int)(pegGlow[i].ElapsedMilliseconds / speedInc)));

                    if (pegs[hitArrayLocations[i]].circle)
                        e.Graphics.DrawEllipse(Pens.Orange, glowEffect);
                    else
                        e.Graphics.DrawRectangle(Pens.Orange, new Rectangle((int)glowEffect.X, (int)glowEffect.Y, (int)glowEffect.Width, (int)glowEffect.Height));
                }
                catch { }
            }

            if (!ballIsShot) //draws aiming line for firing
            {
                Pen aimPen = new Pen(Color.Black, 3);
                int xDif = (int)cursorPoint.X - this.Width / 2;
                int yDif = (int)cursorPoint.Y - 5;
                PointF prevPoint = new PointF(0, 0);

                float magnitude = (float)Math.Sqrt((xDif * xDif) + (yDif * yDif));
                float theta = (float)Math.Atan((cursorPoint.X - (this.Width / 2)) / (cursorPoint.Y - 5));
                if (cursorPoint.Y < 5)
                {
                    yDif = 0;
                    magnitude = xDif;
                    theta *= -1;
                }

                //counts up by the gap in magnitude / 50, adding lines 40 pixels long with 10 pixel spaces and a final smaller line to the cursor
                for (int i = 0; i < (int)(magnitude / 50) + 1; i++)
                {
                    float x = (40 + (10 * i) + (40 * i)) * (float)Math.Sin(theta);
                    float y = (40 + (10 * i) + (40 * i)) * (float)Math.Cos(theta);
                    prevPoint = new PointF(((40 * i) + (10 * i)) * (float)Math.Sin(theta), ((40 * i) + (10 * i)) * (float)Math.Cos(theta));

                    if (i != (int)(magnitude / 50))
                        e.Graphics.DrawLine(aimPen, (this.Width / 2) + prevPoint.X, 5 + prevPoint.Y, (this.Width / 2) + x, 5 + y);
                    else
                    {
                        float yH = Math.Abs(y - prevPoint.Y);
                        if (yH > Math.Abs(cursorPoint.Y - prevPoint.Y))
                            e.Graphics.DrawLine(aimPen, (this.Width / 2) + prevPoint.X, 5 + prevPoint.Y, cursorPoint.X, cursorPoint.Y);
                        else
                            e.Graphics.DrawLine(aimPen, (this.Width / 2) + prevPoint.X, 5 + prevPoint.Y, (this.Width / 2) + x, 5 + y);
                    }
                }
            }

            foreach (Peg peg in pegs) //draws each of the remaining pegs
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

            //uncomment for paddle hitBox
            //e.Graphics.FillRectangle(Brushes.White, paddle.x, paddle.y, paddle.width, paddle.height);
            //e.Graphics.FillRegion(Brushes.White, lPadRegion);
            //e.Graphics.FillRegion(Brushes.White, rPadRegion);

            //draws cannon
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

            //draws circle that covers the cannon
            Rectangle circ = new Rectangle((this.Width / 2) - 60, -60, 120, 120);
            e.Graphics.DrawImage(Resources.goldCircle, circ);

            //draws ball
            e.Graphics.DrawImage(Resources.peggleBall, ball.x, ball.y, ball.size, ball.size);
            //e.Graphics.FillRegion(Brushes.White, ball.ballRegion);

            //left and right border items and bg
            Brush borderBrush = new SolidBrush(Color.FromArgb(50, 50, 50));
            e.Graphics.FillRectangle(borderBrush, 0, 0, 200, this.Height);
            e.Graphics.FillRectangle(borderBrush, this.Width - 200, 0, 200, this.Height);

            RectangleF ballCount = new RectangleF(0, 25, 200, 200);
            RectangleF resized = new RectangleF(ballCount.X + 40, ballCount.Y + 40, 120, 120);
            Brush innerFill = new SolidBrush(Color.FromArgb(100, 255, 255, 255));

            e.Graphics.FillEllipse(innerFill, resized);
            resized = new RectangleF(ballCount.X + 65, ballCount.Y + 65, 70, 70);

            e.Graphics.DrawImage(Resources.goldSideCircle, ballCount);
            e.Graphics.DrawImage(Resources.peggleBall, resized);
            if (lives >= 10)
                PaintText(e.Graphics, Convert.ToString(lives), 30, new PointF(ballCount.X + (ballCount.Width / 2) - 40, ballCount.Y + (ballCount.Height / 2) - 31), Color.FromArgb(255, 193, 98));
            else
                PaintText(e.Graphics, Convert.ToString(lives), 30, new PointF(ballCount.X + (ballCount.Width / 2) - 25, ballCount.Y + (ballCount.Height / 2) - 31), Color.FromArgb(255, 193, 98));

            //fell off & hit a peg
            if (fellOffBottom.IsRunning)
            {
                if (fellOffBottom.ElapsedMilliseconds <= 1000)
                    PaintText(e.Graphics, Convert.ToString(lives) + " balls remaining!", 30, new PointF((this.Width / 2) - 230, this.Height - 200), Color.FromArgb((int)fellOffBottom.ElapsedMilliseconds / 4, 0, 0, 0));
                else
                    PaintText(e.Graphics, Convert.ToString(lives) + " balls remaining!", 30, new PointF((this.Width / 2) - 230, this.Height - 200), Color.FromArgb(0, 0, 0));
            }

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

            //code for spinning effect + outcome
            if (hitNothing.IsRunning && hitNothing.ElapsedMilliseconds < 2000)
            {
                if ((int)(hitNothing.ElapsedMilliseconds / 250) != spinner) //updates every quarter second, changing the image
                {
                    spinner++;
                }

                //variables for drawing purposes
                Rectangle circImg = new Rectangle((this.Width / 2) - 150, (this.Height / 2) - 150, 300, 300);
                Pen linePen = new Pen(Color.FromArgb(133, 158, 180), 30);
                Pen outline = new Pen(Color.FromArgb(30, 0, 0), 32);

                if (returnBall) //returns ball
                {
                    switch (spinner % 4) //tracks state of spinner and outputs appropriately
                    {
                        case 0:
                            e.Graphics.DrawImage(Resources.freeBall, circImg);
                            break;
                        case 1:
                            e.Graphics.DrawLine(outline, this.Width / 2, (this.Height / 2) - 151, this.Width / 2, (this.Height / 2) + 151);
                            e.Graphics.DrawLine(linePen, this.Width / 2, (this.Height / 2) - 150, this.Width / 2, (this.Height / 2) + 150);
                            break;
                        case 2:
                            e.Graphics.DrawImage(Resources.noBall, circImg);
                            break;
                        case 3:
                            e.Graphics.DrawLine(outline, this.Width / 2, (this.Height / 2) - 151, this.Width / 2, (this.Height / 2) + 151);
                            e.Graphics.DrawLine(linePen, this.Width / 2, (this.Height / 2) - 150, this.Width / 2, (this.Height / 2) + 150);
                            break;
                    }
                }
                else //does not return ball
                {
                    switch (spinner % 4)
                    {
                        case 0:
                            e.Graphics.DrawImage(Resources.noBall, circImg);
                            break;
                        case 1:
                            e.Graphics.DrawLine(outline, this.Width / 2, (this.Height / 2) - 151, this.Width / 2, (this.Height / 2) + 151);
                            e.Graphics.DrawLine(linePen, this.Width / 2, (this.Height / 2) - 150, this.Width / 2, (this.Height / 2) + 150);
                            break;
                        case 2:
                            e.Graphics.DrawImage(Resources.freeBall, circImg);
                            break;
                        case 3:
                            e.Graphics.DrawLine(outline, this.Width / 2, (this.Height / 2) - 151, this.Width / 2, (this.Height / 2) + 151);
                            e.Graphics.DrawLine(linePen, this.Width / 2, (this.Height / 2) - 150, this.Width / 2, (this.Height / 2) + 150);
                            break;
                    }
                }
            }
            else if (hitNothing.IsRunning) //displays the outcome after two seconds has passed
            {
                if (returnBall)
                {
                    float speedInc = 10;
                    Rectangle freeBallRect = new Rectangle((this.Width / 2) - 150, (this.Height / 2) - 150, 300, 300);
                    Rectangle glowEffect = new Rectangle(freeBallRect.X - 100 - (int)((hitNothing.ElapsedMilliseconds - 2000) / speedInc), freeBallRect.Y - 100 - (int)((hitNothing.ElapsedMilliseconds - 2000) / speedInc),
                        500 + (2 * (int)((hitNothing.ElapsedMilliseconds - 2000) / speedInc)), 500 + (2 * (int)((hitNothing.ElapsedMilliseconds - 2000) / speedInc)));

                    e.Graphics.DrawImage(Resources.freeBallGlow, glowEffect);
                    e.Graphics.DrawImage(Resources.freeBall, freeBallRect);
                }
                else
                {
                    float speedInc = 10;
                    Rectangle noBallRect = new Rectangle((this.Width / 2) - 150, (this.Height / 2) - 150, 300, 300);
                    Rectangle flameEffect = new Rectangle(noBallRect.X - 100 - (int)(recentlyScored.ElapsedMilliseconds / speedInc), noBallRect.Y - 100 - (int)(recentlyScored.ElapsedMilliseconds / speedInc),
                        500 + (2 * (int)(recentlyScored.ElapsedMilliseconds / speedInc)), 500 + (2 * (int)(recentlyScored.ElapsedMilliseconds / speedInc)));
                    e.Graphics.DrawImage(Resources.flameNoBall, flameEffect);
                    e.Graphics.DrawImage(Resources.noBall, noBallRect);
                }
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //tracks X and Y variables in test state
            if (gameTestState)
            {
                xLabel.Visible = true;
                yLabel.Visible = true;
                xLabel.Text = $"X: {cursorPoint.X}";
                yLabel.Text = $"Y: {cursorPoint.Y}";
            }

            //removes the glow effect after hitting a peg and half a second has passed
            for (int i = 0; i < pegGlow.Count; i++)
            {
                if (pegGlow[i].IsRunning && pegGlow[i].ElapsedMilliseconds > 500)
                {
                    pegGlow.RemoveAt(i);
                    hitArrayLocations.RemoveAt(i);
                }
            }

            //each if corresponds with a different outcome of how the shot was played
            //all lead to the same clearer branch that clears each of the hit pegs that round
            if (hitNothing.ElapsedMilliseconds > 4000)
            {
                if (returnBall)
                    lives++;
                spinner = 0;
                hitNothing.Reset();
                clearer.Start();
            }
            if (recentlyScored.ElapsedMilliseconds > 2000)
            {
                recentlyScored.Reset();
                clearer.Start();
            }
            else if (fellOffBottom.ElapsedMilliseconds > 1500)
            {
                fellOffBottom.Reset();
                clearer.Start();
            }
            //clears each hit peg in a quick animation, then checks if there is still balls, in which case, the player can shoot again
            if (clearer.ElapsedMilliseconds > 100)
            {
                if (hitPegStorage.Count > 0)
                {
                    if (hitArrayLocations.Contains(hitPegStorage[tracker]))
                    {
                        int t = 0;
                        for (int i = 0; i < hitArrayLocations.Count; i++)
                        {
                            if (hitArrayLocations[i] == hitPegStorage[tracker])
                            {
                                t = i;
                                break;
                            }
                        }
                        pegGlow.RemoveAt(t);
                        hitArrayLocations.RemoveAt(t);
                    }
                    pegs.RemoveAt(hitPegStorage[tracker]);
                    tracker++;
                    clearer.Restart();
                }

                if (tracker >= hitPegStorage.Count) //all pegs removed
                {
                    tracker = 0;
                    clearer.Reset();
                    hitPegStorage.Clear();
                    animationState = false;

                    for (int i = 0; i < pegs.Count; i++) //checks if the game has been won
                    {
                        if (pegs[i].colState == "orange")
                        {
                            break;
                        }

                        if (pegs[i] == pegs.Last())
                        {
                            resetGame();
                            Form1.ChangeScreen(this, new WinScreen());
                        }
                    }
                    if (lives == 0) //ends game if player has no more balls
                    {
                        resetGame();
                        Form1.ChangeScreen(this, new LoseScreen());
                    }
                }
            }

            if (ballIsShot) //only runs if the ball is active in the arena
            {
                ball.doGravity(this.Height, this.Width - 200, 200, 0, pegs); //gravity effect
                int hitPeg = -1; //tracks if a peg is hit, and which one if it is, then stores it in the necessary lists
                using (Graphics g = this.CreateGraphics())
                {
                    xLabel.Text = $"X-Position: {ball.x}";
                    yLabel.Text = $"Y-Speed: {ball.ySpeed}";
                    for (int i = 0; i < pegs.Count; i++) //collision check for each peg, with different physics for both kinds
                    {
                        if (pegs[i].circle)
                        {
                            hitPeg = ball.circleCollision(new RectangleF(pegs[i].x, pegs[i].y, pegs[i].width, pegs[i].height), g, i, false, 0);
                            if (hitPeg != -1 && !hitPegStorage.Contains(hitPeg)) { adjustColorState(i); break; }
                            else { hitPeg = -1; }
                        }
                        else
                        {
                            bool state = false;
                            state = ball.BlockCollision(pegs[i], g, i);
                            if (state && !hitPegStorage.Contains(hitPeg)) { adjustColorState(i); hitPeg = i; break; }
                            else { hitPeg = -1; }
                        }

                    }
                    //paddle collisions
                    ball.circleCollision(lPadHitBox, g, 1, true, paddle.speed);
                    ball.circleCollision(rPadHitBox, g, 2, true, paddle.speed);
                }

                //sets up peg for removal and does glow effect if hit
                if (hitPeg != -1 && !hitPegStorage.Contains(hitPeg))
                {
                    hitArrayLocations.Add(hitPeg);
                    pegGlow.Add(new Stopwatch());
                    pegGlow[pegGlow.Count - 1].Start();
                    hitPegStorage.Add(hitPeg);
                }

                //allows for ball pickup if test-state is active
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

                //two checks for if a new shot is available
                if (hitBottom)
                {
                    newShot(false);
                }

                if (new RectangleF(ball.x, ball.y, ball.size, ball.size).IntersectsWith(new RectangleF(paddle.x, paddle.y, paddle.width, paddle.height)))
                {
                    newShot(true);
                }
            }
            //moves paddle back and forth
            paddle.Move(this.Width);
            updateCurve();

            Refresh();
        }

        private void adjustColorState(int i) //adjusts the argb values of the pegs when hit
        {
            int upAdjust = 30; //variable to easily change intensity of hit pegs
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

        private void updateCurve() //updates the curvey part of the paddles hitbox
        {
            //resets each region and redraws them with the updated locations
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
        public void newShot(bool scored) //resets parts of the game so a new shot can be made
        {
            //reset tracking variables, lowers lives count
            hitBottom = false;
            lives--;
            ballIsShot = false;

            //hides ball offscreen
            ball.xSpeed = 0;
            ball.ySpeed = 0;
            ball.x = 2000;
            ball.y = 2000;

            //redraws balls region to avoid it showing up after teleportation
            ball.ballPath.Reset();
            ball.ballPath.AddEllipse(new RectangleF(ball.x, ball.y, ball.size, ball.size));
            ball.ballRegion = new Region(ball.ballPath);

            //organizes list to that it can be cleared without removing the wrong pegs and sets animation state true
            animationState = true;
            hitPegStorage.Sort();
            hitPegStorage.Reverse();
            hitStartingPeg = false;

            //3 cases of how the shot ended, each causing a different outcome
            if (scored)
            {
                lives++;
                recentlyScored.Start();
            }
            else if (hitPegStorage.Count > 0)
            {
                fellOffBottom.Start();
            }
            else
            {
                hitNothing.Start();
                if (randGen.Next(0, 101) > 70) //lose the ball
                    returnBall = false;
                else //return ball back
                    returnBall = true;
            }
        }

        //used for painting text on screen, which saves rescources and makes the game less laggy
        //side note, I stole this from our Brick Breaker game, and Rowan wrote it, so thanks for the free code!
        static public void PaintText(Graphics e, string displayText, int size, PointF position, Color color)
        {
            Font myFont = new Font("Elephant", size, FontStyle.Bold);
            SolidBrush brush = new SolidBrush(color);
            e.DrawString(displayText, myFont, brush, position.X, position.Y);
            brush.Dispose();
            myFont.Dispose();
        }

        //updates cursor's position for cannon tracking
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

        //purely for testing.  Fully disabled upon actual gameplay
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
            if (!ballIsShot && !animationState) //launches the ball if it is in the cannon
            {
                ball.x = shotPosition.X - (ball.size / 2);
                ball.y = shotPosition.Y - (ball.size / 2);
                ball.xSpeed = (float)(0.23 * Ball.initalSpeedMultiplier) * (shotPosition.X - (this.Width / 2));
                ball.ySpeed = (float)(0.23 * Ball.initalSpeedMultiplier) * (shotPosition.Y - 5);
                ballIsShot = true;
            }

            else if (gameTestState && !animationState) //allows for picking up and throwing ball with mouse if in testing state
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

        private void resetGame() //resets the game
        {
            lives = 10;
            pegs.Clear();
            pegGlow.Clear();
            hitArrayLocations.Clear();
            Ball.recentlyHit.Clear();
            Ball.watchReset.Clear();
        }
    }
}
