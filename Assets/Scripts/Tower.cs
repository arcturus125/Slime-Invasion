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


        public void TowerSetup(Tile t)
        {
            inv = new Inventory();
            x = t.x;
            y = t.y;

            t.MakeNonNavicable();
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
