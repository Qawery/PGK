using UnityEngine;
using System.Collections;
using MenuEnum;

public class CreditsMenu : StandardMenu
{
	public MenuManager menuManager;

	public void BackButton()
	{
		menuManager.SwitchToMenu(MenuTypes.MAIN);
		menuManager.PlayMenuReturnSound();
	}
}
