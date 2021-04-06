using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Pathfinder;

namespace HordeSurvivalGame 
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private Slider healthBar;
        public int maxHealth = 5;
        public int remainingHealth;
        public float pathfindingleniancy = 0.5f;
        public float finalSpeed = 0.01f; // the speed of the enemy AFTER all the effects have been applied
        public float defaultSpeed = 0.01f;

        public static float defaultHealthBarTimer = 3.0f; // health bar wil show for 3 seconds before dissapearing
        float healthBarTimeLeft = 0;


        Pathfinding p; // instance of the pathfinder
        public List<Vector3> path; // the current or last path the enemy has treversed
        int i = 0;
        bool FindPathToPlayer = false; // TODO: change to enum with 3 states, "idle, find path and Moving"

        // Start is called before the first frame update
        void Start()
        {
            remainingHealth = maxHealth;
            healthBar.gameObject.SetActive(false);
        }

        private void AStarPathfind()
        {
            p = new Pathfinding();
            path = p.FindPath(this.transform, Player.playerTransform);
        }

        // Update is called once per frame
        void Update()
        {
            enemyPathfinding();

            if (healthBarTimeLeft > 0)
            {
                float percent = (float)remainingHealth / (float)maxHealth;
                Debug.Log(percent);
                healthBar.value = percent;
                healthBar.gameObject.SetActive(true);
                healthBarTimeLeft -= Time.deltaTime;

                if (healthBarTimeLeft <= 0)
                {
                    healthBar.gameObject.SetActive(false);
                }
            }
        }

        private void enemyPathfinding()
        {
            if (FindPathToPlayer)
            {
                // if enemy is not already next to the player
                if (path != null)
                {
                    //transform.localPosition += Vector3.forward * Time.deltaTime * speedMultiplier;
                    transform.position = Vector3.MoveTowards(transform.position, path[i], finalSpeed);
                    //transform.Translate(transform.TransformDirection(Vector3.forward) * Time.deltaTime * speedMultiplier, Space.Self);

                    // if close to the waypoint, set target to the next waypoint
                    if (Vector3.Distance(this.transform.position, path[i]) < pathfindingleniancy)
                    {
                        if (i < path.Count - 1)
                        {
                            i++;
                        }
                        // if the player has just reached the end of their path
                        else
                        {
                            //### stop pathfinding
                            //FindPathToPlayer = false;

                            //### find another path
                            i = 0;
                            AStarPathfind();
                        }
                    }
                }
                // if the enemy is next to the player, keep finding path to effectively chase the player when they move
                else AStarPathfind();
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                AStarPathfind();
                FindPathToPlayer = true;

            }
        }

        public void Damage(int damageNumbers)
        {
            Debug.Log("Enemy damaged!");
            remainingHealth -= damageNumbers;
            if (remainingHealth <=0)
            {
                Destroy(this.gameObject);
            }

            healthBarTimeLeft = defaultHealthBarTimer;
        }
    } 
}

