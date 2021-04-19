using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HordeSurvivalGame;
using System;

namespace Towers
{
    public class IceTower : Tower
    {
        float effectRadius = 5; // the radius the ice tower can slow enemies
        float slowingPower = 0.5f; // a percentage to slow the enemies speed by. default 0.5. 0.5 means enemies will move at 50% speed when in radius

        List<Enemy> lastFrame_Enemies = new List<Enemy>();
        List<Enemy> currentFrame_Enemies = new List<Enemy>();

        public Material enemyEffectNone;
        public Material enemyEffectIce;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            /*  get a list of all enemies within radius of the tower
             *  compare this to a list of all the enemies within radius last frame
             *  
             *  if an enemy has left the radius of this tower: ( in the lastframe but not in the curentFrame)
             *      set the enemies speed back to the default speed;
             *  if an enemy has entered the radius of this tower: ( in the curentFrame but not in the lastframe)
             *      set the enemies speed  to enemies speed * slowing power;
             *      
             *  lastFrame = currentFrame
             *  clear current frame
             */

            Collider[] colls = Physics.OverlapSphere(transform.position, effectRadius);
            foreach (Collider c in colls)
            {
                if (c.gameObject.TryGetComponent(out Enemy e))
                {
                    currentFrame_Enemies.Add(e);
                }
            }

            foreach (Enemy e in currentFrame_Enemies)
            {
                if (lastFrame_Enemies.Contains(e)) continue;
                else
                {
                    // enemy just entered the range of the tower
                    EnemyEnter(e);
                }
            }
            foreach (Enemy e in lastFrame_Enemies)
            {
                if (currentFrame_Enemies.Contains(e)) continue;
                else
                {
                    // enemy just left the range of the tower
                    EnemyLeave(e);
                    e.effectLayer.GetComponent<Renderer>().material = enemyEffectNone;
                }
            }
            lastFrame_Enemies.Clear();
            foreach (Enemy e in currentFrame_Enemies)
                lastFrame_Enemies.Add(e);
            currentFrame_Enemies.Clear();


        }

        private void EnemyLeave(Enemy e)
        {
            e.finalSpeed = e.finalSpeed / (slowingPower / speedMultiplier);
            Debug.Log("speeding back up");
        }

        private void EnemyEnter(Enemy e)
        {
            e.finalSpeed = e.finalSpeed * (slowingPower / speedMultiplier);
            e.effectLayer.GetComponent<Renderer>().material = enemyEffectIce;
            Debug.Log("slowing down");
        }
    }
}
