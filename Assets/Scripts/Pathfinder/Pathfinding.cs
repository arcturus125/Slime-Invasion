using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Pathfinder.tiles;

namespace Pathfinder
{
    class Pathfinding
    {
        internal List<Tile> OpenTiles = new List<Tile>(); // tiles you have yet to check 
        internal List<Tile> closedTiles = new List<Tile>(); // tiles that haev already been checked

        private List<Vector3> path = new List<Vector3>();

        public static int LoopCount = 1000;

        /// <summary>
        /// this function will use A* pathfinding to find an optimal path
        /// it will return a list of Vector3 to be used as waypoints
        /// </summary>
        /// <param name="thisObject"> the transform you want to pathfind from</param>
        /// <param name="targetObject"> the transform you want to pathfind to</param>
        public List<Vector3> FindPath(Transform thisObject, Transform targetObject)
        {
            path.Clear();
            Tile startTile =  Tile.Vector3ToTile(thisObject.position);
            Tile destinationTile = Tile.Vector3ToTile(targetObject.position);

            // if the enemy is already at their destination, return an empty list (otherwise out of bounds indexing error)
            if (startTile == destinationTile)
            {
                return null;
            }


            if(SearchAdjacentTiles(startTile, destinationTile, startTile)) 
            {
                path.Reverse();
                return path;
            }
            Debug.LogWarning("Pathfinder cannot find path");
            return null; // if path cannot be found, an empty list is returned as to not break iterative code dependant on this

        }


        private bool SearchAdjacentTiles(Tile startTile, Tile destinationTile, Tile CurrentTile)
        {
            for(int i = 0; i < LoopCount; i++)
            {
                // if a path is found
                if (CurrentTile == destinationTile)
                {
                    DrawPath(CurrentTile, startTile);
                    return true;
                }
                else
                {
                    CloseTile(CurrentTile);



                    // for each neighbour of the current tile
                    for (int xOffset = -1; xOffset <= 1; xOffset++)
                    {
                        for (int yOffset = -1; yOffset <= 1; yOffset++)
                        {
                            if (!(xOffset == 0 && yOffset == 0)) // dont pathfind the tile you are already on
                            {
                                Tile neighbour = Tile.tileMap[CurrentTile.x + xOffset, CurrentTile.y + yOffset];
                                if (!closedTiles.Contains(neighbour))// dont pathfind a closed tile
                                {
                                    // if ther are errors here. move this if statement to encapsulate the definition of "neighnour"
                                    if (Tile.IsOnMap(CurrentTile.x + xOffset, CurrentTile.y + yOffset)) // check if tile is on the map (within bounds of array)
                                    {
                                        if (neighbour.isWalkable)//  dont pathfind over tiles that aren't walkable
                                        {
                                            // only update the f cost if it is less than it was before
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

                    // search through all open tiles for the one with the lowest fCost

                    Tile bestTile = new Tile();
                    float bestFCost = 99999.0f;
                    if (OpenTiles.Count > 0)
                    {
                        foreach (Tile t in OpenTiles)
                        {
                            if (t.fCost < bestFCost)
                            {
                                bestTile = t;
                                bestFCost = t.fCost;
                            }
                        }
                    }
                    // if there are no open tiles left, the pathfinding cannot find a path
                    else
                    {
                        Debug.LogError("Pathfinder: no path is possible");
                        return false;
                    }

                    CurrentTile = bestTile;
                }
            }
            return false;
        }

        // using the linked list of Tiles (parent tiles) trace back the path to the start
        private void DrawPath(Tile current, Tile end)
        {
            if (current != end)
            {
                path.Add( Tile.TileToVector3(current));
                DrawPath(current.parentTile, end);
            }
        }

        private void CloseTile(Tile t)
        {
            OpenTiles.Remove(t);
            closedTiles.Add(t);
        }
        private void OpenTile(Tile t)
        {
            OpenTiles.Add(t);
        }
    }
}
