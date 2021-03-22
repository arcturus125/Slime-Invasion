using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HordeSurvivalGame
{
    public class Inventory
    {
        public List<Item> items;
        public List<int> quantity;

        public Inventory()
        {
            items = new List<Item>();
            quantity = new List<int>();
        }
        public void addItem(Item item, int quantityToAdd)
        {
            if(IsItemInInv(item))
            {
                quantity[getItemIndex(item)] += quantityToAdd;
            }
            else
            {
                items.Add(item);
                quantity.Add(quantityToAdd);
            }
        }
        public bool removeItem(Item item, int quantityToRemove = 1)
        {
            if (IsItemInInv(item))
            {
                if (quantity[getItemIndex(item)] > quantityToRemove)
                {
                    quantity[getItemIndex(item)] -= quantityToRemove;
                    return true;
                }
                else if (quantity[getItemIndex(item)] == quantityToRemove)
                {
                    quantity.RemoveAt(getItemIndex(item));
                    items.RemoveAt(getItemIndex(item));
                    return true;
                }
                // not enough items in inventory
                else return false;
            }
            // trying to remove an item from inventory that is not there
            else return false;
        }
        public bool IsItemInInv(Item toSearchFor)
        {
            foreach(Item i in items)
            {
                if(i == toSearchFor)
                {
                    return true;
                }
            }
            return false;
        }
        public int getItemIndex(Item toSearchFor)
        {
            for(int i = 0; i< items.Count; i ++)
            {
                Item item = items[i];
                if (item == toSearchFor)
                {
                    return i;
                }
            }
            return -1; // -1 rougue value
        }
    }
    [CreateAssetMenu(fileName = "Data", menuName = "HordeSurvival/Item", order = 1)]
    public class Item : ScriptableObject
    {
        public string itemName;
        public Sprite icon;
    }
}
