using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugOverlayControl : MonoBehaviour
{
    [SerializeField] private GameObject generalInfo;
    [SerializeField] private GameObject devInfo;

    private bool generalInfoShown = false;
    private bool devInfoShown = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            ToggleGenInfo();
        }

        if (Input.GetKeyDown(KeyCode.F3) && Input.GetKeyDown(KeyCode.LeftShift))
        {
            ToggleDevInfo();
        }
    }

    void ToggleGenInfo ()
    {
        if (generalInfoShown)
        {
            generalInfo.SetActive(false);
            devInfo.SetActive(false);

            devInfoShown = !devInfoShown;
        }
        else
        {
            generalInfo.SetActive(true);
        }

        generalInfoShown = !generalInfoShown;
    }

    void ToggleDevInfo ()
    {
        if (devInfoShown)
        {
            devInfo.SetActive(false);
        }
        else
        {
            devInfo.SetActive(true);
        }

        devInfoShown = !devInfoShown;
    }
}
