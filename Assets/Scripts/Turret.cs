using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HordeSurvivalGame
{
    public class Turret : MonoBehaviour
    {
        public bool LookAtEnemy = true;
        public GameObject head; //private
        public float range = 10;

        public Enemy targetEnemy; // private


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (targetEnemy == null)
            {
                LookForEnemies();
                Debug.Log("Looking");
            }
            else
            {
                if (LookAtEnemy)
                {

                    Debug.Log("shooting");
                    head.transform.LookAt(targetEnemy.gameObject.transform);
                }
            }
        }

        private void LookForEnemies()
        {
            if (LookAtEnemy)
            {
                if (head)
                {
                    // get everything in range of the tower
                    Collider[] allCollidersInRange = Physics.OverlapSphere(transform.position, range);
                    foreach (Collider colliderInRange in allCollidersInRange)
                    {
                        if (colliderInRange.gameObject.tag == "Enemy")
                        {
                            Debug.Log("found enemy");
                            targetEnemy = colliderInRange.gameObject.GetComponent<Enemy>();
                        }
                    }
                }
            }
        }
    }
}
