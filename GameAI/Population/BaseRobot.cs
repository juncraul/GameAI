using Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Population
{
    public abstract class BaseRobot : BaseEntity
    {
        protected Vector2 WhereToGo;
        protected Random Random;
        protected double destinationRange;
        protected double DirectionRadian;

        private double _moveSpeed;

        public BaseRobot(Vector2 position, Random rand) : base(position)
        {
            _moveSpeed = 14.5f;
            destinationRange = 10;
            Random = rand;
        }

        public override void Draw(Graphics graphics)
        {

            Brush brush = new SolidBrush(Color);
            graphics.FillEllipse(brush, (float)Position.X - Size.Width / 2, (float)Position.Y - Size.Height / 2, Size.Width, Size.Height);

            //if(WhereToGo != null)
            //{
            //    graphics.FillEllipse(brush, (float)WhereToGo.X - Size.Width / 2, (float)WhereToGo.Y - Size.Height / 2, Size.Width, Size.Height);
            //}

            Pen pen = new Pen(Color.Black);
            Vector2 direction = (new Vector2(15, 0).Rotate(DirectionRadian));
            graphics.DrawLine(pen, (float)Position.X, (float)(Position.Y), (float)(direction + Position).X, (float)(direction + Position).Y);
        }

        public virtual void Move()
        {
            if (WhereToGo == null) return;
            DirectionRadian = Functions.AngleBetweenTwoPoints(Position, WhereToGo);
            Position += new Vector2(1, 0).Rotate(DirectionRadian).Normaize() * _moveSpeed;
        }
    }
}
