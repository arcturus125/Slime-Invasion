using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HordeSurvivalGame
{
    public class MapGeneration : MonoBehaviour
    {
        public GameObject prefab;
        public GameObject map;
        public GameObject Enemy;
        public GameObject Player;
        private int length = 49;
        private int height = 49;



        // Start is called before the first frame update
        void Start()
        {
            for (int x = 1; x <= length; x++)
            {
                for (int y = 1; y <= height; y++)
                {
                    GameObject tileObject = Instantiate(prefab, new Vector3(x, 0,y), Quaternion.identity, map.transform);
                    Tile.tileMap[x, y] = new Tile(x,y, tileObject);
                }
            }

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Pathfinding p = new Pathfinding();
                p.RunPathfinding(Enemy.transform, Player.transform);
            }
        }
    }
}
class tile
{

}
