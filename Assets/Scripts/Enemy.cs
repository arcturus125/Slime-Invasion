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


        Pathfinding p;
        public List<Vector3> path;
        int i = 0;
        bool stopPathfinding = false;

        // Start is called before the first frame update
        void Start()
        {
            AStarPathfind();

        }

        private void AStarPathfind()
        {
            p = new Pathfinding();
            path = p.FindPath(this.transform, Player.playerTransform);
        }

        // Update is called once per frame
        void Update()
        {
            if (!stopPathfinding)
            {
                //transform.localPosition += Vector3.forward * Time.deltaTime * speedMultiplier;
                transform.position = Vector3.MoveTowards(transform.position,path[i], speedMultiplier);
                //transform.Translate(transform.TransformDirection(Vector3.forward) * Time.deltaTime * speedMultiplier, Space.Self);


                if (Vector3.Distance(this.transform.position, path[i]) < pathfindingleniancy)
                {
                    if (i < path.Count-1)
                    {
                        i++;
                    }
                    else
                    {
                        i = 0;
                        AStarPathfind();
                    }
                }
            }
        }
    } 
}

