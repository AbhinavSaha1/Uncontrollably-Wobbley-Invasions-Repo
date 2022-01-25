using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public Animator animator;
    GameObject grabbedObj;
    public Rigidbody rb;
    public int isLeftorRight;
    public bool alreadyGrabbing = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(isLeftorRight))
        {
            if(isLeftorRight == 0)
            {
                animator.SetBool("Left hand  grab", true);
            }
            else if (isLeftorRight == 1)
            {
                Debug.Log("Active");
                animator.SetBool("Right hand grab", true);
            }

            if (grabbedObj != null)
            {
                FixedJoint fj = grabbedObj.AddComponent<FixedJoint>();
                if (fj != null) Debug.Log("Joint added");
                fj.connectedBody = rb;
                fj.breakForce = Mathf.Infinity;
            }
            else Debug.Log("No grabbed object");
            
        } 
        else if (Input.GetMouseButtonUp(isLeftorRight))
        {
            if (isLeftorRight == 0)
            {
                animator.SetBool("Left hand  grab", false);
            }
            else if (isLeftorRight == 1)
            {
                animator.SetBool("Right hand grab", false);
            }

            if(grabbedObj != null)
            {
                Destroy(grabbedObj.GetComponent<FixedJoint>());
            }
            grabbedObj = null;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            grabbedObj = other.gameObject;
            if (grabbedObj != null) Debug.Log("grabbed" + grabbedObj.name);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        grabbedObj = null;
    }
}
