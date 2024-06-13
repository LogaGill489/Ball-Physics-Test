using Ball_Physics_Test;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Peggle
{
    internal class Ball
    {
        public float x, y, size, xSpeed, ySpeed;
        public Region ballRegion = new Region();
        PointF prevPosition = new PointF();
        GraphicsPath ballPath = new GraphicsPath();

        public List<bool> recentlyHit = new List<bool>();
        public List<int> watchReset = new List<int>();
        public Ball(float _x, float _y, float _size, float _xSpeed, float _ySpeed)
        {
            x = _x;
            y = _y;
            size = _size;
            xSpeed = _xSpeed;
            ySpeed = _ySpeed;

            ballPath.AddEllipse(new RectangleF(x, y, size, size));
            ballRegion = new Region(ballPath);
        }

        public void doGravity(int screenHeight, int screenWidth, int xZero, int yZero, List<Peg> pegs) //ball physics with walls + gravity
        {
            prevPosition = new PointF(x, y);
            ballPath.Reset();
            if (!onPeg)
            {
                if (y < screenHeight - size - 4)
                {
                    ySpeed += 1;
                }
                else
                {
                    y = screenHeight - size;
                    ySpeed *= -1;
                    if (-1 > ySpeed || ySpeed > 1)
                    {
                        const int SLOWER = 3;
                        if (ySpeed > 0)
                            ySpeed -= SLOWER;
                        else ySpeed += SLOWER;
                    }
                    else
                    {
                        ySpeed = 0;
                    }
                }
            }
            else if (onBlockTimer.ElapsedMilliseconds >= 1000)
            {
                onBlockTimer.Stop();
                onBlockTimer.Reset();
                onPeg = false;
                pegs.Remove(stoppedPeg);
            }

            if (x > screenWidth - size)
            {
                xSpeed *= -1;
                x = screenWidth - size;
            }
            if (x < xZero)
            {
                xSpeed *= -1;
                x = xZero;
            }
            if (y < yZero)
            {
                ySpeed *= -1;
                y = yZero;
            }
            const float xSLOWER = (float)0.15;
            if (Math.Abs(ySpeed) == 0 || Math.Abs(xSpeed) > 5)
            {
                if (Math.Abs(xSpeed) > 0.3)
                {
                    if (xSpeed > 0)
                        xSpeed -= xSLOWER;
                    else xSpeed += xSLOWER;
                }
                else
                {
                    xSpeed = 0;
                }
            }

            y += ySpeed;
            x += xSpeed;
            ballPath.AddEllipse(new RectangleF(x, y, size, size));
            ballRegion = new Region(ballPath);
            for (int i = 0; i < recentlyHit.Count; i++)
            {
                if (recentlyHit[i])
                {
                    watchReset[i]++;
                    if (watchReset[i] >= 4) { watchReset[i] = 0; recentlyHit[i] = false; ySpeed = (int)ySpeed; }
                }
            }
        }

        float slope;

        public void addPegs(List<Peg> pegs) //adds pegs to internal lists based off of a list of pegs
        {
            foreach (Peg peg in pegs)
            {
                recentlyHit.Add(false);
                watchReset.Add(0);
            }
        }

        public void removePegs(int index) //removes selected peg from internal list
        {
            recentlyHit.RemoveAt(index);
            watchReset.RemoveAt(index);
        }

        bool onPeg = false;
        Stopwatch onBlockTimer = new Stopwatch();
        Peg stoppedPeg;
        Peg lasthitPeg;

        bool hitSide = false;
        int tick = 4;

        public bool BlockCollision(Peg p, Graphics g)
        {
            RectangleF blockRec = new RectangleF(p.x, p.y, p.width, p.height);
            RectangleF ballRec = new RectangleF(x, y, size, size);

            Region blockRegion = new Region(blockRec);
            blockRegion.Intersect(ballRegion);

            if (tick <= 2)
            {
                tick++;
            }
            if (tick == 3)
            {
                tick++;
                hitSide = false;
            }

            if (!blockRegion.IsEmpty(g))
            {
                RectangleF location = blockRegion.GetBounds(g);
                if (location.Width == location.Height)
                {
                    ySpeed *= -1;
                    xSpeed *= -1;
                    if (p.y + (blockRec.Height / 2) > prevPosition.Y + (size / 2) && p.x + (blockRec.Width / 2) > prevPosition.X + (size / 2)) //above and left
                    {
                        x = p.x - size;
                        y = p.y - size;
                    }
                    else if (p.y + (blockRec.Height / 2) > prevPosition.Y + (size / 2) && p.x + (blockRec.Width / 2) < prevPosition.X + (size / 2)) //above and right
                    {
                        x = p.x + p.width;
                        y = p.y - size;
                    }
                    else if (p.x + (blockRec.Width / 2) > x + (size / 2)) //below left
                    {
                        x = p.x - size;
                        y = p.y + p.height;
                    }
                    else //below right
                    {
                        x = p.x + p.width;
                        y = p.y + p.height;
                    }
                }
                if (location.Width > location.Height)
                {
                    if (p.y + (blockRec.Height / 2) > prevPosition.Y + (size / 2)) //above
                    {
                        y = p.y - size;
                        y++;
                        if (Math.Abs(ySpeed) <= 2 && Math.Abs(xSpeed) < 1.5)
                        {
                            ySpeed = 0;
                            onPeg = true;
                            onBlockTimer.Start();
                            stoppedPeg = p;
                        }
                    }
                    else
                    {
                        y = p.y + p.height;
                        y--;
                    }
                    ySpeed *= -1;
                }
                else
                {
                    if (hitSide)
                    {
                        if (p.y + (blockRec.Height / 2) > y + (size / 2)) //above
                        {
                            y = p.y - size;
                            y++;
                        }
                        else //below
                        {
                            y = p.y + p.height;
                            y++;
                        }
                        //hitSide = false;
                        ySpeed *= -1;
                    }
                    else
                    {
                        if (p.x + (blockRec.Width / 2) > x + (size / 2)) //to the left
                        {
                            x = p.x - size;
                        }
                        else
                        {
                            x = p.x + p.width;
                        }

                        xSpeed *= -1;
                        hitSide = true;
                        tick = 0;
                    }
                }
            }
            return blockRec.IntersectsWith(ballRec);
        }

        public bool circleCollision(RectangleF pegHitBox, Graphics e, int listPosition, bool paddleHit, float paddleSpeed)
        {
            float xMath;
            PointF pivotPoint;
            bool clockWise = true;
            int state = 0;

            Region tempBallRegion = ballRegion;
            Region pegRegion = new Region();
            GraphicsPath pegPath = new GraphicsPath();

            pegPath.Reset();
            pegPath.AddEllipse(pegHitBox);
            pegRegion = new Region(pegHitBox);

            if (paddleHit && listPosition == 1)
            {
                pegRegion.Exclude(new RectangleF(pegHitBox.X, pegHitBox.Y + 25, 50, 25));
                pegRegion.Exclude(new RectangleF(pegHitBox.X + 25, pegHitBox.Y, 25, 25));
                state = 1;
            }
            else if (paddleHit && listPosition == 2)
            {
                pegRegion.Exclude(new RectangleF(pegHitBox.X, pegHitBox.Y + 25, 50, 25));
                pegRegion.Exclude(new RectangleF(pegHitBox.X, pegHitBox.Y, 25, 25));
                state = 2;
            }

            pegRegion.Intersect(tempBallRegion);

            //bounce curve modelled in desmos, which can be found here: "https://www.desmos.com/calculator/emiewzq0xk"
            //calculates slope of derivative
            if (!pegRegion.IsEmpty(e) && !recentlyHit[listPosition])
            {
                if (y + (size / 2) < pegHitBox.Y + (pegHitBox.Height / 2) && x + (size / 2) < pegHitBox.X + (pegHitBox.Width / 2) || state == 1) //checks for top-left collision
                {
                    xMath = x + (size / 2) - (pegHitBox.X + pegHitBox.Width / 2);
                    slope = (float)(-xMath / Math.Sqrt(Math.Pow(pegHitBox.Width, 2) - Math.Pow(xMath, 2)));
                    pivotPoint = upperPivot(xMath / 2, pegHitBox, true);
                    state = 1;
                }
                else if (y + (size / 2) < pegHitBox.Y + (pegHitBox.Height / 2) || state == 2) // top-right collision
                {
                    xMath = x + (size / 2) - (pegHitBox.X + pegHitBox.Width / 2);
                    slope = (float)(-xMath / Math.Sqrt(Math.Pow(pegHitBox.Width, 2) - Math.Pow(xMath, 2)));
                    pivotPoint = upperPivot(xMath / 2, pegHitBox, false);

                    if (ySpeed < 0) clockWise = false;
                    else clockWise = true;
                    state = 2;
                }
                else if (y + (size / 2) > pegHitBox.Y + (pegHitBox.Height / 2) && x + (size / 2) < pegHitBox.X + (pegHitBox.Width / 2)) //bottom-left collision
                {
                    xMath = (pegHitBox.X + pegHitBox.Width / 2) - (x + size / 2);
                    slope = (float)(-xMath / Math.Sqrt(Math.Pow(pegHitBox.Width, 2) - Math.Pow(xMath, 2)));
                    pivotPoint = lowerPivot(xMath / 2, pegHitBox, true);

                    if (ySpeed > 0) clockWise = false;
                    else clockWise = true;
                    state = 3;
                }
                else //bottom-right collision
                {
                    xMath = (pegHitBox.X + pegHitBox.Width / 2) - (x + size / 2);
                    slope = (float)(-xMath / Math.Sqrt(Math.Pow(pegHitBox.Width, 2) - Math.Pow(xMath, 2)));
                    pivotPoint = lowerPivot(xMath / 2, pegHitBox, false);
                    state = 4;
                }
                //finds angle between slope and ballSpeed
                float lowerEquation = (float)Math.Sqrt(Math.Pow(xSpeed, 2) + Math.Pow(ySpeed, 2)) * (float)Math.Sqrt(1 + Math.Pow(slope, 2));
                float upperEquation = xSpeed + (ySpeed * slope);
                float theta = (float)Math.Acos(upperEquation / lowerEquation);
                if (theta == Math.PI / 2)
                {
                    xSpeed *= -1;
                    ySpeed *= -1;
                }
                else
                {
                    theta = (float)Math.PI - theta * 2;
                    //figures out if it is ccw / cw
                    float xPosNormal = -slope * (prevPosition.Y - pivotPoint.Y) + pivotPoint.X;
                    switch (state)
                    {
                        case 1:
                            if (xPosNormal > prevPosition.X) clockWise = false;
                            else clockWise = true;

                            if (x >= pegHitBox.X - (pegHitBox.Width / 10) && x <= pegHitBox.X + (3 * pegHitBox.Width / 10))
                            {
                                x = pegHitBox.X - (size / 2);
                                y = pegHitBox.Y - (size / 2);
                            }
                            else
                            {
                                x = pivotPoint.X - (float)(size * 0.8);
                                y = pivotPoint.Y - (float)(size * 0.8);
                            }
                            break;
                        case 2:
                            if (xPosNormal > prevPosition.X) clockWise = true;
                            else clockWise = false;

                            if (x >= pegHitBox.X + (pegHitBox.Width / 2) + (16 * (pegHitBox.Width / 2) / 25))
                            {
                                x = pegHitBox.X + (pegHitBox.Width / 2) + (16 * (pegHitBox.Width / 2) / 25);
                                y = pegHitBox.Y - (size / 2);
                            }
                            else
                            {
                                x = pivotPoint.X - (float)(size * 0.2);
                                y = pivotPoint.Y - (float)(size * 0.8);
                            }
                            break;
                        case 3:
                            if (xPosNormal > prevPosition.X) clockWise = false;
                            else clockWise = true;

                            if (x >= pegHitBox.X - (pegHitBox.Width / 10) && x <= pegHitBox.X + (3 * pegHitBox.Width / 10))
                            {
                                x = pegHitBox.X - (size / 2);
                                y = pegHitBox.Y + pegHitBox.Height - size;
                            }
                            else
                            {
                                x = pivotPoint.X - (float)(size * 0.8);
                                y = pivotPoint.Y - (float)(size * 0.2);
                            }
                            break;
                        case 4:
                            if (xPosNormal > prevPosition.X) clockWise = false;
                            else clockWise = true;

                            if (x >= pegHitBox.X + (pegHitBox.Width / 2) + (16 * (pegHitBox.Width / 2) / 25))
                            {
                                x = pegHitBox.X + (pegHitBox.Width / 2) + (16 * (pegHitBox.Width / 2) / 25);
                                y = pegHitBox.Y + pegHitBox.Height - size;
                            }
                            else
                            {
                                x = pivotPoint.X - (float)(size * 0.2);
                                y = pivotPoint.Y - (float)(size * 0.2);
                            }
                            break;
                    }
                    if (!clockWise)
                    {
                        theta = (float)(2 * Math.PI) - theta;
                    }
                    PointF newDirection = RotatePoint(prevPosition, pivotPoint, theta);
                    // set x/ySpeed based on gap between two points

                    xSpeed = newDirection.X - pivotPoint.X;
                    ySpeed = newDirection.Y - pivotPoint.Y;

                    xSpeed *= (float)0.5;
                    ySpeed *= (float)0.5;

                    if (paddleHit)
                    {
                        if ((paddleSpeed > 0 && state == 2) || (paddleSpeed < 0 && state == 1))
                        {
                            xSpeed += paddleSpeed;
                        }
                    }
                }
                if (!paddleHit)
                    recentlyHit[listPosition] = true;

                return true;
            }
            return false;
        }

        PointF upperPivot(float xP, RectangleF hitBox, bool left)
        {
            float yP;
            yP = (float)Math.Sqrt(Math.Pow(hitBox.Width / 2, 2) - Math.Pow(xP, 2));
            yP = hitBox.Height / 2 + hitBox.Y - Math.Abs(yP);
            if (left) xP = hitBox.X + hitBox.Width / 2 - Math.Abs(xP);
            else xP = hitBox.X + hitBox.Width / 2 + Math.Abs(xP);
            return new PointF(xP, yP);
        }

        PointF lowerPivot(float xP, RectangleF hitBox, bool left)
        {
            float yP;
            yP = (float)Math.Sqrt(Math.Pow(hitBox.Width / 2, 2) - Math.Pow(xP, 2));
            yP = hitBox.Height / 2 + hitBox.Y + Math.Abs(yP);
            if (left) xP = hitBox.X + hitBox.Width / 2 - Math.Abs(xP);
            else xP = hitBox.X + hitBox.Width / 2 + Math.Abs(xP);
            return new PointF(xP, yP);
        }

        public static PointF RotatePoint(PointF point, PointF pivot, double radians)
        {
            var cosTheta = Math.Cos(radians);
            var sinTheta = Math.Sin(radians);

            var x = (cosTheta * (point.X - pivot.X) - sinTheta * (point.Y - pivot.Y) + pivot.X);
            var y = (sinTheta * (point.X - pivot.X) + cosTheta * (point.Y - pivot.Y) + pivot.Y);

            return new PointF((float)x, (float)y);
        }
    }
}
