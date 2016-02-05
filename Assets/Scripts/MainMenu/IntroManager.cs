using UnityEngine;
using System.Collections;
using MenuEnum;

public class IntroManager : MonoBehaviour 
{
	public Camera mainCamera;
	public IntroMenu introMenu;
	
	public AudioSource audioSource;
	public AudioClip menuSelect;
	public AudioClip menuReturn;
	
	private StandardMenu currentMenu;
	
	private Vector3 introMenuOriginalPosition;
	
	private Vector3 menuOffset;
	
	void Start () 
	{
		Cursor.visible = true;
		
		introMenuOriginalPosition = introMenu.transform.position;
		menuOffset = new Vector3 (0, 0, 5);
		
		currentMenu = introMenu;
		SwitchToMenu (MenuTypes.MAIN);
	}
	
	public void SwitchToMenu(MenuTypes menuName)
	{
		placeBackMenus();
		switch (menuName) 
		{

		case MenuTypes.MAIN:
			currentMenu = introMenu;
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
		introMenu.transform.position = introMenuOriginalPosition;
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
