using UnityEngine;
using UnityEngine.EventSystems;

public class ItemCheckUnderOnClick : MonoBehaviour, IPointerDownHandler
{
    
    const float doubleClickTime = 0.5f;//Wish I could trivially plum this into the system's doubleclick time.
    private static GameObject LASTPOINTERDOWNOBJECT = null;//Need to record the last click event's object, so that double clicking doesn't occur when skipping across objects (avoid accidental double click). I tried looking through PointerEventData, but none of the values I expected came back with a record of the previously clicked/hovered object.
    public bool ddclicked = false;
    private UIItem item; 
    public void OnPointerDown(PointerEventData eventData)
    {
        item = GameObject.Find("SelectedItem").GetComponent<UIItem>();

        float timeSinceLastClick = Time.unscaledTime - eventData.clickTime;//Note use of unscaled time. I've tested with Time.timeScale = 0.0f; and this still works.
        bool bSameObjectClicked = LASTPOINTERDOWNOBJECT == eventData.pointerCurrentRaycast.gameObject;

        if (timeSinceLastClick < doubleClickTime && bSameObjectClicked)
        {
            // Debug.Log("DoubleClick pressed!");item.selectedItem.item.title
            ddclicked = true;

            if(item.selectedItem.item != null)
            { 

                Debug.Log(item.selectedItem.item.title);
                //item.putBackInSlot()
                item.tempUI.putBackInSlot();
            }
        }

        LASTPOINTERDOWNOBJECT = eventData.pointerCurrentRaycast.gameObject;
    }

}
