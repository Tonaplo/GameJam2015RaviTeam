using UnityEngine;
using System.Collections;

public class PhysicalPlayerMovement : MonoBehaviour {

    public float movementSpeed = 5f;

	// Update is called once per frame
	void FixedUpdate () {

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        transform.Translate(movementSpeed * input * Time.deltaTime);
	}
}
