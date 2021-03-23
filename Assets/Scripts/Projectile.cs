using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HordeSurvivalGame
{
    public class Projectile : MonoBehaviour
    {

        public float projectileSpeed = 1.0f;
        public Vector3 initialVelocity;

        public void INIT(Vector3 velocity, float speed)
        {
            initialVelocity = velocity;
            projectileSpeed = speed;
            transform.position += initialVelocity.normalized;
        }

        // Update is called once per frame
        void Update()
        {
            transform.position += (initialVelocity * Time.deltaTime * projectileSpeed);
        }
    }
}
