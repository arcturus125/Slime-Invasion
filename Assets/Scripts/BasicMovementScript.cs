using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HordeSurvivalGame
{
    public class BasicMovementScript : MonoBehaviour
    {
        private Rigidbody _rigidbody; // the rigidbody of the player
        public Animator animator;

        // the keys used to move the player
        const KeyCode MOVE_FORWARD = KeyCode.W;
        const KeyCode MOVE_LEFT = KeyCode.A;
        const KeyCode MOVE_BACK = KeyCode.S;
        const KeyCode MOVE_RIGHT = KeyCode.D;

        const float MOVEMENT_SPEED = 1200.0f; // the speed of the player
        private const float DASH_TIMER_AMOUNT = 3.0f;
        private float dashTimer = 0.0f;

        //private float dashTimer = 0.0f;
        //public float timeToDash = 1.0f;
        //public float dashDistance = 1.0f;
        //private float dashingX = 0.0f;
        //private float dashingZ = 0.0f;

        private float movementX = 0.0f;
        private float movementZ = 0.0f;


        public float dashingPower = 1.0f;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            movementX = 0.0f;
            movementZ = 0.0f;

            if (dashTimer >= 0.0f)
            {
                dashTimer -= Time.deltaTime;
            }
            
            Movement(); // move the player based on keyboard inputs
            Dash();
        }

        // moves player with WASD keys
        private void Movement()
        {
            Vector3 movement = Vector3.zero;
            if (Input.GetKey(MOVE_FORWARD))
            {
                movement += Vector3.forward;
                movementZ = 1.0f;
            }                                    // forces are additive so is multiple buttons are pressed, one will not cancel out the other
            if (Input.GetKey(MOVE_LEFT))         // unless they are opposites (eg. W and S pressed at the same time will cancel out)
            {                                    //
                movement += Vector3.left;
                movementX = -1.0f;
            }
            if (Input.GetKey(MOVE_BACK))
            {
                movement += Vector3.back;
                movementZ = -1.0f;
            }
            if (Input.GetKey(MOVE_RIGHT))
            {
                movement += Vector3.right;
                movementX = 1.0f;
            }

            animator.SetFloat("WalkingX", movementX); //Animation
            animator.SetFloat("WalkingZ", movementZ);
            _rigidbody.AddForce(transform.TransformDirection(movement * MOVEMENT_SPEED * Time.deltaTime), ForceMode.Force); // deltaTime is used to make the player move the same speed regardless of the speed of the PC

        }

        private void Dash()
        {
            //if (Input.GetMouseButtonDown(1)) //Right click dash.
            //{
            //    dashTimer = 0;
            //    dashingX = movementX;
            //    dashingZ = movementZ;
            //}

            //if (dashTimer < timeToDash)
            //{
            //    float percent = dashTimer / timeToDash;
            //    Vector3 dash = new Vector3(-dashingX, 0.0f, -dashingZ);
            //    float xDash = -Mathf.Pow((percent - 0.5f) * 2, 2) + 1; //Dampens the dash speed using a curve.
            //    float zDash = -Mathf.Pow((percent - 0.5f) * 2, 2) + 1;

            //    _rigidbody.MovePosition(this.transform.position + new Vector3(dash.x * xDash * dashDistance, 0.0f, dash.z * zDash * dashDistance));
            //    dashTimer += Time.deltaTime;
            //}


            if (dashTimer <= 0.0f)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    _rigidbody.AddForce(new Vector3(-movementX, 0.0f, -movementZ) * dashingPower, ForceMode.Impulse);
                    dashTimer = DASH_TIMER_AMOUNT;
                }
            }

        }
    }
}
