using Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Population
{
    public abstract class BaseItem : BaseEntity
    {
        public int Value;
        
        public BaseItem(Vector2 postition) : base(postition)
        {
            Size = new Size(10, 10);
            Color = Color.Brown;
        }

        public override void Draw(Graphics graphics)
        {
            Brush brush = new SolidBrush(Color);
            graphics.FillEllipse(brush, (float)Position.X - Size.Width / 2, (float)Position.Y - Size.Height / 2, Size.Width, Size.Height);
        }
    }
}
