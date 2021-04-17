using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HordeSurvivalGame
{
    public class Projectile : MonoBehaviour
    {
        public int punchThrough = 1;
        public int projectileDamage = 1;
        public float projectileSpeed = 1.0f;
        public Vector3 initialVelocity;

        public float projectileLifetime = 3.0f;
        private float timeLeft;

        public void INIT(Vector3 velocity, float speed = 1, int damage = 1)
        {
            transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
            GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
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
            if(collision.gameObject.tag == "Map")
            {
                Destroy(this.gameObject);
            }    
            // when the projectile hits an enemt
            if(collision.gameObject.TryGetComponent(out Enemy e))
            {
                e.Damage(projectileDamage);
                punchThrough--;
                if (punchThrough <= 0)
                    Destroy(this.gameObject);
            }
        }
    }
}
