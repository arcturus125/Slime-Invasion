using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpriteAnimator
{
    public class TargetPositionOverride : MonoBehaviour
    {
        public Transform target;

        // Update is called once per frame
        void Update()
        {
            this.transform.position = target.position; //Sets moon position to dummy position in order to keep it facing the camera.
        }
    }
}
