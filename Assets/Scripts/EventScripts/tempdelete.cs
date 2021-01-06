using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempdelete : MonoBehaviour
{

    public GameObject ItemPrefabToUse;
    public GameObject itemToExamine;
    // Start is called before the first frame update
    void Start()
    {
        if (ItemPrefabToUse == null)
        {
            Debug.Log("Choose a prefab");
            return;
        }

        print("HI");
        GameObject copy = (GameObject)Instantiate(ItemPrefabToUse);
        copy.GetComponent<objectInspector>().obToUse = itemToExamine;
        copy.GetComponent<objectInspector>().rootPrefabDelete = copy;


    }

}