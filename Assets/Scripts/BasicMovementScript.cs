using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HordeSurvivalGame
{
    public class BasicMovementScript : MonoBehaviour
    {
        private Rigidbody _rigidbody; // the rigidbody of the player

        // the keys used to move the player
        const KeyCode MOVE_FORWARD = KeyCode.W;
        const KeyCode MOVE_LEFT    = KeyCode.A;
        const KeyCode MOVE_BACK    = KeyCode.S;
        const KeyCode MOVE_RIGHT   = KeyCode.D;

        const float MOVEMENT_SPEED = 1200.0f; // the speed of the player


        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            // move the player based on keyboard inputs
            Movement();

        }

        // moves player with WASD keys
        private void Movement()
        {
            Vector3 movement = Vector3.zero;
            if (Input.GetKey(MOVE_FORWARD))
            {
                movement += Vector3.forward;     //
            }                                    // forces are additive so is multiple buttons are pressed, one will not cancel out the other
            if (Input.GetKey(MOVE_LEFT))         // unless they are opposites (eg. W and S pressed at the same time will cancel out)
            {                                    //
                movement += Vector3.left;
            }
            if (Input.GetKey(MOVE_BACK))
            {
                movement += Vector3.back;
            }
            if (Input.GetKey(MOVE_RIGHT))
            {
                movement += Vector3.right;
            }
            _rigidbody.AddForce(transform.TransformDirection(movement * MOVEMENT_SPEED * Time.deltaTime), ForceMode.Force); // deltaTime is used to make the player move the same speed regardless of the speed of the PC

        }
    }
}
