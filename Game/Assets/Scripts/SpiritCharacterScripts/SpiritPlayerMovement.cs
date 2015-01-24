using UnityEngine;
using System.Collections;

public class SpirtPlayerMovement : MonoBehaviour {
	
	public float MOVEMENT_SPEED = 20;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float horInput = Input.GetAxis("HorizontalController");
		float verInput = Input.GetAxis("VerticalController");
		
		Vector3 input = new Vector3(horInput, verInput, 0);
		transform.Translate(MOVEMENT_SPEED * input * Time.deltaTime); 
	}
}

