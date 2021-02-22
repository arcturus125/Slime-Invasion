using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinder;

namespace HordeSurvivalGame 
{
    public class Enemy : MonoBehaviour
    {
        public GameObject dummy;
        public float pathfindingleniancy = 0.5f;
        public float speedMultiplier = 0.01f;


        Pathfinding p; // instance of the pathfinder
        public List<Vector3> path; // the current or last path the enemy has treversed
        int i = 0;
        bool FindPathToPlayer = false; // TODO: change to enum with 3 states, "idle, find path and Moving"

        // Start is called before the first frame update
        void Start()
        {
            

        }

        private void AStarPathfind()
        {
            p = new Pathfinding();
            path = p.FindPath(this.transform, Player.playerTransform);
        }

        // Update is called once per frame
        void Update()
        {
            if (FindPathToPlayer)
            {
                // if enemy is not already next to the player
                if (path != null)
                {
                    //transform.localPosition += Vector3.forward * Time.deltaTime * speedMultiplier;
                    transform.position = Vector3.MoveTowards(transform.position, path[i], speedMultiplier);
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
            if(Input.GetKeyDown(KeyCode.F))
            {
                AStarPathfind();
                FindPathToPlayer = true;

            }
        }
    } 
}

