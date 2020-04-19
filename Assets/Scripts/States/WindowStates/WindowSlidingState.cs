using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowSlidingState : WindowState
{
	public float moveSpeed;
	public WindowSlidingState(WindowsController window, float moveSpeed) : base(window)
	{
		this.moveSpeed = moveSpeed;
	}

	public override void Slide()
	{
		Vector3 movement = new Vector3(1, 0, 0) * this.moveSpeed * this.slideDirection;
		this.window.transform.Translate(movement);
	}

	public override bool IsOpened()
	{
		return false;
	}

	public override bool IsSliding()
	{
		return true;
	}

	public override void IsEndState()
	{
		//window is closing
		if(this.slideDirection == -1)
		{
			Vector3 distance = this.window.transform.position - this.closedPosition;
			if (distance.magnitude < 0.01f)
			{
				this.window.transform.position = this.closedPosition;
				this.window.windowState = this.window.closedWindowState; 
			}
		}
		//window is opening
		else if (this.slideDirection == 1)
		{
			Vector3 distance = this.window.transform.position - this.openedPosition;
			if (distance.magnitude < 0.01f)
			{
				this.window.transform.position = this.openedPosition;
				this.window.windowState = this.window.openedWindowState;
			}
		}
	}
}
