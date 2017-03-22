using Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Population
{
    public abstract class BaseEntity
    {
        public Vector2 Position;
        protected Size Size;
        protected Color Color;

        public BaseEntity(Vector2 position)
        {
            Position = position;
        }

        public abstract void Draw(Graphics graphics);
    }
}
