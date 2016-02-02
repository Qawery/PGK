using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// do użycia SetHealth(float), SetWin(), SetTemplateDestroyed(), SetLose()
// podpiąć public!
public class MainGameHUDController : MonoBehaviour 
{
	public Slider playerHealthSlider;
	public Slider altarHealthSlider;
    public Text winText;
    public Text altarDestroyedText;
    public Text loseText;

    public void SetPlayerHealth(float value)
    {
		playerHealthSlider.normalizedValue = value;
    }

	public void SetAltarHealth(float value)
	{
		altarHealthSlider.normalizedValue = value;
	}
	
    public void SetWinText()
    {
		winText.text = "Victory!";
    }

    public void SetAltarDestroyedText()
    {
        altarDestroyedText.text = "Altar destroyed!";
    }

    public void SetDeathText()
    {
        loseText.text = "You died!";
    }
}
