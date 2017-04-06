using Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Population
{
    public class RobotMiner : BaseRobot
    {
        private enum RobotMinerState{
            Idle,
            SearchForMiningSite,
            GoToMiningSite,
            Mine
        }

        private RobotMinerState _state;
        private ItemFixed _itemToMine;
        private int _amountToMinePerCycle;
        private int _cycleTickIteration;
        private int _cycleMaxTick;

        public RobotMiner(Vector2 position, Random random) : base(position, random)
        {
            Color = Color.Orchid;
            Size = new Size(20, 20);
            _state = RobotMinerState.SearchForMiningSite;
            _amountToMinePerCycle = 10;
            _cycleMaxTick = 20;
        }

        public ItemMovable DoLogic(List<ItemFixed> fixedItems)
        {
            ItemMovable item = null;

            switch(_state)
            {
                //In Idle state do nothing
                case RobotMinerState.Idle:
                    break;
                case RobotMinerState.SearchForMiningSite:
                    //If there are any resources on the screen, go after the closest one
                    if (fixedItems.Count > 0)
                    {
                        ItemFixed closestItem = Helper.GetClosestItem(Position, fixedItems);
                        if (closestItem == null)
                            break;
                        WhereToGo = closestItem.Position;
                        _state = RobotMinerState.GoToMiningSite;
                    }
                    break;
                //Go to the mining site
                case RobotMinerState.GoToMiningSite:
                    //Check if robot arrived at the resource
                    if (Functions.DistanceBetweenTwoPoints(Position, WhereToGo) < destinationRange)
                    {
                        //Change the robot's state to Mine
                        WhereToGo = null;
                        _state = RobotMinerState.Mine;
                    }
                    break;
                case RobotMinerState.Mine:
                    //Check if it has anything to mine
                    if(_itemToMine == null)
                    {
                        //Get the first resource within the range
                        foreach (ItemFixed i in fixedItems)
                        {
                            if (Functions.DistanceBetweenTwoPoints(Position, i.Position) < destinationRange)
                            {
                                _itemToMine = i;
                                break;
                            }
                        }
                        //If no item in range, change the state to search and go
                        if(_itemToMine == null)
                        {
                            _state = RobotMinerState.SearchForMiningSite;
                        }
                    }
                    else
                    {
                        //Mine the resource every _cycleMaxTick ticks
                        _cycleTickIteration++;
                        if(_cycleTickIteration == _cycleMaxTick)
                        {
                            _cycleTickIteration = 0;
                            item = _itemToMine.Mine(_amountToMinePerCycle);
                            if (!_itemToMine.IsAlive)
                                _itemToMine = null;
                        }
                    }
                    break;
            }

            return item;
        }
    }
}
