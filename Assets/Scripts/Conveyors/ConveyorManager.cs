﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinder.tiles;
using HordeSurvivalGame;
using System;

namespace Conveyors
{
    public class ConveyorManager : MonoBehaviour
    {
        public enum IOController
        {
            None,
            Input,
            Output
        }

        public GameObject[] conveyorArms; // up, down, left, right
        public Vector2[] cardinalDirections;
        public IOController[] armTypes;

        public int visibleArms = 0;
        public int noOfInputs  = 0;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // reset count varables ready for next frame
            visibleArms = 0;
            // if a compatible tower is ajdacent, toggle visibility of the connecting arm
            for (int i = 0; i < conveyorArms.Length; i++)
            {
                UpdateArmVisibility(conveyorArms[i], cardinalDirections[i]);
            }
            // update the number of inputs for output management
            UpdateStats();
            // based on the number of inputs and the visible arms, an algorithm is used to determine which arms are outputs
            OutputManagement();


            // update arrows on conveyors
            for (int i = 0; i < armTypes.Length; i++)
            {
                UpdateConveyorArrows(i);
            }

        }

        private void OutputManagement()
        {
            if (noOfInputs == 1 && visibleArms == 2)
                StraightConveyor();
            else if (noOfInputs == visibleArms - 1 && noOfInputs != 0)
                YConveyor();
        }

        private void YConveyor()
        {
            for (int i = 0; i < conveyorArms.Length; i++)
            {

                // if a conveyor arm is visible and NOT and input, make it an output
                if (armTypes[i] != IOController.Input)
                {
                    Debug.Log("y conveyor");
                    if (conveyorArms[i].activeInHierarchy == true)
                    {

                        Debug.Log("y conveyor active");
                        armTypes[i] = IOController.Output;
                    }
                }
            }
        }

        private void StraightConveyor()
        {
            for(int i = 0; i < conveyorArms.Length;i++)
            {

                // if a conveyor arm is visible and NOT and input, make it an output
                if (armTypes[i] != IOController.Input)
                {
                    Debug.Log("straight conveyor");
                    if (conveyorArms[i].activeInHierarchy == true)
                    {

                        Debug.Log("straight conveyor active");
                        armTypes[i] = IOController.Output;
                    }
                }
            }
        }

        private void UpdateStats()
        {
            noOfInputs  = 0;
            foreach(IOController i in armTypes)
            {
                if (i == IOController.Input)  noOfInputs++;
            }
        }
        private void UpdateConveyorArrows(int i)
        {
            // if conveyor arm type set to none, hide the arrow
            if (armTypes[i] == IOController.None)
            {
                conveyorArms[i].GetComponentInChildren<SpriteRenderer>().enabled = false;
            }
            // if conveyor arm type set to anything other than none, show the arrow again
            else
            {   
                conveyorArms[i].GetComponentInChildren<SpriteRenderer>().enabled = true;

                // if controller arm type is "Output", the arrow will point away from the centre of the conveyor
                if (armTypes[i] == IOController.Output)
                {
                    conveyorArms[i].GetComponentInChildren<SpriteRenderer>().gameObject.transform.localRotation = Quaternion.Euler(90, 90, 0);
                }

                // if controller arm type is "Input", the arrow will point towards the centre of the conveyor
                if (armTypes[i] == IOController.Input)
                {
                    conveyorArms[i].GetComponentInChildren<SpriteRenderer>().gameObject.transform.localRotation = Quaternion.Euler(90, 270, 0);
                }
            }
        }
        private void UpdateArmVisibility(GameObject arm, Vector2 cardinalDirection)
        {
            Tile t = Tile.Vector3ToTile(transform.position);
            //if (Tile.tileMap[t.x + (int)cardinalDirection.x, t.y + (int)cardinalDirection.y].GetTower() != null)
            arm.SetActive(CanConveyorConnect(t, cardinalDirection));
        }
        private bool CanConveyorConnect(Tile t, Vector2 cardinalDirection)
        {


            int armIndex = System.Array.IndexOf(cardinalDirections, cardinalDirection);
            armTypes[armIndex] = IOController.None;


            // dont bother running any of this code if there is no tower adjacent to the conveyor
            if (Tile.tileMap[t.x + (int)cardinalDirection.x, t.y + (int)cardinalDirection.y].GetTower() == null)
            {
                return false;
            }
                

            // conveyor can connect to other conveyors
            ConveyorManager conv;
            if(Tile.tileMap[t.x + (int)cardinalDirection.x, t.y + (int)cardinalDirection.y].GetTower().TryGetComponent<ConveyorManager>(out conv))
            {
                Vector2 oppositeDirection = new Vector2(-cardinalDirection.x, -cardinalDirection.y);
                int theirIndex = System.Array.IndexOf(conv.cardinalDirections, oppositeDirection);
                int myIndex = System.Array.IndexOf(cardinalDirections, cardinalDirection);
                if(conv.armTypes[theirIndex] == IOController.Output)
                {
                    armTypes[myIndex] = IOController.Input;
                    Debug.Log(t.x +","+t.y+"   Conveyor chaining");
                }


                visibleArms++;
                return true;
            }

            // conveyor can connect to mine
            Mine m;
            if (Tile.tileMap[t.x + (int)cardinalDirection.x, t.y + (int)cardinalDirection.y].GetTower().TryGetComponent<Mine>(out m))
            {
                int index = System.Array.IndexOf(cardinalDirections, cardinalDirection);
                armTypes[index] = IOController.Input;
                visibleArms++;
                return true;
            }


            return false;
        }
    }
}
