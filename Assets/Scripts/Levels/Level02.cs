using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/**
 * Opisuje warunki zwycięstwa i przegranej poziomu 02.
 * */
public class Level02 : DefaultLevel
{
	public ZoneTrigger exit;
	public List<GameObject> currentEnemies;

	public override void Start () 
	{
		hud.SetObjectiveText ("Objectives: \n -Reach rally point \n -Eliminate every enemy in rally point");
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
		if(exit.isPlayerInRange && currentEnemies.Count <= 0)
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
			Application.LoadLevel("Level_02");
		}
	}
}