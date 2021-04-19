using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HordeSurvivalGame;
using ItemSystem;

namespace Towers
{
    public class FireTower : Tower
    {
        public ParticleSystem fireParticles;

        float effectRadius = 5; // the radius that this tower will damage enemies
        int DPS = 3; // the damage the fire tower will do each second

        const float DEFAULT_ATTACK_SPEED = 1.0f;
        float attackSpeedTimer = DEFAULT_ATTACK_SPEED;

        List<Enemy> targetEnemies = new List<Enemy>(); // the enemies that this tower will damage
        List<Enemy> lastFrameEnemies = new List<Enemy>(); // the enemies that this tower will damage

        public Material enemyEffectNone;
        public Material enemyEffectFire;

        // Start is called before the first frame update
        void Start()
        {
            inv = new Inventory(); // give tower a temporary inventory until the tower is placed - this elliviates errors and is overwritten later
        }

        // Update is called once per frame
        void Update()
        {

            // set currentFrame enemies
            Collider[] colls = Physics.OverlapSphere(transform.position, effectRadius);
            targetEnemies.Clear();
            foreach (Collider c in colls)
            {
                if (c.gameObject.TryGetComponent(out Enemy e))
                {
                    targetEnemies.Add(e);
                }
            }
            // determine any enemies that have left the range
            foreach (Enemy e in lastFrameEnemies)
            {
                if (targetEnemies.Contains(e)) continue;
                else
                {
                    // enemy just left the range of the tower
                    e.effectLayer.GetComponent<Renderer>().material = enemyEffectNone;
                }
            }


            // if the gun has ammo
            if (inv.IsItemInInv(recievableItem))
            {
                Attack();
            }



            lastFrameEnemies.Clear();
            foreach (Enemy e in targetEnemies)
                lastFrameEnemies.Add(e);
            targetEnemies.Clear();

        }

        private void Attack()
        {
            /* if timer <=0
             * loop through all enemies in radius           
             *      timer = 1;
             *  else
             *      timer -= time.deltatime
             */
            if (attackSpeedTimer <= 0)
            {
                foreach (Enemy e in targetEnemies)
                {
                    e.Damage(DPS);
                    e.effectLayer.GetComponent<Renderer>().material = enemyEffectFire;
                    inv.removeItem(recievableItem);
                }
                attackSpeedTimer = 1 / (DEFAULT_ATTACK_SPEED * speedMultiplier);
            }
            else attackSpeedTimer -= Time.deltaTime;

            
        }
    }
}
