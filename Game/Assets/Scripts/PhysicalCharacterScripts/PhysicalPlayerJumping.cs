using UnityEngine;
using System.Collections;

public class PhysicalPlayerJumping : MonoBehaviour {

    public float jumpForce = 200f;

    bool isJumping = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") > 0f && !isJumping)
        {
            transform.parent.rigidbody2D.AddForce(new Vector2(0, jumpForce));
            isJumping = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        isJumping = false;
    }
}
