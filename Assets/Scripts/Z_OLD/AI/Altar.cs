using UnityEngine;
using System.Collections;

public class Altar : MonoBehaviour 
{
	public ParticleSystem death;
	public ParticleSystem continuus_1;
	public ParticleSystem continuus_2;
	private Health health;

	// Use this for initialization
	void Start ()
	{
		health = GetComponent<Health> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(health.IsAlive())
		{
			death.Stop();
			continuus_1.Play();
			continuus_2.Play();
		}
		else
		{
			continuus_1.Stop();
			continuus_2.Stop();
			death.Play();
		}
	}
}
