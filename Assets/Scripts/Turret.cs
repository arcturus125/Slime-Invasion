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
        public float fireRate = 1.0f; // the time, in seconds, between each shot

        public Enemy targetEnemy; // private
        public Projectile projectilePrefab;


        float timer = 0;

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
                    head.transform.LookAt( new Vector3( 
                        targetEnemy.gameObject.transform.position.x,
                        head.transform.position.y,
                        targetEnemy.gameObject.transform.position.z));
                    if(timer <= 0)
                    {
                        // shoot

                        Projectile test = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                        test.INIT(head.transform.forward);

                        timer = fireRate;
                    }
                    else
                    {
                        timer -= Time.deltaTime;
                    }
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
                else Debug.Log(" no head");
            }
            else Debug.Log(" not set to look at enemy");
        }
    }
}
