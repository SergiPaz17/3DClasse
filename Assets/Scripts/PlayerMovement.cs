using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    Rigidbody RB;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform GroundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;



    public float originalHeight;
    public float reducedHeight;
    public float slideSpeed = 10f;

    bool isGrounded;


    Vector3 velocity;


private void Start()
    {
        RB = GetComponent<Rigidbody>();
        originalHeight = controller.height;
    }


    void Update()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -6f;
            
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        Vector3 move = transform.right * x + transform.forward * z;


        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        
        velocity.y += gravity * Time.deltaTime;


        controller.Move(velocity * Time.deltaTime);


        
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Sliding();
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            GoUp();
        }
    }




    private void Sliding()
    {
        controller.height = reducedHeight;
        RB.AddForce(transform.forward * slideSpeed, ForceMode.VelocityChange);
    }

    private void GoUp()
    {
        controller.height = originalHeight;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            SceneManager.LoadScene(1);
        }
    }


}
