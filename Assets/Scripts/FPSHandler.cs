using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSHandler : MonoBehaviour
{

    double fps30 = 1.0 / 30.0;
    double fps45 = 1.0 / 45.0;

    public float timer = 0;
    public float timerIncrement = 0.5f;

    int cap = 5000;
    int min = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.deltaTime > fps30)
        {
            int num  = (int)(Pathfinder.Pathfinding.LoopCount * 0.75f);
            if (num < min) num = min;
            //Pathfinder.Pathfinding.LoopCount = num;
            //Debug.LogWarning("Lag spike detected, slowing pathfinding");
            timer = timerIncrement;
        }
        if (timer <= 0)
        {
            if (Time.deltaTime < fps45)
            {
                int num = (int)(Pathfinder.Pathfinding.LoopCount * 1.1f);
                if (num > cap) num = cap;
                //Pathfinder.Pathfinding.LoopCount = num;
                //Debug.LogWarning("Spinning back up");
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }

        //Debug.Log("Frametime: " + Time.deltaTime+"   count: " + Pathfinder.Pathfinding.LoopCount + "   target: "+ fps30);
    }
}
