using UnityEngine;
using System.Collections;
using MenuEnum;

public class LevelSelectMenu : StandardMenu
{
	public MenuManager menuManager;
	public Canvas levelSelectCanvas;

	public void Level_1_Button()
	{
		menuManager.PlayMenuSelectSound();
		Application.LoadLevel("Level_01");
	}

	public void Level_2_Button()
	{
		menuManager.PlayMenuSelectSound();
		Application.LoadLevel("Level_02");
	}

	public void Level_3_Button()
	{
		menuManager.PlayMenuSelectSound();
		Application.LoadLevel("Level_03");
	}

	public void BackButton()
	{
		menuManager.SwitchToMenu(MenuTypes.MAIN);
		menuManager.PlayMenuReturnSound();
	}
}
