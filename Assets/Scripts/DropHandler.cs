using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using HordeSurvivalGame;
using Pathfinder.tiles;

public class DropHandler : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private Mine mine;
    [SerializeField]
    private GameObject templateTower;

    [SerializeField]
    private Transform map;
    public Item test;

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("Drag and drop");
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //if (Physics.Raycast(ray, out RaycastHit hit))
        //{
        //    Vector3 clickInWorldspace = hit.collider.gameObject.transform.position;
        //    Vector3 clickInTilespace = new Vector3(Mathf.RoundToInt(clickInWorldspace.x), 0.5f, Mathf.RoundToInt(clickInWorldspace.z));

        //    OreTile ore = new OreTile(Tile.Vector3ToTile(clickInTilespace), test);
        //    Mine mine2 = Instantiate(mine, clickInTilespace, Quaternion.identity, map);
        //    mine2.Setup(ore);

        //}
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
