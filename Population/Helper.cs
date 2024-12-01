using Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Population
{
    public static class Helper
    {
        public static T GetClosestItem<T>(Vector2 position, List<T> movableItems) where T : BaseItem
        {
            //If the list is empty don't return anything
            if (movableItems.Count == 0)
                return null;
            //Set a maximum distance that is bigger to any possible distance
            double minDistance = 100000;
            double distance;
            T closestItem = null;
            foreach (T i in movableItems)
            {
                //Get the distance between the two points
                distance = Functions.DistanceBetweenTwoPoints(position, i.Position);
                if (distance < minDistance)
                {
                    //if the distance is smaller than the previous found one, mark i as the closestItem and set the minDistance
                    closestItem = i;
                    minDistance = distance;
                }
            }
            return closestItem;
        }
    }
}
