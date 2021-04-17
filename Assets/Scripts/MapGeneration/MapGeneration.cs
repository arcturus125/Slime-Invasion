using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinder.tiles;
using UnityEditor;

namespace MapGeneration
{
    public class MapGeneration : MonoBehaviour
    {
        [Header("Debug Settings:")]
        public bool CreateTileObservers = true;

        enum ShadowMap
        {
            None = 0,
            edgeBottom = 1,
            edgeCliffTop = 2,
            edgeRight = 3,
            edgeLeft = 4,
            edgeTop = 5,
            edgeTopLeft = 6,
            edgeTopRight = 7,
            edgeBottomLeft_noShadow = 8,
            edgeBottomLeft_shadow = 9,
            edgeBottomRight_noShadow = 10,
            edgeBottomRight_shadow = 11,
            cube = 12
        };


        [Header("Sprites:")]
        [SerializeField]
        private Transform mapParentTransform;
        [SerializeField]
        private Sprite map;
        [SerializeField]
        private Sprite mapUnderlay;
        [SerializeField]
        private Sprite mapWalkable;

        [SerializeField]
        private GameObject tilePrefab;

        [Header("Primary textures")]
        [SerializeField]
        private Color[] colours;
        [SerializeField]
        private Texture[] textures;
        [SerializeField]
        private ShadowMap[] shadows;
        [SerializeField]
        private GameObject[] shadowPrefabs;
        [SerializeField]
        private GameObject shadowPrefab;


        [Header("Underlay textures")]
        [SerializeField]
        private Color blankUnderlayColour;

        [SerializeField]
        private Color placeableColour;

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
                        // if colours match, place tile corresponding that colour
                        if (CompareColours(map.texture.GetPixel(x,y),c))
                        {
                            createTile(x, y, textures[i],shadows[i]);
                        }
                    }
                }
            }

            Debug.Log("### Map Generation Finished");
        }

        static bool CompareColours(Color a, Color b)
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


        void createTile(int x, int y, Texture texture,ShadowMap shadow)
        {
            // creates the tile gameobject based from the prefab and positions it
            GameObject tileObject = Instantiate(tilePrefab, new Vector3(-x, 0, -y), Quaternion.identity, mapParentTransform);

            // sets the primary texture of each tile
            Renderer rend = tileObject.GetComponentsInChildren<Renderer>()[1];
            rend.material.SetTexture("_BaseColorMap", texture);

            //if necessary, sets the underlay texture of a tile
            if(!CompareColours( mapUnderlay.texture.GetPixel(x,y), blankUnderlayColour))
            {
                for(int i = 0; i < colours.Length;i++)
                {
                    Color c = colours[i];
                    if(CompareColours(c, mapUnderlay.texture.GetPixel(x, y)))
                    {
                        Renderer rend2 = tileObject.GetComponentsInChildren<Renderer>()[2];
                        rend2.material.SetTexture("_BaseColorMap", textures[i]);

                    }
                }
            }

            if(shadow != ShadowMap.None)
            {
                Instantiate(shadowPrefabs[(int)shadow], new Vector3(-x, 0, -y), Quaternion.identity, tileObject.transform);

            }




            // hooks the gameobject tile up to the tile system utilised by the pathfinding algorithm
            Tile.tileMap[x, y] = new Tile(x,y, tileObject);
            tileObject.AddComponent<TileObserver>().INIT(x,y);




            // is the tile walkable?
            if (CompareColours(mapWalkable.texture.GetPixel(x, y), Color.black))
            {
                Tile.tileMap[x, y].isWalkable = false;
            }

            // is the tile placeable?
            if (CompareColours(mapWalkable.texture.GetPixel(x, y),placeableColour))
            {
                Debug.Log("placeable");
                Tile.tileMap[x, y].isPlaceable = true;
            }
        }
    }
}
