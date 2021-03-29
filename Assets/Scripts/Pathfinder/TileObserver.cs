using HordeSurvivalGame;
using ItemSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinder.tiles
{
    public class TileObserver : MonoBehaviour
    {
        [Header("Tile:")]
        public int x;
        public int y;


        public GameObject tileObject;
        public GameObject towerObject = null;
        public bool isWalkable = true;


        [Header("OreTile:")]
        public Item resource;

        // Start is called before the first frame update
        void Start()
        {

        }

        public void INIT(int xt ,int yt)
        {
            x = xt;
            y = yt;
        }

        // Update is called once per frame
        void Update()
        {
            Tile t = Tile.tileMap[x, y];
            if(t is OreTile)
            {
                OreTile o = t as OreTile; 
                tileObject = o.tileObject;
                towerObject = o.towerObject;
                isWalkable = o.isWalkable;
                resource = o.resource;
            }
            else
            {
                tileObject = t.tileObject;
                towerObject = t.towerObject;
                isWalkable = t.isWalkable;
            }
        }
    }
}
