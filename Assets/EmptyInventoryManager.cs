using HordeSurvivalGame;
using ItemSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmptyInventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonPanel;
    [SerializeField]
    private Button giveButton;
    [SerializeField]
    private Text buttonText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Item i = TowerConfigManager.selectedTower.recievableItem;
        int num = PlayerResources.GetInventoryItemCount(i);

        if(num > 0)
        {
            buttonPanel.SetActive(true);
            buttonText.text = "Deposit " + i.name;
        }
        else
        {
            buttonPanel.SetActive(false);
        }
    }

    public void DepositButtonClicked()
    {
        Item i = TowerConfigManager.selectedTower.recievableItem;
        PlayerResources.playerInv.removeItem(i,1);
        TowerConfigManager.selectedTower.inv.addItem(i,1);
    }
}
