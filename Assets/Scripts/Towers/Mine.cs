using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Pathfinder.tiles;
using ItemSystem;
using HordeSurvivalGame;

namespace Towers
{
    public class Mine : Tower
    {
        float miningSpeed = 1;
        int dropSize = 1;
        Item resource = null;

        private float timeSinceLastDrop = 0;
        // Start is called before the first frame update
        void Start()
        {
            // TODO: call TowerSetup() on parent
        }

        public void Setup()
        {
            
        }
        public override void OnPlaced(Tile t)
        {
            base.OnPlaced(t);
            if (Tile.tileMap[x, y] is OreTile)
            {
                Debug.Log("tower placed on ore at" + Tile.TileToVector3(Tile.tileMap[x, y]));
                OreTile ore = Tile.tileMap[x, y] as OreTile;  // super janky way of doing this, will revisit later
                resource = ore.resource;
            }
            else
                Debug.Log("tower NOT placed on ore" + Tile.TileToVector3(Tile.tileMap[x, y]));

        }
            
        // Update is called once per frame
        void Update()
        {
            if (resource) // only drop if the mine is places ontop of a resource
            {
                timeSinceLastDrop += Time.deltaTime;
                if (timeSinceLastDrop >= miningSpeed)
                {
                    timeSinceLastDrop -= miningSpeed;
                    Drop();
                }
            }
        }

        private void Drop()
        {
            inv.addItem(resource, dropSize);
            //Debug.Log("Mine inv: " + inv.quantity[0]+"- "+ inv.items[0].itemName);
        }
    }
}
