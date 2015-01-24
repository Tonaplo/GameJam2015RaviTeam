using UnityEngine;
using System.Collections;

public class PhysicalPlayerShooting : MonoBehaviour {

    public GameObject bullet;
    float shootingInterval = 0.2f;
    float currentShootTime = 0.0f;

    int mouseRayLayer;

    void Awake()
    {
        mouseRayLayer = LayerMask.GetMask("MouseRayCast");
    }

	// Update is called once per frame

	void Update () {

        if (currentShootTime <= 0f)
        {
            
            if (Input.GetMouseButton(0))
            {
                Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

                RaycastHit floorHit;

                if (Physics.Raycast(camRay, out floorHit, 30, mouseRayLayer))
                {
                    Vector3 playerToMouse = floorHit.point - transform.position;
                    playerToMouse.z = 0f;
                    playerToMouse.Normalize();
                    Instantiate(bullet, playerToMouse +  transform.position, transform.rotation);
                    currentShootTime = shootingInterval;
                }
            }
        }   
        else 
        {
            currentShootTime -= Time.deltaTime;
        }
    }
}
