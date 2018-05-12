using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ProektModelirane
{
    class Constant : BuildingBlock, IDraw
    {
        public Constant(Point coord)
            : base()
        {
            rectangle = new Rectangle(coord, new Size(30, 30));
        }

        public void Draw(Graphics g)
        {
            DrawBoth(g, Color.Black, 1);
        }

        public void DrawSelected(Graphics g)
        {
            DrawBoth(g, Color.Blue, 2);
        }

        public void DrawBoth(Graphics g, Color color, float width)
        {
            Pen pen = new Pen(color, width);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.DrawEllipse(pen, rectangle);
            g.DrawString(this.name,
                new Font("Arial", 10),
                new SolidBrush(color),
                rectangle.Left + rectangle.Width / 2 - g.MeasureString(this.name, new Font("Arial", 10)).Width / 2,
                rectangle.Bottom);
        }

    }
}
