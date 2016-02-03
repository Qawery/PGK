using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponsManager : MonoBehaviour 
{
	public DirectProjectileWeapon[] availableWeapons;
	private int currentWeaponIndex;
	private float availableAmmo;
	private const int MAX_AMMO = 100;
	
	void Start () 
	{
		availableAmmo = 0;
		if(availableWeapons != null && availableWeapons.Length > 0)
		{
			for(int i = availableWeapons.Length-1; i >= 0; i--)
			{
				currentWeaponIndex = i;
				HideWeapon();
			}
			EquipWeapon();
		}
	}
	
	void Update () 
	{
		if(Input.GetButton("Fire1"))
		{
			Fire();
		}
		else if(Input.GetKey(KeyCode.R))
		{
			Reload();
		}
		else if (Input.GetKeyDown(KeyCode.E))
		{
			SwitchToNextWeapon();
		}
		else if (Input.GetKeyDown(KeyCode.Q))
		{
			SwitchToPreviousWeapon();
		}
	}

	public void Aim(Vector3 newAimPoint)
	{
		if(availableWeapons[currentWeaponIndex] != null)
		{
			availableWeapons[currentWeaponIndex].Aim (newAimPoint);
			//TODO ANIMACJA CELOWANIA
		}
	}

	public void Fire()
	{
		if(availableWeapons[currentWeaponIndex] != null)
		{
			availableWeapons[currentWeaponIndex].Aim (null);
			availableWeapons[currentWeaponIndex].Fire ();
			//TODO ANIMACJA STRZELANIA
		}
	}

	public void Reload()
	{
		if(availableWeapons[currentWeaponIndex] != null && availableWeapons[currentWeaponIndex].GetRemainingReloadTime() <= 0)
		{
			float ammoNeeded = (availableWeapons[currentWeaponIndex].GetMagazineSize() - availableWeapons[currentWeaponIndex].GetMagazineAmmoCount())*availableWeapons[currentWeaponIndex].GetAmmoPerShot();
			if(availableAmmo >= ammoNeeded)
			{
				if(availableWeapons[currentWeaponIndex].ReloadWeapon(availableWeapons[currentWeaponIndex].GetMagazineSize()))
				{
					availableAmmo -= ammoNeeded;
				}
			}
			else
			{
				int loadedShots = Mathf.FloorToInt(availableAmmo/availableWeapons[currentWeaponIndex].GetAmmoPerShot());
				if(loadedShots > 0)
				{
					if(availableWeapons[currentWeaponIndex].ReloadWeapon(availableWeapons[currentWeaponIndex].GetMagazineAmmoCount() + loadedShots))
					{
						availableAmmo = (availableAmmo - (loadedShots*availableWeapons[currentWeaponIndex].GetAmmoPerShot()));
					}
				}
			}
			//TODO: ANIMACJA PRZEŁADOWANIA
		}
	}

	/**
	 * Uzupełnia zapasy amunicji;
	 * Zwraca ile amunicji zostało jako nadwyżka.
	 * */
	public float AcquireAmmo(float acquiredAmmo)
	{
		availableAmmo += acquiredAmmo;
		if(availableAmmo > MAX_AMMO)
		{
			float surplussAmmo = availableAmmo - MAX_AMMO;
			availableAmmo = MAX_AMMO;
			return surplussAmmo;
		}
		else
		{
			return 0;
		}
	}

	public void SwitchToNextWeapon()
	{
		HideWeapon ();
		currentWeaponIndex++;
		if(currentWeaponIndex >= availableWeapons.Length)
		{
			currentWeaponIndex = 0;
		}
		EquipWeapon();
	}

	public void SwitchToPreviousWeapon()
	{
		HideWeapon ();
		currentWeaponIndex--;
		if(currentWeaponIndex < 0)
		{
			currentWeaponIndex = availableWeapons.Length-1;
		}
		EquipWeapon();
	}

	private void HideWeapon()
	{
		availableWeapons [currentWeaponIndex].GetComponent<Renderer> ().enabled = false;
		Renderer[] childRenderers = availableWeapons [currentWeaponIndex].GetComponentsInChildren<Renderer> ();
		if(childRenderers != null)
		{
			for(int i=0; i < childRenderers.Length; i++)
			{
				childRenderers[i].enabled = false;
			}
		}
		//TODO ANIMACJA CHOWANIA BRONI
	}

	private void EquipWeapon()
	{
		availableWeapons [currentWeaponIndex].GetComponent<Renderer> ().enabled = true;
		Renderer[] childRenderers = availableWeapons [currentWeaponIndex].GetComponentsInChildren<Renderer> ();
		if(childRenderers != null)
		{
			for(int i=0; i < childRenderers.Length; i++)
			{
				childRenderers[i].enabled = true;
			}
		}
		//TODO ANIMACJA WYJMOWANIA BRONI
	}
}