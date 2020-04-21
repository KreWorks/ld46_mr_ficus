using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
	public Image status;

	public Image temperatureLevel;
	public Image waterLevel;
	public Image lightlevel;
	public Image windLevel;

	public GameObject weather;

	public TMP_Text timeText;
	public TMP_Text dayText;
	public TMP_Text tempInsideText;
	public TMP_Text tempOutsideText;

	public GameObject endPanel;
	public TMP_Text hintText;

	public Color red;
	public Color orange;
	public Color yellow;
	public Color blue;
	public Color green;

	public Color dayTextColor;
	public Color nightTextColor;

	bool isHintPressed = false; 

	// Start is called before the first frame update
	void Start()
	{
		lightlevel.gameObject.SetActive(false);
		ChangeTextColor(true);
		endPanel.SetActive(false);
	}

	public void EndGamePanel()
	{
		endPanel.SetActive(true);
	}

	public void GetHint()
	{
		string[] hints = new string[] {
			"Mr Ficus does not like the wind.",
			"Mr Ficus likes when the temperature is between 20 and 22 °C.",
			"Mr Ficus does not like too much water.",
			"The temperature changes faster if you open the window.", 
			"A closed window does not let the wind inside."
		};


		string hint = "You cannot get all the hints at once...";

		if (!isHintPressed)
		{
			hint = hints[UnityEngine.Random.Range(0, hints.Length)];
			isHintPressed = true;
		}

		hintText.text = "Hint: " + hint;
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void ChangeTimeText(string timeString)
	{
		string hour = timeString.Substring(0, 2);
		if(hour == "08")
		{
			ChangeTextColor(false);
		}
		else if(hour == "21")
		{
			ChangeTextColor(true);
		}
		timeText.text = timeString;
	}

	public void ChangeDayText(int day)
	{
		dayText.text = day.ToString() + " days";
	}

	public void ChangeInsideTempText(float temp)
	{
		ChangeTemp(temp, false);
	}

	public void ChangeOutsideTempText(float temp)
	{
		ChangeTemp(temp, true);
	}

	void ChangeTextColor(bool isNight)
	{
		Color newColor = dayTextColor;
		if (isNight)
		{
			newColor = nightTextColor;
		}

		tempOutsideText.color = newColor;
		tempInsideText.color = newColor;

		Image[] icons = weather.GetComponentsInChildren<Image>();
		for (int i = 0; i < icons.Length; i++)
		{
			icons[i].color = newColor;
		}

		dayText.color = newColor;
		timeText.color = newColor;
	}

	void ChangeTemp(float temp, bool isOutside)
	{
		float correctedTemp = Mathf.RoundToInt(temp * 10) / 10.0f;
		string tempText = correctedTemp.ToString() + "°C";
		if (isOutside)
		{
			tempOutsideText.text = tempText;
		}
		else
		{
			tempInsideText.text = "[" + tempText + "]";
		}
	}

	public void ChangeStatusImage(Sprite newImage, Color newColor)
	{
		status.color = newColor;

		Image statusFace = status.GetComponentsInChildren<Image>()[1];

		statusFace.sprite = newImage;
	}

	public void ChangeWeatherIcons(Sprite[] newIcons)
	{
		Image[] icons = weather.GetComponentsInChildren<Image>();
		for(int i = 0; i < icons.Length; i++)
		{
			icons[i].sprite = newIcons[i]; 
		}
	}

	public void ChangeStatusTemperature(float temp)
	{
		if(temp >= 10.0f && temp < 15.0f)
		{
			temperatureLevel.color = red;
		}
		else if (temp >= 15.0f && temp < 20.0f)
		{
			temperatureLevel.color = yellow;
		}
		else if (temp >= 20.0f && temp < 22.0f)
		{
			temperatureLevel.color = green;
		}
		else if (temp >= 22.0f && temp < 28.0f)
		{
			temperatureLevel.color = yellow;
		}
		else
		{
			temperatureLevel.color = red;
		}

	}

	public void ChangeStatusWater(float level)
	{
		if (level < 25)
		{
			waterLevel.color = red;
		}
		else if (level >= 25 && level < 50)
		{
			waterLevel.color = yellow;
		}
		else if (level >= 50 && level < 75)
		{
			waterLevel.color = green;
		}
		else if (level >= 75)
		{
			waterLevel.color = yellow;
		}
	}

	public void ChangeStatusWind(int wind)
	{
		if (wind == 0)
		{
			windLevel.gameObject.SetActive(false);
		}
		else
		{
			windLevel.gameObject.SetActive(true);
			windLevel.color = red;
		}
	}
}

