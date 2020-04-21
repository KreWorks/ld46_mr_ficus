using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class WeatherSimulator : MonoBehaviour
{
	public Action<float> OnTempChangeHandler;
	public Action<int> OnWindChangeHandler;

	public WeatherDataSO sunny;
	public WeatherDataSO cloudy;
	public WeatherDataSO rainy;
	public WeatherDataSO windy;

	public ParticleSystem rain;
	public ParticleSystem wind;

	public Light sun;
	public Volume postProcessing;

	VolumeProfile ppProfile;
	ColorAdjustments colorAdjustment;
	Bloom bloom; 

	List<WeatherObject> forecast;

	WeatherObject currentWeather;
	bool weatherParamsEnabled = false;

	float temperature;

	float goalTemp = 0;
	float goalExposure = 0;
	float goalIntensity = 1;

	bool iterateParamsNeeded = false;

	// Start is called before the first frame update
	void Awake()
    {
		forecast = new List<WeatherObject>(); 

		for(int i = 0; i < 3; i++)
		{
			forecast.Add(GetRandomWeather());
		}

		ChangeWeather();
	}

	void Start()
	{
		rain.Stop();
		rain.gameObject.SetActive(false);

		wind.Stop();
		wind.gameObject.SetActive(false);

		currentWeather = forecast[0];

		ppProfile = postProcessing.GetComponent<UnityEngine.Rendering.Volume>()?.profile;
		if (!ppProfile) throw new System.NullReferenceException(nameof(UnityEngine.Rendering.VolumeProfile));

		if (!ppProfile.TryGet(out colorAdjustment)) throw new System.NullReferenceException(nameof(colorAdjustment));
		if (!ppProfile.TryGet(out bloom)) throw new System.NullReferenceException(nameof(bloom));
	}

	void Update()
	{
		if (!weatherParamsEnabled)
		{
			if (currentWeather.weatherData.rainLevel != 0)
			{
				int rainLevel = currentWeather.weatherData.rainLevel;
				rain.Play();
				rain.gameObject.SetActive(true);
				ParticleSystem.EmissionModule rainEmission = rain.emission;
				rainEmission.rateOverTime = rainLevel * 130;
				ParticleSystem.MainModule rainMain = rain.main;
				rainMain.startSpeedMultiplier = 3 + rainLevel * 3;
			}
			else
			{
				rain.Stop();
				rain.gameObject.SetActive(false);
			}

			if (currentWeather.weatherData.windLevel != 0)
			{
				int windLevel = currentWeather.weatherData.windLevel;
				wind.Play();
				wind.gameObject.SetActive(true);
				ParticleSystem.EmissionModule windEmission = wind.emission;
				windEmission.rateOverTime = windLevel * 40;
				ParticleSystem.MainModule windMain = wind.main;
				windMain.startSpeedMultiplier = 3 + windLevel * 4;
			}
			else
			{
				wind.Stop();
				wind.gameObject.SetActive(false);
			}
			ChangeLightsAndTemp();

			weatherParamsEnabled = true;
		}

		if (iterateParamsNeeded)
		{
			IterateDayNightParams();
		}
		
	}

	WeatherObject GetRandomWeather()
	{
		int random = UnityEngine.Random.Range(0, 4);

		switch (random)
		{
			case 0:
				return new WeatherObject(sunny);
			case 1:
				return new WeatherObject(cloudy);
			case 2:
				return new WeatherObject(rainy);
			case 3:
				return new WeatherObject(windy);
			default:
				return new WeatherObject(sunny);
		}
	}

	void ChangeLightsAndTemp()
	{
		bloom.tint.Override(currentWeather.weatherData.lightColor);
		colorAdjustment.postExposure.Override(currentWeather.weatherData.lightIntensity - 4.0f);

		temperature = currentWeather.weatherData.nightTemp;
		OnTempChangeHandler?.Invoke(temperature);
		OnWindChangeHandler?.Invoke(currentWeather.weatherData.windLevel);
	}

	public void ChangeLightByTime(int hour)
	{
		goalTemp = 0;
		goalExposure = 0;
		goalIntensity = 1;

		if (hour < 6 || hour > 20)
		{
			goalTemp = currentWeather.weatherData.nightTemp;
			goalExposure = currentWeather.weatherData.lightIntensity - 4.0f;
			goalIntensity = 0;
		}
		else
		{
			goalTemp = currentWeather.weatherData.dayTemp;
			goalExposure = currentWeather.weatherData.lightIntensity;
			goalIntensity = 1;
		}

		iterateParamsNeeded = true;
	}

	void IterateDayNightParams()
	{
		int counter = 0; 
		if (Mathf.Abs(colorAdjustment.postExposure.value - goalExposure) > 0.05f)
		{
			colorAdjustment.postExposure.Override(Mathf.Lerp(colorAdjustment.postExposure.value, goalExposure, 0.3f * Time.deltaTime));
			counter++;
		}

		if (Mathf.Abs(temperature - goalTemp) > 0.01f)
		{
			temperature = Mathf.Lerp(temperature, goalTemp, 0.2f * Time.deltaTime);
			counter++;
			OnTempChangeHandler?.Invoke(temperature);
		}

		if (Mathf.Abs(sun.intensity - goalIntensity) > 0.01f)
		{
			sun.intensity = Mathf.Lerp(sun.intensity, goalIntensity, 0.1f * Time.deltaTime);
			counter++;
		}

		iterateParamsNeeded = counter > 0;
	}

	public void ChangeWeather()
	{
		WeatherObject first = forecast[0];
		forecast.Remove(first);

		currentWeather = forecast[0];

		forecast.Add(GetRandomWeather());

		weatherParamsEnabled = false;
	}

	public Sprite[] GetForecastIcons()
	{
		Sprite[] icons = new Sprite[3];

		for(int i = 0; i < icons.Length; i++)
		{
			icons[i] = forecast[i].weatherData.icon;
		}
			
		return icons;
	}

	//
	public void AddListenerOnTempChangeEvent(Action<float> listener)
	{
		OnTempChangeHandler += listener; 
	}
	public void RemoveListenerOnTempChangeEvent(Action<float> listener)
	{
		OnTempChangeHandler -= listener;
	}

	public void AddListenerOnWindChangeEvent(Action<int> listener)
	{
		OnWindChangeHandler += listener;
	}
	public void RemoveListenerOnWindChangeEvent(Action<int> listener)
	{
		OnWindChangeHandler -= listener;
	}

	
}
