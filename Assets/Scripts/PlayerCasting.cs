using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCasting : MonoBehaviour
{
    private float _distToTarget;
    private GameObject ob;
    private Animator anim;
    enum crosshairStates { defaultCrosshair, openedDoor, closedDoor };
  //  int crosshairState = (int)crosshairStates.defaultCrosshair;

    public Sprite crosshairSprite;
    public Sprite itemOverSprite;
    public Sprite openedDoorSprite;
    public GameObject objInFrontOfMe;

    private GameObject hoverCursor;
    private SpriteRenderer cursorRenderer;

    private Texture2D cursor2d;
    private Texture2D item2d;
    private Texture2D door2d;

    private void Start()
    {

        if (crosshairSprite)
        {
            cursor2d = MakeTexture2DFromSprite(crosshairSprite); //Converts Mouse to custom pointer
            item2d = MakeTexture2DFromSprite(itemOverSprite); //Converts Mouse to custom pointer
            door2d = MakeTexture2DFromSprite(openedDoorSprite); //Converts Mouse to custom pointer
            Cursor.SetCursor(cursor2d, GetHotspotFromSprite(crosshairSprite), UnityEngine.CursorMode.Auto); 


            /*
            hoverCursor = new GameObject("hoverCursorHand");
            hoverCursor.transform.parent = this.transform;
            hoverCursor.AddComponent<SpriteRenderer>().sprite = crosshairSprite;
            cursorRenderer = hoverCursor.GetComponent<SpriteRenderer>();
            hoverCursor.transform.localPosition = new Vector3(0.0f, 0.0f, 0.4f);
            hoverCursor.transform.localScale = new Vector3(1.0f * 0.05f, 1.0f * 0.05f, 1.0f * 0.05f);
            hoverCursor.transform.localRotation = Quaternion.identity;
            //  hoverCursor.SetActive(false);
            _distToTarget = 1.3f;
            */
        }



    }

    static private Texture2D MakeTexture2DFromSprite(Sprite sprite)
    {
        Texture2D texture2D = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height, TextureFormat.RGBA32, false);

    #if UNITY_EDITOR
            texture2D.alphaIsTransparency = true;
    #endif
        var pixels = sprite.texture.GetPixels((int)sprite.textureRect.x, (int)sprite.textureRect.y, (int)sprite.textureRect.width, (int)sprite.textureRect.height);
        texture2D.SetPixels(pixels);
        texture2D.Apply();

        return texture2D;
    }

    static private Vector2 GetHotspotFromSprite(Sprite sprite)
    {
        return sprite.pivot;
    }


   

   
    // Update is called once per frame
    void Update()
    {
        if(!crosshairSprite)
        {
            Debug.Log("Need a sprite in PlayerCasting");
            Destroy(this); //Disable this script if there's no hand sprite found
            return;
        }

        //cursorRenderer.sprite = crosshairSprite;
        Cursor.SetCursor(cursor2d, GetHotspotFromSprite(crosshairSprite), UnityEngine.CursorMode.Auto);

        Ray ray; 
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       // if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, _distToTarget))
       // {

          if(Physics.Raycast(ray, out hit))
          {   
            //first put item into variable
            if (hit.collider.gameObject)
                objInFrontOfMe = hit.collider.gameObject;

           // Debug.Log(objInFrontOfMe.name);

            if (hit.collider.tag == "door")
                Cursor.SetCursor(door2d, GetHotspotFromSprite(openedDoorSprite), UnityEngine.CursorMode.Auto);


            if (hit.collider.tag == "item")
                Cursor.SetCursor(item2d, GetHotspotFromSprite(itemOverSprite), UnityEngine.CursorMode.Auto);


            //CHECK FOR DOORS - ONLY ACTIVATES IF TAG IS DOOR AND DIST IS UNDER 1 UNITY AND MOUSE BUTTON CLICKED ONCE
            //if (hit.collider.tag == "door" && hit.distance < _distToTarget && Input.GetMouseButtonUp(0))
            if (hit.collider.tag == "door" && Input.GetMouseButtonUp(0))
            {
                ob = hit.collider.gameObject;
                anim = ob.GetComponent<Animator>();
                anim.SetBool("openDoor", true);

                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                //Debug.Log(hit.distance);                               
            }

            


        }
    }
   
}
