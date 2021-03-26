using HordeSurvivalGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using ItemSystem;
using System;

namespace Conveyors {
    public class ItemFilter : MonoBehaviour
    {
        [SerializeField]
        private Item[] dropdownItems;

        [HideInInspector]
        public ItemFilterUI parentUIManager;

        private int armIndex;
        private int itemIndexInItemFilter;


        private void Update()
        {
            //if (ConveyorManagerUI.selectedConveyor)
            //{
            //    foreach (Item i in ConveyorManagerUI.selectedConveyor.itemFilters[armIndex])
            //    {
            //        Debug.Log(i.name);
            //    }
            //    Debug.Log("---------------");
            //}
        }
        private void OnDestroy()
        {
            parentUIManager.FilterDestroyed();
        }

        public void OnDropdownChanged()
        {
            int index = this.GetComponentInChildren<Dropdown>().value;

            ConveyorManagerUI.selectedConveyor.itemFilters[armIndex][itemIndexInItemFilter] = dropdownItems[index];
            Debug.Log("CXhanging "+itemIndexInItemFilter);
        }

        internal void INIT(int itemFilterIndex, int itemIndex)
        {
            Debug.Log(itemIndex);
            itemIndexInItemFilter = itemIndex;
            armIndex = itemFilterIndex;

            int index = this.GetComponentInChildren<Dropdown>().value;
            ConveyorManagerUI.selectedConveyor.itemFilters[itemFilterIndex].Add(dropdownItems[index]);
        }
        internal void SetItem(Item i)
        {
            this.GetComponentInChildren<Dropdown>().value = System.Array.IndexOf(dropdownItems, i);
            Debug.Log("Setting item: " + i.itemName);
        }
    }
}