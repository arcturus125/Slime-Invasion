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

        // Start is called before the first frame update
        void Start()
        {
            inv = new Inventory(); // give tower a temporary inventory until the tower is placed - this elliviates errors and is overwritten later
        }

        // Update is called once per frame
        void Update()
        {
            // if the gun has ammo
            if (inv.IsItemInInv(recievableItem))
            {
                Attack();
            }
        }

        private void Attack()
        {
            /* if timer <=0
                         *      get a list of all enemies within effectRadius
                         *      damage those enemies by DPS
                         *      timer = 1;
                         *  else
                         *      timer -= time.deltatime
                         */
            if (attackSpeedTimer <= 0)
            {
                Collider[] colls = Physics.OverlapSphere(transform.position, effectRadius);
                targetEnemies.Clear();
                foreach (Collider c in colls)
                {
                    if (c.gameObject.TryGetComponent(out Enemy e))
                    {
                        targetEnemies.Add(e);
                    }
                }
                foreach (Enemy e in targetEnemies)
                {
                    e.Damage(DPS);
                    //Instantiate(fireParticles, e.transform.position, Quaternion.identity);
                    inv.removeItem(recievableItem);
                }
                attackSpeedTimer = 1 / (DEFAULT_ATTACK_SPEED * speedMultiplier);
            }
            else attackSpeedTimer -= Time.deltaTime;
        }
    }
}
