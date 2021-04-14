using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Pathfinder.tiles;
using HordeSurvivalGame;
using ItemSystem;
using Towers;

namespace Conveyors
{
    public class ConveyorManager : MonoBehaviour
    {
        /* Developer Note:
         *  
         *  each conveyor will connect to neighboring towers and coveyors.
         *  each conveyor has 4 child gameobject that display a conveyor connecting in a particular direction
         *  these are often referred to as  the conveyor's "Arms" in most code relating to conveyors
         *  
         *  each arm will have an arrow displaying the direction of flow of items over the conveyor
         *  these can be overriden by the player in the UI. overridden arrows display as red.
         *  arrows displaying as white are controlled by a number of algorithms below, and will automatically adjust if more inputs and outputs are introduces
         *  arrows displaying as red are controlled by the player only, and may need to be ajusted by said player if new inputs or outputs are created
         */

        public enum IOController
        {
            None,
            Input,
            Output,
            OutputToTower
        }

        [SerializeField]
        private string UnityInspectorDebugging = ""; // this exists only for debugging things in unity.

        /*  ###############################################
         *              Conveyor Model Management
         *  ###############################################
         */
        public GameObject[] conveyorArms; //  assigned in inspector. contains a reference to the conveyors arms in the following order: up, down, left, right
        public Vector2[] cardinalDirections; // assigned in inspector, commonIndex with conveyorArms. contains the direction each arm is facing
        public IOController[] armTypes; // automatically generated inputs and outputs for each conveyor arm
        public IOController[] CustomArmTypes; // user overridden inputs and outputs - so user can change the default functionality of a conveyor
        public IOController[] TrueArmTypes; // a combination of the above two, where CustomArmTypes Overrides armTypes if the value is not 
        public bool hasItemFilters = false;
        public List<Item>[] itemFilters = new List<Item>[4];
        public int visibleArms = 0; // used in algorithm decision making to decide which arms are inputs and which arms are outputs
        public int noOfInputs  = 0; //


        /*  ###############################################
         *              Conveyor Item Movement
         *  ###############################################
         */

        enum ConveyorState
        {
            Idle,
            Inputting,
            LookingForOutput,
            Outputting
        }

        [SerializeField]
        private GameObject conveyorSprite;
        public Inventory ConveyorInv = new Inventory(); // the items currently held within the conveyor
        public int maxCapacity = 10; // the max number of items that the conveyor can hold at one time
        private float timeToTransport = 1.0f;

        bool placed = false;
        bool active = false;
        float timeElapsed = 0;
        List<Vector2> inputDirections = new List<Vector2>();
        List<Vector2> outputDirections = new List<Vector2>();
        List<GameObject> spriteObjects = new List<GameObject>();
        List<Inventory> outputInventories = new List<Inventory>();
        ConveyorState state;

        List<Sprite> sprites = new List<Sprite>();
        bool firstframe = false;





        // initialise lists and flood arrays that require a default value
        void Start()
        {
            for(int i = 0; i < itemFilters.Length;i++)
            {
                itemFilters[i] = new List<Item>();
            }
            for(int i =0; i < CustomArmTypes.Length;i++)
            {
                CustomArmTypes[i] = IOController.None;
            }
        }
        void Update()
        {
            UpdateConveyorModel();

            if (visibleArms > noOfInputs) active = true;
            if(active && placed)
                ItemTransfer();
        }
        public void OnPlaced()
        {
            placed = true;
            state = ConveyorState.Idle;
        }

