using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventOneScript : MonoBehaviour
{
    private int eventPhase= 0;
    // look out the window
    void doEvent(int go)
    {
        eventPhase = go;
        Debug.Log("OK WORKING");

        var tooltip = GetComponent<SimpleTooltip>();
        tooltip.infoLeft = tooltip.itemToolTipGroup[tooltip.tipA].text[1];

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
