using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Conveyors
{
    public class ConveyorManagerUI : MonoBehaviour
    {

        private static ConveyorManagerUI singleton;
        public static ConveyorManager selectedConveyor = null;
        public static List<GameObject> destroyOnLoad = new List<GameObject>();

        // headings
        [SerializeField]
        private GameObject conveyorManagerPanel;
        [SerializeField]
        private Dropdown conveyorType;

        // item filters
        [SerializeField]
        private ItemFilterUI[] filterAreas;
        [SerializeField]
        private RectTransform contentWindow;
        [SerializeField]
        private GameObject filtersPanel;
        [SerializeField]
        private GameObject splitText;

        // Input/Output editor
        [SerializeField]
        private GameObject IOEditorPanel;
        [SerializeField]
        private Button[] UIArms;


        bool showSubMenus;
        bool nextFrameLoadExistingData = false;

        // Start is called before the first frame update
        void Start()
        {
            singleton = this;
            IOEditorPanel.SetActive(false);
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
                            if (nextFrameLoadExistingData)
                            {
                                filterAreas[i].LoadExistingData();
                            }

                            // update Ui positions
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

                    nextFrameLoadExistingData = false;
                }

                if (IOEditorPanel.activeInHierarchy)
                {
                    UpdateEditPanel();
                }
            }
        }

        // used to reset the panel ready for new data to be generated
        public static void DestroyWindow()
        {
            foreach(GameObject temp in destroyOnLoad)
            {
                Destroy(temp);
            }
            destroyOnLoad.Clear();
            singleton.IOEditorPanel.SetActive(false);
            singleton.conveyorType.value = 0;
        }

        public void ConveyorTypeChanged()
        {
            showSubMenus = conveyorType.value == 1;
            nextFrameLoadExistingData = true;
        }

        public void RemoveButtonClicked()
        {
            Destroy(selectedConveyor.gameObject);
        }
        public void EditButtonClicked()
        {
            IOEditorPanel.SetActive(!IOEditorPanel.activeSelf);
        }


        public void OverrideButtonClicked(int buttonIndex)
        {
            if(selectedConveyor.CustomArmTypes[buttonIndex] == ConveyorManager.IOController.None)
            {
                selectedConveyor.CustomArmTypes[buttonIndex] = ConveyorManager.IOController.Input;
            }
            else if (selectedConveyor.CustomArmTypes[buttonIndex] == ConveyorManager.IOController.Input)
            {
                selectedConveyor.CustomArmTypes[buttonIndex] = ConveyorManager.IOController.Output;
            }
            else if (selectedConveyor.CustomArmTypes[buttonIndex] == ConveyorManager.IOController.Output)
            {
                selectedConveyor.CustomArmTypes[buttonIndex] = ConveyorManager.IOController.None;
            }
        }

        public void UpdateEditPanel()
        {
            for (int i = 0; i < selectedConveyor.conveyorArms.Length; i++)
            {
                if (!selectedConveyor.conveyorArms[i].activeInHierarchy)
                {
                    UIArms[i].interactable = false;
                    Debug.Log(i + " " + UIArms[i].interactable);
                    UIArms[i].GetComponentsInChildren<Image>()[1].enabled = false;
                }
                else
                {
                    UIArms[i].interactable = true;
                    UIArms[i].GetComponentsInChildren<Image>()[1].enabled = true;
                    if (selectedConveyor.TrueArmTypes[i] == ConveyorManager.IOController.Output)
                    {
                        UIArms[i].GetComponentsInChildren<Image>()[1].enabled = true;
                        UIArms[i].GetComponentsInChildren<RectTransform>()[1].localRotation = Quaternion.Euler(0, 0, 0);
                    }
                    else if (selectedConveyor.TrueArmTypes[i] == ConveyorManager.IOController.Input)
                    {
                        UIArms[i].GetComponentsInChildren<Image>()[1].enabled = true;
                        UIArms[i].GetComponentsInChildren<RectTransform>()[1].localRotation = Quaternion.Euler(0, 0, 180);
                    }
                    else UIArms[i].GetComponentsInChildren<Image>()[1].enabled = false;


                }

                if (selectedConveyor.CustomArmTypes[i] != ConveyorManager.IOController.None)
                    UIArms[i].GetComponentsInChildren<Image>()[1].color = Color.red;
                else
                    UIArms[i].GetComponentsInChildren<Image>()[1].color = Color.white;
            }
        }
    }
}