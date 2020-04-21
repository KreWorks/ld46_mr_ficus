using UnityEngine;

public class SadPlantState : PlantState
{
	public SadPlantState(PlantController plant, Sprite icon, Color color) : base(plant, icon, color)
	{

	}

	public override void GetBetter()
	{
		this.plant.TransitionToState(this.plant.neutralState);
	}

	public override void GetWorse()
	{
		this.plant.TransitionToState(this.plant.tragicState);
	}
}
