using UnityEngine;

public class DelightedPlantState : PlantState
{
	public DelightedPlantState(PlantController plant, Sprite icon, Color color) : base(plant, icon, color)
	{

	}

	public override void GetBetter()
	{
		return;
	}

	public override void GetWorse()
	{
		this.plant.TransitionToState(this.plant.pleaseState);
	}
}
