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
        public bool IsVisible;
        
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

        public void UpdateVisibility(bool[,] mapVisibility, SizeF cellSize)
        {
            for(int i = 0; i < mapVisibility.GetLength(0); i ++)
            {
                for(int j = 0; j < mapVisibility.GetLength(1); j ++)
                {
                    bool isCollidingWithThisCell = Functions.CircleCircleCollision(new Vector2(j * cellSize.Width + cellSize.Width / 2, i * cellSize.Height + cellSize.Height / 2), cellSize.Width / 2, 
                                                                                    Position, Size.Width / 2);
                    if(isCollidingWithThisCell)
                    {
                        if(!mapVisibility[i,j])
                        {
                            IsVisible = false;
                            return;
                        }
                    }
                }
            }
            IsVisible = true;
        }
    }
}
