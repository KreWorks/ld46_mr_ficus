using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsController : MonoBehaviour
{
	public InputController inputController;

	public WindowState windowState;

	public WindowOpenedState openedWindowState;
	public WindowClosedState closedWindowState;
	public WindowSlidingState slidingState; 

	void Start()
	{
		openedWindowState = new WindowOpenedState(this);
		closedWindowState = new WindowClosedState(this);
		slidingState = new WindowSlidingState(this, 0.001f);

		inputController.AddListenerOnMouseClickedEvent(IsWindowClicked);

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


}
