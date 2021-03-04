using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HordeSurvivalGame
{
    public class CameraScript : MonoBehaviour
    {
        [SerializeField]
        private Transform cameraTarget;

        // Update is called once per frame
        void Update()
        {
            // if the camera has a target. the camera will smoothly follow behind the players movement using linear interpolation
            // if no target, camera will stay put
            if (cameraTarget)
            {
                transform.position = cameraTarget.position; //Vector3.Lerp(transform.position, cameraTarget.position, LerpTime);
            }

        }
    }
}
