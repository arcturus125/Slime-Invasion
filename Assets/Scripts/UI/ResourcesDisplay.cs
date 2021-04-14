using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HordeSurvivalGame
{
    public class ResourcesDisplay : MonoBehaviour
    {
        public Text moneyText;

        void Update()
        {
            moneyText.text = "Money- " + PlayerResources.GetMoney();
        }
    }
    
}

