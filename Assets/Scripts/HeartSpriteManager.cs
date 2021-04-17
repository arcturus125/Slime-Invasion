using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HordeSurvivalGame
{
    public class HeartSpriteManager : MonoBehaviour
    {
        public Image hearts;
        public Sprite[] heartSprites;

        // Start is called before the first frame update
        void Start()
        {
            hearts.sprite = heartSprites[heartSprites.Length - 1];
        }
        private void Update()
        {
            if (PlayerResources.GetPlayerHealth() >= 0)
            {
                hearts.sprite = heartSprites[PlayerResources.GetPlayerHealth()]; //Currently in Update. There were issues with the function below, so for now, it's done here. //TODO: this.
            }
        }

        public void HeartsChanged()
        {
            //hearts.sprite = heartSprites[PlayerResources.GetPlayerHealth()];
        }
    }
}
