using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Określa zmysł omnisense widzący przez wszystkie przeszkody.
 * */
public class OmniSense : MonoBehaviour
{
	public float omniSenseRange;	//Określa zasięg omnisense
	private List<GameObject> gameObjectsInRange;

	void Start()
	{
		gameObjectsInRange = new List<GameObject> ();
	}

	/**
	 * Zwraca wykryte przez omnisense obiekty w formie tablicy.
	 * */
	public void detectGameObjects()
	{
		gameObjectsInRange.Clear();
		Collider[] collidersInRange = Physics.OverlapSphere(transform.position, omniSenseRange);
		if(collidersInRange == null || collidersInRange.Length == 0)
		{
			return;
		}

		for (var i = 0; i < collidersInRange.Length; i++) 
		{
			if(collidersInRange[i].gameObject.tag != "Enviorment" && collidersInRange[i].gameObject != gameObject)
			{
				gameObjectsInRange.Add(collidersInRange[i].gameObject);
			}
		}
		return;
	}
	
	/**
	 * Sprawdza czy "gameObject" jest w zasięgu omnisense z pozycji "ownerPosition".
	 * */
	public bool isDetected(GameObject gameObject)
	{
		return gameObjectsInRange.Contains(gameObject);
	}

	public List<GameObject> getObjectsInRange()
	{
		return gameObjectsInRange;
	}
}
