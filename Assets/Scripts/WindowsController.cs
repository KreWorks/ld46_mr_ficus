using System;
using UnityEngine;

public class WindowsController : MonoBehaviour
{
	public Action OnWindowSlideHandler;

	public WindowState windowState;

	public WindowOpenedState openedWindowState;
	public WindowClosedState closedWindowState;
	public WindowSlidingState slidingState; 

	public 

	void Start()
	{
		openedWindowState = new WindowOpenedState(this);
		closedWindowState = new WindowClosedState(this);
		slidingState = new WindowSlidingState(this, 0.001f);

		windowState = closedWindowState;
	}

	private void Update()
	{
		windowState.Slide();
		windowState.IsEndState();
	}

	public void IsWindowClicked(string tag)
	{
		if(tag == "Window")
		{
			MoveWindow();
		}
	}

	void MoveWindow()
	{
		//Allow opening or closing when window isn't moving
		if (!windowState.IsSliding())
		{
			OnWindowSlideHandler?.Invoke();
			if (windowState.IsOpened())
			{
				windowState.Close();
			}
			else
			{
				windowState.Open();
			}
		}
	}

	public void AddListenerOnWindowSlideEvent(Action listener)
	{
		OnWindowSlideHandler += listener;
	}
	public void RempveListenerOnWindowSlideEvent(Action listener)
	{
		OnWindowSlideHandler -= listener;
	}
}
