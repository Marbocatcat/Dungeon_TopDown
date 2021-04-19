using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wizzard : MonoBehaviour
{
    [SerializeField]
    float speed;

    private Transform staffTransform;
    private Camera theCam;

    public Transform firePoint;
    public GameObject bulletTofire;

    Rigidbody2D rigidBody2d;
    Animator animator; 
    
    Vector3 mousePos;
    Vector3 baseScale;
    Vector2 movement;

    float horizontal;
    float vertical;

    bool isDashing;
    bool isShooting;

    string facingRight = "facingRight";
    string facingLeft = "facingLeft";
    string facingUp = "facingUp";
    string facingDown = "facingDown";

    string faceDirection;

    private void Awake()
    {
        staffTransform = transform.Find("green_staff"); // find the child object called green_staff and store it in the staffTransform variable.
    }
    void Start()
    {
        rigidBody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        baseScale = transform.localScale;
        theCam = Camera.main;
 
    }

    void handleAnimation()
    {
        // if the wizzard is moving left or right run the appropriate animation
        if(rigidBody2d.velocity.x > 0.1 || rigidBody2d.velocity.x < -0.1)
        {
            animator.Play("run_anim");
        }
        else
        {
            animator.Play("idle_anim");
        }
    }
    void handleMoving()
    {


        rigidBody2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed;

       if(rigidBody2d.velocity.y > 0)
        {
            faceDirection = facingUp;
        }
       else if(rigidBody2d.velocity.y < 0)
        {
            faceDirection = facingDown;
        }

    }
    void dashing(string direction)
    {
        
        

        if(direction == facingRight)
        {
            rigidBody2d.velocity = new Vector2(200, 0);
        }
        else if(direction == facingLeft)
        {
            rigidBody2d.velocity = new Vector2(-200, 0);
        }
        else if(direction == facingUp)
        {
            rigidBody2d.velocity = new Vector2(rigidBody2d.velocity.x, 200);
        }
        else if(direction == facingDown)
        {
            rigidBody2d.velocity = new Vector2(rigidBody2d.velocity.x, -200);
        }

        isDashing = false;

    }

    void handleAiming()
    {

        string newFaceDirection = faceDirection;
        Vector3 newBaseScale = baseScale; // set another instance of the basescale

        mousePos = theCam.ScreenToWorldPoint(Input.mousePosition); // this tracks where our mouse is relative to the main camera's position.
        Vector3 aimDir = (mousePos - transform.position).normalized; // calculates the aim direction
        // Z rotation
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg - 90f; // calculated the approriate z position for the sprite to rotate;
        staffTransform.eulerAngles = new Vector3(0, 0, angle); // moves the z.


        // flip the sprite scale when facing left or right;
        if(aimDir.x > 0)
        {
            newBaseScale.x = baseScale.x;
            newFaceDirection = facingRight;
        }
        else if(aimDir.x <0)
        {
            newBaseScale.x = -baseScale.x;
            newFaceDirection = facingLeft;
        }

        faceDirection = newFaceDirection;
        transform.localScale = newBaseScale;

    }
    void handleShooting()
    {
        Instantiate(bulletTofire, firePoint.position, staffTransform.rotation);
        isShooting = false;
    }


    void Update()
    {
        handleAiming();
        handleAnimation();

        if(Input.GetButtonDown("Jump"))
        {
            isDashing = true;
        }
        if(Input.GetMouseButtonDown(0))
        {
            isShooting = true;
        
        }

    } 

    void FixedUpdate()
    {
        handleMoving();

        if(isDashing)
        {
            dashing(faceDirection);
        }
        if(isShooting)
        {
            handleShooting();
        }


    }
}
