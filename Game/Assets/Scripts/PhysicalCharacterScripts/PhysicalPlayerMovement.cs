using UnityEngine;
using System.Collections;

public class PhysicalPlayerMovement : MonoBehaviour {

    public float movementSpeed = 5f;
    public Facing facing;

	// Update is called once per frame
	void FixedUpdate () {

        float horizontal = Input.GetAxis("Horizontal");
        Vector3 input = new Vector3(horizontal, 0, 0);
        transform.Translate(movementSpeed * input * Time.deltaTime);

        if (Input.GetMouseButtonDown(1))
        {
            if (horizontal < 0f)
                facing = Facing.Left;
            else
                facing = Facing.Right;
        }

	}

    public enum Facing
    {
        Left,
        Right
    }
}
