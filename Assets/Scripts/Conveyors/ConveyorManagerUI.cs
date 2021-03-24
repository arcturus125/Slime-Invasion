using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Conveyors
{
    public class ConveyorManagerUI : MonoBehaviour
    {
        public static ConveyorManager selectedConveyor = null;
        public static List<GameObject> destroyOnLoad = new List<GameObject>();

        [SerializeField]
        private GameObject conveyorManagerPanel;
        [SerializeField]
        private Dropdown conveyorType;

        [SerializeField]
        private ItemFilterUI[] filterAreas;
        [SerializeField]
        private RectTransform contentWindow;
        [SerializeField]
        private GameObject filtersPanel;
        [SerializeField]
        private GameObject splitText;


        bool showSubMenus;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            conveyorManagerPanel.SetActive(!(selectedConveyor == null));
            filtersPanel.SetActive(showSubMenus);
            splitText.SetActive(!showSubMenus);
            if (selectedConveyor)
            {
                if (showSubMenus)
                {
                    //Debug.Log("Upddating sub menus");
                    float offset = 0;
                    for (int i = 0; i < filterAreas.Length; i++)
                    {
                        if (selectedConveyor.armTypes[i] == ConveyorManager.IOController.Output)
                        {
                            filterAreas[i].gameObject.SetActive(true);
                            filterAreas[i].transform.position = contentWindow.position + new Vector3(filterAreas[i].gameObject.GetComponent<RectTransform>().sizeDelta.x / 2,
                                                                                                     offset - contentWindow.sizeDelta.y - filterAreas[i].gameObject.GetComponent<RectTransform>().sizeDelta.y / 2,
                                                                                                     0);
                            offset += filterAreas[i].effectiveOffset;
                        }
                        else
                            filterAreas[i].gameObject.SetActive(false);
                    }
                    contentWindow.sizeDelta = new Vector2(contentWindow.sizeDelta.x,
                                                           offset);
                }
            }
        }

        public void UpdateGUI()
        {
            
        }
        // used to reset the panel ready for new data to be generated
        public static void DestroyWindow()
        {
            foreach(GameObject temp in destroyOnLoad)
            {
                Destroy(temp);
            }
            destroyOnLoad.Clear();
        }

        public void ConveyorTypeChanged()
        {
            showSubMenus = conveyorType.value == 1;
        }
    }
}