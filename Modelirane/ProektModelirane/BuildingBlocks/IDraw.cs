using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ProektModelirane
{
    public interface IDraw
    {
        void Draw(Graphics g);
        void DrawSelected(Graphics g);
        void DrawBoth(Graphics g, Color color, float width);
    }
}
