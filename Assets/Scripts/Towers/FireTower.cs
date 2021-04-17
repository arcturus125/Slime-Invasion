using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HordeSurvivalGame;

namespace Towers
{
    public class FireTower : Tower
    {

        float effectRadius = 5; // the radius that this tower will damage enemies
        int DPS = 3; // the damage the fire tower will do each second

        float timer = 1;

        List<Enemy> targetEnemies = new List<Enemy>(); // the enemies that this tower will damage

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            /* if timer <=0
             *      get a list of all enemies within effectRadius
             *      damage those enemies by DPS
             *      timer = 1;
             *  else
             *      timer -= time.deltatime
             */
            if (timer <= 0)
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
                foreach(Enemy e in targetEnemies)
                {
                    e.Damage(DPS);
                }
                timer ++;
            }
            else
                timer -= Time.deltaTime;
        }
    }
}
