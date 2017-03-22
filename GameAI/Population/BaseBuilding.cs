using Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Population
{
    public abstract class BaseBuilding : BaseEntity
    {
        public BaseBuilding(Vector2 position) : base(position)
        {
            Color = Color.Blue;
            Size = new Size(30, 30);
        }

        public override void Draw(Graphics graphics)
        {
            Brush brush = new SolidBrush(Color);
            graphics.FillRectangle(brush, (float)Position.X - Size.Width / 2, (float)Position.Y - Size.Height / 2, Size.Width, Size.Height);
        }
    }
}
