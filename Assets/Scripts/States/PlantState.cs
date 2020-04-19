using UnityEngine;

public abstract class PlantState 
{
	protected Sprite icon;
	protected PlantController plant;

	public PlantState(PlantController plant, Sprite icon)
	{
		this.icon = icon;
		this.plant = plant;
	}

	public abstract void GetBetter();
	public abstract void GetWorse();


	public virtual void Grow() { }
	public virtual void LeafDie() { }

	public virtual Sprite GetIcon()
	{
		return this.icon;
	}
}

