using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peggle
{
    internal class Peg
    {
        public float x, y, width, height;
        public bool circle, upScale;
        public RectangleF rectangle;
        public GraphicsPath circPath = new GraphicsPath();
        public Region circRegion = new Region();
        public Brush colour = null;
        public string colState = null;

        public Peg(float _x, float _y, float _width, float _height, bool _circle, bool _upScale)
        {
            x = _x; 
            y = _y; 
            width = _width; 
            height = _height; 
            circle = _circle;
            upScale = _upScale;

            rectangle = new RectangleF(x, y, width, height);

            if (circle) //creates a circle region if a circle is requested
            {
                circPath.Reset();
                circPath.AddEllipse(x, y, width, height);
                circRegion = new Region(circPath);
            }
        }
    }
}