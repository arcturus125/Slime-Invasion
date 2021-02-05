using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HordeSurvivalGame
{
    class Tile
    {
        public static Tile[,] tileMap = new Tile[50,50];
        const float WidthOfTile = 1.0f;



        //attributes
        public int x;
        public int y;
        public GameObject tileObject;
        public bool isWalkable = true;

        public float gCost = -1; // distance from the start point
        public float hCost = -1; // distance from the end point
        public float fCost = 99999; // gCost + hCost
        public Tile parentTile;

        public Tile()//delete later
        {

        }
        public Tile(int xCoOrd, int yCoOrd, GameObject tileGameObject)
        {
            x = xCoOrd;
            y = yCoOrd;
            tileObject = tileGameObject;
        }

        public void UpdateValues( Tile startingTile, Tile destinationTile, Tile sourceTile)
        {
            // calculate gCost
            gCost = (float)Math.Sqrt(Math.Pow(startingTile.x - this.x, 2) + Math.Pow(startingTile.y - this.y, 2));
            // calculate hCost
            hCost = (float)Math.Sqrt(Math.Pow(destinationTile.x - this.x, 2) + Math.Pow(destinationTile.y - this.y, 2));

            fCost = gCost + hCost;
            parentTile = sourceTile;
            tileObject.GetComponentInChildren<TextMesh>().text = "" + Math.Round(fCost,2);
        }
        public float CheckFCost(Tile startingTile, Tile destinationTile)
        {
            // calculate gCost
            gCost = (float)Math.Sqrt(Math.Pow(startingTile.x - this.x, 2) + Math.Pow(startingTile.y - this.y, 2));
            // calculate hCost
            hCost = (float)Math.Sqrt(Math.Pow(destinationTile.x - this.x, 2) + Math.Pow(destinationTile.y - this.y, 2));
            return gCost + hCost;
        }
        public void MakeNonNavicable()
        {
            Material mat = new Material(Shader.Find("Specular"));
            mat.color = Color.black;
            tileObject.GetComponent<MeshRenderer>().material = mat;
            isWalkable = false;
        }

        public static Tile Vector3ToTile(Vector3 vector)
        {
            return tileMap[(int)vector.x, (int)vector.z];
        }
        public static Vector3 TileToVector3(Tile t)
        {
            return new Vector3(t.x + WidthOfTile/2, 0, t.y + WidthOfTile / 2);
        }
        public static bool IsOnMap(int x, int y)
        {
            if(x < 1 || y < 1)
                return false;
            if (x > 50 || y > 50)
                return false;

            return true;

        }
    }
}
