using System;
using UnityEngine;

public class TimeController : MonoBehaviour
{
	public float inGameHour;
	public int startHour;

	int days;
	int hour;
	float time;
	

	private Action<int> OnDayCountUpdateHandler;
	private Action<int> OnHourUpdateHandler;
	private Action<string> OnTimeUpdateHandler;

	// Start is called before the first frame update
	void Start()
    {
		time = startHour * inGameHour;
		days = 0;
		hour = GetHourTime();
    }

    // Update is called once per frame
    void Update()
    {
		time += Time.deltaTime;
		int newHour = GetHourTime();

		if(hour != newHour)
		{
			OnHourUpdateHandler?.Invoke(GetHourTime());
		}

		hour = newHour;
		OnTimeUpdateHandler?.Invoke(GetTimeString());
		

		if ((time - days * inGameHour * 24) >= inGameHour * 24)
		{
			days++;

			OnDayCountUpdateHandler?.Invoke(days);
		}
    }

	int GetHourTime()
	{
		float justTime = time - (days * 24 * inGameHour);
		int hour = Mathf.FloorToInt(justTime / inGameHour);

		return hour;
	}

	string GetTimeString()
	{
		string timeString = "";
		float justTime = time - (days * 24 * inGameHour);

		int hour = Mathf.FloorToInt(justTime / inGameHour);
		int minute = Mathf.FloorToInt((justTime % inGameHour) * 60.0f / inGameHour);

		if (hour < 10)
		{
			timeString += "0";
		}
		timeString += hour.ToString() + ":"; 

		if(minute < 10)
		{
			timeString += "0";
		}
		timeString += minute.ToString();

		return timeString;
	}

	//Listeners
	public void AddListenerOnDayCountUpdateEvent(Action<int> listener)
	{
		OnDayCountUpdateHandler += listener;
	}
	public void RemoveListenerOnDayCountUpdateEvent(Action<int> listener)
	{
		OnDayCountUpdateHandler -= listener;
	}

	public void AddListenerOnHourUpdateEvent(Action<int> listener)
	{
		OnHourUpdateHandler += listener;
	}
	public void RemoveListenerOnHourUpdateEvent(Action<int> listener)
	{
		OnHourUpdateHandler -= listener;
	}

	public void AddListenerOnTimeUpdateEvent(Action<string> listener)
	{
		OnTimeUpdateHandler += listener;
	}
	public void RemoveListenerOnTimeUpdateEvent(Action<string> listener)
	{
		OnTimeUpdateHandler -= listener;
	}
}
