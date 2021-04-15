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

        public int armIndex;
        public int itemIndexInItemFilter;


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

            if(loop)
                ConveyorManagerUI.selectedConveyor.itemFilters[armIndex][itemIndexInItemFilter] = dropdownItems[index];
            loop = false;                                                                                 //
            Debug.Log("CXhanging "+itemIndexInItemFilter);
        }

        internal void INIT(int itemFilterIndex, int itemIndex)
        {
            //Debug.Log(itemIndex);
            itemIndexInItemFilter = itemIndex;
            armIndex = itemFilterIndex;

            int index = this.GetComponentInChildren<Dropdown>().value;
            ConveyorManagerUI.selectedConveyor.itemFilters[itemFilterIndex].Add(dropdownItems[index]);
        }
        bool loop = true;
        internal void SetItem(Item i)
        {
            loop = false;
            this.GetComponentInChildren<Dropdown>().value = System.Array.IndexOf(dropdownItems, i);
            bool canBeEdited = ConveyorManagerUI.selectedConveyor.canItemFiterBeEdited[armIndex];
            if (!canBeEdited) this.GetComponentInChildren<Dropdown>().interactable = false;
            else this.GetComponentInChildren<Dropdown>().interactable = true;
            Debug.Log("Setting item: " + i.itemName);
        }
    }
}