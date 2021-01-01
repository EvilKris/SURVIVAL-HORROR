using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventTwoScript : MonoBehaviour
{

    //Debug.Log("CursorPos " + Input.mousePosition + " Centre of Screen " + Screen.width / 2 + ", " + Screen.height / 2);

    private int eventPhase = 0;

    private Canvas EscCan;
    private GameObject clone;
    // look out the window
    /*
    private void Looky(GameObject) // Look rotation (UP down is Camera) (Left right is Transform rotation)
    {
        thing.transform.rotation.y += Input.GetAxis("Mouse X");
        ob.rotation.x += -Input.GetAxis("Mouse Y");
        ob.rotation.x = Mathf.Clamp(rotation.x, -15f, 15f);
        transform.eulerAngles = new Vector2(0, rotation.y) * lookSpeed;
        Camera.main.transform.localRotation = Quaternion.Euler(rotation.x * lookSpeed, 0, 0);
    }*/


    void doEvent(int go)
    {
        eventPhase = go;
        GameObject tempObject = GameObject.Find("MainCode");
        Debug.Log("OK WORKING");
        
        

        if (tempObject != null)
        {
            //If we found the object , get the Canvas component from it.
            //EscCan = tempObject.GetComponent<Canvas>();
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
            /*
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
