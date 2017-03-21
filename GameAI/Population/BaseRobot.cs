using Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Population
{
    public abstract class BaseRobot
    {
        public Vector2 Position;

        private double _directionRadian;
        private Color _color;
        private Size _size;
        private Vector2 _whereToGo;
        private double _moveSpeed;

        public BaseRobot(Vector2 position, Color color)
        {
            Position = position;
            _color = color;
            _size = new Size(20, 20);
            _moveSpeed = 1.5f;
        }

        public void Draw(Graphics graphics, Bitmap bitmap)
        {

            Brush brush = new SolidBrush(_color);
            graphics.FillEllipse(brush, (float)Position.X - _size.Width / 2, bitmap.Height - ((float)Position.Y + _size.Height / 2), _size.Width, _size.Height);

            Pen pen = new Pen(Color.Black);
            Vector2 direction = (new Vector2(15, 0).Rotate(_directionRadian));
            graphics.DrawLine(pen, (float)Position.X, (float)(bitmap.Height - Position.Y), (float)(direction + Position).X, (float)(bitmap.Height - (direction + Position).Y));
        }

        public void Move()
        {
            Position += new Vector2(1, 0).Rotate(_directionRadian).Normaize() * _moveSpeed;
        }
    }
}
