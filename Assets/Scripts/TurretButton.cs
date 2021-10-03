﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretButton : MonoBehaviour
{
    private GameObject infoPanel;

    public Shop shop;

    [SerializeField] private string towerName;
    private Button button;

    private bool infoPanelVisible;

    //public PlayerStats playerStats;

    private void Start()
    {
        button = GetComponent<Button>(); 

        infoPanel = transform.GetChild(1).gameObject;
        Debug.Log(infoPanel);
    }

    private void Update()
    {
        bool afford = shop.CanAfford(towerName);
        
        if (afford)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    public void ToggleInfoPanel ()
    {        
        if (!infoPanelVisible)
        {
            infoPanel.SetActive(true);
        }
        else
        {
            infoPanel.SetActive(false);
        }

        infoPanelVisible = !infoPanelVisible;
    }

    /*private void OnMouseEnter()
    {
        infoPanel.SetActive(true);
        Debug.Log("enabled");
    }

    private void OnMouseExit()
    {
        infoPanel.SetActive(false);
        Debug.Log("disabled");
    }*/
}
