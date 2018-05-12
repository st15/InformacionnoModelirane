using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ProektModelirane
{
    class Flow : BuildingBlock, IDraw
    {
        Point startPoint, endPoint;
        Stock startStock, endStock;

        public Flow(String name, String expression)
            : base(name, expression)
        {
        }

        public Flow(Stock startStock, Point endPoint)
            : base()
        {
            this.startStock = startStock;
            this.endPoint = endPoint;
        }

        public Flow(Point startPoint, Stock endStock)
            : base()
        {
            this.startPoint = startPoint;
            this.endStock = endStock;
        }

        public Flow(Stock startStock, Stock endStock)
            : base()
        {
            this.startStock = startStock;
            this.endStock = endStock;
        }

        public Flow(Point startPoint, Point endPoint)
            : base()
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
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
            if (startStock != null)
                startPoint = new Point((startStock).GetBounds().X + (startStock).GetBounds().Width / 2,
                                        (startStock).GetBounds().Y + (startStock).GetBounds().Height / 2);
            if (endStock != null)
                endPoint = new Point((endStock).GetBounds().X + (endStock).GetBounds().Width / 2,
                                      (endStock).GetBounds().Y + (endStock).GetBounds().Height / 2);

            Point middle = new Point(
                ((startPoint.X > endPoint.X) ? endPoint.X : startPoint.X) + Math.Abs(startPoint.X - endPoint.X) / 2 - 10,
                ((startPoint.Y > endPoint.Y) ? endPoint.Y : startPoint.Y) + Math.Abs(startPoint.Y - endPoint.Y) / 2 - 10);
            this.rectangle = new Rectangle(middle, new Size(20, 20));

            Pen pen = new Pen(color, 2);
            pen.StartCap = LineCap.Square;
            pen.CustomEndCap = new AdjustableArrowCap(8, 8);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawLine(pen, startPoint, endPoint);
            g.FillEllipse(Brushes.White, rectangle);
            g.DrawEllipse(new Pen(color, width), rectangle);
            g.DrawString(this.name,
                new Font("Arial", 10),
                new SolidBrush(color),
                rectangle.Left + rectangle.Width / 2 - g.MeasureString(this.name, new Font("Arial", 10)).Width / 2,
                rectangle.Bottom);
        }


        //викаме този метод при местенето на мишката в панела, в който чертаем
        //изчертава Flow преди да сме го свързали с крайна точка или BuildingBlock
        static public void Draw(Point startPoint, Point endPoint, Graphics g)
        {
            //Stock startStock = (Stock)start;
            //Point startPoint = new Point((startStock).GetBounds().X + (startStock).GetBounds().Width / 2, (startStock).GetBounds().Y + (startStock).GetBounds().Height / 2);
            
            Point middle = new Point(
                ((startPoint.X > endPoint.X) ? endPoint.X : startPoint.X) + Math.Abs(startPoint.X - endPoint.X) / 2 - 10,
                ((startPoint.Y > endPoint.Y) ? endPoint.Y : startPoint.Y) + Math.Abs(startPoint.Y - endPoint.Y) / 2 - 10);

            Rectangle rectangle = new Rectangle(middle, new Size(20, 20));

            Pen pen = new Pen(Color.Black, 2);
            pen.StartCap = LineCap.Square;
            pen.CustomEndCap = new AdjustableArrowCap(8, 8);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawLine(pen, startPoint, endPoint);
            g.FillEllipse(Brushes.White, rectangle);
            g.DrawEllipse(new Pen(Color.Black, 1), rectangle);
        }

        public void RemoveStocks()
        {
            if (startStock != null)
            {
                startStock.RemoveInFlow();
                startStock.RemoveOutFlow();
            }
            if (endStock != null)
            {
                endStock.RemoveInFlow();
                endStock.RemoveOutFlow();
            }
        }
    }
}