        private void ItemTransfer()
        {
            //////////////////////////////////////////
            ///     IDLE
            //////////////////////////////////////////
            if (state == ConveyorState.Idle)
            {
                foreach (GameObject s in spriteObjects)  //
                    Destroy(s);                          // reset all the sprites ready for the next IO cycle
                spriteObjects.Clear();                   //
                inputDirections.Clear();
                for (int i = 0; i < TrueArmTypes.Length; i++)
                {
                    if (TrueArmTypes[i] == IOController.Input)
                    {
                        Tile t = Tile.Vector3ToTile(transform.position);
                        GameObject tower = Tile.tileMap[t.x + (int)cardinalDirections[i].x, t.y + (int)cardinalDirections[i].y].GetTower();
                        if (tower.TryGetComponent<Mine>(out Mine m))
                        {
                            if (m.inv.items.Count > 0)
                            {
                                // get the item and the quantity we want the conveyor to take  
                                Item item = m.inv.items[0];                                    
                                int quantity = m.inv.quantity[0];                              
                                //remove the item from the towers inventory and add it to the conveyors
                                m.inv.removeItem(item, quantity);
                                ConveyorInv.addItem(item, quantity);
                                // set the conveyor to "Busy" so it doesnt suck in items until it has finished moving the items it already has
                                state = ConveyorState.Inputting;
                                inputDirections.Add(new Vector3(-cardinalDirections[i].x, -cardinalDirections[i].y));
                                sprites.Add(item.icon);
                                timeElapsed = 0;
                                firstframe = true;

                            }
                        }
                        
                    }
                }
            }
            //////////////////////////////////////////
            ///     INPUT_CYCLE
            //////////////////////////////////////////
            else if (state == ConveyorState.Inputting)
            {
                //  onfirst frame
                //      for each input
                //          create a sprite
                //  after first frame
                //      calculate the percentage (timeElapsed/timeToTransport)
                //      set the position of sprite to inputDirection * (1-percentage)
                //  when percentage >= 100
                //      delete the sprite
                //      move on to Looking For Output

                timeElapsed += Time.deltaTime;
                if (firstframe)
                {
                    firstframe = false;
                    for(int i = 0; i < inputDirections.Count;i++)
                    {
                        GameObject spriteObject = Instantiate(conveyorSprite, this.gameObject.transform);
                        spriteObject.GetComponent<SpriteRenderer>().sprite = sprites[i];
                        spriteObject.transform.localPosition = new Vector3(inputDirections[i].x, 0, inputDirections[i].y);
                        spriteObjects.Add(spriteObject);
                    }
                }
                else if(timeElapsed > 0 && timeElapsed < timeToTransport)
                {
                    for (int i = 0; i < inputDirections.Count; i++)
                    {
                        GameObject spriteObject = spriteObjects[i];
                        float percent = timeElapsed / timeToTransport;
                        spriteObject.transform.localPosition = new Vector3(inputDirections[i].x, 0, inputDirections[i].y) * (1 - percent);
                        spriteObject.transform.localPosition += Vector3.up;
                    }
                }
                else if (timeElapsed >= timeToTransport)
                {

                    state = ConveyorState.LookingForOutput;
                    timeElapsed = 0;

                    inputDirections.Clear(); // now that the inputs are complete, delete the input directions ready for the next inputs.
                    for (int i = 0; i < TrueArmTypes.Length; i++){
                        if (TrueArmTypes[i] == IOController.Output || TrueArmTypes[i] == IOController.OutputToTower)
                        {
                            outputDirections.Add(cardinalDirections[i]);
                        }
                    }
                }
            }
            else if(state == ConveyorState.LookingForOutput)
            {
                outputDirections.Clear();                                //
                for (int i = 0; i < TrueArmTypes.Length; i++)            //
                {                                                        // constantly check for new outputs, otherwise when items stop, they will never start again
                    if (TrueArmTypes[i] == IOController.Output || TrueArmTypes[i] == IOController.OutputToTower)         
                    {                                                    
                        outputDirections.Add(cardinalDirections[i]);     
                    }                                                    
                }

                // Are we ready to output?
                bool readyForOutput = true;                        // by default we are ready for output. 
                foreach (Vector2 direction in outputDirections)    // then we loop through all the neighboring conveyors and if they are Not Idle (not ready to recieve an item)
                {                                                  // then we set readyForOutput to false and chech again on the next frame TODO: use a timer instead of checking every frame
                    Tile t = Tile.Vector3ToTile(this.transform.position + new Vector3(-direction.x, 0, -direction.y));
                    if (t.GetTower().TryGetComponent<ConveyorManager>(out ConveyorManager conv))
                    {
                        if (!(conv.state == ConveyorState.Idle))
                        {
                            readyForOutput = false;
                        }
                    }

                }
                // when we are ready to output, proceed
                if(readyForOutput && outputDirections.Count > 0)
                {
                    state = ConveyorState.Outputting;
                    firstframe = true;

                    // destroy all the sprites and clear the list
                    foreach (GameObject spriteObject in spriteObjects)
                        Destroy(spriteObject);
                    spriteObjects.Clear();
                }
            }
            //////////////////////////////////////////
            ///     OUTPUT_CYCLE
            //////////////////////////////////////////
            else if (state == ConveyorState.Outputting)
            {
                //  onfirst frame
                //      for each output
                //          create an inventory for each output direction
                //          if an item is allowed (by item filters) to go in a direction, it will be added to the inventory headed in that direction
                //          if an item is NOT allowed a certain direction, it will not be added to that output Inventory, and therefore will not go that direction
                //          create a sprite
                //  after first frame
                //      calculate the percentage (timeElapsed/timeToTransport)
                //      set the position of sprite to outputDirection * percentage
                //  when percentage >= 100
                //      delete the sprite
                //      move on to Looking For Output

                timeElapsed += Time.deltaTime;
                if (firstframe)
                {
                    firstframe = false;


                    outputInventories.Clear();
                    List<Vector2> itemOutputDirections = new List<Vector2>();
                    int numOfItemsDirections = 0;
                    for(int j = 0; j <  ConveyorInv.items.Count; j++)
                    {
                        Item item = ConveyorInv.items[j];
                        for (int i = 0; i < outputDirections.Count;i++)
                        {
                            Vector2 outputDirection = outputDirections[i];
                            // if the item is in the item filter OR if the item filter is empty
                            // add it to the itemOutputDirections (therefore allowing the item to go that direction)
                            // NOTE: empty item filters will accept all items
                            int outputDirectionCardinalIndex = GetCardinalDirection(outputDirection);
                            if (itemFilters[outputDirectionCardinalIndex].Contains(item) || itemFilters[outputDirectionCardinalIndex].Count == 0)
                            {
                                numOfItemsDirections++;
                                itemOutputDirections.Add(outputDirection);
                            }
                        }

                        for (int k = 0; k < outputDirections.Count; k++) // in order to avoid errors
                        {                                                // an output where Zero items are sent must
                            spriteObjects.Add(new GameObject());         // have an empty inventory - the absence of an inventory will cause errors
                            outputInventories.Add(new Inventory());      //
                        }

                        for ( int k = 0; k< itemOutputDirections.Count;k++)                        // if an item is meant to travel in a particular direction
                        {                                                                          // it *OVERWRITES* the before code
                            Inventory tempInv = new Inventory();                                   // therefore every output will have an inventory
                            tempInv.addItem(item, ConveyorInv.quantity[j]); // still duplicating   // and output with zero items, will be an inventory of length 0
                            int temp = outputDirections.IndexOf(itemOutputDirections[k]);          //
                            outputInventories[temp] = tempInv;                                     //
                        }
                    }

                    // the above creates an inventory for each output that respects item filters.
                    // these inventories are then used to pass items to the next tower
                    // also used to change the sprite of items scrolling across the conveyor

                    for (int i = 0; i < outputDirections.Count; i++)
                    {
                        // if the output inventory contains an item
                        // show that item scrolling across the conveyor
                        if (outputInventories[i].items.Count > 0)
                        {
                            GameObject spriteObject = Instantiate(conveyorSprite, this.gameObject.transform);
                            spriteObject.GetComponent<SpriteRenderer>().sprite = outputInventories[i].items[0].icon; // TODO: loop through all elements, instead of just taking the first
                            spriteObjects[i] = spriteObject;                                                         // ( show all items scrolling across the conveyor, not just the one in slot 0)
                        }
                    }                                                                                                                  
                }           
                // move items across the conveyor in the appropriate direction
                else if (timeElapsed > 0 && timeElapsed < timeToTransport)                                                             
                {                                                                                                
                    for (int i = 0; i < outputDirections.Count; i++)                                                                   
                    {
                        if (outputInventories[i].items.Count > 0)
                        {
                            GameObject spriteObject = spriteObjects[i];
                            float percent = timeElapsed / timeToTransport;
                            spriteObject.transform.localPosition = new Vector3(-outputDirections[i].x, 0, -outputDirections[i].y) * percent;
                            spriteObject.transform.localPosition += Vector3.up;
                        }
                    }                                                                                                                        
                }                                                                                                                            
                else if (timeElapsed >= timeToTransport)                                                                                     
                {                                                                                                                            
                    state = ConveyorState.Idle;                                                                                              
                                                                                                                                             
                    for (int i = 0; i < outputDirections.Count; i++)                                                                         
                    {

                        if (outputInventories[i].items.Count > 0)
                        {
                            GameObject spriteObject = spriteObjects[0];
                            Tile t = Tile.Vector3ToTile(this.transform.position + new Vector3(-outputDirections[i].x, 0, -outputDirections[i].y));
                            if (t.GetTower())
                            {
                                // if connected to a conveyor
                                // "Link" the conveyor so that inputs and outputs match up
                                if (t.GetTower().TryGetComponent<ConveyorManager>(out ConveyorManager conv))
                                {
                                    conv.link(outputInventories[i].items[0], outputInventories[i].quantity[0], outputDirections[i]);
                                    outputInventories[i].items.RemoveAt(0);
                                    outputInventories[i].quantity.RemoveAt(0);
                                    spriteObjects.Remove(spriteObject);
                                    Destroy(spriteObject);

                                }
                                // if connected to a tower
                                // output the conveyors inventory into the towers
                                if (t.GetTower().TryGetComponent(out Tower tower))
                                {
                                    // for each item in the conveyors inventory
                                    // add that item to the towers inventory
                                    for (int j = 0; j < outputInventories[i].items.Count; j++)
                                    {
                                        tower.inv.addItem(outputInventories[i].items[j], outputInventories[i].quantity[j]);
                                        outputInventories[i].items.RemoveAt(j);
                                        outputInventories[i].quantity.RemoveAt(j);
                                    }
                                }
                            }
                        }
                    }
                    sprites.Clear();      
                }
            }
        }
        public void link(Item item, int quantity, Vector2 output)
        {
            ConveyorInv.addItem(item, quantity);
            // set the conveyor to "Busy" so it doesnt suck in items until it has finished moving the items it already has
            state = ConveyorState.Inputting;
            inputDirections.Add(new Vector3(output.x, output.y));
            sprites.Add(item.icon);
            timeElapsed = 0;
            firstframe = true;
        }

