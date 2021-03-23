using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Pathfinder.tiles;

public class DragHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
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
            Vector3 clickInWorldspace = hit.collider.gameObject.transform.position;
            Vector3 clickInTilespace = new Vector3(Mathf.RoundToInt(clickInWorldspace.x), 0.0f, Mathf.RoundToInt(clickInWorldspace.z));

            Tower.transform.position = clickInTilespace;
            if( Tile.Vector3ToTile(clickInTilespace).isWalkable)
            {
                Renderer[] rend = Tower.GetComponentsInChildren<Renderer>();
                foreach (Renderer r in rend)
                    r.material.SetColor("_BaseColor", Color.green);
            }
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
        Renderer[] rend = Tower.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rend)
        {
            r.material.SetColor("_BaseColor", Color.white);
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
