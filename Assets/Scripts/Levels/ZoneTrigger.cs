using UnityEngine;
using System.Collections;

public class ZoneTrigger : MonoBehaviour 
{
	public bool isPlayerInRange;
	private float timeSciencePlayerInRange;

	void Start()
	{
		isPlayerInRange = false;
	}

	void Update()
	{
		if(timeSciencePlayerInRange >= 0)
		{
			timeSciencePlayerInRange -= Time.deltaTime;
			if(timeSciencePlayerInRange < 0)
			{
				isPlayerInRange = false;
			}
		}
	}

	public void OnTriggerEnter(Collider collider)
	{
		OnTriggerStay (collider);
	}

	public void OnTriggerStay(Collider collider)
	{
		if (collider.tag == "Player") 
		{
			isPlayerInRange = true;
			timeSciencePlayerInRange = 0.5f;
		}
	}

	public void OnTriggerExit(Collider collider)
	{
		if (collider.tag == "Player") 
		{
			isPlayerInRange = false;
			timeSciencePlayerInRange = 0.5f;
		}
	}
}
