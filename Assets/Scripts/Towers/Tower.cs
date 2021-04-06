using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinder.tiles;
using ItemSystem;

namespace Towers
{
    public class Tower : MonoBehaviour
    {

        public Item recievableItem; // acts as a filter, only allowing items to be placed in the tower if they match this item
        public Inventory inv;


        /// <summary>
        /// Initialise the inventory, set the towers position in tilespace and make the tile non-navicable
        /// </summary>
        /// <param name="t"></param>
        public virtual void OnPlaced(Tile t)
        {
            inv = new Inventory();

            t.MakeNonNavicable();
        }
    }
}
