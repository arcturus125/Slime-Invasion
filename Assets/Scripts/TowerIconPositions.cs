using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerIconPositions : MonoBehaviour
{
    public GameObject[] towerIcons;

    void Awake()
    {
        const float PERCENT = 10.0f; //The percent gap of the screen the icons will be placed apart.

        float screenWidth = Screen.width;
        float leftSide = 0.0f - (screenWidth / 2); //The left side of the screen.
        float percentGap = screenWidth / PERCENT; //X% of the screens width.
        float currentPercentGap = percentGap; //Counts how much to add to the left side of the screen to place the next icon.

        for (int i = 0; i < towerIcons.Length; i++) //Loops through icons.
        {
            float percentFromLeft = leftSide + currentPercentGap; //Works out the pos X position.
            towerIcons[i].transform.GetComponent<RectTransform>().transform.localPosition = new Vector3(percentFromLeft, 0.0f, 0.0f); //Sets position.
            currentPercentGap += percentGap; //Changes position for next icon.
        }
        
    }
    
}
