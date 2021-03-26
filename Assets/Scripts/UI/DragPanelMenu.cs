using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragPanelMenu : MonoBehaviour
{

    public GameObject dragPanel;


    public void ToggleDragMenuVisibility()
    {
        dragPanel.SetActive( !dragPanel.activeSelf);
    }

    // Start is called before the first frame update
    void Start()
    {
        dragPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
