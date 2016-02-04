using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/**
 * Opisuje warunki zwycięstwa i przegranej poziomu 01.
 * */
public class Level01 : DefaultLevel
{
	public ZoneTrigger exit;
	/*
	public override void Start () 
	{
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
	}*/

	protected override bool VictoryCondition()
	{
		if(isDefeatConditionMet)
		{
			return false;
		}
		if(exit.isPlayerInRange)
		{
			isVictoryConditionMet = true;
		}
		return isVictoryConditionMet;
	}
}