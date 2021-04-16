using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


using Pathfinder.tiles;
using Towers;

namespace HordeSurvivalGame
{
    public class TowerPlacement : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        GameObject Tower;
        public GameObject prefab;
        public int moneyCost = 0;
        public int ironCost = 0;
        bool readyToPlace = false;
        bool allowPlacement = false;
        bool developerMode = false; //Costs for towers are ignored, to be able to test features, without removing the cost entirely.


        public void OnBeginDrag(PointerEventData eventData)
        {
            readyToPlace = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            //dont do anything until the player has dragged their cursor onto the map - do not place towers through the UI
            if (EventSystem.current.IsPointerOverGameObject())
            {
                if (Tower)               //
                {                        // player can cancel placing a tower by dragging it back onto the UI
                    Destroy(Tower);      //
                    readyToPlace = true; //
                }                        //
                return;
            }
            // on the first frame that the player has dragged their cursor onto the map, create the object
            if (readyToPlace)
            {
                Tower = Instantiate(prefab, Vector3.zero, Quaternion.identity);
                readyToPlace = false;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // convert cursor position into worldspace and tilespace
                Vector3 clickInWorldspace = hit.collider.gameObject.transform.position;
                Vector3 clickInTilespace = new Vector3(Mathf.RoundToInt(clickInWorldspace.x), 0.0f, Mathf.RoundToInt(clickInWorldspace.z));

                // postion the tower under the cursor
                Tower.transform.position = clickInTilespace;

                allowPlacement = false;
                // if tower is walkable, change model colour to green
                if ((Tile.Vector3ToTile(clickInTilespace).isWalkable))
                {
                    if (developerMode) //If it is on, it just places without checking.
                    {
                        Renderer[] rend = Tower.GetComponentsInChildren<Renderer>();
                        foreach (Renderer r in rend) r.material.SetColor("_BaseColor", Color.green);
                        allowPlacement = true;
                    }
                    else if (!developerMode && (PlayerResources.GetMoney() >= moneyCost) && (PlayerResources.GetIron() >= ironCost)) //Dev mode is not on, player needs to pay.
                    {
                        Renderer[] rend = Tower.GetComponentsInChildren<Renderer>();
                        foreach (Renderer r in rend) r.material.SetColor("_BaseColor", Color.green);
                        allowPlacement = true;
                    }
                    else
                    {

                        Renderer[] rend = Tower.GetComponentsInChildren<Renderer>();
                        foreach (Renderer r in rend) r.material.SetColor("_BaseColor", Color.red);
                    }
                    
                    
                }
                // if tower is NOT walkable, change model colour to red
                else
                {
                    Renderer[] rend = Tower.GetComponentsInChildren<Renderer>();
                    foreach (Renderer r in rend) r.material.SetColor("_BaseColor", Color.red);
                    allowPlacement = false;
                }

                if(Tile.Vector3ToTile(clickInTilespace).towerObject != null)
                {
                    Renderer[] rend = Tower.GetComponentsInChildren<Renderer>();
                    foreach (Renderer r in rend) r.material.SetColor("_BaseColor", Color.red);
                    allowPlacement = false;

                    Debug.LogWarning("Player attempted to place a tower ontop of another tower. cancelling placement");
                }


            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (Tower && allowPlacement)
            {
                // set object to white (default colours) when it is dropped
                Renderer[] rend = Tower.GetComponentsInChildren<Renderer>();
                foreach (Renderer r in rend) r.material.SetColor("_BaseColor", Color.white);

                // once the tower is placed, set the towerObject of the tile (used to decide whether or not towers can connect)
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Vector3 clickInWorldspace = hit.collider.gameObject.transform.position;
                    Vector3 clickInTilespace = new Vector3(Mathf.RoundToInt(clickInWorldspace.x), 0.0f, Mathf.RoundToInt(clickInWorldspace.z)); // this line is effectively the same as the one above, but when you remove it, conveyors break about 15% of the time.

                    Tower.transform.position = clickInTilespace;
                    Tile t = Tile.Vector3ToTile(clickInTilespace);
                    t.SetTower(Tower);
                    if (Tower.TryGetComponent<Tower>(out Tower towerClassInstance))
                    {
                        towerClassInstance.OnPlaced(t);
                        //t.isWalkable = false; // tile can no longer be walked on since there has been a tower placed on it
                    }
                    if (Tower.TryGetComponent<Conveyors.ConveyorManager>(out Conveyors.ConveyorManager conveyorClassInstance))
                    {
                        conveyorClassInstance.OnPlaced();
                    }


                    if (!developerMode)
                    {
                        PlayerResources.DecrementMoney(moneyCost);
                        PlayerResources.DecrementIron(ironCost);
                    }
                }
                Tower = null;


            }
            if (Tower && !allowPlacement) Destroy(Tower);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P)) //Enter dev mode.
            {
                developerMode = !developerMode;
                Debug.LogWarning("Dev mode is "+ developerMode);
            }
        }
    }
}
