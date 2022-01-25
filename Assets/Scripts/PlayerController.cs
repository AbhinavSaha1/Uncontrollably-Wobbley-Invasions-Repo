using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float strafeSpeed;
    public float jumpForce;
    public Animator animator;

    public Rigidbody hips;
    public bool isGrounded;

    void Start()
    {
        hips = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isRunning", true);
            hips.AddForce(hips.transform.forward * speed);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isSlideLeft", true);
            hips.AddForce(-hips.transform.right * speed);
        }
        else
        {
            animator.SetBool("isSlideLeft", false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("isRunning", true);
            hips.AddForce(-hips.transform.forward * speed);
        }
        else if(!Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isRunning", false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isSlideRight", true);
            hips.AddForce(hips.transform.right * speed);
        }
        else
        {
            animator.SetBool("isSlideRight", false);
        }

        if(Input.GetKey(KeyCode.Space))
        {
            if(isGrounded)
            {
                hips.AddForce(new Vector3(0, jumpForce, 0));
                isGrounded = false;
            }
        }
    }
}
