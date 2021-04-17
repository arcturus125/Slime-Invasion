using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Conveyors;
using Towers;

namespace HordeSurvivalGame
{
    public class ProjectileShooting : MonoBehaviour
    {
        public Transform playerPosition;
        public Projectile proj;

        float projectileSpeed = 3;
        int projectileDamage = 1;

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
                    // if conveyor clicked
                    if (hit.collider.gameObject.TryGetComponent(out ConveyorManager conv))
                    {
                        ConveyorManagerUI.DestroyWindow();
                        ConveyorManagerUI.selectedConveyor = conv;
                    }
                    else ConveyorManagerUI.selectedConveyor = null;

                    // if tower clicked
                    if (hit.collider.gameObject.TryGetComponent(out Tower t))
                    {
                        TowerConfigManager.selectedTower = t;
                    }
                    else TowerConfigManager.selectedTower = null;


                    // if teleport button clicked
                    if (hit.collider.gameObject.TryGetComponent(out TeleportButton tb))
                    {
                        Debug.Log("clicked teleport arrow");
                        tb.Teleport();
                        return; // when the player click a teleport button, don't shoot
                    }
                }

                Vector3 velocity = pointClicked - playerPosition.position;
                Vector3 finalVelocity = new Vector3(velocity.x, 0.0f, velocity.z);// removes y component so projectiles shoot perfectly flat - eliminates z fighting on peojectiles



                Projectile test = Instantiate(proj, playerPosition.position, Quaternion.identity);
                test.INIT(finalVelocity.normalized, projectileSpeed, projectileDamage);

            }
        }
    }
}
