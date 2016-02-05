using UnityEngine;
using System.Collections;
using MenuEnum;

public class IntroMenu : StandardMenu
{
	public IntroManager menuManager;
	
	public void StartButton()
	{
		menuManager.PlayMenuSelectSound ();
		Application.LoadLevel ("Level_01");
	}
}
