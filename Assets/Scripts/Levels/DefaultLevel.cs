using UnityEngine;
using System.Collections;

public class DefaultLevel : MonoBehaviour 
{
	public HUD hud;
	public GameObject player;

	protected Health playerHealth;

	protected float transitionTime;
	private const float DEFAULT_TRANSITION_TIME = 4f;

	protected float originalTimeScale;


	void Start () 
	{
		if(player != null)
		{
			playerHealth = player.GetComponent<Health>();
		}
		transitionTime = DEFAULT_TRANSITION_TIME;
		originalTimeScale = 1f;
	}
	
	void Update () 
	{
		InputResolve();
		ScenarioStatusUpdate();
		HudUpdate();
	}
	
	protected void InputResolve()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			AbortLevel();
		}
		if(Input.GetKeyDown(KeyCode.P))
		{
			TogglePause();
		}
	}

	public void TogglePause()
	{
		if(Time.timeScale == 0)
		{
			ResumeLevel();
		}
		else
		{
			PauseLevel();
		}
	}

	public void ResumeLevel()
	{
		if(Time.timeScale == 0)
		{
			hud.HidePause();
			Time.timeScale = originalTimeScale;
		}
	}

	public void PauseLevel()
	{
		if(Time.timeScale != 0)
		{
			hud.ShowPause();
			originalTimeScale = Time.timeScale;
			Time.timeScale = 0;
		}
	}

	public void RestartLevel()
	{
		Application.LoadLevel(Application.loadedLevelName);
	}

	public void AbortLevel()
	{
		Application.LoadLevel("MainMenu");
	}

	protected virtual void HudUpdate()
	{
		if(playerHealth != null)
		{
			hud.SetPlayerHealth(playerHealth.GetCurrentHealth()/playerHealth.MAX_HEALTH);
		}
		else
		{
			hud.SetPlayerHealth(1f);
		}
	}

	protected virtual void ScenarioStatusUpdate()
	{
		if(VictoryCondition())
		{
			VictoryTransition();
		}
		else if(DefeatCondition())
		{
			DefeatTransition();
		}
		else
		{
			ScenarioPlayUpdate();
		}
	}
	
	protected virtual bool VictoryCondition()
	{
		return false;
	}
	
	protected virtual void VictoryTransition()
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
	
	protected virtual bool DefeatCondition()
	{
		if(playerHealth != null  && !playerHealth.IsAlive())
		{
			return true;
		}
		return false;
	}
	
	protected virtual void DefeatTransition()
	{
		if(transitionTime >= DEFAULT_TRANSITION_TIME)
		{
			hud.SetMissionStatus("Defeat");
			hud.ShowAnnoucment ();
		}
		
		transitionTime -= Time.deltaTime;
		if(transitionTime <= 0)
		{
			Application.LoadLevel("MainMenu");
		}
	}
	
	protected virtual void ScenarioPlayUpdate()
	{
		
	}
}
