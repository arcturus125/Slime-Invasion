using UnityEngine;
using UnityEditor;
using Conveyors;
using ItemSystem;

[CustomEditor(typeof(ConveyorManager))]
public class InventoryEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ConveyorManager conveyor = (ConveyorManager)target;

        string text = "####################################################\n           INVENTORY\n####################################################\n";

        for(int i = 0; i <  conveyor.ConveyorInv.items.Count;i++)
        {
            Item item = conveyor.ConveyorInv.items[i];
            int quantity = conveyor.ConveyorInv.quantity[i];

            text += (item.name + ": " + quantity);
        }
        if (conveyor.ConveyorInv.items.Count == 0) text += "Inv empty";


        GUILayout.Label(text);
    }
}
