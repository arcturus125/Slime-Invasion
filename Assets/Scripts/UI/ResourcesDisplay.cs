using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HordeSurvivalGame
{
    public class ResourcesDisplay : MonoBehaviour
    {
        public Text moneyText;
        public Text ironText;
        public Text coalText;
        public Text leadText;

        void Update()
        {
            moneyText.text = PlayerResources.GetMoney().ToString();
            ironText.text = PlayerResources.GetIron().ToString();
            coalText.text = PlayerResources.GetCoal().ToString();
            leadText.text = PlayerResources.GetLead().ToString();
        }
    }
    
}

