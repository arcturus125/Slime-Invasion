using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinder.tiles;

namespace HordeSurvivalGame
{
    public class OreTile : Tile
    {
        public Item resource;

        public OreTile(Tile t, Item drops) : base(t)
        {
            resource = drops;
            Tile.tileMap[t.x, t.y] = this;
        }
    }
}
