using Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Population
{
    public class ItemMovable : BaseItem
    {
        public ItemMovable(Vector2 position, int value) : base(position)
        {
            Color = System.Drawing.Color.Aqua;
        }
    }
}
