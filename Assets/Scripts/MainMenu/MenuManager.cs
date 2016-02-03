using UnityEngine;
using System.Collections;
using MenuEnum;

public class MenuManager : MonoBehaviour 
{
	public Camera mainCamera;
	public MainMenu mainMenu;
	public LevelSelectMenu levelSelect;
	public HelpMenu help;
	public CreditsMenu credits;

	public AudioSource audioSource;
	public AudioClip menuSelect;
	public AudioClip menuReturn;

	private StandardMenu currentMenu;

	private Vector3 mainMenuOriginalPosition;
	private Vector3 levelSelectOriginalPosition;
	private Vector3 helpOriginalPosition;
	private Vector3 creditsOriginalPosition;

	private Vector3 menuOffset;

	void Start () 
	{
		Cursor.visible = true;

		mainMenuOriginalPosition = mainMenu.transform.position;
		levelSelectOriginalPosition = levelSelect.transform.position;
		helpOriginalPosition = help.transform.position;
		creditsOriginalPosition = credits.transform.position;
		menuOffset = new Vector3 (0, 0, 5);

		currentMenu = mainMenu;
		SwitchToMenu (MenuTypes.MAIN);
	}

	public void SwitchToMenu(MenuTypes menuName)
	{
		placeBackMenus();
		switch (menuName) 
		{
			case MenuTypes.MAIN:
				currentMenu = mainMenu;
			break;

			case MenuTypes.LEVEL_SELECT:
				currentMenu = levelSelect;
			break;

			case MenuTypes.HELP:
				currentMenu = help;
			break;
		
			case MenuTypes.CREDITS:
				currentMenu = credits;
			break;

			default:
			break;
		}
		currentMenu.transform.position = mainCamera.transform.position;
		currentMenu.transform.position += menuOffset;
		return;
	}

	private void placeBackMenus()
	{
		mainMenu.transform.position = mainMenuOriginalPosition;
		levelSelect.transform.position = levelSelectOriginalPosition;
		credits.transform.position = creditsOriginalPosition;
		help.transform.position = helpOriginalPosition;
	}

	public void PlayMenuSelectSound()
	{
		if(menuSelect != null && audioSource != null)
		{
			audioSource.PlayOneShot(menuSelect);
		}
	}

	public void PlayMenuReturnSound()
	{
		if(menuReturn != null && audioSource != null)
		{
			audioSource.PlayOneShot(menuReturn);
		}
	}
}
