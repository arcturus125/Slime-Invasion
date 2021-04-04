using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HordeSurvivalGame
{
    public class Projectile : MonoBehaviour
    {
        public int projectileDamage = 1;
        public float projectileSpeed = 1.0f;
        public Vector3 initialVelocity;

        public float projectileLifetime = 3.0f;
        private float timeLeft;

        public void INIT(Vector3 velocity, float speed, int damage = 1)
        {
            initialVelocity = velocity;
            projectileSpeed = speed;
            transform.position += initialVelocity.normalized;
            projectileDamage = damage;
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

        void OnCollisionEnter(Collision collision)
        {
            // when the projectile hits an enemt
            if(collision.gameObject.TryGetComponent(out Enemy e))
            {
                e.Damage(projectileDamage);
            }
        }
    }
}
