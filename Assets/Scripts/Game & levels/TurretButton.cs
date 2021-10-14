using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex < 4 )
        {
            return;
        }

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
