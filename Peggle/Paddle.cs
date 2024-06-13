using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ball_Physics_Test
{
    internal class Paddle
    {
        public float x, y, width, height, speed;
        float speedAdjustFactor = (float)0.36; //adjust how much the speed changes
        int speedSetter = 3; //adjust to set paddle back n' forth speed
        int counter = 0;

        public Paddle(float _x, float _y, float _width, float _height, float _speed)
        {
            x = _x;
            y = _y;
            width = _width;
            height = _height;
            speed = _speed;
        }

        public void Move(int screenWidth)
        {
            x += speed;
            counter++;
            if (counter >= speedSetter)
            {
                if (x + (width / 2) < screenWidth / 2) //to the left of the center of the screen
                {
                    speed += speedAdjustFactor;
                }
                else //to the right of the center of the screen
                {
                    speed -= speedAdjustFactor;
                }
                counter = 0;
            }
        }
    }
}
