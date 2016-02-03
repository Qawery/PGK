using UnityEngine;
using System.Collections;
using MenuEnum;

public class MainMenu : StandardMenu
{
	public MenuManager menuManager;
	public Canvas mainMenuCanvas;
	
	public void NewGameButton()
	{
		menuManager.PlayMenuSelectSound();
		Application.LoadLevel("Level_01");
	}

	public void LevelSelectButton()
	{
		menuManager.PlayMenuSelectSound();
		menuManager.SwitchToMenu(MenuTypes.LEVEL_SELECT);
	}

	public void HelpButton()
	{
		menuManager.PlayMenuSelectSound();
		menuManager.SwitchToMenu(MenuTypes.HELP);
	}

	public void CreditsButton()
	{
		menuManager.PlayMenuSelectSound();
		menuManager.SwitchToMenu(MenuTypes.CREDITS);
	}

	public void ExitButton()
	{
		menuManager.PlayMenuReturnSound ();
		Application.Quit();
	}
}
