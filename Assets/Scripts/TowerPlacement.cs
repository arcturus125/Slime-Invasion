using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HordeSurvivalGame
{
    public class TowerPlacement : MonoBehaviour
    {
        [SerializeField]
        private GameObject templateBuilding;
        [SerializeField]
        private Transform map;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 test = hit.collider.gameObject.transform.position;


                    Vector3 pos = new Vector3(Mathf.RoundToInt(test.x), 0.5f, Mathf.RoundToInt(test.z));
                    Instantiate(templateBuilding, pos, Quaternion.identity, map);

                    Tile.Vector3ToTile(pos).MakeNonNavicable();
                }
                
            }
        }
    }
}
