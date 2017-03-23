using Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Population
{
    public class RobotTransporter : BaseRobot
    {
        private enum RobotTransporterState
        {
            Idle,
            GoForResource,
            ReturnResourceToBase
        }

        private RobotTransporterState _state;
        private ItemMovable _pickedUpItem;

        public RobotTransporter(Vector2 position, Random random) : base(position, random)
        {
            Color = Color.Violet;
            Size = new Size(20, 20);
            _state = RobotTransporterState.GoForResource;
        }

        public void DoLogic(List<ItemMovable> movableItems, BuildingHQ hq)
        {
            switch (_state)
            {
                case RobotTransporterState.Idle:
                    break;
                case RobotTransporterState.GoForResource:
                    if (WhereToGo == null)
                    {
                        if (movableItems.Count > 0)
                        {
                            ItemMovable closestItem = GetClosestItem(movableItems.Where(a => a.IsAvailableToBePickedUp).ToList());
                            WhereToGo = closestItem.Position;
                        }
                    }
                    else
                    {
                        if (Functions.DistanceBetweenTwoPoints(Position, WhereToGo) < destinationRange)
                        {
                            ItemMovable closestItem = GetClosestItem(movableItems.Where(a=>a.IsAvailableToBePickedUp).ToList());
                            if(Functions.DistanceBetweenTwoPoints(Position, closestItem.Position) < destinationRange)
                            {
                                _pickedUpItem = closestItem;
                                closestItem.IsAvailableToBePickedUp = false;
                                WhereToGo = hq.Position;
                                _state = RobotTransporterState.ReturnResourceToBase;
                            }
                            else
                            {
                                WhereToGo = null;
                            }
                        }
                    }
                    break;
                case RobotTransporterState.ReturnResourceToBase:
                    if (Functions.DistanceBetweenTwoPoints(Position, WhereToGo) < destinationRange)
                    {
                        _pickedUpItem = null;
                        _state = RobotTransporterState.GoForResource;
                        WhereToGo = null;
                    }
                    break;
            }
        }

        public override void Move()
        {
            base.Move();
            if(_pickedUpItem != null)
            {
                _pickedUpItem.Position = Position + (new Vector2(1, 0).Rotate(DirectionRadian) * 20);
            }
        }

        ItemMovable GetClosestItem(List<ItemMovable> movableItems)
        {
            double minDistance = 100000;
            double distance;
            ItemMovable closestItem = movableItems[0];
            foreach (ItemMovable i in movableItems)
            {
                distance = Functions.DistanceBetweenTwoPoints(Position, i.Position);
                if (distance < minDistance)
                {
                    closestItem = i;
                    minDistance = distance;
                }
            }
            return closestItem;
        }
    }
}
