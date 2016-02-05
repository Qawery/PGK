using UnityEngine;
using System.Collections;
using MenuEnum;

public class EpilogueManager : MonoBehaviour 
{
	public Camera mainCamera;
	public EpilogueMenu epilogueMenu;
	
	public AudioSource audioSource;
	public AudioClip menuSelect;
	public AudioClip menuReturn;
	
	private StandardMenu currentMenu;
	
	private Vector3 epilogueMenuOriginalPosition;
	
	private Vector3 menuOffset;
	
	void Start () 
	{
		Cursor.visible = true;
		
		epilogueMenuOriginalPosition = epilogueMenu.transform.position;
		menuOffset = new Vector3 (0, 0, 5);
		
		currentMenu = epilogueMenu;
		SwitchToMenu (MenuTypes.MAIN);
	}
	
	public void SwitchToMenu(MenuTypes menuName)
	{
		placeBackMenus();
		switch (menuName) 
		{
			
		case MenuTypes.MAIN:
			currentMenu = epilogueMenu;
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
		epilogueMenu.transform.position = epilogueMenuOriginalPosition;
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
