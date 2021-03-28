using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinder.tiles;
using ItemSystem;

namespace HordeSurvivalGame
{
    public class Tower : MonoBehaviour
    {
        public int x; // delete if unused
        public int y; //

        public bool canRecieveItems = false;
        public Inventory inv;


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
    }
}
