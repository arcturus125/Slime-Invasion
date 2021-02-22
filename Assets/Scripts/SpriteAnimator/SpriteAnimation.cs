using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpriteAnimator
{
    public class SpriteAnimation : MonoBehaviour
    {
        public Sprite[] frames; //Array of all the frames in the animation for the planet.
        public float timeBetweenFrames = 0.1f; //The amount of time between switching frames.
        private float animatorDeltaTime = 0.0f; //Time since the last frame in the animator.
        private int index = 0; //The current frame in the animation.
        private SpriteRenderer renderer;

        // Start is called before the first frame update
        void Start()
        {
            renderer = GetComponent<SpriteRenderer>(); //Get renderer
        }

        // Update is called once per frame
        void Update()
        {
            if (animatorDeltaTime >= timeBetweenFrames) //Iterates through frames when the waiting time between frames has been met.
            {
                index++;

                renderer.sprite = frames[index]; //Sets sprite to current frame.

                if(index >= frames.Length-1) //Loops animation if the last frame has been displayed.
                {
                    index = 0;
                }
                    
                animatorDeltaTime -= timeBetweenFrames; //Resets animationDeltaTime ready for the next frame.
            }
            animatorDeltaTime += Time.deltaTime; //Tracks time.
        }
    }
}
