using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Opisuje fabrykę tworzącą potwory.
 * */
public class EnemySpawner : MonoBehaviour 
{
	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}

	public List<GameObject> spawnEnemyGroup(SpawnInformation spawnInformation)
	{
		List<GameObject> spawnedEnemies = new List<GameObject>();

		if(spawnInformation != null && spawnInformation.enemyType != null && spawnInformation.enemyNumber > 0)
		{
			for(int i = 0; i < spawnInformation.enemyNumber; i++)
			{
				GameObject singleSpawnedEnemy = SpawnEnemy(spawnInformation.enemyType);
				singleSpawnedEnemy.GetComponent<PatrolAI>().SetPatrolRoute(spawnInformation.patrolRoute);
				singleSpawnedEnemy.GetComponent<PatrolAI>().SetPatrolEnding(spawnInformation.patrolEndingPolicy);
				singleSpawnedEnemy.GetComponent<PatrolAI>().SetTargetResolvePolicy(spawnInformation.targetResolvePolicy);
				spawnedEnemies.Add(singleSpawnedEnemy);
			}
		}
		return spawnedEnemies;
	}

	private GameObject SpawnEnemy(GameObject enemyToSpawn)
	{
		GameObject newEnemyInstance = (GameObject) Instantiate(enemyToSpawn, transform.position, transform.rotation) as GameObject;
		return newEnemyInstance;
	}
}
