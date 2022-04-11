using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMoveMent : MonoBehaviour
{


    public Transform orientation;
    public Transform respawn;

    private Rigidbody rb;

    public float moveSpeed = 4500;
    public float maxSpeed = 20;
    public float speed = 10f;
    public bool grounded;
    public LayerMask whatIsGround;

    public float counterMovement = 0.175f;
    private float threshold = 0.01f;
    public float maxSlopeAngle = 35f;


    private bool readyToJump = true;
    private float jumpCooldown = 0.25f;
    public float jumpForce = 550f;
    public float jumpForceForward = 1;

    public float delay;
    public ParticleSystem dust;
    Animator CameraLand;
    Animator CameraW;
    private bool Onland = true;

    public GameObject Cam;


    float x, y;
    bool jumping, sprinting, crouching;


    private Vector3 normalVector = Vector3.up;
    private Vector3 wallNormalVector;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        CameraLand = GetComponent<Animator>();
        CameraW = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {
        MyInput();

        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = respawn.position;
        }

        if (rb.velocity.magnitude < 25f)
        {
            rb.AddForce(transform.forward * speed * 10);
        }
    }



    private void MyInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        jumping = Input.GetButton("Jump");
        crouching = Input.GetKey(KeyCode.LeftControl);


    }



    private void Movement()
    {

        rb.AddForce(Vector3.down * Time.deltaTime * 10);


        Vector2 mag = FindVelRelativeToLook();
        float xMag = mag.x, yMag = mag.y;

        CounterMovement(x, y, mag);


        if (readyToJump && jumping) Jump();


        float maxSpeed = this.maxSpeed;

        //if(grounded && rb.velocity.magnitude < 30f)
        //{
           // if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W)))
            //{
            //    print("Spriting");
            //    rb.AddForce(transform.forward * maxSpeed * 25f);
            //}
       // }




        if (x > 0 && xMag > maxSpeed) x = 0;
        if (x < 0 && xMag < -maxSpeed) x = 0;
        if (y > 0 && yMag > maxSpeed) y = 0;
        if (y < 0 && yMag < -maxSpeed) y = 0;

        float multiplier = 1f, multiplierV = 1f;

        if (!grounded)
        {
            multiplier = 0.5f;
            multiplierV = 0.5f;
        }



        rb.AddForce(orientation.transform.forward * y * moveSpeed * Time.deltaTime * multiplier * multiplierV);
        rb.AddForce(orientation.transform.right * x * moveSpeed * Time.deltaTime * multiplier);
    }

    private void Jump()
    {
        if (grounded && readyToJump)
        {
            readyToJump = false;

            rb.AddForce(Vector2.up * jumpForce * 1.5f);
            rb.AddForce(transform.forward * jumpForce * jumpForceForward);
            rb.AddForce(normalVector * jumpForce * 0.5f);


            Vector3 vel = rb.velocity;
            if (rb.velocity.y < 0.5f)
                rb.velocity = new Vector3(vel.x, 0, vel.z);
            else if (rb.velocity.y > 0)
                rb.velocity = new Vector3(vel.x, vel.y / 2, vel.z);

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private float desiredX;

    private void CounterMovement(float x, float y, Vector2 mag)
    {
        if (!grounded || jumping) return;




        if (Mathf.Abs(mag.x) > threshold && Mathf.Abs(x) < 0.05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0))
        {
            rb.AddForce(moveSpeed * orientation.transform.right * Time.deltaTime * -mag.x * counterMovement);
        }
        if (Mathf.Abs(mag.y) > threshold && Mathf.Abs(y) < 0.05f || (mag.y < -threshold && y > 0) || (mag.y > threshold && y < 0))
        {
            rb.AddForce(moveSpeed * orientation.transform.forward * Time.deltaTime * -mag.y * counterMovement);
        }

        if (Mathf.Sqrt((Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2))) > maxSpeed)
        {
            float fallspeed = rb.velocity.y;
            Vector3 n = rb.velocity.normalized * maxSpeed;
            rb.velocity = new Vector3(n.x, fallspeed, n.z);
        }
    }


    public Vector2 FindVelRelativeToLook()
    {
        float lookAngle = orientation.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitue = rb.velocity.magnitude;
        float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);

        return new Vector2(xMag, yMag);
    }

    private bool IsFloor(Vector3 v)
    {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < maxSlopeAngle;
    }

    private bool cancellingGrounded;


    private void OnCollisionStay(Collision other)
    {

        int layer = other.gameObject.layer;
        if (whatIsGround != (whatIsGround | (1 << layer))) return;


        for (int i = 0; i < other.contactCount; i++)
        {
            Vector3 normal = other.contacts[i].normal;

            if (IsFloor(normal))
            {
                grounded = true;
                cancellingGrounded = false;
                normalVector = normal;
                CancelInvoke(nameof(StopGrounded));
            }
        }

        if (grounded)
        {
            print("Is Grounded");

            if (Onland)
            {
                print("Landed");
                CameraLand.SetTrigger("Land");
                //StartCoroutine(LandDust());
                Onland = false;
            }
        }


        float delay = 3f;
        if (!cancellingGrounded)
        {
            cancellingGrounded = true;
            Invoke(nameof(StopGrounded), Time.deltaTime * delay);
        }
    }

    private void StopGrounded()
    {
        grounded = false;
        Onland = true;


    }

    private void OnTriggerEnter(Collider trigger)
    {


      //  if (trigger.CompareTag("mist"))
       // {
         //   print("respawn");
          //  transform.position = respawn.position;



     //   }

    }

    private void OnCollisionEnter(Collision collision)
    {

    }
    }

    





    //private IEnumerator LandDust()
    // {
    //dust.Play();
    //  yield return new WaitForSeconds(delay);
    //  dust.Stop();
    //}











