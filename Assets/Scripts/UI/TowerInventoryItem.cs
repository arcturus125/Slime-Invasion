using HordeSurvivalGame;
using ItemSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInventoryItem : MonoBehaviour
{
    Item i;
    int quantity;

    [SerializeField]
    private GameObject confirmationPanel;
    [SerializeField]
    private Text fieldText;
    [SerializeField]
    private Text placeholderText;
    [SerializeField]
    private Button takeButton;
    [SerializeField]
    private Button depositButton;
    [SerializeField]
    private Button confirmButton;

    static bool take = false;
    static bool deposit = false;
    int amount;


    public void INIT(Item pItem, int pQuantity)
    {
        i = pItem;
        quantity = pQuantity;
        this.GetComponent<Text>().text = "(" + quantity + ") " + i.name;
    }

    // Start is called before the first frame update
    void Start()
    {
        confirmationPanel.SetActive(false);
        this.GetComponent<Text>().text = "(" + quantity + ") " + i.name;
    }

    // Update is called once per frame
    void Update()
    {
        takeButton.interactable = !deposit;
        depositButton.interactable = !take;

        confirmationPanel.SetActive(take || deposit);
        
    }

    public void TakeButtonClicked()
    {
        take = !take;
        fieldText.text = "";
    }
    public void DepositButtonCLicked()
    {
        deposit = !deposit;
        fieldText.text = "";
    }
    public void ConfirmButtonClicked()
    {
        if (take) Take(amount);
        if (deposit) Deposit(amount);

        take = false;
        deposit = false;
    }
    public void OnTextFieldChanged()
    {
        if (confirmButton.isActiveAndEnabled) // error catching
        {
            if (int.TryParse(fieldText.text, out amount))
            {
                if (deposit)
                {
                    if (amount <= PlayerResources.GetInventoryItemCount(i) && amount > 0)
                    {
                        confirmButton.interactable = true;
                    }
                    else
                    {
                        confirmButton.interactable = false;
                    }
                }
                if(take)
                {
                    if (amount <= quantity && amount > 0)
                    {
                        confirmButton.interactable = true;
                    }
                    else
                    {
                        confirmButton.interactable = false;
                    }
                }

            }
            else
            {
                confirmButton.interactable = false;
            }
        }
    }


    private void Take(int amount)
    {
        PlayerResources.playerInv.addItem(i, amount);
        TowerConfigManager.selectedTower.inv.removeItem(i, amount);
    }
    private void Deposit(int amount)
    {
        PlayerResources.playerInv.removeItem(i, amount);
        TowerConfigManager.selectedTower.inv.addItem(i, amount);
    }

}
