using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HordeSurvivalGame
{
    public class CameraFollowScript : MonoBehaviour
    {
        [SerializeField]
        private Transform cameraTarget;
        public float LerpTime = 0.1f;
        public float snapdistance = 0.1f;

        // Update is called once per frame
        void Update()
        {
            if (cameraTarget)
            {
                // if the camera is very close to the target, just smap to the target
                if(Vector3.Distance(transform.position, cameraTarget.position) < snapdistance)
                    transform.position = cameraTarget.position; 
                // if far from the target (teleporting), lerp the the distance to smooth the transition
                else
                    transform.position = Vector3.Lerp(transform.position, cameraTarget.position, LerpTime);
            }

        }
    }
}
