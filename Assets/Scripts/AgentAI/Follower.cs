using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Follower : MonoBehaviour 
{
	private OmniSense sense;
	private PatrolAI patrolAI;
	private GameObject objectToFollow;

	void Start()
	{
		sense = GetComponent<OmniSense> ();
		patrolAI = GetComponent<PatrolAI> ();
		objectToFollow = null;
	}

	void Update()
	{
		if (sense != null && patrolAI != null && objectToFollow == null) 
		{
			List<GameObject> objects = sense.getObjectsInRange();
			if(objects != null && objects.Count > 0)
			{
				for(int i = 0; i < objects.Count; i++)
				{
					if(objects[i].tag == "Player")
					{
						objectToFollow = objects[i];
						break;
					}
				}
			}
		}
		if (objectToFollow != null)
		{
			patrolAI.SetPatrolRoute(objectToFollow.transform.position);
		}
	}
}
