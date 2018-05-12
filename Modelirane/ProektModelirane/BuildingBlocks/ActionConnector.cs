using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ProektModelirane
{
    public class ActionConnector : IDraw
    {
        BuildingBlock start;
        BuildingBlock end;

        public ActionConnector(BuildingBlock start, BuildingBlock end)
        {
            this.start = start;
            this.end = end;

            start.Connect(this);
        }

        public void Draw(Graphics g)
        {
            Rectangle start = this.start.GetBounds();
            Rectangle end = this.end.GetBounds();
            start.X += start.Width / 2;
            start.Y += start.Height / 2;
            end.X += end.Width / 2;
            end.Y += end.Height / 2;

            Pen pen = new Pen(Brushes.Red);
            pen.StartCap = LineCap.Round;
            pen.CustomEndCap = new AdjustableArrowCap(5, 7);

            Point middle = new Point((start.X + end.X + 0) / 2, (start.Y + end.Y - 140) / 2);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawCurve(pen, new Point[] { start.Location, middle, end.Location }, 0.68f);
        }

        //викаме този метод при местенето на мишката в панела, в който чертаем
        //изчертава ActionConnector преди да сме го свързали с краен BuildingBlock
        static public void Draw(BuildingBlock start, Point end, Graphics g)
        {
            Rectangle rect = start.GetBounds();
            rect.X += rect.Width / 2;
            rect.Y += rect.Height / 2;

            Pen pen = new Pen(Brushes.Red);
            pen.StartCap = LineCap.Round;
            pen.CustomEndCap = new AdjustableArrowCap(5, 7);

            Point middle = new Point((rect.X + end.X + 0) / 2, (rect.Y + end.Y - 140) / 2);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawCurve(pen, new Point[] { rect.Location, middle, end }, 0.68f);
        }
        

        public BuildingBlock getStart()
        {
            return this.start;
        }

        public BuildingBlock getEnd()
        {
            return this.end;
        }

        public void DrawSelected(Graphics g)
        {
            throw new NotImplementedException();
        }

        public void DrawBoth(Graphics g, Color color, float width)
        {
            throw new NotImplementedException();
        }
    }
}
