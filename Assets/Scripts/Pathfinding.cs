using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HordeSurvivalGame
{
    class Pathfinding
    {
        public List<Tile> OpenTiles = new List<Tile>();
        public List<Tile> closedTiles = new List<Tile>();

        public static int count = 0;

        public void RunPathfinding(Transform thisObject, Transform targetObject)
        {
            Tile startTile =  Tile.Vector3ToTile(thisObject.position);
            Tile destinationTile = Tile.Vector3ToTile(targetObject.position);


            SearchAdjacentTiles(startTile, destinationTile, startTile);

        }


        private void SearchAdjacentTiles(Tile startTile, Tile destinationTile, Tile CurrentTile)
        {
            Debug.Log("Calculating best adjacent tile from :" + CurrentTile.x + "," + CurrentTile.y);

            if (CurrentTile == destinationTile)
            {
                // win
                DrawPath(CurrentTile, startTile);
            }
            else
            {
                CloseTile(CurrentTile);


                // ###
                // for each neighbour
                // ###
                for (int xOffset = -1; xOffset <= 1; xOffset++){
                for (int yOffset = -1; yOffset <= 1; yOffset++)
                {
                    if (!(xOffset == 0 && yOffset == 0)) // dont check the tile you are already on
                    {
                        Tile neighbour = Tile.tileMap[CurrentTile.x + xOffset, CurrentTile.y + yOffset];
                        if (!closedTiles.Contains(neighbour))// dont check a closed tile
                        {
                            if (Tile.IsOnMap(CurrentTile.x + xOffset, CurrentTile.y + yOffset)) // check if tile is on the map (within bounds of array)
                            {
                                if (neighbour.isWalkable)//  dont check tiles that aren't walkable
                                {
                                    // only update the f cost if it is less than  it was before
                                    if ((neighbour.CheckFCost(startTile, destinationTile) < neighbour.fCost) || !OpenTiles.Contains(neighbour))
                                    {
                                        neighbour.UpdateValues(startTile, destinationTile, Tile.tileMap[CurrentTile.x, CurrentTile.y]);
                                        OpenTile(Tile.tileMap[CurrentTile.x + xOffset, CurrentTile.y + yOffset]);
                                    }
                                }
                            }
                        }
                    }
                }
            }

                // search through all open tiles and find the best one

                Tile bestTile = new Tile();
                float bestFCost = 99999.0f;
                foreach (Tile t in OpenTiles)
                {
                    //Debug.Log("searching: "+t.fCost);
                    if (t.fCost < bestFCost)
                    {
                        bestTile = t;
                        bestFCost = t.fCost;
                    }
                }

                // run this function on the best open tile
                if (count < 10000)
                {
                    count++;
                    SearchAdjacentTiles(startTile, destinationTile, bestTile);
                }
                else
                {
                    Debug.LogError("Pathfinding failed");
                }
            }
        }
        private void DrawPath(Tile current, Tile end)
        {
            if (current != end)
            {
                Material mat = new Material(Shader.Find("Specular"));
                mat.color = Color.cyan;
                current.tileObject.GetComponent<MeshRenderer>().material = mat;
                DrawPath(current.parentTile, end);
            }
        }
        private void CloseTile(Tile t)
        {
            Material mat = new Material(Shader.Find("Specular"));
            mat.color = Color.red;

            OpenTiles.Remove(t);
            closedTiles.Add(t);

            t.tileObject.GetComponent<MeshRenderer>().material = mat;
        }
        private void OpenTile(Tile t)
        {
            Material mat = new Material(Shader.Find("Specular"));
            mat.color = Color.green;
            OpenTiles.Add(t);
            t.tileObject.GetComponent<MeshRenderer>().material = mat;
        }
    }
}
