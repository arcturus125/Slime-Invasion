using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Pathfinder.tiles;

public class TowerPlacement : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    GameObject Tower;
    public GameObject prefab;



    public void OnBeginDrag(PointerEventData eventData)
    {
        Tower = Instantiate(prefab, Vector3.zero, Quaternion.identity);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // convert cursor position into worldspace and tilespace
            Vector3 clickInWorldspace = hit.collider.gameObject.transform.position;
            Vector3 clickInTilespace = new Vector3(Mathf.RoundToInt(clickInWorldspace.x), 0.0f, Mathf.RoundToInt(clickInWorldspace.z));
            
            // postion the tower under the cursor
            Tower.transform.position = clickInTilespace;

            // if tower is walkable, change model colour to green
            if( Tile.Vector3ToTile(clickInTilespace).isWalkable)
            {
                Renderer[] rend = Tower.GetComponentsInChildren<Renderer>();
                foreach (Renderer r in rend)
                    r.material.SetColor("_BaseColor", Color.green);
            }
            // if tower is NOT walkable, change model colour to red
            else
            {
                Renderer[] rend = Tower.GetComponentsInChildren<Renderer>();
                foreach (Renderer r in rend)
                    r.material.SetColor("_BaseColor", Color.red);
            }


        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // set object to white (default colours) when it is dropped
        Renderer[] rend = Tower.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rend)
            r.material.SetColor("_BaseColor", Color.white);

        // once the tower is placed, set the towerObject of the tile (used to decide whether or not towers can connect)
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 clickInWorldspace = hit.collider.gameObject.transform.position;
            Vector3 clickInTilespace = new Vector3(Mathf.RoundToInt(clickInWorldspace.x), 0.0f, Mathf.RoundToInt(clickInWorldspace.z)); // this line is effectively the same as the one above, but when you remove it, conveyors break about 15% of the time.
            Tile.Vector3ToTile(clickInTilespace).SetTower(Tower);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
