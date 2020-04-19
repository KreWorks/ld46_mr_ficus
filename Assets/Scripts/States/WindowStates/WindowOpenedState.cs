using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowOpenedState : WindowState
{
	public WindowOpenedState(WindowsController window): base(window)
	{

	}

	public override void Close()
	{
		this.window.windowState = this.window.slidingState;
		this.window.windowState.SetSlideDirection(-1);
	}

	public override bool IsOpened()
	{
		return true;
	}

	public override bool IsSliding()
	{
		return false;
	}
}
