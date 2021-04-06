using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HordeSurvivalGame;


namespace Towers
{
    public class IceTower : Tower
    {
        float effectRadius = 7; // the radius the ice tower can slow enemies
        float slowingPower = 0.5f; // a percentage to slow the enemies speed by. default 0.5. 0.5 means enemies will move at 50% speed when in radius

        List<Enemy> lastFrame_Enemies = new List<Enemy>();
        List<Enemy> currentFrame_Enemies = new List<Enemy>();

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
             *  for each enemy in currentFrame:
             *      enemies speed = enemies default speed * slowingPower
             *      
             *  lastFrame = currentFrame
             */
            
        }
    }
}
