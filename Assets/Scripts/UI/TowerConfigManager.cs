using System.Collections;
using System.Collections.Generic;
using Towers;
using UnityEngine;

public class TowerConfigManager : MonoBehaviour
{
    public static Tower selectedTower;
    [SerializeField]
    private GameObject towerConfigPanel;
    [SerializeField]
    private TowerInventoryItem prefab;
    [SerializeField]
    private RectTransform contentWindow;
    [SerializeField]
    private GameObject scrollView;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedTower == null)
            towerConfigPanel.SetActive(false);
        else
            towerConfigPanel.SetActive(true);
    }
}
