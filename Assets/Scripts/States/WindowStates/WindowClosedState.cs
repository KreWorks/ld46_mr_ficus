using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowClosedState: WindowState
{
	public WindowClosedState(WindowsController window) : base(window)
	{

	}

	public override void Open()
	{
		this.window.windowState = this.window.slidingState;
		this.window.windowState.SetSlideDirection(1);
	}

	public override bool IsOpened()
	{
		return false;
	}

	public override bool IsSliding()
	{
		return false;
	}
}
