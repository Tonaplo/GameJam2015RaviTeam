using UnityEngine;
using System.Collections;

public class PhysicalPlayerDragging : MonoBehaviour {

    public PhysicalPlayerMovement.Facing side;
    GameObject player;
    PhysicalPlayerMovement playerMovementScript;
    Transform draggingObject;
    Vector3 offset;

    void Awake()
    {
        player = transform.parent.gameObject;
        playerMovementScript = player.GetComponent<PhysicalPlayerMovement>();
    }

    void Update()
    {
        if (draggingObject != null)
        {
            draggingObject.position = transform.position + offset;
        }


        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Block")
        {
            if (Input.GetMouseButtonDown(1))
            {

                if (draggingObject == null)
                {
                    if (playerMovementScript.facing != side)
                    {
                        draggingObject = other.transform;
                        offset = draggingObject.transform.position - transform.position;
                    }
                }

            }
            
            if (Input.GetMouseButtonUp(1))
            {
                Debug.Log("Reset Dragging Object");
                draggingObject = null;
            }
        }
    }
}
