using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WindowState
{
	protected int slideDirection;

	protected WindowsController window;
	protected Vector3 openedPosition;
	protected Vector3 closedPosition;

	public WindowState(WindowsController window)
	{
		this.window = window;
		this.closedPosition = window.transform.position;
		this.openedPosition = new Vector3(-1 * this.closedPosition.x, this.closedPosition.y, this.closedPosition.z);
	}

	public virtual void Open() { }
	public virtual void Slide() { }
	public virtual void Close() { }
	public virtual void IsEndState() { }

	public virtual void SetSlideDirection(int direction)
	{
		this.slideDirection = direction;
	}

	
	public abstract bool IsOpened();
	public abstract bool IsSliding();

}
