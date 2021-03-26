﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HordeSurvivalGame
{
    public class Projectile : MonoBehaviour
    {

        public float projectileSpeed = 1.0f;
        public Vector3 initialVelocity;

        public float projectileLifetime = 3.0f;
        private float timeLeft;

        public void INIT(Vector3 velocity, float speed)
        {
            initialVelocity = velocity;
            projectileSpeed = speed;
            transform.position += initialVelocity.normalized;
        }

        private void Start()
        {
            timeLeft = projectileLifetime;
        }
        // Update is called once per frame
        void Update()
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0) Destroy(this.gameObject);

            transform.position += (initialVelocity * Time.deltaTime * projectileSpeed);
        }
    }
}
