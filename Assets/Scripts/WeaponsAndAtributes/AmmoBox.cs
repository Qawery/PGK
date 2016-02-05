using UnityEngine;
using System.Collections;

public class AmmoBox : MonoBehaviour 
{
	public float availableAmmo;

	void Update()
	{
		if(availableAmmo <= 0)
		{
			Destroy(gameObject);
		}
	}

	public float AcquireAmmo(float ammoReduction)
	{
		if(ammoReduction <= availableAmmo)
		{
			availableAmmo -= ammoReduction;
			return ammoReduction;
		}
		else
		{
			float ammoReturned = availableAmmo;
			availableAmmo  = 0;
			return ammoReturned;
		}
	}

	public void ReturnAmmo(float ammo)
	{
		availableAmmo += ammo;
	}
}
