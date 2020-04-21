using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weather Data", menuName = "MeFicus/WeatherData")]
public class WeatherDataSO : ScriptableObject
{
	public string weatherName;
	public Sprite icon;
	public int dayTemp;
	public int nightTemp;
	public int rainLevel;
	public int windLevel;
	public Color lightColor;
	public float lightIntensity;
}
