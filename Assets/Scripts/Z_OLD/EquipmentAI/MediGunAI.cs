using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MediGunAI : EquipmentPieceAI
{
	public float personalSpaceRadius = 3;
	
	public override List<TargetRate> rateTargets(List<GameObject> detectedGameObjects)
	{
		List<TargetRate> targetRateList = new List<TargetRate> ();
		if(detectedGameObjects != null && detectedGameObjects.Count > 0)
		{
			//Wstępna filtracja obiektów
			List<GameObject> tempDetectedObjects = new List<GameObject>();
			for(int i = 0; i < detectedGameObjects.Count; i++)
			{
				if(detectedGameObjects[i].GetComponent<Health>() != null &&
				   Team.isItMyAlly(gameObject, detectedGameObjects[i]))
				{
					tempDetectedObjects.Add(detectedGameObjects[i]);
				}
			}
			
			//Rating wybranych obiektów
			if(tempDetectedObjects.Count > 0)
			{
				float rate = 0f;
				float currentHealth = 0f;
				for(int i = 0; i < tempDetectedObjects.Count; i++)
				{
					//sprawdzenie poziomu życia
					currentHealth = tempDetectedObjects[i].GetComponent<Health>().getCurrentHealth() / tempDetectedObjects[i].GetComponent<Health>().MAX_HEALTH;
					if(currentHealth < 0.5)
					{
						rate = 8;
					}
					else
					{
						rate = 4;
					}
					
					//sprawdzenie czy poprzednio to był nasz cel
					EquipmentAI equipmentAI = GetComponent<EquipmentAI>();
					if(equipmentAI != null && equipmentAI.lastTarget != null && equipmentAI.lastTarget == tempDetectedObjects[i])
					{
						rate += 4;
					}
					
					//sprawdzenie odległości
					if(Vector3.Distance(transform.position, tempDetectedObjects[i].transform.position) < personalSpaceRadius)
					{
						rate += 5;
					}
					
					//normalizacja i zapisanie wyniku
					rate = rate/17;
					targetRateList.Add(new TargetRate(tempDetectedObjects[i], rate));
				}
			}
		}
		return targetRateList;
	}
}
