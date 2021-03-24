using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinder.tiles;
using System;

namespace HordeSurvivalGame
{
    public class Mine : Tower
    {
        float miningSpeed = 1;
        int dropSize = 1;
        Item resource = null;

        private float timeSinceLastDrop = 0;
        //public Mine(Tile t, float minerSpeed) : base(t)
        //{
        //    miningSpeed = minerSpeed;
        //}
        //// dummy constructor to test the system before we implemented the ore generation
        //public Mine(Tile t, float minerSpeed, Item test) : base(t)
        //{
        //    miningSpeed = minerSpeed;
        //    ore = new OreTile(t, test);
        //    resource = ore.resource;
        //}
        // Start is called before the first frame update
        void Start()
        {

        }

        public void Setup(OreTile ore)
        {
            TowerSetup(ore);
            resource = ore.resource;
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
            Debug.Log("Mine inv: " + inv.quantity[0]+"- "+ inv.items[0].itemName);
        }
    }
}
