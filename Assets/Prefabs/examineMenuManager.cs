using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class examineMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject refTop;

    // If Leave() and Take() names changed, will also need to update the buttons on the prefab accordingly
    public void Leave()
    {
        //Just go straight back to the menu
       // print("LEAVE");

        DestroyThis();
    }

    public void Take()
    {
        //go back but add to inventory

        //print("TAKE");
        DestroyThis();
    }

    public void DestroyThis()
    {
        /*
        objectInspector objcomp = gameObject.GetComponentInParent(typeof(objectInspector)) as objectInspector;

        if (objcomp != null)
            objcomp.mainCamera.enabled = true;

        //Trash the prefab
        Debug.Log(objcomp.rootPrefabDelete.name);*/

        
       
        refTop.GetComponent<objectInspector>().obCamera.enabled = false;
        refTop.GetComponent<objectInspector>().mainCamera.enabled = true;
        Destroy(refTop);

        tankControls refvar = (tankControls)FindObjectOfType(typeof(tankControls));

        //bug is here
        refvar.examiningItem = false;
    }

}
