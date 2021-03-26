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
            for (int i = 0; i < ConveyorManagerUI.selectedConveyor.itemFilters[itemFilterIndex].Count; i++)
            {
                // move the UI
                Vector3 oldPosition = plus.GetComponent<RectTransform>().position;
                plus.GetComponent<RectTransform>().position += new Vector3(0, -30, 0);
                effectiveOffset -= 30;
                ItemFilter filterObject = Instantiate(filter, this.transform);
                filterObject.gameObject.transform.position = oldPosition;
                filterObject.parentUIManager = this;
                ConveyorManagerUI.destroyOnLoad.Add(filterObject.gameObject);
                filterObject.SetItem(ConveyorManagerUI.selectedConveyor.itemFilters[itemFilterIndex][i]);
                numberOfFilters++;
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
