using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float moveAmount;
    public bool isMoving; 
    public Vector3 moveDirection;

    public float movementSpeed = 1.5f;
    public float rotationSpeed = 5;
    // Start is called before the first frame update
    private GameObject player;

    [HideInInspector]
    public float deltaTime; 

    void Start()
    {
        isMoving = false;
        mainRoutineScript myScript = GetComponent<mainRoutineScript>();
        player = myScript.myPlayer;
    }

    void tankControls(GameObject ob)
    {
        if(horizontal==0 && vertical==0)
        {
            isMoving = false;
            return;
        }

        isMoving = true;
        //CONTROL FORWARD 
        float forwardAmount = vertical * movementSpeed;
        Vector3 forward = ob.transform.forward*forwardAmount*deltaTime;

        float rotAmount = horizontal * rotationSpeed * deltaTime;


       // Vector3 rotationVector = new Vector3(0, 30, 0);
       // Quaternion rotation = Quaternion.Euler(rotationVector);
        
        ob.transform.Translate(forward);
        ob.transform.rotation *= Quaternion.Euler(0, 90f * rotAmount, 0);

    }

    // Update is called once per frame
    void Update()
    {
        deltaTime = Time.deltaTime;
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        tankControls(player);
      
    }
}
