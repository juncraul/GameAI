using Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Items
{
    public abstract class BaseItem
    {
        public Vector2 Position;
        public int Value;

        protected Size size;
        protected Color color;

        public BaseItem(Vector2 postition)
        {
            Position = postition;
            size = new Size(10, 10);
            color = Color.Brown;
        }

        public virtual void Draw(Graphics graphics)
        {
            
        }
    }
}
