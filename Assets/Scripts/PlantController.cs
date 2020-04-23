using System.Collections.Generic;
using UnityEngine;
using System;

public class PlantController : MonoBehaviour
{
	public Sprite deadIcon;
	public Sprite freezeIcon;
	public Sprite delightIcon;
	public Sprite pleaseIcon;
	public Sprite neutralIcon;
	public Sprite sadIcon;
	public Sprite tragicIcon;

	public UiController uiController;
	public WindowsController window;

	public ParticleSystem sparkle;

	public PlantState plantState;

	public DeadPlantState deadState;
	public FreezePlantState freezeState;
	public DelightedPlantState delightState;
	public PleasedPlantState pleaseState;
	public NeutralPlantState neutralState;
	public SadPlantState sadState;
	public TragicPlantState tragicState;

	public Action<float> OnWaterLevelChangeHandler;
	public Action<float> OnTempChangeHandler;

	float feelScale;

	float waterLevel;
	// 0- 100
	/*
	0-25 dry
	25-50 low 
	50-75 normal
	75-100 over 
	*/
	float outsideTemp;
	float temperature; // 10-35
	/*
	 10-15 freeze
	 15-20 cold
	 20-25 good
	 25-30 warn
	 30-35 hot
		 */
	float lightLevel; // 0 -5
	int windLevel; // 0-5

	List<GameObject> leaves;
	List<GameObject> parts;

	// Start is called before the first frame update
	void Start()
    {
		deadState =  new DeadPlantState(this, deadIcon, uiController.red);
		freezeState = new FreezePlantState(this, freezeIcon, uiController.blue);
		delightState = new DelightedPlantState(this, delightIcon, uiController.green);
		pleaseState = new PleasedPlantState(this, pleaseIcon, uiController.green);
		neutralState = new NeutralPlantState(this, neutralIcon, uiController.yellow);
		sadState = new SadPlantState(this, sadIcon, uiController.red);
		tragicState = new TragicPlantState(this, tragicIcon, uiController.red);

		TransitionToState( pleaseState);

		waterLevel = 60.0f;
		temperature = 21.0f;
		lightLevel = 2;
		windLevel = 0;

	}

    // Update is called once per frame
    void Update()
    {
		Grow();
		UseWater();
		ChangeTemperature();
		CheckFeel();
    }

	void UseWater()
	{
		float water = GetWaterUsage();
		waterLevel -= water * Time.deltaTime;

		OnWaterLevelChangeHandler?.Invoke(waterLevel);
	}

	void ChangeTemperature()
	{
		if (window.windowState.IsOpened())
		{
			temperature = Mathf.Lerp(temperature, outsideTemp, 0.05f * Time.deltaTime);
		}
		else
		{
			temperature = Mathf.Lerp(temperature, outsideTemp, 0.005f * Time.deltaTime);
		}

		OnTempChangeHandler?.Invoke(temperature);
	}

	void Grow()
	{

	}

	void CheckFeel()
	{
		int feel = GetFeelIndex();

		feelScale += feel * Time.deltaTime;

		if (feelScale > 20)
		{
			plantState.GetBetter();
		}
		if (feelScale < -10)
		{
			plantState.GetWorse();
		}

	}

	int GetFeelIndex()
	{
		int feel = 0;
		if (temperature >= 10.0f && temperature < 15.0f)
		{
			feel += -2;
		}
		else if (temperature >= 15.0f && temperature < 20.0f)
		{
			feel += -1;
		}
		else if (temperature >= 20.0f && temperature < 22.0f)
		{
			feel += 1;
		}
		else if (temperature >= 22.0f && temperature < 28.0f)
		{
			feel += -1;
		}
		else if (temperature >= 28.0f)
		{
			feel += -2;
		}

		//Modifier based on the current water level
		if (waterLevel > 75.0f)
		{
			feel += -1;
		}
		else if(waterLevel >= 50.0f && waterLevel < 75.0f)
		{
			feel += 1;
		}
		else if (waterLevel >= 25.0f && waterLevel < 50.0f)
		{
			feel += -2;
		}
		else if (waterLevel < 25.0f)
		{
			feel += -3;
		}

		if (window.windowState.IsOpened())
		{
			feel += -1 * windLevel;
		}
		else
		{
			feel += 1;
		}

		return feel;
	}

	public void TransitionToState(PlantState newState)
	{
		uiController.ChangeStatusImage(newState.GetIcon(), newState.GetColor());
		plantState = newState;

		feelScale = 0;

		if(newState == deadState)
		{
			uiController.EndGamePanel();
		}
	}

	public void WeatherChangeAffect(int windLevel)
	{
		this.windLevel = windLevel;
		if (window.windowState.IsOpened() || window.windowState.IsSliding())
		{
			uiController.ChangeStatusWind(windLevel);
		}
		else
		{
			uiController.ChangeStatusWind(0);
		}
	}

	public void OpenWindowAffect()
	{
		WeatherChangeAffect(windLevel);
	}

	public void SetOutsideTemperature(float temp)
	{
		outsideTemp = temp;
	}

	public float GetWaterUsage()
	{
		float waterUsage = 0.0f;
		// Based on the temperature
		if (temperature >= 10.0f && temperature < 15.0f)
		{
			waterUsage = 1.0f;
		}
		else if(temperature >= 15.0f && temperature < 20.0f)
		{
			waterUsage = 1.7f;
		} 
		else if(temperature >= 20.0f && temperature < 22.0f)
		{
			waterUsage = 2.0f;
		} 
		else if(temperature >= 22.0f && temperature < 28.0f)
		{
			waterUsage = 2.5f;
		}
		else if (temperature >= 28.0f)
		{
			waterUsage = 3.0f;
		}
		//Modifier based on the current water level
		if (waterLevel > 75.0f)
		{
			waterUsage *= 0.9f;
		} 
		else if(waterLevel >= 25.0f && waterLevel < 50.0f)
		{
			waterUsage *= 1.1f;
		}
		else if(waterLevel < 25.0f)
		{
			waterUsage *= 1.2f;
		}

		return waterUsage * 0.7f;
	}

	public void IsPlantClicked(string tag)
	{
		if (tag == "PlantPot")
		{
			WaterPlant();
		}
	}

	void WaterPlant()
	{
		if(waterLevel < 100)
		{
			waterLevel += 3;
			sparkle.Play();
		}
	}

	public void AddListenerOnWaterLevelChangeEvent(Action<float> listener)
	{
		OnWaterLevelChangeHandler += listener;
	}
	public void RemoveListenerOnWaterLevelChangeEvent(Action<float> listener)
	{
		OnWaterLevelChangeHandler -= listener;
	}

	public void AddListenerOnTempChangeEvent(Action<float> listener)
	{
		OnTempChangeHandler += listener;
	}
	public void RemoveListenerOnTempChangeEvent(Action<float> listener)
	{
		OnTempChangeHandler -= listener;
	}
}
