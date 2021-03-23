using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HordeSurvivalGame
{
    public class ProjectileShooting : MonoBehaviour
    {
        public float projectileSpeed = 1;
        public Transform playerPosition;
        public Projectile proj;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0)) //Semi auto with no cooldown as of the moment.
            {
                // raycast
                Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit hit;
                Vector3 pointClicked = Vector3.zero;
                if (Physics.Raycast(cursorPosition, Camera.main.transform.forward, out hit))
                {
                    pointClicked = hit.transform.position;
                }
                Vector3 velocity = pointClicked - playerPosition.position;



                Projectile test = Instantiate(proj, playerPosition.position, Quaternion.identity);
                test.INIT(velocity.normalized, projectileSpeed);

            }
        }
    }
}
