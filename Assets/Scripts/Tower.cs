using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinder.tiles;

namespace HordeSurvivalGame
{
    public class Tower : MonoBehaviour
    {
        public int x;
        public int y;

        public bool canRecieveItems = false;


        public Tower(Tile t)
        {
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
