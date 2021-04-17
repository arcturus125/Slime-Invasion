using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinder.tiles;
using ItemSystem;

namespace Towers
{
    public class Tower : MonoBehaviour
    {
        public int x; 
        public int y; 

        public Item recievableItem; // acts as a filter, only allowing items to be placed in the tower if they match this item
        public Inventory inv;
        private const float DEFAULT_SPEED_MULTIPLIER = 1.0f;
        public float speedMultiplier = DEFAULT_SPEED_MULTIPLIER;
        public int powerNeedToSpeedUp = 0;
         


        /// <summary>
        /// Initialise the inventory, set the towers position in tilespace and make the tile non-navicable
        /// </summary>
        /// <param name="t"></param>
        public virtual void OnPlaced(Tile t)
        {
            inv = new Inventory();
            x = t.x;
            y = t.y;

            t.MakeNonNavicable();
        }

        public void SetSpeedMultiplier(float multiplier)
        {
            speedMultiplier = multiplier;
        }
    }
}
