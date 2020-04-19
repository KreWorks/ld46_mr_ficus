using System;
using UnityEngine;

public class TimeController : MonoBehaviour
{
	public float inGameHour;
	public int startHour;

	int daysCount;
	float time;

	private Action<int> OnDayCountUpdateHandler;
	private Action<string> OnTimeUpdateHandler;

	// Start is called before the first frame update
	void Start()
    {
		time = startHour;
		daysCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
		time += Time.deltaTime;
		if (time >= inGameHour * 24)
		{
			daysCount++;
			time -= inGameHour * 24;

			OnDayCountUpdateHandler?.Invoke(daysCount);
		}
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

	public void AddListenerOnTimeUpdateEvent(Action<string> listener)
	{
		OnTimeUpdateHandler += listener;
	}
	public void RemoveListenerOnTimeUpdateEvent(Action<string> listener)
	{
		OnTimeUpdateHandler -= listener;
	}
}
