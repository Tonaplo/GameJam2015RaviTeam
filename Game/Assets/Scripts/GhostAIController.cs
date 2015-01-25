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
	public float roamingSpeed;
	public float roamWaitTime = 0.5f;

	public float leftLimit, rightLimit, upLimit, downLimit;

	private bool hasGeneratedPoint;
	private Vector3 generatedPosition;
	private bool waitingToGenerate;

	private bool hasGeneratedGoToLocation;
	private Vector3 goToLocation;

	private float totalTime, currentTime;

	private Vector3 v0;
	private Vector3 v1;
	private Vector3 v2;

	void Start () {

		currentBehavior = Behavior.Roam;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch(currentBehavior)
		{
			case Behavior.Idle:
				ExecIdleCycle();
			break;

		case Behavior.Roam:
				ExecRoamCycle();
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
		transform.position = transform.position + (follow * (float)idleSpeed * Time.deltaTime);

		pointToGhost = transform.position - generatedPosition;

		if(pointToGhost.sqrMagnitude > idleRadius*idleRadius)
		{
			transform.position = generatedPosition + (pointToGhost.normalized * (float)idleRadius);
		}
	}

	void ExecRoamCycle()
	{
		//Reset hasGeneratedPoint so that it no longer interferes when we change behaviors.
		hasGeneratedPoint = false;

		if(hasGeneratedGoToLocation)
		{
			moveToLocation();
			if(Vector3.Distance(transform.position, goToLocation) < roamingSpeed * Time.deltaTime)
			{
				hasGeneratedGoToLocation = false;
			}
		}

		else
		{
			if(!waitingToGenerate)
			{
				StartCoroutine("FindNewPoint");
				waitingToGenerate = true;
			}
		}
	}

	void moveToLocation()
	{
		//This abstracts movement to a particular position
//		transform.position = Vector3.MoveTowards(transform.position, goToLocation, roamingSpeed * Time.deltaTime);

		currentTime += Time.deltaTime;
		float t = currentTime / totalTime;

		if(t >=0 && t<=1)
		{
			transform.position = new Vector3(((1.0f-t) * (1.0f-t) * v0.x + t*v1.x) + t * ((1.0f-t)*v1.x + t*v2.x), ((1.0f-t) * (1.0f-t) * v0.y + t*v1.y) + t * ((1.0f-t)*v1.y + t*v2.y), 0.0f);
		}

		if(t > 1)
		{
			hasGeneratedGoToLocation = false;
		}
	}

	void LateUpdate()
	{
		//Fix Position within Box.
		{
			Vector3 tempPos = transform.position;
			if(transform.position.y > upLimit)
			{
				tempPos.y = upLimit;
			}
			if(transform.position.y < downLimit)
			{
				tempPos.y = downLimit;
			}
			if(transform.position.x > rightLimit)
			{
				tempPos.x = rightLimit;
			}
			if(transform.position.x < leftLimit)
			{
				tempPos.x = leftLimit;
			}
			
			transform.position = tempPos;
		}
	}

	IEnumerator FindNewPoint()
	{
		//Here is where I generate a go to Location, for the moment it is random.
		//The ghost stays in the place for roamWaitTime sec
		yield return new WaitForSeconds(roamWaitTime);
		goToLocation.Set(Random.Range(leftLimit, rightLimit), Random.Range(downLimit, upLimit), 0);

		v0 = transform.position;
		v1.Set(Random.Range(leftLimit, rightLimit), Random.Range(downLimit, upLimit), 0);
		v2 = goToLocation;

		totalTime = Vector3.Distance(v0, v1)/roamingSpeed + Vector3.Distance(v1, v2)/roamingSpeed;
		currentTime = 0;

		hasGeneratedGoToLocation = true;
		waitingToGenerate = false;
	}
}
