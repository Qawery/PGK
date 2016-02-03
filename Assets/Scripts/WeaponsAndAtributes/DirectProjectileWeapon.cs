using UnityEngine;
using System.Collections;

/**
 * Broń strzelająca bezpośrednio.
 * */
public class DirectProjectileWeapon : EquipmentAndWeapon
{
	public AudioSource audioSource;
	public AudioClip firingSound;
	public GameObject bullet;
	public float reloadTime;
	private float remainingReloadTime;
	private Vector3 aimpoint;

	void Start()
	{
		remainingReloadTime = 0;
	}

	void Update()
	{
		if(remainingReloadTime > 0)
		{
			remainingReloadTime -= Time.deltaTime;
			if(remainingReloadTime < 0)
			{
				remainingReloadTime = 0;
			}
		}
	}

	public void aim(GameObject gameObject)
	{
		aim (gameObject.transform.position);
	}

	public void aim(Vector3 point)
	{
		aimpoint = point;
	}

	public void fire()
	{
		if(!isReadyToFire())
		{
			return;
		}
		remainingReloadTime = reloadTime;

		//Strzelanie pociskami
		Quaternion aimRotation;
		Vector3 directionToAim;
		directionToAim = (aimpoint - transform.position).normalized;
		aimRotation = Quaternion.LookRotation(directionToAim);
		var newBulletInstance = (GameObject) Instantiate(bullet, transform.position + (aimpoint - transform.position).normalized, aimRotation) as GameObject;
		newBulletInstance.GetComponent<Bullet> ().setOwner (gameObject);
		if(newBulletInstance.GetComponent<Rigidbody>() != null)
		{
			newBulletInstance.GetComponent<Rigidbody>().AddForce((transform.position-directionToAim).normalized * newBulletInstance.GetComponent<Bullet>().getSpeed());
		}

		if(audioSource != null && firingSound != null)
		{
			audioSource.PlayOneShot(firingSound);
		}
	}

	public bool isReadyToFire()
	{
		if(remainingReloadTime > 0)
		{
			return false;
		}
		return true;
	}

	public float getRemainingReloadTime()
	{
		return remainingReloadTime;
	}
}
