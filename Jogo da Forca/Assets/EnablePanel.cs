using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePanel : MonoBehaviour
{

    public void Enable(GameObject panelToEnable)
    {
        if (panelToEnable.activeSelf == false)
        {
            panelToEnable.SetActive(true);
        }
    }

    public void Disable(GameObject panelToDisable)
    {
        panelToDisable.SetActive(false);
    }
}
