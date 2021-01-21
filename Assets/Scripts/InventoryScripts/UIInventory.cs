using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour {
    public List<UIItem> uiItems = new List<UIItem>();
    public GameObject slotPrefab;
    public Transform slotPanel;

    //Worst fucking third-party code I've ever used but I got in too deep to abandon it
    //puts mouse listeners on every single slot (inc unused ones). 
    void Awake()
    {
        for(int i = 0; i < 10; i++)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel);
            uiItems.Add(instance.GetComponentInChildren<UIItem>());
        }
    }

    public void UpdateSlot(int slot, Item item)
    {
        uiItems[slot].UpdateItem(item);
    }

    public void AddNewItem(Item item)
    {
        UpdateSlot(uiItems.FindIndex(i=> i.item == null), item);
    }

    public void RemoveItem(Item item)
    {
        UpdateSlot(uiItems.FindIndex(i=> i.item == item), null);
    }
}
