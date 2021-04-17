using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HordeSurvivalGame;
using System;
using ItemSystem;

namespace Towers
{
    public class Turret : Tower
    {
        public GameObject head; //private

        public float range = 10;
        public float fireRate = 1.0f; // How many times the turret will fire per second.
        public int projectileDamage = 1; // the damage that each projectile will do to an enemy
        public float projectileSpeed = 3; // the speed the tower will force out projectiles

        /* #### multiple possible types of turret tower
         *      basic turret:
         *          1 shot per second
         *          projectiles do 3 damage
         *          projectiles move at a speed of 3
         *          
         *      machine gun: (more shots, less damage)
         *          5 shots per second
         *          prodectiles do 1 damage
         *          projectiles move at a speed of 5
         *          
         *      sniper (WAY more range and power, but firerate is extremely slow
         *          range = 50
         *          1 shot every 7 seconds
         *          projectiles do 10 damage
         *          projectiel speed = 10
         */




        public Enemy targetEnemy; // private
        public Projectile projectilePrefab;


        float timer = 0;

        // Start is called before the first frame update
        void Start()
        {
            inv = new Inventory();
        }

        // Update is called once per frame
        void Update()
        {
            // if the tower has not yet found a target, look for one
            if (targetEnemy == null)
            {
                LookForEnemies();
            }
            // if the tower has a target, shoot at it
            else
            {
                ShootAtEnemy();
            }
            PrintInv();
        }

        private void PrintInv()
        {
            for(int i = 0; i < inv.items.Count;i++)
            {
                Debug.Log( inv.items[i].name +" : "+ inv.quantity[i]);
            }
        }

        private void ShootAtEnemy()
        {
            // rotate the head of the turret to look at the enemy
            head.transform.LookAt(new Vector3(
                targetEnemy.gameObject.transform.position.x,
                head.transform.position.y,
                targetEnemy.gameObject.transform.position.z));
            // shoot the enemy every x seconds (x = fireRate)
            if (timer <= 0)
            {
                // shoot

                Projectile test = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                test.INIT(head.transform.forward,projectileSpeed, projectileDamage);

                timer = 1 /(fireRate * speedMultiplier);
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }

        private void LookForEnemies()
        {
            if (head)
            {
                // get everything in range of the tower
                Collider[] allCollidersInRange = Physics.OverlapSphere(transform.position, range);
                foreach (Collider colliderInRange in allCollidersInRange)
                {
                    // if it has the tag "enemy", target that entity
                    if (colliderInRange.gameObject.tag == "Enemy")
                    {
                        targetEnemy = colliderInRange.gameObject.GetComponent<Enemy>();
                    }
                }
            }
        }
    }
}
