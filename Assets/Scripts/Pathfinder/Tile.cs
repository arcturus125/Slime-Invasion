using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Pathfinder.tiles
{
    public class Tile
    {
        // static settings
        public static int MapSize = 250;
        public static Tile[,] tileMap = new Tile[MapSize, MapSize];


        //     ###############  each tile has a G H and F Cost
        //     #  g       h  #   gCost = distance from the start of the pathfinding
        //     #             #   hCost = distance from the end of Pathfinding
        //     #      f      #   fCost = gCost + hCost
        //     #             #
        //     ###############  each tile has a ParentTile; the previous tile used to get to this one with the shortest distance
        //                           (this may changea as shorter paths to the current tile are found)
        //      
        //      Starting Tile = the tile the entity was on when it began pathfinding
        //      Destination tile = the tile the enetity is trying to pathfind to
        //      if you take the Destination tile, and follow the subsequent parent tiles (like a linked list)
        //      it will lead you the shortest path to the starting Tile




        // attributes assigned on creation
        public int x;
        public int y;
        public GameObject tileObject;
        public bool isWalkable = true;

        // attributes assigned per each entity pathfinding
        private float gCost = -1; // distance from the start point
        private float hCost = -1; // distance from the end point
        public float fCost = 99999; // gCost + hCost
        public Tile parentTile;

        public Tile() { }//used as a dummy constructor

        // instantiates a tile and attaches a gameobject to it
        public Tile(int xCoOrd, int yCoOrd, GameObject tileGameObject)
        {
            x = xCoOrd;
            y = yCoOrd;
            tileObject = tileGameObject;
        }
        public Tile(Tile t)
        {
            x = t.x;
            y = t.x;
            tileObject = t.tileObject;
        }

        // updates the G and H values to calculate a new F value
        public void UpdateValues( Tile startingTile, Tile destinationTile, Tile sourceTile)
        {
            // calculate gCost
            gCost = (float)Math.Sqrt(Math.Pow(startingTile.x - this.x, 2) + Math.Pow(startingTile.y - this.y, 2));
            // calculate hCost
            hCost = (float)Math.Sqrt(Math.Pow(destinationTile.x - this.x, 2) + Math.Pow(destinationTile.y - this.y, 2));

            fCost = gCost + hCost;
            parentTile = sourceTile;
        }
        // used to check the Fcost without changing it
        public float CheckFCost(Tile startingTile, Tile destinationTile)
        {
            // calculate gCost
            gCost = (float)Math.Sqrt(Math.Pow(startingTile.x - this.x, 2) + Math.Pow(startingTile.y - this.y, 2));
            // calculate hCost
            hCost = (float)Math.Sqrt(Math.Pow(destinationTile.x - this.x, 2) + Math.Pow(destinationTile.y - this.y, 2));
            return gCost + hCost;
        }
        // used to make tiles that can not be used for pathfinding
        public void MakeNonNavicable()
        {
            isWalkable = false;
        }

        // convert worldspace into a reference to a tile class
        public static Tile Vector3ToTile(Vector3 vector)
        {
            return tileMap[(int)-vector.x, (int)-vector.z];
        }
        public static Vector3 TileToVector3(Tile t)
        {
            return new Vector3(-t.x + 0.0f, 0, -t.y+ 0.0f);
        }
        // checks any indexers before they create out of bounds errors
        public static bool IsOnMap(int x, int y)
        {
            if(x < 1 || y < 1)
                return false;
            if (x > MapSize || y > MapSize)
                return false;

            return true;

        }
    }
}
