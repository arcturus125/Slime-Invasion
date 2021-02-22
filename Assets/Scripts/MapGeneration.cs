using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinder.tiles;

namespace HordeSurvivalGame
{
    public class MapGeneration : MonoBehaviour
    {
        public GameObject prefab;
        public GameObject map;
        public GameObject Enemy;
        public GameObject Player;



        // Start is called before the first frame update
        void Awake()
        {
            for (int x = 1; x < Tile.MapSize; x++)
            {
                for (int y = 1; y < Tile.MapSize; y++)
                {
                    GameObject tileObject = Instantiate(prefab, new Vector3(x, 0,y), Quaternion.identity, map.transform);
                    Tile.tileMap[x, y] = new Tile(x,y, tileObject);
                }
            }

        }
    }
}
