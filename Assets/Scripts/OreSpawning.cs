using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinder.tiles;
using ItemSystem;
using MapGeneration;

namespace HordeSurvivalGame
{
    public class OreSpawning : MonoBehaviour
    {
        [SerializeField]
        private Transform parent;
        [SerializeField]
        private Sprite map; //The walkable map, which will be used to make sure the player can access all of the ores.
        [SerializeField]
        private GameObject prefab; //The prefab of the ore texture.
        [SerializeField]
        private Item itemToSpawn; //Item that will be spawned when mining the ore.
        [SerializeField]
        private float originX; //The X and Z position the clusters will originate from.
        [SerializeField]
        private float originZ;
        [Range(3.0f, 10.0f)]
        public float distance; //How far the spawn circle will be from the origin.
        [Range(1.0f, 10.0f)]
        public int clusterSize; //How many veins are going to be spawned.
        [Range(3.0f, 8.0f)]
        public int veinSize; //The size of an individual vein.

        // Start is called before the first frame update
        void Start()
        {
            Vector3 origin = new Vector3(originX, 0.0f, originZ); //Defines origin point from inspector inputted variables.
            for (int i = 0; i < clusterSize; i++)
            {
                SpawnOreVein(origin, distance); //Spawns the veins, repeated for how many are in the cluster.
            }
        }

        void SpawnOreVein(Vector3 origin, float inputRange) //Spawns a vein of ore.
        {
            float epoch = Random.Range(0, 360); //Direction the spawn point will go from the origin.
            float range = Random.Range(0.0f, inputRange); //How far in the secected direction the spawn point will go.

            float radians = epoch * Mathf.Deg2Rad; //Calculates radians.
            float x = Mathf.Cos(radians) * range; //The X and Y points.
            float y = Mathf.Sin(radians) * range;

            Vector3 pos = new Vector3((int)x, 0.1f, (int)y) + origin; //X and Y put into a Vector.
            Tile.Vector3ToTile(pos);

            if (ValidTile(pos))
            {
                Instantiate(prefab, pos, Quaternion.identity, parent); //Uses the prefab.
                new OreTile(Tile.Vector3ToTile(pos), itemToSpawn); //Sets tile to spawn the related ore.
            }

            Vector3 currentOrePos = pos;
            for (int i = 0; i < veinSize; i++) //Chooses an adjacent tile.
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

                if (ValidTile(currentOrePos))
                {
                    Instantiate(prefab, currentOrePos, Quaternion.identity, parent); //Changes the tile to an ore, like it did with the start of the vein.
                    new OreTile(Tile.Vector3ToTile(currentOrePos), itemToSpawn);
                }
            }
        }

        private bool ValidTile(Vector3 position) //Checks if the tile is in an accesible area.
        {
            if (map.texture.GetPixel((int)-position.x, (int)-position.z) != Color.black) //If the colour of the pixel is black, the tile is invalid, and will retutn false.
            {
                return true;
            }
            else return false;
        }
    }

}

