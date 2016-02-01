using UnityEngine;
using System.Collections;
using MenuEnum;

public class MainMenu : StandardMenu 
{
	public AudioSource audioSource;
	public AudioClip menuSelect;
	public AudioClip menuReturn;

	public MenuManager menuManager;
	public Canvas mainMenuCanvas;
	
	public void NewGameButton()
	{
		if(menuSelect != null && audioSource != null)
		{
			audioSource.PlayOneShot(menuSelect);
		}
		//MenuManager fadeout
		Application.LoadLevel("Level_01");
	}

	public void CreditsButton()
	{
		if(menuSelect != null && audioSource != null)
		{
			audioSource.PlayOneShot(menuSelect);
		}
		menuManager.SwitchToMenu(MenuTypes.CREDITS);
	}

	public void ExitButton()
	{
		if(menuReturn != null && audioSource != null)
		{
			audioSource.PlayOneShot(menuReturn);
		}
		//MenuManager fadeout
		Application.Quit();
	}
}
