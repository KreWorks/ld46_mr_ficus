using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public TimeController timeController;

	public WeatherSimulator weatherSimulator;
	public PlantController plantController;
	public WindowsController windowController;

	public UiController uiController;
	public InputController inputController; 


    // Start is called before the first frame update
    void Start()
    {
		uiController.ChangeWeatherIcons(weatherSimulator.GetForecastIcons());

		inputController.AddListenerOnMouseClickedEvent(plantController.IsPlantClicked);
		inputController.AddListenerOnMouseClickedEvent(windowController.IsWindowClicked);

		timeController.AddListenerOnTimeUpdateEvent(uiController.ChangeTimeText);
		timeController.AddListenerOnHourUpdateEvent(weatherSimulator.ChangeLightByTime);
		timeController.AddListenerOnDayCountUpdateEvent(uiController.ChangeDayText);
		timeController.AddListenerOnDayCountUpdateEvent(this.ChangeWeatherForecastIcons);

		plantController.AddListenerOnWaterLevelChangeEvent(uiController.ChangeStatusWater);
		plantController.AddListenerOnTempChangeEvent(uiController.ChangeInsideTempText);
		plantController.AddListenerOnTempChangeEvent(uiController.ChangeStatusTemperature);

		weatherSimulator.AddListenerOnTempChangeEvent(uiController.ChangeOutsideTempText);
		weatherSimulator.AddListenerOnTempChangeEvent(plantController.SetOutsideTemperature);
		weatherSimulator.AddListenerOnWindChangeEvent(plantController.WeatherChangeAffect);

		windowController.AddListenerOnWindowSlideEvent(plantController.OpenWindowAffect);
	}

	public void ChangeWeatherForecastIcons(int days)
	{
		weatherSimulator.ChangeWeather();

		uiController.ChangeWeatherIcons(weatherSimulator.GetForecastIcons());
	}
}
