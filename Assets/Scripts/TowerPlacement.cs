﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinder.tiles;

namespace HordeSurvivalGame
{
    public class TowerPlacement : MonoBehaviour
    {
        [SerializeField]
        private Mine mine;
        [SerializeField]
        private GameObject templateTower;

        [SerializeField]
        private Transform map;
        public Item test;


        // Update is called once per frame
        void Update()
        {
            RaycastCursor();
        }

        //when the player clicks, raycast from the location of their cursor and place a building on the tile they clicked
        // TODO: make sure they click a tile 
        // TODO: make sure a tile is empty before placing anything
        private void RaycastCursor()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Vector3 clickInWorldspace = hit.collider.gameObject.transform.position;
                    Vector3 clickInTilespace = new Vector3(Mathf.RoundToInt(clickInWorldspace.x), 0.5f, Mathf.RoundToInt(clickInWorldspace.z));

                    OreTile ore = new OreTile(Tile.Vector3ToTile(clickInTilespace), test);
                    Mine mine2 = Instantiate(mine, clickInTilespace, Quaternion.identity, map);
                    mine2.Setup(ore);
                    
                }

            }
            else if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Vector3 clickInWorldspace = hit.collider.gameObject.transform.position;


                    Vector3 clickInTilespace = new Vector3(Mathf.RoundToInt(clickInWorldspace.x), 0.25f, Mathf.RoundToInt(clickInWorldspace.z));
                    Instantiate(templateTower, clickInTilespace, Quaternion.identity, map);

                    Tile.Vector3ToTile(clickInTilespace).MakeNonNavicable();
                }

            }
        }
    }
}
