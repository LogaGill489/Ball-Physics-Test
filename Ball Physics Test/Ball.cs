using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public void doGravity(int screenHeight, int screenWidth, List<Peg> pegs) //ball physics with walls + gravity
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
            if (x < 0)
            {
                xSpeed *= -1;
                x = 0;
            }
            if (y < 0)
            {
                ySpeed *= -1;
                y = 0;
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
                    if (watchReset[i] >= 3) { watchReset[i] = 0; recentlyHit[i] = false; ySpeed = (int)ySpeed; }
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

        public bool BlockCollision(Peg p)
        {
            RectangleF blockRec = new RectangleF(p.x, p.y, p.width, p.height);
            RectangleF ballRec = new RectangleF(x, y, size, size);

            if (ballRec.IntersectsWith(blockRec))
            {
                int[] colEdgesMove;
                int[] colEdgesBrick;
                int distX;
                int distY;
                double timeX;
                double timeY;
                if (xSpeed > 0)
                {
                    if (ySpeed > 0)
                    {
                        colEdgesMove = new int[] { (int)ballRec.Right, (int)ballRec.Bottom };
                        colEdgesBrick = new int[] { (int)blockRec.Left, (int)blockRec.Top };
                        distX = Math.Abs(colEdgesBrick[0] - colEdgesMove[0]);
                        distY = Math.Abs(colEdgesBrick[1] - colEdgesMove[1]);
                        timeX = Math.Abs((double)distX / xSpeed);
                        timeY = Math.Abs((double)distY / ySpeed);
                        if (timeX > timeY)
                            ySpeed *= -1;
                        else if (timeY > timeX)
                            xSpeed *= -1;
                        else
                            Console.WriteLine("Collided at a corner");
                    }
                    else
                    {
                        colEdgesMove = new int[] { (int)ballRec.Right, (int)ballRec.Top };
                        colEdgesBrick = new int[] { (int)blockRec.Left, (int)blockRec.Bottom };
                        distX = Math.Abs(colEdgesBrick[0] - colEdgesMove[0]);
                        distY = Math.Abs(colEdgesBrick[1] - colEdgesMove[1]);
                        timeX = Math.Abs((double)distX / xSpeed);
                        timeY = Math.Abs((double)distY / ySpeed);
                        if (timeX > timeY)
                        {
                            ySpeed *= -1;
                            y = p.y - size;
                        }
                        else if (timeY > timeX)
                            xSpeed *= -1;
                        else
                            Console.WriteLine("Collided at a corner");
                    }
                }
                else
                {
                    if (ySpeed > 0)
                    {
                        colEdgesMove = new int[] { (int)ballRec.Left, (int)ballRec.Bottom };
                        colEdgesBrick = new int[] { (int)blockRec.Right, (int)blockRec.Top };
                        distX = Math.Abs(colEdgesBrick[0] - colEdgesMove[0]);
                        distY = Math.Abs(colEdgesBrick[1] - colEdgesMove[1]);
                        timeX = Math.Abs((double)distX / xSpeed);
                        timeY = Math.Abs((double)distY / ySpeed);
                        if (timeX > timeY)
                        {
                            ySpeed *= -1;
                            if (Math.Abs(ySpeed) <= 2 && Math.Abs(xSpeed) < 1.5)
                            {
                                ySpeed = 0;
                                onPeg = true;
                                onBlockTimer.Start();
                                stoppedPeg = p;
                            }
                            else
                            {
                                ySpeed += 3;
                            }
                            y = p.y - size;
                        }
                        else if (timeY > timeX)
                            xSpeed *= -1;
                        else
                            Console.WriteLine("Collided at a corner");
                    }
                    else
                    {
                        colEdgesMove = new int[] { (int)ballRec.Left, (int)ballRec.Top };
                        colEdgesBrick = new int[] { (int)blockRec.Right, (int)blockRec.Bottom };
                        distX = Math.Abs(colEdgesBrick[0] - colEdgesMove[0]);
                        distY = Math.Abs(colEdgesBrick[1] - colEdgesMove[1]);
                        timeX = Math.Abs((double)distX / xSpeed);
                        timeY = Math.Abs((double)distY / ySpeed);
                        if (timeX > timeY)
                            ySpeed *= -1;
                        else if (timeY > timeX)
                            xSpeed *= -1;
                        else
                            Console.WriteLine("Collided at a corner");
                    }
                }
            }
            return blockRec.IntersectsWith(ballRec);
        }

        public bool circleCollision(RectangleF pegHitBox, Graphics e, int listPosition)
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

            pegRegion.Intersect(tempBallRegion);

            //bounce curve modelled in desmos, which can be found here: "https://www.desmos.com/calculator/emiewzq0xk"
            //calculates slope of derivative
            if (!pegRegion.IsEmpty(e) && !recentlyHit[listPosition])
            {
                if (y + (size / 2) < pegHitBox.Y + (pegHitBox.Height / 2) && x + (size / 2) < pegHitBox.X + (pegHitBox.Width / 2)) //checks for top-left collision
                {
                    xMath = x + (size / 2) - (pegHitBox.X + pegHitBox.Width / 2);
                    slope = (float)(-xMath / Math.Sqrt(Math.Pow(pegHitBox.Width, 2) - Math.Pow(xMath, 2)));
                    pivotPoint = upperPivot(xMath / 2, pegHitBox, true);
                    state = 1;
                }
                else if (y + (size / 2) < pegHitBox.Y + (pegHitBox.Height / 2)) // top-right collision
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
                }
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
