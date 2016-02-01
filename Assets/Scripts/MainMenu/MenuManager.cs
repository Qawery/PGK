using UnityEngine;
using System.Collections;
using MenuEnum;

public class MenuManager : MonoBehaviour 
{
	public Camera mainCamera;
	public MainMenu mainMenu;
	public CreditsMenu creditsMenu;

	private StandardMenu currentMenu;
	private Vector3 mainMenuOriginalPosition;
	private Vector3 creditsMenuOriginalPosition;
	private Vector3 menuOffset;

	void Start () 
	{
		mainMenuOriginalPosition = mainMenu.transform.position;
		creditsMenuOriginalPosition = creditsMenu.transform.position;
		menuOffset = new Vector3 (0, 0, 5);

		currentMenu = mainMenu;
		SwitchToMenu (MenuTypes.MAIN);
	}

	public void SwitchToMenu(MenuTypes menuName)
	{
		currentMenu.DeactivateMenu();
		placeBackMenus();
		switch (menuName) 
		{
			case MenuTypes.MAIN:
				currentMenu = mainMenu;
			break;
		
			case MenuTypes.CREDITS:
				currentMenu = creditsMenu;
			break;

			default:
			break;
		}
		currentMenu.transform.position = mainCamera.transform.position;
		currentMenu.transform.position += menuOffset;
		currentMenu.ActivateMenu();
		return;
	}

	private void placeBackMenus()
	{
		mainMenu.transform.position = mainMenuOriginalPosition;
		creditsMenu.transform.position = creditsMenuOriginalPosition;
	}
}
