using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    FollowPointRotation fpr;
    [SerializeField]
    GameObject body;
    private Transform cameraFollow;
    private Rigidbody rb;

    public float MoveSpeed;
    public float jumpForce;
    private bool jumpBool = false;
    bool space = false;
    private bool Grounded = true;

    Vector3 rotation;

    private Animator anim;
    private Animator camAnim;

    public Transform WallRayDetector;
    private bool WallJumpBool = false;

    [SerializeField]
    float inputX;
    [SerializeField]
    float inputZ;

    [SerializeField]
    float animationSpeed;
    [SerializeField]
    float allowPlayerRotation = 0.1f;

    [Header("Animation Smoothing")]
    [Range(0, 1f)]
    public float HorizontalAnimSmoothTime = 0.2f;
    [Range(0, 1f)]
    public float VerticalAnimTime = 0.2f;
    [Range(0, 1f)]
    public float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        jump();
        InputMagnitude();
        move();
        //WallJump();
    }
    private void Update()
    {
        //jump - I have found that the getKeyDown works better in update, however, the physics obviously works better in fixedupdate, so I combined the detection in update, and the movement in fixedupdate
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            jumpBool = true;
        }

        //space key
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            space = true;
        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Joystick1Button0))
        {
            space = false;
        }
    }

    void SetAnimatorTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }

    private void move()
    {
        Vector3 realVelocity = rb.velocity;
        
        Vector3 inputVector;

        //inputs
        inputX = Input.GetAxis("Horizontal");  // A and D or Left and Right arrow
        inputZ = Input.GetAxis("Vertical");

        //camera relative directions based on horizontal and vertical
        Vector3 XMOVE = Camera.main.transform.right * inputX;
        Vector3 YMOVE = Camera.main.transform.forward * inputZ;

        inputVector = XMOVE + YMOVE;         //create a single movement vector from the inputs
        inputVector *= MoveSpeed;            //multiply it by speed

        inputVector.y = rb.velocity.y; //make sure you do not affect the y velocity (gravity)
        if(!WallJumpBool)
            rb.velocity = inputVector;    //assignZ

        //rotation
        if((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && !WallJumpBool) // this makes sure the rotation does not reset to 0 if the player is not pressing any key
        {
            //body.transform.rotation = Quaternion.Euler(0,fpr.followTransform.transform.rotation.eulerAngles.y,0);
            rotation = new Vector3(inputVector.x, 0, inputVector.z);
            body.transform.rotation = Quaternion.LookRotation(rotation);
        }
       
        if(inputX !=0 || inputZ!= 0)
        {
            //anim.SetBool("Run", true);
        }
        else
        {
            //anim.SetBool("Run", false);
        }
    }
    private void jump()
    {
        if (jumpBool && Grounded)
        {
            rb.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);
            jumpBool = false;
            Grounded = false;
            //anim.SetBool("Jump", true);
        }
        else
        {
            jumpBool = false;
        }
    }
    /*void WallJump()
    {

        //used to detect if player facing wall
        Ray wall = new Ray(WallRayDetector.transform.position, WallRayDetector.transform.forward); //raycasr
        RaycastHit hit;

        //used to see if player is off the ground, before walljumping
        RaycastHit hitdown; //declare a raycast hit detector
        Ray downRay = new Ray(transform.position, -Vector3.up); //shoot a raycast downward
        Physics.Raycast(downRay, out hitdown); //tells unity if the downray hit something, and transfers result into hitdown.

        bool offground = false;
        offground = hitdown.distance > 0.2f;

        if (Physics.Raycast(wall, out hit, 0.7f) && hit.normal.y < 0.05 && offground && rb.velocity.y <= 0) //use a layer mask if you have triggers around the course
        {
            rb.drag = 5;
            WallJumpBool = true;
            //anim.SetBool("WallJump", true);

            
            //anim.SetBool("Jump", false);



            if (!Grounded && Physics.Raycast(wall, out hit, 0.7f) && hit.normal.y < 0.2 && space)
            {
                space = false;
                WallJumpBool = true;
                transform.eulerAngles += new Vector3(0, 180, 0);//face the opposite direction of the wall
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);//set to 0 and then change on next line, so we always stabilize the vlelocity increase
                rb.velocity = new Vector3(hit.normal.x * 8, rb.velocity.y + 20, hit.normal.z * 7); //bounce off to direction of normal


                //anim.SetBool("WallJump", false);
                //anim.SetBool("Jump", true);

            }
        }
        else
        {
            //anim.SetBool("WallJump", false);
            rb.drag = 0;
        }

    }*/

    private void OnCollisionStay(Collision collision)
    {
        if (collision.contacts[0].normal.y > 0.5 && !Input.GetKey(KeyCode.Space))
        {
            Grounded = true;
            WallJumpBool = false;
        }
    }

    void InputMagnitude()
    {
        //Calculate Input Vectors
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        //anim.SetFloat ("InputZ", InputZ, VerticalAnimTime, Time.deltaTime * 2f);
        //anim.SetFloat ("InputX", InputX, HorizontalAnimSmoothTime, Time.deltaTime * 2f);

        //Calculate the Input Magnitude
        animationSpeed = new Vector2(inputX, inputZ).sqrMagnitude;

        //Physically move player

        if (animationSpeed > allowPlayerRotation)
        {
            anim.SetFloat("Blend", animationSpeed, StartAnimTime, Time.deltaTime);
        }
        else if (animationSpeed < allowPlayerRotation)
        {
            anim.SetFloat("Blend", animationSpeed, StopAnimTime, Time.deltaTime);
        }
    }

}