        // NOTE: the order in which each function is called is ### Super Important ### do not play with this function unless you know what you are doing
        private void UpdateConveyorModel()
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

            // if the user has overridden any of the I/O controllers these new overridden values need to be taken into account
            CalculateTrueArmTypes();
            // based on the number of inputs and the visible arms, an algorithm is used to determine which arms are outputs
            OutputManagement();
            // now that the auto-generated outputs have been determined, we need to recalculate the true arm types, taking the new outputs into account
            CalculateTrueArmTypes();

            // update arrows on conveyors
            for (int i = 0; i < armTypes.Length; i++)
            {
                UpdateConveyorArrows(i);
            }
        }

        private void CalculateTrueArmTypes()
        {
            for(int i = 0; i < armTypes.Length;i++)
            {
                if(CustomArmTypes[i] != IOController.None)
                {
                    TrueArmTypes[i] = CustomArmTypes[i];
                }
                else
                {
                    TrueArmTypes[i] = armTypes[i];
                }
            }
        }

        /* algoritm that auto-magically decides which arms should be inputs and outputs
         *      How the Algoritm works:
         *          - when a conveyor connects to a tower that outputs items, the connecting arm is set to an input
         *          - if a conveyors neighbor is outputting items into the current conveyor, the arm that connects to the neighbor is labelled an input (their output = my input)
         *              - this is called "Conveyor Chaining"
         *          there are multiple types of conveyor that aid the program's decision making process
         *              - SConveyor - ("Straight" Conveyor with 2 arms and one input)
         *                  if there is only 2 active arms, and one is labelled an input, the other must be an output
         *              - YConveyor - (Conveyor with multiple inputs, but only 1 output)
         *                  like before, if there is only 3 active arms and  2 are labelled input, then the remaining arm must be an output
         *              - XConveyor - ( Conveyor with more than one output)
         *                  - there is a little more decision making for an X conveyor, as the program will have to decide which direction to send the items (which output do we use?)
         *                      - there are currently 2 modes:
         *                          - Split: items are split evenly between the 2 outputs
         *                          - Item Filter: an item filter is used to decide which item should go which direction.
         *                  - with an X conveyor, andything that is not an input will be labelled an output.
         */

