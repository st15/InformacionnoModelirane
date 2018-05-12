using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ProektModelirane
{
    class Stock : BuildingBlock, IDraw
    {
        Flow inFlow;
        Flow outFlow;

        public Stock(Point coord) : base ()
        {
            rectangle = new Rectangle(coord, new Size(80, 60));
            inFlow = null;
            outFlow = null;
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
            Pen p = new Pen(color, width);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.DrawRectangle(p, rectangle);
            g.DrawString(this.name,
                new Font("Arial", 10),
                new SolidBrush(color),
                rectangle.Left + rectangle.Width / 2 - g.MeasureString(this.name, new Font("Arial", 10)).Width/2,
                rectangle.Top - 16);
        }

        public bool AddInFlow(Flow inFlow)
        {
            if (this.inFlow == null)
            {
                this.inFlow = inFlow;
                return true;
            }
            else
                return false;
        }

        public bool AddOutFlow(Flow outFlow)
        {
            if (this.outFlow == null)
            {
                this.outFlow = outFlow;
                return true;
            }
            else
                return false;
        }

        public void RemoveInFlow()
        {
            this.inFlow = null;
        }

        public void RemoveOutFlow()
        {
            this.outFlow = null;
        }

        public Flow getInFlow()
        {
            return this.inFlow;
        }

        public Flow getOutFlow()
        {
            return this.outFlow;
        }

        public Double CreateVariable()
        { 
            Double value = Double.NaN;
            if (Double.TryParse(this.expression, out value))
                return value;
            else
                return Double.NaN;
        }

        public String CreateDerivative()
        {
            String derivative = String.Empty;
            if (inFlow != null)
                derivative = inFlow.GetExpression();
            if (outFlow != null)
                derivative = derivative + "-" + outFlow.GetExpression();

            return derivative;
        }

    }
}
