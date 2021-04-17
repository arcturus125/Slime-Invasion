using ItemSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInventoryItem : MonoBehaviour
{
    Item i;
    int quantity;


    public void INIT(Item pItem, int pQuantity)
    {
        i = pItem;
        quantity = pQuantity;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Text>().text = "(" + quantity + ") " + i.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
