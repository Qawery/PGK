using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Opisuje warunki zwycięstwa i przegranej poziomu 01.
 * */
public class Level01 : MonoBehaviour 
{
	public GameObject player;
	public GameObject altar;
	private MainGameHUDController hudController;

	public List<EnemyWave> waveList;
	public List<EnemyWave> reinforcmentsList;
	private Health altarHealth;
	private Health playerHealth;
	private List<GameObject> currentEnemies;

	private bool changingScreen;
	private bool endGameInfoShown;
	private float endingDelay;
	private bool firstWave = true;

	void Start () 
	{
		changingScreen = false;
		endGameInfoShown = false;
		endingDelay = 3f;
		altarHealth = altar.GetComponent<Health> ();
		playerHealth = player.GetComponent<Health>();
		currentEnemies = new List<GameObject>();
		hudController = GetComponent<MainGameHUDController> ();
	}
	
	void Update () 
	{
		if(Input.GetButtonDown("Cancel"))
		{
			Application.LoadLevel("MainMenu");
		}
		if(Input.GetKeyDown(KeyCode.P))
		{
			//Pomoc ołtarza
			altarHealth.REGENERATION += 10;
		}
		if(Input.GetKeyDown(KeyCode.O))
		{
			//Kara ołtarza
			altarHealth.REGENERATION -= 10;
		}

		if(Input.GetKeyDown(KeyCode.L))
		{
			//pomoc gracza
			playerHealth.REGENERATION += 1;
		}
		if(Input.GetKeyDown(KeyCode.K))
		{
			//kara gracza
			playerHealth.REGENERATION -= 1;
		}

		if(hudController != null && playerHealth != null)
		{
			//uaktualnienie poziomu życia gracza
			hudController.SetPlayerHealth(playerHealth.getCurrentHealth()/playerHealth.MAX_HEALTH);
		}
		if(hudController != null && playerHealth != null)
		{
			//uaktualnienie poziomu życia ołtarza
			hudController.SetAltarHealth(altarHealth.getCurrentHealth()/altarHealth.MAX_HEALTH);
		}

		if(changingScreen)
		{
			changeToMainMenu();
		}

		if(FaliureCondition())
		{
			//Przegrana
			if(!endGameInfoShown)
			{
				if(player != null && player.GetComponent<Health>().IsAlive())
				{
					//wyświetlenie komunikatu o zniszczeniu ołtarza
					hudController.SetAltarDestroyedText();
				}
				else
				{
					//wyświetlenie komunikatu o śmierci
					hudController.SetDeathText();
				}
				endGameInfoShown = true;
			}

			if (!changingScreen)
			{
				changingScreen = true;
			}
			return;
		}
		if(VictoryCondition())
		{
			//Wygrana
			if(!endGameInfoShown)
			{
				//wyswietlenie komunikatu o wygranej
				hudController.SetWinText();
				endGameInfoShown = true;
			}

			if(!changingScreen)
			{
				changingScreen = true;
			}
			return;
		}

		//Rozgrywka kontynuuje
		Scenario ();
	}

	/**
	 * Zwraca warunek przegranej
	 * */
	public bool FaliureCondition()
	{
		return (altarHealth == null || !altarHealth.IsAlive() || playerHealth == null || !playerHealth.IsAlive());
	}

	/**
	 * Zwraca warunek wygranej
	 * */
	public bool VictoryCondition()
	{
		return (currentEnemies.Count <= 0 && waveList.Count <= 0);
	}

	/**
	 * Zarzdza rozgrywaniem scenariusza.
	 * */
	private void Scenario()
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
			}
			waveList.RemoveAt(0);
			if(firstWave)
			{
				firstWave = false;
			}
			else
			{
				player.GetComponent<Evolution>().IncreaseEvolution();
			}
			if(reinforcmentsList != null && reinforcmentsList.Count > 0)
			{
				//spawnowanie wsparcia
				reinforcmentsList[0].SpawnWave();
				reinforcmentsList.RemoveAt(0);
			}
			return;
		}
	}

	public void changeToMainMenu() 
	{
		endingDelay -= Time.deltaTime;
		if(endingDelay <= 0)
		{
			Application.LoadLevel("MainMenu");
		}
	}
}
