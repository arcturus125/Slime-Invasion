using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinder.tiles;
using ItemSystem;

namespace HordeSurvivalGame
{
    public class OreTile : Tile
    {
        public Item resource;

        public OreTile(Tile t, Item drops) : base()
        {
            x = t.x;
            y = t.y;
            tileObject = t.tileObject;
            towerObject = t.towerObject;
            isWalkable = t.isWalkable;
            vector = t.vector;
            isPlaceable = t.isPlaceable;

        resource = drops;
            Tile.tileMap[t.x, t.y] = this;
        }
    }
}
