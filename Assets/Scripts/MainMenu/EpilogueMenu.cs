using UnityEngine;
using System.Collections;
using MenuEnum;

public class EpilogueMenu : StandardMenu
{
	public EpilogueManager menuManager;
	
	public void ExitButton()
	{
		menuManager.PlayMenuSelectSound ();
		Application.LoadLevel ("MainMenu");
	}
}
