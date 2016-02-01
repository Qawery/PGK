using UnityEngine;
using System.Collections;

/**
 * Określa ewolucję gracza.
 * */
public class Evolution : MonoBehaviour 
{
	public GameObject playerAnimatorObject;
	private Animator animator;
	private int MAX_EVOLUTION_LEVEL = 4;
	private float[] movementModifiers = {400.0f, 450.0f, 500.0f, 550.0f, 600.0f};
	public GameObject[] corpses;
	private int currentEvolutionLevel = 0;
	public DirectProjectileWeapon[] weapons;

	void Start()
	{
		if(animator == null)
		{
			animator = playerAnimatorObject.GetComponent<Animator>();
		}
		for( int i = 0; i< weapons.Length; i++)
		{
			weapons[i].audioSource = gameObject.GetComponent<AudioSource>();
		}
	}

	public void IncreaseEvolution()
	{
		if(currentEvolutionLevel < MAX_EVOLUTION_LEVEL)
		{
			currentEvolutionLevel++;
			UpdatePlayerData();
		}
	}

	public int getEvolutionLevel()
	{
		return currentEvolutionLevel;
	}

	/**
	 * Uaktualnia dane gracza zgodnie z aktualnym poziomem ewolucji.
	 * */
	private void UpdatePlayerData()
	{
		GetComponent<Health>().corpse = corpses[currentEvolutionLevel];
		GetComponent<CameraAndControll> ().SetPlayerMovementModifier (movementModifiers[currentEvolutionLevel]);
		animator.SetTrigger("level_up");
	}
}
