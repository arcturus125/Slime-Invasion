using HordeSurvivalGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using ItemSystem;

namespace Conveyors {
    public class ItemFilter : MonoBehaviour
    {
        [SerializeField]
        private Item[] dropdownItems;

        [HideInInspector]
        public ItemFilterUI parentUIManager;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDestroy()
        {
            parentUIManager.FilterDestroyed();
        }

        public void OnDropdownChanged()
        {
            int index = this.GetComponentInChildren<Dropdown>().value;
            Debug.Log(index);
        }
    }
}