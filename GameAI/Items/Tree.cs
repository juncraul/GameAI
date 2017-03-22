using Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Items
{
    public class Tree : BaseItem
    {
        public Tree(Vector2 position) : base(position)
        {

        }

        public override void Draw(Graphics graphics)
        {
            base.Draw(graphics);
            Brush brush = new SolidBrush(color);
            graphics.FillEllipse(brush, (float)Position.X - size.Width / 2, (float)Position.Y - size.Height / 2, size.Width, size.Height);
        }
    }
}
