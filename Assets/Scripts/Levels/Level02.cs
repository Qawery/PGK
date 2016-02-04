using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/**
 * Opisuje warunki zwycięstwa i przegranej poziomu 02.
 * */
public class Level02 : DefaultLevel
{
	public ZoneTrigger exit;
	
	public override void Start () 
	{
		hud.SetObjectiveText ("Objectives: \n Reach rally point \n Eliminate every enemy in rally point");
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
		return isVictoryConditionMet;
	}
}