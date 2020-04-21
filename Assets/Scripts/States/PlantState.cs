using UnityEngine;

public abstract class PlantState 
{
	protected Sprite icon;
	protected Color color;
	protected PlantController plant;

	public PlantState(PlantController plant, Sprite icon, Color color)
	{
		this.icon = icon;
		this.plant = plant;
		this.color = color;
	}

	public abstract void GetBetter();
	public abstract void GetWorse();


	public virtual void Grow() { }
	public virtual void LeafDie() { }

	public virtual Sprite GetIcon()
	{
		return this.icon;
	}

	public virtual Color GetColor()
	{
		return this.color;
	}
}

