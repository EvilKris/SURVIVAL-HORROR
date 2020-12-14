using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventTwoScript : MonoBehaviour
{

    //Debug.Log("CursorPos " + Input.mousePosition + " Centre of Screen " + Screen.width / 2 + ", " + Screen.height / 2);

    private int eventPhase = 0;

    public float lookSpeed = 3;
    private Vector2 rotation = Vector2.zero;


    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }
    public void Look() // Look rotation (UP down is Camera) (Left right is Transform rotation)
    {
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        rotation.x = Mathf.Clamp(rotation.x, -15f, 15f);
        transform.eulerAngles = new Vector2(0, rotation.y) * lookSpeed;
        Camera.main.transform.localRotation = Quaternion.Euler(rotation.x * lookSpeed, 0, 0);
    }
    // look out the window
    void doEvent(int go)
    {
        eventPhase = go;
        Debug.Log("OK WORKING");
        PauseGame();
        //var tooltip = GetComponent<SimpleTooltip>();
        //tooltip.infoLeft = tooltip.itemToolTipGroup[tooltip.tipA].text[1];
        //Destroy(gameObject);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
