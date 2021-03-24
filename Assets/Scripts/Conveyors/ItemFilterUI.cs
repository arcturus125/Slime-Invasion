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


        void Awake()
        {
            defaultPosition = GetComponent<RectTransform>().localPosition;
        }

        // Update is called once per frame
        void Update()
        {

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
        }
        public void FilterDestroyed()
        {
            plus.GetComponent<RectTransform>().position += new Vector3(0, 30, 0);
            effectiveOffset += 30;
        }
    }
}
