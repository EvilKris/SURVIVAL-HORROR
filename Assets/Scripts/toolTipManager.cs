using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toolTipManager : MonoBehaviour
{
    [SerializeField]
    private List<itemToolTipClass> itemToolTipGroup = new List<itemToolTipClass>();



    //private Dictionary<string, GameObject> myDoorsDic = new Dictionary<string, GameObject>();

    //TOOLTIPS
    private SimpleTooltipStyle dynamicTooltipStyle;
    private string tooltipText = "default";
    private STDynamicExample tooltipScript;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(itemToolTipGroup[0].text[0]);
       
        addToolTipDescriptionsToItems();
    }

    private void addToolTipDescriptionsToItems()
    {
        int count;
        count = 0;

        foreach (var obj in itemToolTipGroup)
        {
            //Debug.Log(obj.gameobject.name);
            var tooltipScript = obj.gameobject.AddComponent<SimpleTooltip>();
            var tooltip = obj.gameobject.GetComponent<SimpleTooltip>();
            tooltip.infoLeft = itemToolTipGroup[count].text[0];
            count++;
        }
    }


}
