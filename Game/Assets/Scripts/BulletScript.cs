using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    public float bulletSpeed = 10f;

    Vector3 trajectory;

    void Awake()
    {
        int mouseRayLayer = LayerMask.GetMask("MouseRayCast");
        Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, 30, mouseRayLayer))
        {
            trajectory = floorHit.point - transform.position;
            trajectory.z = 0f;
            trajectory.Normalize();

            Debug.Log(floorHit.point);
            Debug.Log(transform.position);
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
        transform.Translate(bulletSpeed * trajectory * Time.deltaTime); 
	}
}
