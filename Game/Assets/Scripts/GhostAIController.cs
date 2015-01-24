using UnityEngine;
using System.Collections;

enum Behavior
{
	Idle,
	Roam,
	Annoy,
	AttackSpirit,
	AttackBlock
}

public class GhostAIController : MonoBehaviour {

	// Use this for initialization

	Behavior currentBehavior;
	public double idleRadius;
	public double idleSpeed;

	public double leftLimit, rightLimit, upLimit, downLimit;

	private bool hasGeneratedPoint;

	private Vector3 generatedPosition;

	void Start () {

		currentBehavior = Behavior.Idle;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch(currentBehavior)
		{
			case Behavior.Idle:
				ExecIdleCycle();
			break;

		}
	}

	void ExecIdleCycle()
	{
		// When Idling, move around in a circular fashion.
		// Generate a point either to the left or right of the ghost at a distance
		if(!hasGeneratedPoint)
		{
			//Generate a point
			int dir = (Random.Range(-1,1)<0)?-1: 1;
			{
				generatedPosition = transform.position;
				generatedPosition.x +=  (float)(dir*idleRadius);
			}
			hasGeneratedPoint = true;
		}

		//Do a cross product of the vector coming out of the screen and the
		//vector from the ghost to that point to get a radial vector
		//normalize and multiply it by the speed to get a new point.

		Vector3 pointToGhost = transform.position - generatedPosition;

		Vector3 follow = Vector3.Cross(new Vector3(0,0,-1), pointToGhost).normalized;

		Debug.DrawLine(transform.position, transform.position + (follow * (float)idleRadius), Color.red);
		Debug.DrawLine(transform.position, generatedPosition);
		transform.position = transform.position + (follow * (float)idleSpeed);

		pointToGhost = transform.position - generatedPosition;

		if(pointToGhost.sqrMagnitude > idleRadius*idleRadius)
		{
			transform.position = generatedPosition + (pointToGhost.normalized * (float)idleRadius);
		}
	}
}
