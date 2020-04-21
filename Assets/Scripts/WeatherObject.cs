using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherObject 
{
	public WeatherDataSO weatherData;
	public int dayTemp;
	public int nightTemp;
    
	public WeatherObject(WeatherDataSO weatherData)
	{
		this.weatherData = weatherData;
		this.dayTemp = weatherData.dayTemp;
		this.nightTemp = weatherData.nightTemp;
	}

}
