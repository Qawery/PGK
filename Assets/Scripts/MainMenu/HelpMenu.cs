using UnityEngine;
using System.Collections;
using MenuEnum;

public class HelpMenu : StandardMenu 
{
	public MenuManager menuManager;
	public AudioSource audioSource;
	public AudioClip menuReturn;
	
	public void BackButton()
	{
		menuManager.SwitchToMenu(MenuTypes.MAIN);
		if(menuReturn != null && audioSource != null)
		{
			audioSource.PlayOneShot(menuReturn);
		}
	}
}
