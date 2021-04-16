using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ItemSystem;

namespace HordeSurvivalGame
{
    class PlayerResources
    {
        // used to reference specific item types in inventory
        public static Item iron;
        public static Item coal;
        public static Item lead;


        private static int playerMoney = 0;
        private static Inventory playerInv = new Inventory();

        // return the amount of money the player has
        public static int GetMoney()
        {
            return playerMoney;
        }
        // return the amount of iron in the players inventory
        public static int GetIron()
        {
            return GetInventoryItemCount(iron);
        }
        // return the amount of coal in the players inventory
        public static int GetCoal()
        {
            return GetInventoryItemCount(coal);
        }
        // return the amount of lead in the players inventory
        public static int GetLead()
        {
            return GetInventoryItemCount(lead);
        }


        public static void AddMoney(int amount)
        {
            playerMoney += amount;
        }
        public static void AddIron(int amount)
        {
            UpdateInventory(iron, amount);
        }
        public static void AddCoal(int amount)
        {
            UpdateInventory(coal, amount);
        }
        public static void AddLead(int amount)
        {
            UpdateInventory(lead, amount);
        }

        private static void UpdateInventory(Item i , int count)
        {
            if (count < 0)
            {
                playerInv.removeItem(i, count);
            }
            else
            {
                playerInv.addItem(i, count);
            }
        }
        private static int GetInventoryItemCount(Item i)
        {
            if (playerInv.IsItemInInv(i))
            {
                int index = playerInv.getItemIndex(i);
                return playerInv.quantity[index];
            }
            return 0;
        }

    }
}

