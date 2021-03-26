using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinder.tiles;
using ItemSystem;

namespace HordeSurvivalGame
{
    public class OreSpawning : MonoBehaviour
    {
    
        public Item itemToSpawn;
        public float originX = 85.0f;
        public float originZ = 50.0f;
        public float distance;
        public int clusterSize;
        public int veinSize;

        // Start is called before the first frame update
        void Start()
        {
            Vector3 origin = new Vector3(originX, 0.0f, originZ);
            for (int i = 0; i < clusterSize; i++)
            {
                SpawnOreVein(origin, distance);
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        void SpawnOreVein(Vector3 origin, float inputRange)
        {
            float epoch = Random.Range(0, 360);
            float range = Random.Range(0.0f, inputRange);

            float radians = epoch * Mathf.Deg2Rad;
            float x = Mathf.Cos(radians) * range;
            float y = Mathf.Sin(radians) * range;

            Vector3 pos = new Vector3(x, 0, y) + origin;
            Tile.Vector3ToTile(pos);
            Debug.Log("Ore spawned at " + pos);
            new OreTile(Tile.Vector3ToTile(pos), itemToSpawn);

            Vector3 currentOrePos = pos;
            for (int i = 0; i < veinSize; i++)
            {
                float adjacent = Random.Range(1, 5);
                if (adjacent == 1)
                {
                    currentOrePos.z -= 1;
                }
                else if (adjacent == 2)
                {
                    currentOrePos.x -= 1;
                }
                else if (adjacent == 3)
                {
                    currentOrePos.z += 1;
                }
                else if (adjacent == 4 || adjacent == 5)
                {
                    currentOrePos.x += 1;
                }
                Tile.Vector3ToTile(currentOrePos);
                Debug.Log("Ore spawned at " + currentOrePos);
                new OreTile(Tile.Vector3ToTile(currentOrePos), itemToSpawn);
            }
        }
    }

}

