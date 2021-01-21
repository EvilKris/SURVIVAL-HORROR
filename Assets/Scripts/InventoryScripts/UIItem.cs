using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler {
    public Item item;
    public UIItem tempUI;
    private Image spriteImage;
    public UIItem selectedItem;
    private Tooltip tooltip;
    private bool doubleClicked; 


    void Awake()
    {        
        selectedItem = GameObject.Find("SelectedItem").GetComponent<UIItem>();
        tooltip = GameObject.Find("Tooltip").GetComponent<Tooltip>();

        spriteImage = GetComponent<Image>();
         UpdateItem(null);
    }

    public void UpdateItem(Item item)
    {
        Debug.Log(this.item);
        //places an icon in the inventory slot
        this.item = item;
        if (this.item != null)
        {
            spriteImage.color = Color.white;
            spriteImage.sprite = item.icon;
        }
        else
        {
            //if the slot has been clicked on then clear the image while
            //the object is selected
            spriteImage.color = Color.clear;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {    
            
        if (this.item != null)
        {
            if (selectedItem.item != null)
            {
                //SWITCH FULL ONE FOR ANOTHER
                Debug.Log("1");
                //if there's no selected item then clone the original and use FollowMouse
                Item clone = new Item(selectedItem.item);
                selectedItem.UpdateItem(this.item);
                UpdateItem(clone);
            }
            else
            {
                //TAKE ONE 
                Debug.Log("2");

                selectedItem.tempUI = this;

                selectedItem.UpdateItem(this.item);
                UpdateItem(null);
            }
        }
        else if (selectedItem.item != null)
        {
            //PUT IT BACK IN EMPTY 
            Debug.Log("3");
            //UpdateItem(selectedItem.item);
            //selectedItem.UpdateItem(null);
            putBackInSlot();
            
        }
    }

    public void putBackInSlot()
    {
        UpdateItem(selectedItem.item);
        selectedItem.UpdateItem(null);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //loads up the tooltip if the mouse goes over the icon in the inventory panel
       if (this.item != null)
       {
            tooltip.GenerateTooltip(this.item);
       }        
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //removes the tooltip if the mouse isn't over the inventory icon housing
        tooltip.gameObject.SetActive(false);
    }

   

}
