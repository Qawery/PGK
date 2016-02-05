using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level03 : DefaultLevel
{
	public List<GameObject> currentEnemies;
	public List<EnemyWave> waveList;
	public EnemyWave reinforcments;

	public override void Start () 
	{
		hud.SetObjectiveText ("Objectives: \n -Hold out as long as you can");
		if(player != null)
		{
			playerHealth = player.GetComponent<Health>();
			if(player.GetComponent<WeaponsManager>() != null)
			{
				player.GetComponent<WeaponsManager>().AcquireAmmo(100f);
			}
		}
		transitionTime = DEFAULT_TRANSITION_TIME;
		originalTimeScale = 1f;
		
		isVictoryConditionMet = false;
		isDefeatConditionMet = false;
	}
	
	protected override bool VictoryCondition()
	{
		if(isDefeatConditionMet)
		{
			return false;
		}
		if(currentEnemies.Count <= 0 && waveList.Count <= 0)
		{
			isVictoryConditionMet = true;
		}
		return isVictoryConditionMet;
	}
	
	protected override void ScenarioPlayUpdate()
	{
		if(currentEnemies.Count > 0)
		{
			//walka gracza z falą, oczyszczamy listę z trupów
			int i=0; 
			while(i < currentEnemies.Count)
			{
				if(currentEnemies[i] == null || currentEnemies[i].GetComponent<Health>() == null || !currentEnemies[i].GetComponent<Health>().IsAlive())
				{
					currentEnemies.RemoveAt(i);
				}
				else
				{
					i++;
				}
				if(currentEnemies.Count <= 0)
				{
					break;
				}
			}
			return;
		}
		else if(waveList != null && waveList.Count > 0)
		{
			//spawnowanie nowej fali
			if(waveList[0] != null)
			{
				List<GameObject> spawnedEnemyList = waveList[0].SpawnWave();
				if(spawnedEnemyList != null && spawnedEnemyList.Count > 0)
				{
					currentEnemies = spawnedEnemyList;
				}
				if(waveList.Count == 1)
				{
					reinforcments.SpawnWave();
				}
			}
			waveList.RemoveAt(0);
			return;
		}
	}
	
	protected override void VictoryTransition()
	{
		if(transitionTime >= DEFAULT_TRANSITION_TIME)
		{
			hud.SetMissionStatus("Victory");
			hud.ShowAnnoucment ();
		}
		
		transitionTime -= Time.deltaTime;
		if(transitionTime <= 0)
		{
			Application.LoadLevel("MainMenu");
		}
	}
}
