using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventTwoScript : MonoBehaviour
{
    
    private int eventPhase = 0;
    


    void doEvent(int go)
    {
        Debug.Log("GSDASD");
        adminManager tempObject = (adminManager)FindObjectOfType(typeof(adminManager));
                
                eventPhase = go;
        
       // Debug.Log("OK WORKING");
        
        

        if (tempObject != null)
        {
            //If we found the object , get the Canvas component from it.

            //get ItemPrefab
           
            GameObject copy = (GameObject)Instantiate(tempObject.itemMenuPrefab);
            
            copy.GetComponent<objectInspector>().obToUse = this.gameObject;
            copy.GetComponent<objectInspector>().rootPrefabDelete = copy;

            /*
            tempObject.GetComponent<PauseMenu>().pauseMenuUI.SetActive(true);

            clone = this.gameObject;
            gameObject.layer = 9; //LIGHTS LAYER
            clone.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 0.5f;
            clone.transform.parent = Camera.main.transform;


            // Add the light component

            GameObject lightGameObject = new GameObject("The Light");
            Light lightComp = lightGameObject.AddComponent<Light>();
            lightComp.color = Color.green;
            lightGameObject.transform.position=Camera.main.transform.position + Camera.main.transform.forward * 0.5f;
            lightGameObject.GetComponent<Light>().cullingMask = 1 << 9;
            lightGameObject.transform.parent = clone.transform;

            // Instantiate(this.gameObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            //clone.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            // Destroy(clone.GetComponent<eventTwoScript>());
            
            if (EscCan == null)
            {
                Debug.Log("Could not locate Canvas component on " + tempObject.name);
            }

            */
            eventPhase = 1;
        }
        else
        {
            Debug.Log("Cannot find PauseMenuCanvas");
        }
        //PauseGame();
        //var tooltip = GetComponent<SimpleTooltip>();
        //tooltip.infoLeft = tooltip.itemToolTipGroup[tooltip.tipA].text[1];
        //Destroy(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        if(eventPhase==1)
        {
           // clone.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 0.5f;
           // clone.transform.rotation = new Quaternion(0.0f, Camera.main.transform.rotation.y, 0.0f, Camera.main.transform.rotation.w);
        }
    }
}
