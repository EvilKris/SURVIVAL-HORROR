using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class tankControls : MonoBehaviour
{


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
        
    }

    void movePlayer()
    {
        deltaTime = Time.deltaTime;
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");


        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {

            isMoving = true;


            //this aligns the gameobject to move in relation to the current camera
            var forward = cam.transform.forward;
            var right = cam.transform.right;

            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            if (Input.GetButton("SKey") || Input.GetButton("WKey"))
            {

                //FORWARD AND BACKWARD MOVEMENT
                if (Input.GetButton("SKey"))
                { 
                    //thePlayer.GetComponent<Animator>().Play("WalkBack");
                }
                else
                { 
                    //thePlayer.GetComponent<Animator>().Play("Walk");                  

                }

                var desiredMoveDirection = forward * vertical + right * horizontal;
                transform.Translate(desiredMoveDirection * movementSpeed * deltaTime);
            }
            else if(Input.GetButton("DKey") || Input.GetButton("AKey"))
            {

               

            }

        }
        else
        {
            isMoving = false;
           // thePlayer.GetComponent<Animator>().Play("Idle");
        }
    }
    // Update is called once per frame

    public void MoveInDirectionOfInput()
    {
        Vector3 dir = Vector3.zero;

        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");

        Vector3 camDirection = Camera.main.transform.rotation * dir; 
        Vector3 targetDirection = new Vector3(camDirection.x, 0, camDirection.z); //This line removes the "space ship" 3D flying effect. We take the cam direction but remove the y axis value

       // print(targetDirection);

        if (dir != Vector3.zero)
        { //turn the character to face the direction of travel when there is input
            transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(targetDirection),
            Time.deltaTime * rotationSpeed
            );
        }

        _rigidbody.velocity = targetDirection.normalized * movementSpeed;     //normalized prevents char moving faster than it should with diagonal input

    }

    void Update()
    {       
        
        
    }

    void FixedUpdate()
    {


        MoveInDirectionOfInput();
       // movePlayer();

    }
}
