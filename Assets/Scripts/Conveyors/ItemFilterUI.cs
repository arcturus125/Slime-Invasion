using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Conveyors
{
    public class ItemFilterUI : MonoBehaviour
    {
        [SerializeField]
        private Button plus;
        [SerializeField]
        private ItemFilter filter;
        [SerializeField]
        private int itemFilterIndex;

        public float effectiveOffset = -60;
        public Vector3 defaultPosition;

        public int numberOfFilters = 0;


        void Awake()
        {
            defaultPosition = GetComponent<RectTransform>().localPosition;
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void LoadExistingData()
        {
            if (ConveyorManagerUI.selectedConveyor)
            {
                if (ConveyorManagerUI.selectedConveyor.itemFilters[itemFilterIndex].Count > 0)
                {
                    Debug.Log("Item Filter already exists, loading existing filters into the UI");
                    loadExistingData();
                }
            }
        }

        private void loadExistingData()
        {
            // for all the filters on a specific arm
            int loops = ConveyorManagerUI.selectedConveyor.itemFilters[itemFilterIndex].Count;
            for (int i = 0; i < loops; i++)
            {
                // move the UI
                Vector3 oldPosition = plus.GetComponent<RectTransform>().position;
                plus.GetComponent<RectTransform>().position += new Vector3(0, -30, 0);
                effectiveOffset -= 30;
                ItemFilter filterObject = Instantiate(filter, this.transform);
                filterObject.gameObject.transform.position = oldPosition;
                filterObject.parentUIManager = this;
                filterObject.armIndex = itemFilterIndex;
                filterObject.itemIndexInItemFilter = numberOfFilters;
                ConveyorManagerUI.destroyOnLoad.Add(filterObject.gameObject);
                filterObject.SetItem(ConveyorManagerUI.selectedConveyor.itemFilters[itemFilterIndex][i]);
                numberOfFilters++;


                bool canBeEdited = ConveyorManagerUI.selectedConveyor.canItemFiterBeEdited[itemFilterIndex];
                if (!canBeEdited) plus.interactable = false;
                else plus.interactable = true;
            }
        }
        public void PlusButtonClicked()
        {
            Vector3 oldPosition = plus.GetComponent<RectTransform>().position;
            plus.GetComponent<RectTransform>().position += new Vector3(0, -30, 0);
            effectiveOffset -= 30;
            ItemFilter filterObject = Instantiate(filter, this.transform);
            filterObject.gameObject.transform.position = oldPosition;
            filterObject.parentUIManager = this;
            ConveyorManagerUI.destroyOnLoad.Add(filterObject.gameObject);

            filterObject.INIT(itemFilterIndex, numberOfFilters);
            numberOfFilters++;
        }

        public void FilterDestroyed()
        {
            plus.GetComponent<RectTransform>().position += new Vector3(0, 30, 0);
            effectiveOffset += 30;
            numberOfFilters--;
        }
    }
}
