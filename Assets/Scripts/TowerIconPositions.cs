using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerIconPositions : MonoBehaviour
{
    public GameObject[] towerIcons;

    void Awake()
    {

        float screenWidth = Screen.width;
        float leftSide = 0.0f - (screenWidth / 2);

        float percentGap = screenWidth / 10.0f;

        float currentPercentGap = percentGap;

        for (int i = 0; i < towerIcons.Length; i++) //Loops through icons.
        {
            float percentFromLeft = leftSide + currentPercentGap; //Works out the pos X position.
            //towerIcons[i].transform.GetComponent<RectTransform>().rect.Set(percentFromLeft, 0.0f, 100.0f, 100.0f); //Sets the position.
            towerIcons[i].transform.GetComponent<RectTransform>().transform.localPosition = new Vector3(percentFromLeft, 0.0f, 0.0f);
            currentPercentGap += percentGap; //Changes position for next icon.
        }
        
    }
    
}
