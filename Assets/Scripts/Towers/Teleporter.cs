using System.Collections;
using System.Collections.Generic;
using Towers;
using UnityEngine;

namespace HordeSurvivalGame
{
    public class Teleporter : Tower
    {
        public TeleportButton prefab;

        public float teleportAcceptanceRadius = 1;
        public float teleportRange = 25;

        private List<GameObject> arrows = new List<GameObject>();

        // Update is called once per frame
        void Update()
        {
            foreach (GameObject a in arrows)
                Destroy(a);
            arrows.Clear();
            // if the player is close enough
            if (Vector3.Distance(Player.playerTransform.position, transform.position) < teleportAcceptanceRadius)
            {
                // get all teleporters in range
                Collider[] colls = Physics.OverlapSphere(transform.position, teleportRange);
                foreach (Collider c in colls)
                {
                    if (c.gameObject.TryGetComponent(out Teleporter t))
                    {
                        if (t != this)
                        {
                            TeleportButton arrow = Instantiate(prefab, this.transform.position, Quaternion.identity, this.transform);
                            arrow.gameObject.transform.LookAt(t.transform.position);
                            arrow.teleportDestinationPosition = t.transform.position;
                            arrows.Add(arrow.gameObject);
                        }
                    }
                }
            }
        }
    }
}
