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
            Color = Color.LightSeaGreen;
            Size = new Size(20, 20);
            _state = RobotTransporterState.GoForResource;
        }

        public void DoLogic(List<ItemMovable> movableItems, BuildingHQ hq)
        {
            switch (_state)
            {
                    //In Idle state do nothing
                case RobotTransporterState.Idle:
                    break;
                    //Find a resource nearby and go after it
                case RobotTransporterState.GoForResource:
                    //Check if it has a resource that it goes after
                    if (WhereToGo == null)
                    {
                        //If there are any resources on the screen, go after the closest one
                        if (movableItems.Count > 0)
                        {
                            ItemMovable closestItem = Helper.GetClosestItem(Position, movableItems.Where(a => a.IsAvailableToBePickedUp).ToList());
                            if (closestItem == null)
                                break;
                            WhereToGo = closestItem.Position;
                        }
                    }
                    else
                    {
                        //Check if robot arrived at the resource
                        if (Functions.DistanceBetweenTwoPoints(Position, WhereToGo) < destinationRange)
                        {
                            //If it got to the resource, pick the closest one(maybe there are more in within the rage)
                            ItemMovable closestItem = Helper.GetClosestItem(Position, movableItems.Where(a=>a.IsAvailableToBePickedUp).ToList());
                            if (closestItem == null)
                                break;
                            //Change the states of the robot and the item so no other robot will pick this one up
                            _pickedUpItem = closestItem;
                            closestItem.IsAvailableToBePickedUp = false;
                            WhereToGo = hq.Position;
                            _state = RobotTransporterState.ReturnResourceToBase;
                        }
                    }
                    break;
                    //After the resource is picked up, bring it back to the base
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

        public override void Move(SizeF cellSize, bool[,] mapVisibility)
        {
            base.Move(cellSize, mapVisibility);
            if (_pickedUpItem != null)
            {
                _pickedUpItem.Position = Position + (new Vector2(1, 0).Rotate(DirectionRadian) * 20);
            }
        }

        
    }
}
