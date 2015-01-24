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

        secondsAsABlock -= Time.deltaTime;

        if (secondsAsABlock <= 0f)
        {
            //Spawn ghost in this position.
        }
	}
}
