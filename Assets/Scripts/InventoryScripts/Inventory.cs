using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Inventory : MonoBehaviour {
    public List<Item> characterItems = new List<Item>();
    public ItemDatabase itemDatabase;
    public UIInventory inventoryUI;
    private Vector2 uiPosition; 
    private bool transDone;
    
    [SerializeField]
    private bool openInvMenu;
    [SerializeField]
    private Vector2 mousePos;

    private double uiTwentyPercent;
    [SerializeField]
    private GameObject itemOb;
    void Start()
    {
        uiPosition.x = inventoryUI.gameObject.transform.position.x;
        uiPosition.y = inventoryUI.gameObject.transform.position.y;
        transDone = true;
        openInvMenu = false;
        uiTwentyPercent= Screen.height - (0.2 * Screen.height);
        inventoryUI.gameObject.SetActive(false);
        
        GiveItem(1);
        GiveItem(0);
        GiveItem(1);
        GiveItem(2);
        
    }

    void Update()
    {
        mousePos = Input.mousePosition;
        
        

        if (openInvMenu==false)
        {
            //if (mousePos.y > uiTwentyPercent && mousePos.y < Screen.height)
            if(mousePos.y > 560)
            {
                openInvMenu = true;
                inventoryUI.gameObject.SetActive(true);

                if (inventoryUI.gameObject.activeSelf && transDone)
                {
                    transDone = false;
                    //DoMove(pos, time) 
                    inventoryUI.gameObject.transform.DOMoveY(uiPosition.y + 30, 0.1f).From().OnComplete(() => transDone = true);
                }
            }                           //    openInvMenu = true;
                //  StartCoroutine("WaitUntilMouseOffInvMenu");
        }

        if (mousePos.y < 560)
        {
            if(itemOb.GetComponent<UIItem>().selectedItem.item==null)
            { 
                openInvMenu = false;
                inventoryUI.gameObject.SetActive(false);
            }
        }


        /*
        if (Input.GetKeyDown(KeyCode.I))
        {
           // selectedItem.UpdateItem(this.item);
           // UpdateItem(null);
            inventoryUI.gameObject.SetActive(!inventoryUI.gameObject.activeSelf);
            Debug.Log("PAUSING");


            //Tween down from the top of the screen if active
            //inventoryUI.gameObject.transform.transform.DORestart();
            if (inventoryUI.gameObject.activeSelf && transDone)
            {
                transDone = false;
                //DoMove(pos, time) 
                inventoryUI.gameObject.transform.DOMoveY(uiPosition.y + 30, 0.1f).From().OnComplete(() => transDone=true);
            }
            //else
              //  inventoryUI.gameObject.transform.Translate(uiPosition);
        }*/
    }

    IEnumerator WaitUntilMouseOffInvMenu()
    {
        if (mousePos.y < uiTwentyPercent)
        {
            inventoryUI.gameObject.SetActive(false);

            yield return openInvMenu == false;
        }
    }

    public void GiveItem(int id)
    {
        Item itemToAdd = itemDatabase.GetItem(id);
        characterItems.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd);
        Debug.Log("Added item: " + itemToAdd.title);
    }

    public void GiveItem(string itemName)
    {
        Item itemToAdd = itemDatabase.GetItem(itemName);
        characterItems.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd);
        Debug.Log("Added item: " + itemToAdd.title);
    }

    public Item CheckForItem(int id)
    {
        return characterItems.Find(item => item.id == id);
    }

    public void RemoveItem(int id)
    {
        Item itemToRemove = CheckForItem(id);
        if (itemToRemove != null)
        {
            characterItems.Remove(itemToRemove);
            inventoryUI.RemoveItem(itemToRemove);
            Debug.Log("Removed item: " + itemToRemove.title);
        }
    }
}
