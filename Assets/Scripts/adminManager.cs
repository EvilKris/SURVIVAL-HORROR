using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adminManager : MonoBehaviour
{
    public GameObject itemMenuPrefab;

    void Start()
    {
        itemScripts();    
    }
       
    private void itemScripts()
    {
        //DOORS
        var objects = GameObject.FindGameObjectsWithTag("item");
        int count;
        count = 0;

        // STDynamicExample tooltipScript;
        //ADD THE SCRIPT
        foreach (var obj in objects)
        {
            obj.AddComponent<itemSFX>();
           
            /*
            var tooltipScript = obj.AddComponent<SimpleTooltip>();
            var tooltip = obj.GetComponent<SimpleTooltip>();
            tooltip.infoLeft = itemToolTipGroup[count].text[0];
            */
            count++;
           
        }

    }



}
