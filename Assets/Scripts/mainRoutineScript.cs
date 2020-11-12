using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainRoutineScript : MonoBehaviour
{
    public GameObject myPrefab;
    [SerializeField]
    private Vector3 baseOffset;


    public GameObject myPlayer;
    // Start is called before the first frame update
    void Awake()
    {
        LoadMainCharacter();
    }

    void LoadMainCharacter()
    {
        if (myPrefab)
        {   // Instantiate at position (0, 0, 0) and zero rotation.
            //drop in Geedy at pos 0,0,0
            myPlayer= Instantiate(myPrefab, new Vector3(0,0,0), Quaternion.identity);
            //myBrick = Instantiate(myPrefab, new Vector3(0, 0, 0), Camera.main.transform.rotation) as GameObject;
           // myPlayer.transform.rotation *= Quaternion.Euler(0, 180f, 0);
            //myBrick.transform.position = playerCam.transform.position;
            //myBrick.transform.rotation = Camera.main.transform.rotation;
            //   baseOffset = new Vector3(-1.8f, 0.5f, 0.4f);
            //set the custom offset values to place Geedy (inside the camera) 
            //myBrick.transform.position = myBrick.transform.position + baseOffset;
            //myBrick.transform.parent = playerCam.transform;
            //transform = Camera.main.transform;
            //transform.position.z = Camera.main.transform + 5;
            //Debug.Log(myBrick.transform.position);

            //myPrefab.transform.SetParent(playerCam.transform);

            // hasTransObject = myBrick.transform.Find("tshirt").gameObject;
            /*
             //If the child was found.
             if (hasTransObject == null)
             {
                 Debug.Log("object/mesh not found");
                 return;

             }
             else
             {
                 hasTransObject.GetComponent<Renderer>().enabled = false;

             }*/
        }
        else
        {
            Debug.Log("No prefab found for weaponScript");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
   
}