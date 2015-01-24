using UnityEngine;
using System.Collections;

public class PhysicalPlayerMovement : MonoBehaviour {

    public float movementSpeed = 5f;
    public float jumpForce = 200f;

    bool isJumping = false;

	// Use this for initialization
	void Awake () {
	    
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        transform.Translate(movementSpeed * input * Time.deltaTime);
        if (Input.GetAxis("Vertical") > 0f && !isJumping)
        {
            rigidbody2D.AddForce(new Vector2(0, jumpForce));
            isJumping = true;
        }
	}

    void OnTriggerStay2D(Collider2D other)
    {
        isJumping = false;
    }
}
