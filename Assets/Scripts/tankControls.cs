using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


[RequireComponent(typeof(Rigidbody))]
public class tankControls : MonoBehaviour
{
    //Welcome to EvilKris School of How To Code Absolute Trash

    //get Cinemachine Virtual Camera ref
    // public CinemachineVirtualCamera vcam;
    //CheckForCameraBlending m_MyEvent;

    //Main character class, at the moment it deals with movement and collision detection

    private float _distToTarget;

    Rigidbody _rigidbody; 
    public GameObject thePlayer;
    public bool isMoving;
    public float horizontal;
    public float vertical;
    public float moveAmount;
    public Vector3 moveDirection;
    public bool examiningItem = false;

    //cam magic
    public Camera myCam;
    public Vector3 camRelativeEmpty;
    public Vector3 oldDirForward;
    public Vector3 oldDirRight;
    public Quaternion oldRot;

    [SerializeField]
    private float movementSpeed = 12f;
    [SerializeField]
    private float rotationSpeed = 2f;
    // Start is called before the first frame update

    [HideInInspector]
    public float deltaTime;
        
   


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

    }
    // Start is called before the first frame update
    void Start()
    {
        _distToTarget = 1.3f;
        isMoving = false;
        myCam=Camera.main;
        camRelativeEmpty = Camera.main.transform.position;
        oldDirForward = Camera.main.transform.forward;
        oldDirRight= Camera.main.transform.right;
        oldRot = Camera.main.transform.rotation;
        
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

    void showCam()
    {

    }
    public void TankControls()
    {
        //Moves player RES EVIL style using AD to rot and WS to move forward and back
        
        //temp movement speed Setting
        movementSpeed = 0.05f; 

        //get the inputs
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        //amount of rot = horizontal input * the speed 
        float turn = horizontal * rotationSpeed;

        //calc the angle of rotation
        Quaternion turnRotation = Quaternion.Euler(0, turn, 0);
        _rigidbody.MoveRotation( _rigidbody.rotation * turnRotation);


        //for forward or back get the transform * up/down input * speed
        Vector3 movement = transform.forward * vertical * movementSpeed;

        //move the body
        _rigidbody.MovePosition(_rigidbody.position + movement);


        //TO DO: 

    }

    void checkForThings()
    {
        RaycastHit hit;
        GameObject ob;
        Animator anim;
        //Sends out a ray in the forward transform OF THE PLAYER at a given distance (1.3f)        
        if (Physics.Raycast(thePlayer.transform.position, thePlayer.transform.TransformDirection(Vector3.forward), out hit, _distToTarget))
        {
            //give var ob the name of the gameobject detected 
            ob = hit.collider.gameObject;

            //First check- doors
            //if item has "door" tag, and mouse clicked+released once
            
            if (hit.collider.tag == "door" && Input.GetMouseButtonUp(0))
            {
                Debug.Log("OPEN SESAME!");                
                //get Bool 'openDoor' in Animator
                anim = ob.GetComponent<Animator>();
                //only Animate if the door is closed WARNING - one way system at the moment. 
                if(anim.GetBool("openDoor")==false)
                    anim.SetBool("openDoor", true);

                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                //Don't bother with the rest of the sweep if a door has been found
                return;
            }
           
            //Next sweep, if not already in the item menu (examiningItem) and tag 'eventTrigger' is found, and clicked up
            if ((!examiningItem) &&  hit.collider.tag == "eventTrigger" && Input.GetMouseButtonUp(0))
            {                                                               
                RaycastHit[] hits;
                
                //Proximity to object near forward transform has been established, now check all under mouse
                hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
                
                int i = 0;
                while (i < hits.Length)
                {
                    RaycastHit hitList = hits[i];

                    //if the same object has been found, activate the event on the object and bounce
                    if (hits[i].collider.gameObject == ob)
                    {
                        examiningItem = true;
                        ob.SendMessage("doEvent", 1);
                        //break out if object detected
                        break;
                    }
                    i++;
                }
            }
        }
    }

    void Update()
    {
        checkForThings(); 
    }

    /*
      void CamTransformControls()
      {
          //Moves Player relative to Cam. Uses one tranform until Key release and snaps afterwards ensuring 
          //that player moves continually at the original transform vector 

          if (!isMoving)
          {
              //isMoving = false;
              oldDirForward = Camera.main.transform.forward;
              oldDirRight = Camera.main.transform.right;
              oldRot = Camera.main.transform.rotation;

          }

              //Gives zero vector (0,0,0) every frame 
              Vector3 dir = Vector3.zero;

              //checks axis inputs 
              dir.x = Input.GetAxis("Horizontal");
              dir.z = Input.GetAxis("Vertical");
              //will be zero if nothing entered else something like 0.12577 or 0.2322 


          //camRelativeEmpty = targetDirection;

          //get normalized vector of cam forward and right 1,0,1 for example
          Vector3 camForwardFlat = new Vector3(oldDirForward.x, 0f, oldDirForward.z).normalized;
          Vector3 camRightFlat = new Vector3(oldDirRight.x, 0f, oldDirRight.z).normalized;

          //move player
          _rigidbody.velocity = ((camRightFlat * dir.x) + (camForwardFlat * dir.z) + new Vector3(0f, _rigidbody.velocity.y, 0f)) * movementSpeed;


          //rotate direction to cam rot
          Vector3 camDirection = oldRot * dir;
          Vector3 targetDirection = new Vector3(camDirection.x, 0, camDirection.z);

          //UNUSED if the player is moving rotate the gameOb gradually to face the direction of the movement
     /*
          /*if (Vector3.Dot(newDir, oldDir) == -1)
          {
              Mathf.Sign(...) == -1
              //The guy flipped in the exact opposite direction
          }*/
    
    //turn the character to face the direction of travel when there is no input 0,0,0 
    /*
    if (dir != Vector3.zero)
    { 
        isMoving = true;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), rotationSpeed);
        //Debug.Log("IS MOVING"+dir);

    }
    else
        isMoving = false;
}*/




    void FixedUpdate()
    {
        TankControls();
        //CamTransformControls();
    }
        

}
