using UnityEngine;

public class weaponScript : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject myPrefab;
    public Camera playerCam;
    public GameObject hasTransObject;
    [SerializeField]
    private Vector3 baseOffset; 
    private GameObject myPlayer;
    // This script will simply instantiate the Prefab when the game starts.
    void Start()
    {
        if (myPrefab)
        {    myPlayer = Instantiate(myPrefab, new Vector3(0,0,0), Camera.main.transform.rotation)  as GameObject;
           // myBrick.transform.rotation *= Quaternion.Euler(0, 180f, 0);
          
        }
        else
        {
            Debug.Log("No prefab found for weaponScript");
        }
            

    }

    private void Update()
    {
        
        //myBrick.transform.position = myBrick.transform.position + baseOffset;
    }
}