using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinder.tiles;
using ItemSystem;
using HordeSurvivalGame;

namespace Towers
{
    public class Tower : MonoBehaviour
    {
        public int x; 
        public int y;

        public int moneyCost;
        public int ironCost;

        public Item recievableItem; // acts as a filter, only allowing items to be placed in the tower if they match this item
        public Inventory inv;
        private const float DEFAULT_SPEED_MULTIPLIER = 1.0f;
        public float speedMultiplier = DEFAULT_SPEED_MULTIPLIER;
        public int powerNeedToSpeedUp = 0;

        public static float refundPercent = 0.85f;
         


        /// <summary>
        /// Initialise the inventory, set the towers position in tilespace and make the tile non-navicable
        /// </summary>
        /// <param name="t"></param>
        public virtual void OnPlaced(Tile t, int pMoneyCost, int pIronCost)
        {
            inv = new Inventory();
            x = t.x;
            y = t.y;
            moneyCost = pMoneyCost;
            ironCost = pIronCost;

            t.MakeNonNavicable();
        }

        public void SetSpeedMultiplier(float multiplier)
        {
            speedMultiplier = multiplier;
        }


        private void OnDestroy()
        {
            Tile t = Tile.Vector3ToTile(transform.position);
            t.tileObject = null;
            t.isWalkable = true;

            PlayerResources.AddMoney((int)(moneyCost *refundPercent));
            PlayerResources.AddIron((int)(ironCost * refundPercent));
            for(int i= 0; i < inv.items.Count;i++)
            {
                PlayerResources.UpdateInventory(inv.items[i], inv.quantity[i]);
            }

        }
    }
}
