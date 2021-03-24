using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
                //if the user clicks the UI, do not shoot
                if (EventSystem.current.IsPointerOverGameObject()) return;

                // raycast
                Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit hit;
                Vector3 pointClicked = Vector3.zero;
                if (Physics.Raycast(cursorPosition, Camera.main.transform.forward, out hit))
                {
                    pointClicked = hit.transform.position;
                }
                Vector3 velocity = pointClicked - playerPosition.position;
                Vector3 finalVelocity = new Vector3(velocity.x, 0.0f, velocity.z);



                Projectile test = Instantiate(proj, playerPosition.position, Quaternion.identity);
                test.INIT(finalVelocity.normalized, projectileSpeed);

            }
        }
    }
}
