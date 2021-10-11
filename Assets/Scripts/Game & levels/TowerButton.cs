using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    public GameObject infoPanel;
    public string turretName;

    public Shop shop;

    private Button button;

    private void Start()
    {
        button = gameObject.GetComponent<Button>();
    }

    private void FixedUpdate()
    {
        if (shop.CanAfford(turretName))
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    public void ShowPanel ()
    {
        infoPanel.SetActive(true);
    }

    public void HidePanel ()
    {
        infoPanel.SetActive(false);
    }
}
