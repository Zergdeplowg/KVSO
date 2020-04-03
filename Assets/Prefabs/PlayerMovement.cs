using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	// Use this for initialization
    private float speed = 10f;
    private float jumpSpeed = 5f;
    private Vector3 direction = Vector3.zero;
    private float verticalVelocity = 0;

    private CharacterController cc;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }

	// Update is called once per frame
	void Update ()
	{
		direction = transform.rotation * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

	    if (direction.magnitude > 1f)
	    {
	        direction = direction.normalized;
	    }
        
        anim.SetFloat("Speed", direction.magnitude);

	    if (cc.isGrounded)
	    {
	        if (Input.GetButton("Jump"))
	        {
	            verticalVelocity = jumpSpeed;
	        }
	        else
	        {
	            verticalVelocity = 0;
	            anim.SetBool("Jump", false);
            }
	    }
	    else
	    {
	        anim.SetBool("Jump",true);
	    }

        if (cc.isGrounded && Input.GetButton("Jump"))
	    {
	        verticalVelocity = jumpSpeed;
	    }

	}

    void FixedUpdate()
    {
        Vector3 dist = direction * speed * Time.deltaTime;

        verticalVelocity += Physics.gravity.y * Time.deltaTime;
        

        dist.y = verticalVelocity * Time.deltaTime;

        cc.Move(dist);
    }
}
