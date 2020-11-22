using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class tankControls : MonoBehaviour
{
    //Main character class, at the moment it deals with movement and collision detection

    private float _distToTarget;

    Rigidbody _rigidbody; 
    public GameObject thePlayer;
    public bool isMoving;
    public float horizontal;
    public float vertical;
    public float moveAmount;
    public Vector3 moveDirection;

    [SerializeField]
    private float movementSpeed = 6f;
    [SerializeField]
    private float rotationSpeed = 5f;
    // Start is called before the first frame update
    
    [HideInInspector]
    public float deltaTime;
    public Camera cam;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        if (cam == null)
            cam = Camera.main;
               

    }
    // Start is called before the first frame update
    void Start()
    {
        _distToTarget = 1.3f;
    }
    void OnCollisionEnter(Collision collision)
    {
        //Respawn is the placeholder name I'm using for items that can be picked up
        //this method ensures that items that can be picked up do not interract with the player
        if (collision.gameObject.tag == "Respawn")
        {
            GameObject ob = collision.gameObject; 
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }

        //TO DO: want to animated my main guy to actually move his arms in the direction of the items, poss with IK hands. 
        //not too concerned with how good it looks as long as it doesn't look like utter rubbish. 
    }

    public void MoveInDirectionOfInput()
    {
        //Moves player RES EVIL style using the camera forward vector to alter the angle of the motion keys

        Vector3 dir = Vector3.zero;

        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");

        Vector3 camForwardFlat = new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z).normalized;
        Vector3 camRightFlat = new Vector3(cam.transform.right.x, 0f, cam.transform.right.z).normalized;

        _rigidbody.velocity = ((camRightFlat * dir.x) + (camForwardFlat * dir.z) + new Vector3(0f, _rigidbody.velocity.y, 0f)) * movementSpeed;

        Vector3 camDirection = Camera.main.transform.rotation * dir;
        Vector3 targetDirection = new Vector3(camDirection.x, 0, camDirection.z);

        if (dir != Vector3.zero)
        { //turn the character to face the direction of travel when there is input
            transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(targetDirection),
            Time.deltaTime * rotationSpeed
            );
        }
                      
        //TO DO: I THINK it moves at the correct angle BUT the player spins around, also have to add animations at some point. 
        
    }

    void checkForDoors()
    {
        //Sends out a ray in the forward transform only at a given distance and if it's clicked (mouse button up) on a door, open 
        //door must have a bool called 'openDoor' 

        RaycastHit hit;
        GameObject ob;
        Animator anim;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, _distToTarget))
        {


            //CHECK FOR DOORS - ONLY ACTIVATES IF TAG IS DOOR AND DIST IS UNDER 1 UNITY AND MOUSE BUTTON CLICKED ONCE

            if (hit.collider.tag == "door" && Input.GetMouseButtonUp(0))
            {
                Debug.Log("OPEN SESAME!");
                ob = hit.collider.gameObject;
                anim = ob.GetComponent<Animator>();
                anim.SetBool("openDoor", true);

                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                //Debug.Log(hit.distance);                               
            }
        }
    }
       
    void Update()
    {
        checkForDoors(); 
    }
   
    void FixedUpdate()
    {
        MoveInDirectionOfInput();
     
    }
}
