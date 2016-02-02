using UnityEngine;
using System.Collections;

/**
 * Opisuje sposoby rozróżniania swój-wróg w drużynach.
 * */
public static class Team
{
	/**
	 * Określa czy itsGameObject jest wrogiem myGameObject na podstawie ich tagów.
	 * Zwraca true jeśli są wrogami.
	 * */
	public static bool isItMyEnemy(GameObject myGameObject, GameObject itsGameObject)
	{
		if((myGameObject.tag == "Enemy" && itsGameObject.tag == "Player") || 
		   (myGameObject.tag == "Player" && itsGameObject.tag == "Enemy") || 
		   (myGameObject.tag == "Enemy" && itsGameObject.tag == "Ally") || 
		   (myGameObject.tag == "Ally" && itsGameObject.tag == "Enemy")
		   )
		{
			return true;
		}
		return false;
	}

	public static bool isItMyEnemy(string myTag, GameObject itsGameObject)
	{
		if((myTag == "Enemy" && itsGameObject.tag == "Player") || 
		   (myTag == "Player" && itsGameObject.tag == "Enemy") || 
		   (myTag == "Enemy" && itsGameObject.tag == "Ally") || 
		   (myTag == "Ally" && itsGameObject.tag == "Enemy")
		   )
		{
			return true;
		}
		return false;
	}

	/**
	 * Określa czy itsGameObject jest sojusznikiem myGameObject na podstawie ich tagów.
	 * Zwraca true jeśli są sojusznikami.
	 * */
	public static bool isItMyAlly(GameObject myGameObject, GameObject itsGameObject)
	{
		if((myGameObject == itsGameObject) ||
			(myGameObject.tag == itsGameObject.tag) ||
		     (myGameObject.tag == "Player" && itsGameObject.tag == "Ally") || 
		     (myGameObject.tag == "Ally" && itsGameObject.tag == "Player")
		     )
		{
			return true;
		}
		return false;
	}

	public static bool isItMyAlly(string myTag, GameObject itsGameObject)
	{
		if((myTag == itsGameObject.tag) ||
		   (myTag == "Player" && itsGameObject.tag == "Ally") || 
		   (myTag == "Ally" && itsGameObject.tag == "Player")
		   )
		{
			return true;
		}
		return false;
	}
}
