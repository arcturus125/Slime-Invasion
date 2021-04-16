using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotation : MonoBehaviour
{
    [Range(-1, 1)]
    public int xMult;
    [Range(-1, 1)]
    public int yMult;
    [Range(-1, 1)]
    public int zMult;

    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(speed*xMult,speed*yMult, speed*zMult));
    }
}
