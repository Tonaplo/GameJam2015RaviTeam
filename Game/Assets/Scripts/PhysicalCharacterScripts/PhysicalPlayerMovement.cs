using UnityEngine;
using System.Collections;

public class PhysicalPlayerMovement : MonoBehaviour {

    float movementSpeed = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"),0,0);
        transform.Translate(movementSpeed * input * Time.deltaTime); 
	}
}
