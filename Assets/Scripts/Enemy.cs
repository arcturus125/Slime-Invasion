using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Pathfinder;
using System;

namespace HordeSurvivalGame 
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private Slider healthBar;
        public int maxHealth = 5;
        public int remainingHealth;
        public float finalSpeed = 0.01f; // the speed of the enemy AFTER all the effects have been applied
        public float defaultSpeed = 0.01f;

        public static float defaultHealthBarTimer = 3.0f; // health bar wil show for 3 seconds before dissapearing
        float healthBarTimeLeft = 0;

        [Header("Pathfinding Settings")]
        Pathfinding p; // instance of the pathfinder
        public List<Vector3> path; // the current or last path the enemy has treversed
        int pathIndexer = 0;
        bool FindPathToPlayer = false; // pathfinding can be disabled here
        public float pathfindingleniancy = 0.5f; // the distance from a waypoint before the enemy targets the next (higher = smoother path bit more clipping of objects)
        public int repathfindAfterTicks = 5;
        public float pathfindingErrorTimer = 3; // if pathfinding fails, wait until this many seconds pass before re-atteempting
        float errorTimer = 0;

        // Start is called before the first frame update
        void Start()
        {
            remainingHealth = maxHealth;
            healthBar.gameObject.SetActive(false);
            //Moved this code to the start function rather than waiting for a Keypress so the enemies imediately start chasing the player.
            AStarPathfind();
            FindPathToPlayer = true;

        }

        private void AStarPathfind()
        {
            p = new Pathfinding();
            path = p.FindPath(this.transform, Player.playerTransform);
            pathIndexer = 0;
        }

        // Update is called once per frame
        void Update()
        {
            enemyPathfinding();

            if (healthBarTimeLeft > 0)
            {
                float percent = (float)remainingHealth / (float)maxHealth;
                //Debug.Log(percent);
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
                    transform.position = Vector3.MoveTowards(transform.position, path[pathIndexer], finalSpeed);
                    //transform.Translate(transform.TransformDirection(Vector3.forward) * Time.deltaTime * speedMultiplier, Space.Self);

                    // if close to the waypoint, set target to the next waypoint
                    if (Vector3.Distance(this.transform.position, path[pathIndexer]) < pathfindingleniancy)
                    {
                        if (pathIndexer < path.Count - 1)
                        {
                            PathfindingTick();
                            pathIndexer++;
                        }
                        // if the player has just reached the end of their path
                        else
                        {
                            //### stop pathfinding
                            //FindPathToPlayer = false;

                            //### find another path
                            pathIndexer = 0;
                            AStarPathfind();
                        }
                    }
                }
                // if pathfinding fails OR enemy is on the same tile as the player
                // wait 3 seconds before pathfinding again
                else
                {
                    if (errorTimer > 0)
                    {
                        if (errorTimer <= 0)
                            AStarPathfind();
                        else
                            errorTimer -= Time.deltaTime;
                    }
                    else
                        errorTimer += pathfindingErrorTimer;
                }
            }
            //if(Input.GetKeyDown(KeyCode.F))
            //{
            //    AStarPathfind();
            //    FindPathToPlayer = true;

            //}
        }

        int tickNumber = 0;
        private void PathfindingTick()
        {
            tickNumber++;
            if(tickNumber >= repathfindAfterTicks)
            {
                AStarPathfind();
            }
        }

        public void Damage(int damageNumbers)
        {
            remainingHealth -= damageNumbers;
            if (remainingHealth <=0)
            {
                Destroy(this.gameObject);
            }

            healthBarTimeLeft = defaultHealthBarTimer;
        }
    } 
}

