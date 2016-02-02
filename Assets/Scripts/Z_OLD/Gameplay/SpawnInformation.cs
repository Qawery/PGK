using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PatrolEnums;

public class SpawnInformation : MonoBehaviour
{
	public EnemySpawner enemySpawner = null;
	public TargetResolvePolicy targetResolvePolicy;
	public PatrolEndingPolicy patrolEndingPolicy;
	public List<Vector3> patrolRoute;
	public GameObject enemyType = null;
	public int enemyNumber = 0;
}
