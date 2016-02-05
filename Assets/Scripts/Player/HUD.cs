using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * 
 * */
public class HUD : MonoBehaviour 
{
	public Slider healthSlider;
	public Text missionAnnoucmentText;
	public Text missionStatusText;
	public Text pauseText;
	public Text objectiveText;
	public Text ammoCount;
	public Text weaponName;

	private float annoucmentTime;

	void Start()
	{
		annoucmentTime = -1f;
		missionAnnoucmentText.enabled = false;
		missionStatusText.enabled = false;
		pauseText.enabled = false;
		objectiveText.enabled = false;
	}

	void Update()
	{
		if(annoucmentTime >= 0)
		{
			annoucmentTime -= Time.deltaTime;
			if(annoucmentTime < 0)
			{
				if(missionAnnoucmentText.enabled)
				{
					missionAnnoucmentText.enabled = false;
				}
			}
		}
	}

	//Metody uaktualniania pasków i wskaźników
    public void SetPlayerHealth(float value)
    {
		healthSlider.normalizedValue = value;
    }

	//Metody textu ogłoszenia
	public void SetAnnoucment(string annoucmentText)
	{
		missionAnnoucmentText.text = annoucmentText;
	}

	public void ShowAnnoucment()
	{
		missionAnnoucmentText.enabled = true;
	}

	public void ShowAnnoucment(float time)
	{
		annoucmentTime = time;
		missionAnnoucmentText.enabled = true;
	}

	public void HideAnnoucment()
	{
		annoucmentTime = -1;
		missionAnnoucmentText.enabled = false;
	}

	//Metody textu stanu misji
	public void SetMissionStatus(string annoucmentText)
	{
		missionAnnoucmentText.text = annoucmentText;
	}
	
	public void ShowMissionStatus()
	{
		missionAnnoucmentText.enabled = true;
	}
	
	public void HideMissionStatus()
	{
		missionAnnoucmentText.enabled = false;
	}

	//Metody textu pauzy	
	public void ShowPause()
	{
		pauseText.enabled = true;
		objectiveText.enabled = true;
	}
	
	public void HidePause()
	{
		pauseText.enabled = false;
		objectiveText.enabled = false;
	}

	public void SetObjectiveText(string text)
	{
		objectiveText.text = text;
	}

	public void SetWeaponCount(float magazine, float magazineCapacity, float total)
	{
		ammoCount.text = ("Weapon: " + magazine + "/" + magazineCapacity + "\n" + "Total: " + total);
	}

	public void SetWeaponName(string name)
	{
		weaponName.text = name;
	}
}
