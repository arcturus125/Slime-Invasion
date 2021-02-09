using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HordeSurvivalGame
{
    public class Player : MonoBehaviour
    {
        public static Transform playerTransform;
        // Start is called before the first frame update
        void Awake()
        {
            playerTransform = this.transform;
        }

    }
}
