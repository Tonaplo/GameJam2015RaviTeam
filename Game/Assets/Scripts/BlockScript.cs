using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlockScript : MonoBehaviour {

    float secondsAsABlock = 5f;
    float timeUntillGhost;

	// Use this for initialization
	void Start () {
        timeUntillGhost = secondsAsABlock;
	}
	
	// Update is called once per frame
	void Update () {

        timeUntillGhost -= Time.deltaTime;

        if (timeUntillGhost <= 0f)
        {
            //Spawn ghost in this position.
        }
	}
}
