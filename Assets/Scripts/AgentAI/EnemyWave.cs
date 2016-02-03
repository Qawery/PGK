using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyWave : MonoBehaviour 
{
	public List<SpawnInformation> spawnInformationList;

	public List<GameObject> SpawnWave()
	{
		List<GameObject> enemiesSpawnedInWave = new List<GameObject> ();
		if(spawnInformationList != null && spawnInformationList.Count > 0)
		{
			for(int i = 0; i < spawnInformationList.Count; i++)
			{
				if(spawnInformationList[i] != null && spawnInformationList[i].enemySpawner != null && spawnInformationList[i].enemyNumber > 0 && spawnInformationList[i].enemyType != null)
				{
					List<GameObject> enemiesSpawnedBySpawner = spawnInformationList[i].enemySpawner.spawnEnemyGroup(spawnInformationList[i]);
					for(int j = 0; j < enemiesSpawnedBySpawner.Count; j++)
					{
						enemiesSpawnedInWave.Add(enemiesSpawnedBySpawner[j]);
					}
				}
			}
		}
		return enemiesSpawnedInWave;
	}
}
