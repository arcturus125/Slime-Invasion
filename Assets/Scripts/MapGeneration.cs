using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinder.tiles;
using UnityEditor;

namespace HordeSurvivalGame
{
    public class MapGeneration : MonoBehaviour
    {
        [SerializeField]
        private Sprite map;

        [SerializeField]
        private Color[] colours;
        [SerializeField]
        private GameObject[] prefabs;

        int height = 10;
        int width = 10;

        private void Awake()
        {
            Debug.Log("### map generation started");
            height = map.texture.height;
            width = map.texture.width;

            for(int i = 0; i < colours.Length; i++)
            {
                Color c = colours[i];
                for(int y = 0; y < height; y++)
                { 
                    for(int x = 0; x<width;x++)
                    {
                        //Debug.Log("rendering tile " + x + "," + y);
                        if (CompareColours(map.texture.GetPixel(x,y),c))
                        {
                            createTile(x, y, prefabs[i]);
                        }
                    }
                }
            }
            Debug.Log("### Map Generation Finished");
        }

        bool CompareColours(Color a, Color b)
        {
            //Debug.Log(a.r + "," + a.g + "," + a.b + " and " + b.r + "," + b.g + "," + b.b);
            if (Mathf.RoundToInt(a.r*255) == Mathf.RoundToInt(b.r * 255))
            {
                //Debug.Log(" red match");
                if (Mathf.RoundToInt(a.g * 255) == Mathf.RoundToInt(b.g * 255))
                {
                    //Debug.Log(" green match");
                    if (Mathf.RoundToInt(a.b * 255) == Mathf.RoundToInt(b.b * 255))
                    {
                        //Debug.Log(" blue match");
                        return true;
                    }
                }    
            }
            return false;
        }
        void createTile(int x, int y, GameObject prefab)
        {
            GameObject tileObject = Instantiate(prefab, new Vector3(-x, 0, -y), Quaternion.identity);
            Tile.tileMap[x, y] = new Tile(x,y, tileObject);
        }
    }
}