        private void OutputManagement()
        {
            if (noOfInputs == 1 && visibleArms == 2)
                SConveyor();
            else if (noOfInputs == visibleArms - 1 && noOfInputs != 0)
                YConveyor();
            else if (noOfInputs < visibleArms - 1 && noOfInputs != 0)
                XConveyor();
            //else if (noOfInputs == 0)
            //{
            //    UnityInspectorDebugging = "NO Conveyor";
            //    Debug.Log("No conveyor");
            //    for (int i = 0; i < conveyorArms.Length; i++)
            //    {
            //        armTypes[i] = IOController.None;
            //    }
            //}
        }

        private void XConveyor()
        {
            UnityInspectorDebugging = "XConveyor";
            for (int i = 0; i < conveyorArms.Length; i++)
            {

                // if a conveyor arm is visible and NOT and input, make it an output
                if (TrueArmTypes[i] != IOController.Input)
                {
                    //Debug.Log("y conveyor");
                    if (conveyorArms[i].activeInHierarchy == true)
                    {

                        //Debug.Log("y conveyor active");
                        armTypes[i] = IOController.Output;
                    }
                }
            }
        }
        private void YConveyor()
        {
            UnityInspectorDebugging = "YConveyor";
            for (int i = 0; i < conveyorArms.Length; i++)
            {

                // if a conveyor arm is visible and NOT and input, make it an output
                if (TrueArmTypes[i] != IOController.Input)
                {
                    //Debug.Log("y conveyor");
                    if (conveyorArms[i].activeInHierarchy == true)
                    {

                        //Debug.Log("y conveyor active");
                        armTypes[i] = IOController.Output;
                    }
                }
            }
        }
        private void SConveyor()
        {
            UnityInspectorDebugging = "SConveyor";
            for (int i = 0; i < conveyorArms.Length;i++)
            {

                // if a conveyor arm is visible and NOT and input, make it an output
                if (TrueArmTypes[i] != IOController.Input)
                {
                    //Debug.Log("straight conveyor");
                    if (conveyorArms[i].activeInHierarchy == true)
                    {

                        //Debug.Log("straight conveyor active");
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
            if (TrueArmTypes[i] == IOController.None)
            {
                conveyorArms[i].GetComponentInChildren<SpriteRenderer>().enabled = false;
            }
            // if conveyor arm type set to anything other than none, show the arrow again
            else
            {   
                conveyorArms[i].GetComponentInChildren<SpriteRenderer>().enabled = true;

                // if controller arm type is "Output", the arrow will point away from the centre of the conveyor
                if (TrueArmTypes[i] == IOController.Output || TrueArmTypes[i] == IOController.OutputToTower)
                {
                    conveyorArms[i].GetComponentInChildren<SpriteRenderer>().gameObject.transform.localRotation = Quaternion.Euler(90, 90, 0);
                }

                // if controller arm type is "Input", the arrow will point towards the centre of the conveyor
                if (TrueArmTypes[i] == IOController.Input)
                {
                    conveyorArms[i].GetComponentInChildren<SpriteRenderer>().gameObject.transform.localRotation = Quaternion.Euler(90, 270, 0);
                }
            }
        }
        private void UpdateArmVisibility(GameObject arm, Vector2 cardinalDirection)
        {
            Tile t = Tile.Vector3ToTile(transform.position);
            arm.SetActive(CanConveyorConnect(t, cardinalDirection));
        }
        private bool CanConveyorConnect(Tile t, Vector2 cardinalDirection)
        {
            int armIndex = System.Array.IndexOf(cardinalDirections, cardinalDirection);
            armTypes[armIndex] = IOController.None;
            Tile t_neighbour = Tile.Vector3ToTile(transform.position + new Vector3(-cardinalDirection.x, 0, -cardinalDirection.y));


            // dont bother running any of the below code if there is no tower adjacent to the conveyor
            if (t_neighbour.towerObject == null) return false;

            // conveyor can connect to other conveyors
            if (t_neighbour.GetTower().TryGetComponent(out ConveyorManager conv))
            {
                ConveyorChaining(cardinalDirection, conv);
                visibleArms++;
                return true;
            }

            // conveyor can connect to mine
            if (t_neighbour.GetTower().TryGetComponent(out Mine m))
            {
                int index = System.Array.IndexOf(cardinalDirections, cardinalDirection);
                armTypes[armIndex] = IOController.Input;
                visibleArms++;
                return true;
            }

            //conveyor can connect to towers
            if (t_neighbour.GetTower().TryGetComponent(out Tower turrret))
            {
                int index = System.Array.IndexOf(cardinalDirections, cardinalDirection);
                armTypes[armIndex] = IOController.OutputToTower;
                visibleArms++;
                return true;
            }
            return false;
        }


        // the other conveyros output should be this conveyors input - makes sure all the inputs and outputs line up
        private void ConveyorChaining(Vector2 cardinalDirection, ConveyorManager otherConveyor)
        {
            Vector2 oppositeDirection = new Vector2(-cardinalDirection.x, -cardinalDirection.y);
            int theirIndex = System.Array.IndexOf(otherConveyor.cardinalDirections, oppositeDirection);
            int myIndex = System.Array.IndexOf(cardinalDirections, cardinalDirection);
            if (otherConveyor.TrueArmTypes[theirIndex] == IOController.Output)
            {
                armTypes[myIndex] = IOController.Input;
            }
        }
        // take a vector and return it's cardinal index (0 = up, 1 = down, 2 = left, 3 = right)
        private int GetCardinalDirection(Vector2 outputDirection)
        {
            for (int cardinal = 0; cardinal < cardinalDirections.Length; cardinal++)
            {
                if (outputDirection == cardinalDirections[cardinal])
                {
                    return cardinal;
                }
            }

            return -1;
        }
    }
}
