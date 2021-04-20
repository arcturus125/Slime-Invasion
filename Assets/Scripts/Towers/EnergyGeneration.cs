using ItemSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Towers
{
    public class EnergyGeneration : Tower
    {
        public int range = 10; //The radius of tiles the tower can reach.
        public float energySpeedBoostMultiplier = 1.5f; //The speed that will be multiplied with tower variables.
        public int powerBank = 100; //The amount of power that can be distributed among towers.


        // Start is called before the first frame update
        void Start()
        {
            inv = new Inventory(); // give tower a temporary inventory until the tower is placed - this elliviates errors and is overwritten later
        }
        // Update is called once per frame
        void Update()
        {
            int amountOfPowerLeft = powerBank; //Sets the number of towers the 
            Collider[] colls = Physics.OverlapSphere(transform.position, range);
            foreach (Collider c in colls)
            {
                if (c.gameObject.TryGetComponent(out Tower t) && amountOfPowerLeft > 0)
                {
                    amountOfPowerLeft -= t.powerNeedToSpeedUp;
                    t.SetSpeedMultiplier(energySpeedBoostMultiplier);
                }

            }
        }
    }
}

