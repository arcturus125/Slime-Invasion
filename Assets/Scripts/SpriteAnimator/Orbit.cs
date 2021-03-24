using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpriteAnimator
{
    public class Orbit : MonoBehaviour
    {
        public float orbitSpeed = 0.1f;

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.up * orbitSpeed); //Every frame, rotates moon around local Y axis of the planet.
        }
    }
}
