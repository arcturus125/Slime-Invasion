using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HordeSurvivalGame
{
    class PlayerResources
    {
        //Member Variables.
        private static int playerMoney = 0;
        private static int playerIron = 0;
        private static int playerCoal = 0;
        private static int playerLead = 0;

        //Getters.
        public static int GetMoney()
        {
            return playerMoney;
        }
        public static int GetIron()
        {
            return playerIron;
        }
        public static int GetCoal()
        {
            return playerCoal;
        }
        public static int GetLead()
        {
            return playerLead;
        }

        //Setters.
        public static void IncrementMoney(int value)
        {
            playerMoney += value;
        }
        public static void DecrementMoney(int value)
        {
            playerMoney -= value;
        }
        public static void IncrementIron(int value)
        {
            playerIron += value;
        }
        public static void DecrementIron(int value)
        {
            playerIron -= value;
        }
        public static void IncrementCoal(int value)
        {
            playerCoal += value;
        }
        public static void DecrementCoal(int value)
        {
            playerCoal -= value;
        }
        public static void IncrementLead(int value)
        {
            playerLead += value;
        }
        public static void DecrementLead(int value)
        {
            playerLead -= value;
        }

    }
}

